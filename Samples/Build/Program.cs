﻿using HostApi;
using JetBrains.TeamCity.ServiceMessages.Write.Special;
using NuGet.Versioning;

if (!Props.TryGetValue("configuration", out var configuration)) configuration = "Release";

const string packageId = "MySampleLib";
if (!Props.TryGetValue<SemanticVersion>("version", out var packageVersion))
{
    Info("Evaluate next NuGet package version.");
    packageVersion =
        GetService<INuGet>()
            .Restore(new NuGetRestoreSettings(packageId).WithVersionRange(VersionRange.Parse("*")))
            .Where(i => i.Name == packageId)
            .Select(i => i.NuGetVersion)
            .Select(i => new NuGetVersion(i.Major, i.Minor, i.Patch + 1))
            .DefaultIfEmpty(new NuGetVersion(1, 0, 0))
            .Max()!;
}

var props = new[] {("Version", packageVersion.ToString())};

var requiredSdkVersion = new Version(6, 0);
Version? sdkVersion = default;
if (
    new DotNetCustom("--version")
        .WithShortName($"Checking the .NET SDK version {requiredSdkVersion}")
        .Run(output=> Version.TryParse(output.Line, out sdkVersion)) == 0
    && sdkVersion != default
    && sdkVersion < requiredSdkVersion)
{
    Error($".NET SDK {requiredSdkVersion} or newer is required.");
    return 1;
}

var msbuild = new MSBuild()
    .WithShortName("Rebuilding solution")
    .AddProps(("configuration", configuration))
    .WithProject("MySampleLib.sln")
    .WithTarget("Rebuild")
    .WithRestore(true)
    .AddProps(props);

if (msbuild.Build().ExitCode != 0)
{
    Error("Build failed.");
    return 1;
}

var vstest = new VSTest()
    .WithTestFileNames(Path.Combine("MySampleLib.Tests", "bin", configuration, "net8.0", "MySampleLib.Tests.dll"));

var test = new DotNetTest()
    .WithNoBuild(true)
    .WithConfiguration(configuration);

var testInContainer = new DockerRun(test, $"mcr.microsoft.com/dotnet/sdk:{requiredSdkVersion}")
    .WithPlatform("linux")
    .AddVolumes((Environment.CurrentDirectory, "/project"))
    .WithContainerWorkingDirectory("/project");

var results = await Task.WhenAll(
    testInContainer.BuildAsync(),
    test.BuildAsync(),
    vstest.BuildAsync());

if (results.Any(i => i.ExitCode != 0))
{
    Error("Tests failed.");
    return 1;
}

var pack = new DotNetPack()
    .WithShortName("Packing MySampleLib")
    .WithWorkingDirectory("MySampleLib")
    .WithConfiguration(configuration)
    .WithOutput("bin")
    .WithIncludeSymbols(true)
    .WithIncludeSource(true)
    .AddProps(props);

if (pack.Build().ExitCode != 0)
{
    Error("Packing MySampleLib failed.");
    return 1;
}

Info("Publishing artifacts.");
var teamCityWriter = GetService<ITeamCityWriter>();

var packages =
    from packageExtension in new[] {"nupkg", "symbols.nupkg"}
    select Path.Combine("MySampleLib", "bin", $"{packageId}.{packageVersion}.{packageExtension}");

foreach (var package in packages)
{
    if (!File.Exists(package))
    {
        Error($"\"{package}\" is not exist.");
        return 1;
    }
    
    teamCityWriter.PublishArtifact($"{package} => .");
}

return 0;