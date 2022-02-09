using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[CheckBuildProjectConfigurations]
[ShutdownDotNetAfterServerBuild]
class Build : NukeBuild
{
    const string RootNamespace = "Aidan.Common";

    public static int Main () => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    static readonly Dictionary<string, string> Libraries;

    static readonly string NugetSource;
    static readonly string NugetApiKey;
    static readonly string SecondToLastCommit;

    [ Solution ] readonly Solution Solution;
    [ Parameter ] readonly string ChangesFile;
    [ Parameter ] readonly string Library;

    string ChangesAbsoluteFilePath => $"{RootDirectory}\\{RootNamespace}.Build\\{ChangesFile}";

    static Build( )
    {
        NugetSource = Environment.GetEnvironmentVariable( "NUGET_SOURCE" );
        NugetApiKey = Environment.GetEnvironmentVariable( "NUGET_API_KEY" );
        SecondToLastCommit = Environment.GetEnvironmentVariable( "GITHUB_BEFORE_COMMIT" );
        Libraries = new Dictionary<string, string>
        {
            { "core", $"{RootNamespace}.Core" },
            { "utils", $"{RootNamespace}.Utils" },
            { "di", $"{RootNamespace}.DependencyInjection" }
        };
    }

    Target Clean => _ => _
        .Before( Restore )
        .Executes( ( ) =>
        {
        } );

    Target Restore => _ => _
        .Executes( ( ) =>
        {
            DotNetRestore( s => s
                .SetProjectFile( Solution ) );
        } );

    Target Compile => _ => _
        .DependsOn( Restore )
        .Executes( ( ) =>
        {
            DotNetBuild( s => s
                .SetProjectFile( Solution )
                .EnableNoRestore( ) );
        } );

    Target Push => _ => _
        .Executes( ( ) =>
        {
            var currentLib = Libraries[ Library ];
            var libs = ReadLibraries( );
            WriteLibrary( libs, currentLib );
            WriteLibraries( libs );
            var version = GetLatestVersion( currentLib );    
            var newVersion = $"{version.MajorVersion}.{version.MinorVersion}.{version.PatchVersion}";
            DotNetPack( s => s
                .SetProject( Solution.GetProject( currentLib ) )
                .SetConfiguration( Configuration.Release )
                .SetVersion( newVersion ) );
            DotNetNuGetPush( s => s
                .SetSource( NugetSource )
                .SetApiKey( NugetApiKey )
                .SetTargetPath( $"{RootDirectory}\\{currentLib}\\bin\\Release\\{currentLib}.{newVersion}.nupkg" ) );
        } );

    Target Test => _ => _
        .Executes( ( ) => DotNetTest( settings => settings.SetVerbosity( DotNetVerbosity.Normal ) ) );

    private void WriteLibrary(
        Dictionary<string, LibDto> currentFile,
        string libName )
    {
        var lib = currentFile[ libName ];
        lib.Version.PatchVersion++;
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
}
