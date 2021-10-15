using System;
using System.Linq;
using System.Text.RegularExpressions;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[CheckBuildProjectConfigurations]
[ShutdownDotNetAfterServerBuild]
class Build : NukeBuild
{

    public static int Main () => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    static readonly string[] Libraries;

    static readonly string NugetSource;
    static readonly string NugetApiKey;

    [Solution] readonly Solution Solution;

    static Build( )
    {
        NugetSource = Environment.GetEnvironmentVariable( "NUGET_SOURCE" );
        NugetApiKey = Environment.GetEnvironmentVariable( "NUGET_API_KEY" );
        const string rootNamespace = "Aidan.Common";
        Libraries = new [ ]
        {
            $"{rootNamespace}.Core",
            $"{rootNamespace}.Utils",
            $"{rootNamespace}.DependencyInjection"
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

    Target Pack => _ => _
        .Executes( ( ) =>
        {
            foreach( var library in Libraries )
            {
                if( HasChanged( library ) )
                {
                    var version = GetLatestVersion( library );
                    var versionParts = version
                        .Split( "." )
                        .Select( x => int.Parse( x ) )
                        .ToArray( );
                    var (majorVersion, minorVersion, patchVersion) =
                        ( versionParts[ 0 ], versionParts[ 1 ], versionParts[ 2 ] );
                    var newVersion = $"{majorVersion}.{minorVersion}.{patchVersion + 1}";
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

    private bool HasChanged( string libName )
    {
        var process = ProcessTasks.StartProcess( "git", "status .", $"{RootDirectory}\\{libName}" );
        process.WaitForExit(  );
        return process
            .Output
            .Count > 5;
    }
    
    private string GetLatestVersion( string libName )
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
