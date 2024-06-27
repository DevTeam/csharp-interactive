using System.Runtime.InteropServices;
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
var configuration = GetProperty("configuration", "Release");
var apiKey = GetProperty("apiKey", "");
var integrationTests = bool.Parse(GetProperty("integrationTests", UnderTeamCity.ToString()));
var defaultVersion = NuGetVersion.Parse(GetProperty("version", "1.0.0-dev", UnderTeamCity));
var outputDir = Path.Combine(currentDir, "CSharpInteractive", "bin", configuration);
var templateOutputDir = Path.Combine(currentDir, "CSharpInteractive.Templates", "bin", configuration);
var dockerLinuxTests = HasLinuxDocker();
if (!dockerLinuxTests)
{
    Warning("The docker Linux container is not available. Integration tests are skipped.");
}

var packageVersion = new[]
{
    GetNextNuGetVersion(new NuGetRestoreSettings(toolPackageId).WithPackageType(NuGetPackageType.Tool), defaultVersion),
    GetNextNuGetVersion(new NuGetRestoreSettings(packageId), defaultVersion)
}.Max()!;

var packages = new[]
{
    new PackageInfo(
        packageId,
        Path.Combine("CSharpInteractive", "CSharpInteractive.csproj"),
        Path.Combine(outputDir, "CSharpInteractive", $"{packageId}.{packageVersion.ToString()}.nupkg"),
        true),
    
    new PackageInfo(
        toolPackageId,
        Path.Combine("CSharpInteractive", "CSharpInteractive.Tool.csproj"),
        Path.Combine(outputDir, "CSharpInteractive.Tool", $"{toolPackageId}.{packageVersion.ToString()}.nupkg"),
        true),
    
    new PackageInfo(
        templatesPackageId,
        Path.Combine("CSharpInteractive.Templates", "CSharpInteractive.Templates.csproj"),
        Path.Combine(templateOutputDir, $"{templatesPackageId}.{packageVersion.ToString()}.nupkg"),
        false)
};

Succeed(new DotNetToolRestore().Run(), "Restore tools");

Succeed(
    new DotNetClean()
        .WithProject(solutionFile)
        .WithVerbosity(DotNetVerbosity.Quiet)
        .WithConfiguration(configuration)
        .Build()
);

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
Succeed(new MSBuild()
    .WithProject(Path.Combine(currentDir, "CSharpInteractive", "CSharpInteractive.Tool.csproj"))
    .WithRestore(true)
    .WithTarget("Rebuild;GetDependencyTargetPaths")
    .WithProps(buildProps)
    .Build());

Succeed(new DotNetPack()
    .WithProject(solutionFile)
    .WithConfiguration(configuration)
    .WithProps(buildProps)
    .Build());

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
    Succeed(
        test
            .Customize(cmd =>
                cmd.WithArgs("dotcover")
                    .AddArgs(cmd.Args)
                    .AddArgs(
                        $"--dcOutput={dotCoverSnapshot}",
                        "--dcFilters=+:module=CSharpInteractive.HostApi;+:module=dotnet-csi",
                        "--dcAttributeFilters=System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage"))
            .Build());

    var dotCoverReportXml = Path.Combine(reportDir, "dotCover.xml");
    Succeed(new DotNetCustom("dotCover", "report", $"--source={dotCoverSnapshot}", $"--output={dotCoverReportXml}", "--reportType=TeamCityXml").Run(), "Generating the code coverage reports");
    
    if (TryGetCoverage(dotCoverReportXml, out coveragePercentage))
    {
        switch (coveragePercentage)
        {
            case < 75:
                Error($"The coverage percentage {coveragePercentage} is too low.");
                Exit();
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

if (uninstallTool.Run(_ => { } ) != 0)
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

if (installTool.Run(output => WriteLine(output.Line)) != 0)
{
    Warning($"{installTool} failed.");
}

Succeed(new DotNetCustom("csi", "/?").Run(), "Checking tool");

var uninstallTemplates = new DotNetCustom("new", "uninstall", templatesPackageId)
    .WithShortName("Uninstalling template");

if (uninstallTemplates.Run(output => WriteLine(output.Line)) != 0)
{
    Warning($"{uninstallTemplates} failed.");
}

var installTemplates = new DotNetCustom("new", "install", $"{templatesPackageId}::{packageVersion.ToString()}", "--nuget-source", templateOutputDir)
    .WithShortName("Installing template");

Succeed(installTemplates.Run(), installTemplates.ShortName);

foreach (var framework in frameworks)
{
    var buildProjectDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()[..4]);
    Directory.CreateDirectory(buildProjectDir);
    var sampleProjectName = $"sample project for {framework}";
    try
    {
        var sampleProjectDir = Path.Combine("Samples", "MySampleLib", "MySampleLib.Tests");
        Succeed(new DotNetNew("build", $"--package-version={packageVersion}", "-T", framework, "--no-restore").WithWorkingDirectory(buildProjectDir).Run(), $"Creating a new {sampleProjectName}");
        Succeed(new DotNetBuild().WithProject(buildProjectDir).WithSources(defaultNuGetSource, Path.Combine(outputDir, "CSharpInteractive")).WithShortName($"Building the {sampleProjectName}").Build());
        Succeed(new DotNetRun().WithProject(buildProjectDir).WithNoBuild(true).WithWorkingDirectory(sampleProjectDir).Run(), $"Running a build for the {sampleProjectName}");
        Succeed(new DotNetCustom("csi", Path.Combine(buildProjectDir, "Program.csx")).WithWorkingDirectory(sampleProjectDir).Run(), $"Running a build as a C# script for the {sampleProjectName}");
    }
    finally
    {
        Directory.Delete(buildProjectDir, true);
    }
}

if (!string.IsNullOrWhiteSpace(apiKey) && packageVersion.Release != "dev" && packageVersion.Release != "dev")
{
    var push = new DotNetNuGetPush().WithApiKey(apiKey).WithSources(defaultNuGetSource);
    foreach (var package in packages.Where(i => i.Publish))
    {
        Succeed(push.WithPackage(package.Package).Run(), $"Pushing {Path.GetFileName(package.Package)}");
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
    Succeed(test.WithFilter(filter).Build());
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

internal record PackageInfo(string Id, string Project, string Package, bool Publish);