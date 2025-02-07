using System.Web;
using HostApi;
using Microsoft.Extensions.DependencyInjection;
using NuGet.Versioning;
// ReSharper disable SeparateLocalFunctionsWithJumpStatement
// ReSharper disable UnusedVariable

// Output, logging and tracing API
WriteLine("Hello");
WriteLine("Hello !!!", Color.Highlighted);
Summary("Summary message");
Error("Error details", "ErrorId");
Warning("Warning");
Info("Some info");
Trace("Trace message");

// API for arguments and parameters
Info("First argument: " + (Args.Count > 0 ? Args[0] : "empty"));
Info("Version: " + Props.Get("version", "1.0.0"));
Props["version"] = "1.0.1";

var configuration = Props.Get("configuration", "Release");
Info($"Configuration: {configuration}");

// Command line API
var cmd = new CommandLine("whoami");

cmd.Run().EnsureSuccess();

// Asynchronous way
await cmd.RunAsync().EnsureSuccess();

// API for Docker CLI
await new DockerRun("ubuntu")
    .WithCommandLine(cmd)
    .WithPull(DockerPullType.Always)
    .WithAutoRemove(true)
    .RunAsync()
    .EnsureSuccess();

// Microsoft DI API to resolve dependencies
var nuget = GetService<INuGet>();

// Creating a custom service provider
var serviceCollection = GetService<IServiceCollection>();
serviceCollection.AddSingleton<MyTool>();

var myServiceProvider = serviceCollection.BuildServiceProvider();
var tool = myServiceProvider.GetRequiredService<MyTool>();

// API for NuGet
var settings = new NuGetRestoreSettings("MySampleLib")
    .WithVersionRange(VersionRange.Parse("[1.0.14, 1.1)"))
    .WithTargetFrameworkMoniker("net6.0")
    .WithPackagesPath(".packages");

var packages = nuget.Restore(settings);
foreach (var package in packages)
{
    Info(package.Path);
}

// API for .NET CLI
var buildResult = new DotNetBuild()
    .WithConfiguration(configuration)
    .WithNoLogo(true)
    .Build().EnsureSuccess();

var warnings = buildResult.Warnings
    .Where(warn => Path.GetFileName(warn.File) == "Calculator.cs")
    .Select(warn => $"{warn.Code}({warn.LineNumber}:{warn.ColumnNumber})")
    .Distinct();

foreach (var warning in warnings)
{
    await new HttpClient().GetAsync(
        "https://api.telegram.org/bot7102686717:AAEHw7HZinme_5kfIRV7TwXK4Xql9WPPpM3/" +
        "sendMessage?chat_id=878745093&text="
        + HttpUtility.UrlEncode(warning));
}

// Asynchronous way
var cts = new CancellationTokenSource();
await new DotNetTest()
    .WithConfiguration(configuration)
    .WithNoLogo(true)
    .WithNoBuild(true)
    .BuildAsync(CancellationOnFirstFailedTest, cts.Token);

void CancellationOnFirstFailedTest(BuildMessage message)
{
    if (message.TestResult is {State: TestState.Failed}) cts.Cancel();
}

// Parallel tests
var results = await Task.WhenAll(
    RunTestsAsync("7.0", "bookworm-slim", "alpine"),
    RunTestsAsync("8.0", "bookworm-slim", "alpine", "noble"));
results.SelectMany(i => i).EnsureSuccess();

async Task<IEnumerable<IBuildResult>> RunTestsAsync(string framework, params string[] platforms)
{
    var publish = new DotNetPublish()
        .WithWorkingDirectory("MySampleLib.Tests")
        .WithFramework($"net{framework}")
        .WithConfiguration(configuration)
        .WithNoBuild(true);

    await publish.BuildAsync(cancellationToken: cts.Token).EnsureSuccess();
    var publishPath = Path.Combine(publish.WorkingDirectory, "bin", configuration, $"net{framework}", "publish");

    var test = new VSTest()
        .WithTestFileNames("*.Tests.dll");

    var testInDocker = new DockerRun()
        .WithCommandLine(test)
        .WithAutoRemove(true)
        .WithQuiet(true)
        .WithVolumes((Path.GetFullPath(publishPath), "/app"))
        .WithContainerWorkingDirectory("/app");

    var tasks = from platform in platforms
        let image = $"mcr.microsoft.com/dotnet/sdk:{framework}-{platform}"
        select testInDocker
            .WithImage(image)
            .BuildAsync(CancellationOnFirstFailedTest, cts.Token);
    return await Task.WhenAll(tasks);
}

#pragma warning disable CS9113// Parameter is unread.

internal class MyTool(INuGet nuGet);

#pragma warning restore CS9113// Parameter is unread.