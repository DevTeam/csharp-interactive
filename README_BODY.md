#  Build automation system for .NET

[![NuGet csi](https://buildstats.info/nuget/dotnet-csi?includePreReleases=true)](https://www.nuget.org/packages/dotnet-csi) ![GitHub](https://img.shields.io/github/license/devteam/csharp-interactive) [<img src="http://teamcity.jetbrains.com/app/rest/builds/buildType:(id:OpenSourceProjects_DevTeam_CScriptInteractive_BuildAndTest)/statusIcon.svg"/>](http://teamcity.jetbrains.com/viewType.html?buildTypeId=OpenSourceProjects_DevTeam_CScriptInteractive_BuildAndTest&guest=1)

![](docs/csharp_cat.png)

Example of usage:

```c#
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
var buildResult = new DotNetBuild().WithConfiguration(configuration).WithNoLogo(true)
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
    .BuildAsync(CancellationOnFirstFailedTest, cts.Token)
    .EnsureSuccess();

void CancellationOnFirstFailedTest(BuildMessage message)
{
    if (message.TestResult is { State: TestState.Failed }) cts.Cancel();
}

// Parallel tests
var tempDir = Directory.CreateTempSubdirectory();
try
{
    new DotNetPublish()
        .WithConfiguration(configuration)
        .WithNoLogo(true)
        .WithNoBuild(true)
        .WithFramework("net8.0")
        .AddProps(("PublishDir", tempDir.FullName))
        .Build().EnsureSuccess();

    var test = new VSTest().WithTestFileNames("*.Tests.dll");

    var tasks = from tagSuffix in new[] {"bookworm-slim", "alpine", "noble"}
        let image = $"mcr.microsoft.com/dotnet/sdk:8.0-{tagSuffix}"
        let dockerRun = new DockerRun(image)
            .WithCommandLine(test)
            .WithAutoRemove(true)
            .WithVolumes((tempDir.FullName, "/app"))
            .WithContainerWorkingDirectory("/app")
        select dockerRun.BuildAsync(CancellationOnFirstFailedTest, cts.Token);

    await Task.WhenAll(tasks).EnsureSuccess();
}
finally { tempDir.Delete(); }

class MyTool(INuGet nuGet);
```

## Usage

### Script runner tool

It can be installed as a command-line tool on Windows, Linux, or macOS. The dotnet tool requires [.NET 6+ runtime](https://dotnet.microsoft.com/en-us/download).

After installing tool you can use this tool to run C# scripts from the command line. dotnet-csi is available as a [NuGet package](https://www.nuget.org/packages/dotnet-csi/).

Before installing dotnet-csi as a local tool dot not forget to create .NET local tool manifest file if it is not exist:

```Shell
dotnet new tool-manifest
```

Install the tool and add to the local tool manifest:

```Shell
dotnet tool install dotnet-csi
```

Or install the tool for the current user:

```Shell
dotnet tool install dotnet-csi -g
```

Launch the tool in the interactive mode:

```Shell
dotnet csi
```

Run a specified script with a given argument:

```Shell
dotnet csi Samples/Scripts/hello.csx World 
```

Run a single script located in the _MyDirectory_ directory:

```Shell
dotnet csi Samples/Build
```

Usage:

```Shell
dotnet csi [options] [--] [script] [script arguments]
```

Executes a script if specified, otherwise launches an interactive REPL (Read Eval Print Loop).

Supported arguments:

| Option                  | Description                                                                                                                                                     | Alternative form                                                                              |
|:------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------|
| script                  | The path to the script file to run. If no such file is found, the command will treat it as a directory and look for a single script file inside that directory. |                                                                                               |
| script arguments        | Script arguments are accessible in a script via the global list Args[index] by an argument index.                                                               |                                                                                               |
| --                      | Indicates that the remaining arguments should not be treated as options.                                                                                        |                                                                                               |
| --help                  | Show how to use the command.                                                                                                                                    | `/?`, `-h`, `/h`, `/help`                                                                     |
| --version               | Display the tool version.                                                                                                                                       | `/version`                                                                                    |
| --source                | Specify the NuGet package source to use. Supported formats: URL, or a UNC directory path.                                                                       | `-s`, `/s`, `/source`                                                                         |
| --property <key=value>  | Define a key-value pair(s) for the script properties called _Props_, which is accessible in scripts.                                                            | `-p`, `/property`, `/p`                                                                       |
| --property:<key=value>  | Define a key-value pair(s) in MSBuild style for the script properties called _Props_, which is accessible in scripts.                                           | `-p:<key=value>`, `/property:<key=value>`, `/p:<key=value>`, `--property:key1=val1;key2=val2` |
| @file                   | Read the response file for more options.                                                                                                                        |                                                                                               |

```using HostApi;``` directive in a script allows you to use host API types without specifying the fully qualified namespace of these types.

### .NET project

Install the C# script template [CSharpInteractive.Templates](https://www.nuget.org/packages/CSharpInteractive.Templates)

```shell
dotnet new install CSharpInteractive.Templates
```

Create a console project "Build" containing a script from the template *__build__*

```shell
dotnet new build -o ./Build
```

This projects contains the script *__./Build/Program.csx__*. To run this script from the command line from the directory *__Build__*:

```shell
dotnet csi Build
```

To run as a .NET console application:

```shell
dotnet run --project Build
```

