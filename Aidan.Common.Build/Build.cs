using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.Git;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[CheckBuildProjectConfigurations]
[ShutdownDotNetAfterServerBuild]
class Build : NukeBuild
{
    const string RootNamespace = "Aidan.Common";

    public static int Main () => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    static readonly string[] Libraries;

    static readonly string NugetSource;
    static readonly string NugetApiKey;

    [Solution] readonly Solution Solution;
    [ Parameter ] readonly string ChangesFile;

    string ChangesAbsoluteFilePath => $"{RootDirectory}\\{RootNamespace}.Build\\{ChangesFile}";

    static Build( )
    {
        NugetSource = Environment.GetEnvironmentVariable( "NUGET_SOURCE" );
        NugetApiKey = Environment.GetEnvironmentVariable( "NUGET_API_KEY" );
        Libraries = new [ ]
        {
            $"{RootNamespace}.Core",
            $"{RootNamespace}.Utils",
            $"{RootNamespace}.DependencyInjection"
        };
    }

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .EnableNoRestore( ) );
        });

    Target Push => _ => _
        .Executes( ( ) =>
        {
            foreach( var library in Libraries )
            {
                var version = GetLatestVersion( library );
                var lastUpdatedCommit = version.LastUpdatedCommit;
                var secondToLastCommit = GitTasks
                    .Git( "rev-parse HEAD~" )
                    .ToArray(  )[ 0 ]
                    .Text;
                if( lastUpdatedCommit != secondToLastCommit )
                {
                    throw new Exception( "you must run the command 'configure-git' to add the git hooks for publishing" );
                }
                if( HasChanged( library ) )
                {
                    var newVersion = $"{version.MajorVersion}.{version.MinorVersion}.{version.PatchVersion}";
                    DotNetPack( s => s
                        .SetProject( Solution.GetProject( library ) )
                        .SetConfiguration( Configuration.Release )
                        .SetVersion( newVersion ) );
                    DotNetNuGetPush( s => s
                        .SetSource( NugetSource )
                        .SetApiKey( NugetApiKey )
                        .SetTargetPath( $"{RootDirectory}\\{library}\\bin\\Release\\{library}.{newVersion}.nupkg" ) );
                }
            }
        } );

    Target UpdateLibraryChanges => _ => _
        .Executes( ( ) =>
        {
            var libs = ReadLibraries( );
            foreach( var library in Libraries )
            {
                WriteLibrary( libs, library );
            }
            WriteLibraries( libs );
        } );

    private void WriteLibrary(
        Dictionary<string, LibDto> currentFile,
        string libName )
    {
        var lib = currentFile[ libName ];
        var libHasChanged = HasChangedGit( libName );
        lib.Changed = libHasChanged;
        if( libHasChanged )
        {
            lib.Version.PatchVersion++;
        }
        lib.Version.LastUpdatedCommit = GitTasks.GitCurrentCommit( );
    }

    private bool HasChangedGit( string libName )
    {
        var process = ProcessTasks.StartProcess( "git", "status .", $"{RootDirectory}\\{libName}" );
        process.WaitForExit(  );
        return process
            .Output
            .Count > 5;
    }
    
    private bool HasChanged( string libName )
    {
        var items = ReadLibraries( );
        return items[ libName ].Changed;
    }

    private VersionDto GetLatestVersion( string libName )
    {
        var items = ReadLibraries( );
        return items[ libName ].Version;
    }

    private Dictionary<string, LibDto> ReadLibraries( )
    {
        using var r = new StreamReader( ChangesAbsoluteFilePath );
        var json = r.ReadToEnd( );
        return JsonConvert.DeserializeObject<Dictionary<string, LibDto>>( json );
    }

    private void WriteLibraries( Dictionary<string, LibDto> data )
    {
        var json = JsonConvert.SerializeObject( data, Formatting.Indented );
        Console.Write( json );
        File.WriteAllText( ChangesAbsoluteFilePath, json );
    }

    private string GetLatestVersionDotnetSearch( string libName )
    {
        var process = ProcessTasks.StartProcess( "dotnet", $"search {libName}" );
        process.WaitForExit(  );
        return new Regex( @"[0-9].[0-9].[0-9]" )
            .Match( process
                .Output
                .Select( x => x.Text )
                .ToArray(  )[ 2 ] )
            .ToString( );;
    }

}
