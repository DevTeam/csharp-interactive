using System.Runtime.InteropServices;
using HostApi;
using JetBrains.TeamCity.ServiceMessages.Write.Special;
using NuGet.Versioning;
using static Tools;

const string solutionFile = "CSharpInteractive.sln";
const string packageId = "CSharpInteractive";
const string toolPackageId = "dotnet-csi";
const string templatesPackageId = "CSharpInteractive.Templates";

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

const int minSdk = 6;
var maxSdk = minSdk;
new DotNet().WithVersion(true)
    .Run(output => maxSdk = NuGetVersion.Parse(output.Line).Major)
    .EnsureSuccess();

var allFrameworks = Enumerable.Range(6, maxSdk - minSdk + 1).Select(i => $"net{i}.0").ToArray();
var frameworks = string.Join(";", allFrameworks);
var framework = $"net{maxSdk}.0";

Info($"frameworks: {frameworks}");
Info($"framework: {framework}");

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

new DotNetClean()
    .WithProject(solutionFile)
    .WithVerbosity(DotNetVerbosity.Quiet)
    .WithConfiguration(configuration)
    .Build().EnsureSuccess();

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

var buildProps = new[]
{
    ("configuration", configuration),
    ("version", packageVersion.ToString())
};

new MSBuild()
    .WithProject(Path.Combine(currentDir, "CSharpInteractive", "CSharpInteractive.Tool.csproj"))
    .WithRestore(true)
    .WithTarget("Rebuild;GetDependencyTargetPaths")
    .WithProps(buildProps)
    .Build().EnsureSuccess();

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
        .Build();
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
    new DotNetToolRestore()
        .Run().EnsureSuccess();

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
    new DotNetCustom("dotCover", "report", $"--source={dotCoverSnapshot}", $"--output={dotCoverReportXml}", "--reportType=TeamCityXml")
        .WithShortName("Generating the code coverage reports")
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

var hasTool = new DotNetToolList()
    .WithPackage(toolPackageId)
    .WithGlobal(true)
    .Run().ExitCode == 0;

if (hasTool)
{
    new DotNetToolUninstall()
        .WithPackage(toolPackageId)
        .WithGlobal(true)
        .Run().EnsureSuccess();
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

new DotNetBuild()
    .WithProject(Path.Combine("Samples", "MySampleLib"))
    .Build().EnsureSuccess();

new DotNetToolInstall()
    .WithPackage(toolPackageId)
    .WithGlobal(true)
    .WithNoCache(true)
    .WithVersion(packageVersion.ToString())
    .AddSources(Path.Combine(outputDir, "CSharpInteractive.Tool"))
    .Run().EnsureSuccess();

new DotNetCsi()
    .WithVersion(true)
    .WithShortName("Checking csi tool")
    .Run().EnsureSuccess();

new DotNetNewUninstall()
    .WithPackage(templatesPackageId)
    .Run();

new DotNetNewInstall()
    .WithPackage($"{templatesPackageId}::{packageVersion.ToString()}")
    .AddSources(templateOutputDir)
    .Run().EnsureSuccess();

foreach (var checkingFramework in allFrameworks)
{
    CheckCompatibilityAsync(checkingFramework, packageVersion, defaultNuGetSource, outputDir);
}

if (!skipTests && (integrationTests || dockerLinuxTests))
{
    var logicOp = integrationTests && dockerLinuxTests ? "|" : "&";
    test
        .WithFilter($"Integration={integrationTests}{logicOp}Docker={dockerLinuxTests}")
        .Build().EnsureSuccess(buildResult => buildResult is {ExitCode: 0, Summary.FailedTests: 0});
}

if (!string.IsNullOrWhiteSpace(apiKey) && packageVersion.Release != "dev" && packageVersion.Release != "dev")
{
    var push = new DotNetNuGetPush()
        .WithApiKey(apiKey)
        .WithSource(defaultNuGetSource);

    foreach (var package in packages.Where(i => i.Publish))
    {
        push.WithPackage(package.Package)
            .Build().EnsureSuccess();
    }
}
else
{
    Info("Pushing NuGet packages was skipped.");
}

Info($"Tool and package version: {packageVersion}");
Info($"Template version: {packageVersion}");
Info($"The coverage percentage: {coveragePercentage}");

return 0;

void CheckCompatibilityAsync(
    string checkingFramework,
    NuGetVersion nuGetVersion,
    string nuGetSource,
    string output)
{
    var buildProjectDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()[..4]);
    Directory.CreateDirectory(buildProjectDir);
    try
    {
        var sampleProjectDir = Path.Combine("Samples", "MySampleLib", "MySampleLib.Tests");
        new DotNetNew()
            .WithTemplateName("build")
            .WithNoRestore(true)
            .WithArgs($"--version={nuGetVersion}", "-T", checkingFramework)
            .WithWorkingDirectory(buildProjectDir)
            .Run().EnsureSuccess();

        new DotNetBuild()
            .WithProject(buildProjectDir)
            .WithSources(nuGetSource, Path.Combine(output, "CSharpInteractive"))
            .Build().EnsureSuccess();

        new DotNetRun()
            .WithWorkingDirectory(buildProjectDir)
            .WithNoRestore(true)
            .WithNoBuild(true)
            .WithFramework(checkingFramework)
            .Run().EnsureSuccess();

        new DotNetCsi()
            .WithScript(Path.Combine(buildProjectDir, "Program.csx"))
            .WithWorkingDirectory(sampleProjectDir)
            .Run().EnsureSuccess();
    }
    finally
    {
        Directory.Delete(buildProjectDir, true);
    }
}

internal record PackageInfo(string Id, string Package, bool Publish);