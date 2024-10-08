﻿using System.Runtime.InteropServices;
using HostApi;
using JetBrains.TeamCity.ServiceMessages.Write.Special;
using NuGet.Versioning;
using static Tools;

const string solutionFile = "CSharpInteractive.sln";
const string packageId = "CSharpInteractive";
const string toolPackageId = "dotnet-csi";
const string templatesPackageId = "CSharpInteractive.Templates";
var frameworks = new[] {"net6.0", "net7.0", "net8.0"};

var currentDir = Environment.CurrentDirectory;
if (!File.Exists(solutionFile))
{
    Error($"Cannot find the solution \"{solutionFile}\". Current directory is \"{currentDir}\".");
    return 1;
}

const string defaultNuGetSource = "https://api.nuget.org/v3/index.json";
Environment.SetEnvironmentVariable("DOTNET_NUGET_SIGNATURE_VERIFICATION", "false");
var configuration = GetProperty("configuration", "Release");
var apiKey = GetProperty("apiKey", "");
var integrationTests = bool.Parse(GetProperty("integrationTests", CI.ToString()));
var defaultVersion = NuGetVersion.Parse(GetProperty("version", "1.1.0-dev", CI));
var outputDir = Path.Combine(currentDir, "CSharpInteractive", "bin", configuration);
var templateOutputDir = Path.Combine(currentDir, "CSharpInteractive.Templates", "bin", configuration);
var dockerLinuxTests = HasLinuxDocker();
if (!dockerLinuxTests)
{
    Warning("The docker Linux container is unavailable. Integration tests are skipped.");
}

var packageVersion = new[]
{
    defaultVersion,
    GetNextNuGetVersion(new NuGetRestoreSettings(toolPackageId).WithPackageType(NuGetPackageType.Tool), defaultVersion),
    GetNextNuGetVersion(new NuGetRestoreSettings(packageId), defaultVersion)
}.Max()!;

Info(packageVersion.ToString());

var packages = new[]
{
    new PackageInfo(
        packageId,
        Path.Combine(outputDir, "CSharpInteractive", $"{packageId}.{packageVersion.ToString()}.nupkg"),
        true),

    new PackageInfo(
        toolPackageId,
        Path.Combine(outputDir, "CSharpInteractive.Tool", $"{toolPackageId}.{packageVersion.ToString()}.nupkg"),
        true),

    new PackageInfo(
        templatesPackageId,
        Path.Combine(templateOutputDir, $"{templatesPackageId}.{packageVersion.ToString()}.nupkg"),
        false)
};

new DotNetToolRestore().WithShortName("Restoring tools").Run().EnsureSuccess();

new DotNetClean()
    .WithProject(solutionFile)
    .WithVerbosity(DotNetVerbosity.Quiet)
    .WithConfiguration(configuration)
    .Build()
    .EnsureSuccess();

foreach (var package in packages)
{
    var nuGetPackagePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".nuget", "packages", package.Id, packageVersion.ToString());
    if (Directory.Exists(nuGetPackagePath))
    {
        Directory.Delete(nuGetPackagePath, true);
    }

    var packageOutput = Path.GetDirectoryName(package.Package);
    if (Directory.Exists(packageOutput))
    {
        Directory.Delete(packageOutput, true);
    }
}

var buildProps = new[] {("version", packageVersion.ToString())};
new MSBuild()
    .WithProject(Path.Combine(currentDir, "CSharpInteractive", "CSharpInteractive.Tool.csproj"))
    .WithRestore(true)
    .WithTarget("Rebuild;GetDependencyTargetPaths")
    .WithProps(buildProps)
    .Build()
    .EnsureSuccess();

const string templateJson = "CSharpInteractive.Templates/content/ConsoleApplication-CSharp/.template.config/template.json";
var content = File.ReadAllText(templateJson);
var newContent = content.Replace("$(version)", packageVersion.ToString());
File.WriteAllText(templateJson, newContent);
IBuildResult result;
try
{
    result = new DotNetPack()
        .WithProject(solutionFile)
        .WithConfiguration(configuration)
        .WithProps(buildProps)
        .Build()
        .EnsureSuccess();
}
finally
{
    File.WriteAllText(templateJson, content);
}

result.EnsureSuccess();

foreach (var package in packages)
{
    if (!File.Exists(package.Package))
    {
        Error($"The NuGet package {package.Package} was not found.");
        return 1;
    }

    GetService<ITeamCityWriter>().PublishArtifact($"{package.Package} => .");
}

var test = new DotNetTest()
    .WithProject(Path.Combine(currentDir, "CSharpInteractive.Tests", "CSharpInteractive.Tests.csproj"))
    .WithConfiguration(configuration)
    .WithProps(buildProps)
    .WithFilter("Integration!=true&Docker!=true");

var coveragePercentage = 0;
var skipTests = bool.Parse(GetProperty("skipTests", "false"));
if (skipTests)
{
    Warning("The test was skipped.");
}
else
{
    var reportDir = Path.Combine(currentDir, ".reports");
    var dotCoverSnapshot = Path.Combine(reportDir, "dotCover.dcvr");
    test
        .Customize(cmd =>
            cmd.WithArgs("dotcover")
                .AddArgs(cmd.Args)
                .AddArgs(
                    $"--dcOutput={dotCoverSnapshot}",
                    "--dcFilters=+:module=CSharpInteractive.HostApi;+:module=dotnet-csi",
                    "--dcAttributeFilters=System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage"))
        .Build()
        .EnsureSuccess(buildResult => buildResult is {ExitCode: 0, Summary.FailedTests: 0});

    var dotCoverReportXml = Path.Combine(reportDir, "dotCover.xml");
    new DotNetCustom("dotCover", "report", $"--source={dotCoverSnapshot}", $"--output={dotCoverReportXml}", "--reportType=TeamCityXml").WithShortName("Generating the code coverage reports")
        .Run().EnsureSuccess();

    if (TryGetCoverage(dotCoverReportXml, out coveragePercentage))
    {
        switch (coveragePercentage)
        {
            case < 75:
                Error($"The coverage percentage {coveragePercentage} is too low.");
                break;

            case < 80:
                Warning($"The coverage percentage {coveragePercentage} is too low.");
                break;
        }
    }
    else
    {
        Warning("Percentage of coverage not determined.");
    }
}

var uninstallTool = new DotNetCustom("tool", "uninstall", toolPackageId, "-g")
    .WithShortName("Uninstalling tool");

if (uninstallTool.Run(_ => { }).ExitCode != 0)
{
    Warning($"{uninstallTool} failed.");
}

if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
{
    var toolsDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".dotnet", "tools");
    var pathEnvVar = Environment.GetEnvironmentVariable("PATH");
    Info($"Prev PATH={pathEnvVar}");
    pathEnvVar = $"{pathEnvVar}:{toolsDir}";
    Info($"New PATH={pathEnvVar}");
    Environment.SetEnvironmentVariable("PATH", pathEnvVar);
}

var installTool = new DotNetCustom("tool", "install", toolPackageId, "-g", "--version", packageVersion.ToString(), "--add-source", Path.Combine(outputDir, "CSharpInteractive.Tool"))
    .WithShortName("Installing tool");

installTool.Run(output =>
{
    output.Handled = true;
    WriteLine(output.Line);
}).EnsureSuccess(_ => true);

new DotNetCustom("csi", "/?").WithShortName("Checking tool").Run().EnsureSuccess();

var uninstallTemplates = new DotNetCustom("new", "uninstall", templatesPackageId)
    .WithShortName("Uninstalling template");

uninstallTemplates.Run(output =>
{
    output.Handled = true;
    WriteLine(output.Line);
}).EnsureSuccess(_ => true);

var installTemplates = new DotNetCustom("new", "install", $"{templatesPackageId}::{packageVersion.ToString()}", "--nuget-source", templateOutputDir)
    .WithShortName("Installing template");

installTemplates.WithShortName(installTemplates.ShortName).Run().EnsureSuccess();
foreach (var framework in frameworks)
{
    await CheckCompatibilityAsync(framework, packageVersion, defaultNuGetSource, outputDir);
}

if (!string.IsNullOrWhiteSpace(apiKey) && packageVersion.Release != "dev" && packageVersion.Release != "dev")
{
    var push = new DotNetNuGetPush().WithApiKey(apiKey).WithSources(defaultNuGetSource);
    foreach (var package in packages.Where(i => i.Publish))
    {
        push.WithPackage(package.Package)
            .WithShortName($"Pushing {Path.GetFileName(package.Package)}")
            .Build().EnsureSuccess();
    }
}
else
{
    Info("Pushing NuGet packages were skipped.");
}

if (integrationTests || dockerLinuxTests)
{
    var logicOp = integrationTests && dockerLinuxTests ? "|" : "&";
    var filter = $"Integration={integrationTests}{logicOp}Docker={dockerLinuxTests}";
    test
        .WithFilter(filter)
        .Build().EnsureSuccess(buildResult => buildResult is {ExitCode: 0, Summary.FailedTests: 0});
}

WriteLine("To use the csi tool:", Color.Highlighted);
WriteLine("    dotnet csi /?", Color.Highlighted);
WriteLine("To create a build project from the template:", Color.Highlighted);
WriteLine($"    dotnet new build --package-version={packageVersion}", Color.Highlighted);
WriteLine($"Tool and package version: {packageVersion}", Color.Highlighted);
WriteLine($"Template version: {packageVersion}", Color.Highlighted);
if (skipTests)
{
    WriteLine($"The coverage percentage: {coveragePercentage}", Color.Highlighted);
}

return 0;

async Task CheckCompatibilityAsync(
    string framework,
    NuGetVersion nuGetVersion,
    string nuGetSource,
    string output)
{
    var buildProjectDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()[..4]);
    Directory.CreateDirectory(buildProjectDir);
    var sampleProjectName = $"sample project for {framework}";
    try
    {
        var sampleProjectDir = Path.Combine("Samples", "MySampleLib", "MySampleLib.Tests");
        await new DotNetNew("build", $"--version={nuGetVersion}", "-T", framework, "--no-restore")
            .WithWorkingDirectory(buildProjectDir)
            .WithShortName($"Creating a new {sampleProjectName}")
            .RunAsync().EnsureSuccess();

        await new DotNetBuild()
            .WithProject(buildProjectDir)
            .WithSources(nuGetSource, Path.Combine(output, "CSharpInteractive"))
            .WithShortName($"Building the {sampleProjectName}")
            .BuildAsync().EnsureSuccess();

        await new DotNetRun()
            .WithProject(buildProjectDir)
            .WithNoBuild(true)
            .WithWorkingDirectory(sampleProjectDir)
            .WithShortName($"Running a build for the {sampleProjectName}")
            .RunAsync().EnsureSuccess();

        await new DotNetCustom("csi", Path.Combine(buildProjectDir, "Program.csx"))
            .WithWorkingDirectory(sampleProjectDir)
            .WithShortName($"Running a build as a C# script for the {sampleProjectName}")
            .RunAsync().EnsureSuccess();
    }
    finally
    {
        Directory.Delete(buildProjectDir, true);
    }
}

internal record PackageInfo(string Id, string Package, bool Publish);