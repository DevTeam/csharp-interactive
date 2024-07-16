using System.Web;
using HostApi;
using Microsoft.Extensions.DependencyInjection;
using NuGet.Versioning;

// Output API
WriteLine("Hello");
WriteLine("Hello !!!", Color.Highlighted);
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
    .Build()
    .EnsureSuccess();

foreach (var warn in buildResult.Warnings
             .Where(warn => Path.GetFileName(warn.File) == "Calculator.cs")
             .Select(warn => $"{warn.Code}({warn.LineNumber}:{warn.ColumnNumber})")
             .Distinct())
{
    await new HttpClient().GetAsync(
        "https://api.telegram.org/bot7102686717:AAEHw7HZinme_5kfIRV7TwXK4Xql9WPPpM3/sendMessage?chat_id=878745093&text=" +
        HttpUtility.UrlEncode(warn));
}

var publishDir = Path.GetFullPath(".publish"); 
new DotNetPublish()
    .WithConfiguration(configuration).WithNoLogo(true).WithNoBuild(true)
    .WithFramework("net8.0").WithOutput(publishDir)
    .Build()
    .EnsureSuccess();

var test = new DotNetTest()
    .WithWorkingDirectory(publishDir)
    .WithProject("MySampleLib.Tests.dll");

test.Build().EnsureSuccess();

// Asynchronous way
var cts = new CancellationTokenSource();
await new DotNetTest()
    .WithConfiguration(configuration)
    .WithNoLogo(true)
    .WithNoBuild(true)
    .BuildAsync(i =>
    {
        if (i.TestResult is { State: TestState.Failed })
        {
            cts.Cancel();
        }
    }, cts.Token)
    .EnsureSuccess();

#pragma warning disable CS9113 // Parameter is unread.
class MyTool(INuGet nuGet);
#pragma warning restore CS9113 // Parameter is unread.
