#  Build automation system for .NET

[![NuGet csi](https://img.shields.io/nuget/v/dotnet-csi?includePreReleases=true)](https://www.nuget.org/packages/dotnet-csi)
![GitHub](https://img.shields.io/github/license/devteam/csharp-interactive)
[<img src="http://teamcity.jetbrains.com/app/rest/builds/buildType:(id:OpenSourceProjects_DevTeam_CScriptInteractive_BuildAndTest)/statusIcon.svg"/>](http://teamcity.jetbrains.com/viewType.html?buildTypeId=OpenSourceProjects_DevTeam_CScriptInteractive_BuildAndTest&guest=1)
![GitHub Build](https://github.com/DevTeam/csharp-interactive/actions/workflows/main.yml/badge.svg)

C# interactive build automation system makes it easy to build .NET projects. It can be part of your solution as a regular console cross-platform .NET application, run C# scripts without compiling them, or even run in REPL mode - allowing you to run C# interactively.

![](docs/icon.png)

## Advantages

- [X] 3 compatible [operating modes](#operating-modes)
- [X] Cross-platform
- [X] Debugging capability
- [X] No model limitations (Task, Target, DependsOn, etc.)
  - Regular .NET code
  - Best programming practices
- [X] Powerful API for building .NET projects
- [X] Summarised statistics

## Operating modes

- [Interactive](#interactive-repl)
- [Running C# script](#running-c-script)
- [Regular .NET build project](#net-build-project)

These modes are practically compatible, i.e., for example, a script can be run as a .NET project, and vice versa, with minimal or no changes.

## Interactive (REPL)

Please see [this page](https://github.com/DevTeam/csharp-interactive/wiki/Install-the-C%23-script-template) for installation details.

Launch the tool in the interactive mode:

```Shell
dotnet csi
```
Simply enter C# commands sequentially one line after another and get the result in console output.

## Running C# script

Run a specified script with a given argument:

```Shell
dotnet csi ./MyDirectory/hello.csx World 
```

Run a single script located in the _MyDirectory_ directory:

```Shell
dotnet csi ./MyDirectory World
```

<details>
<summary>Usage details</summary>

```Shell
dotnet csi [options] [--] [script] [script arguments]
```

Executes a script if specified, otherwise launches an interactive REPL (Read Eval Print Loop).

`--` - Indicates that the remaining arguments should not be treated as options.

`script` - The path to the script file to run. If no such file is found, the command will treat it as a directory and look for a single script file inside that directory.

`script arguments` - Script arguments are accessible in a script via the global list `Args[index]` by an argument index.

`@file` - Read the response file for more options.

Supported options:

| Option                  | Description                                                                                                                                                     | Alternative form                                                                              |
|:------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------|
| --help                  | Show how to use the command.                                                                                                                                    | `/?`, `-h`, `/h`, `/help`                                                                     |
| --version               | Display the tool version.                                                                                                                                       | `/version`                                                                                    |
| --source                | Specify the NuGet package source to use. Supported formats: URL, or a UNC directory path.                                                                       | `-s`, `/s`, `/source`                                                                         |
| --property <key=value>  | Define a key-value pair(s) for the script properties called _Props_, which is accessible in scripts.                                                            | `-p`, `/property`, `/p`                                                                       |
| --property:<key=value>  | Define a key-value pair(s) in MSBuild style for the script properties called _Props_, which is accessible in scripts.                                           | `-p:<key=value>`, `/property:<key=value>`, `/p:<key=value>`, `--property:key1=val1;key2=val2` |

</details>

## .NET build project

Please see [this page](https://github.com/DevTeam/csharp-interactive/wiki/Install-the-C%23-script-template) for details on how to install the [project template](https://www.nuget.org/packages/CSharpInteractive.Templates).

Create a console project *__Build__* containing a script from the template *__build__*

```shell
dotnet new build -o ./Build
```

The created project contains 2 entry points:
- _Program.csx_ to run as a script
  ```shell
  dotnet csi ./Build
  ```
- _Program.cs_ to run as .NET application
  ```shell
  dotnet run --project ./Build
  ```

## NuGet packages

| Package name                | Link                                                                                                                                 | Description                                        | Installation                                   |
|-----------------------------|--------------------------------------------------------------------------------------------------------------------------------------|:---------------------------------------------------|------------------------------------------------|
| dotnet-csi                  | [![NuGet](https://img.shields.io/nuget/v/dotnet-csi)](https://www.nuget.org/packages/dotnet-csi)                                     | Interactive .NET tool for REPL and running scripts | dotnet tool install --global dotnet-csi        |
| CSharpInteractive.Templates | [![NuGet](https://img.shields.io/nuget/v/CSharpInteractive.Templates )](https://www.nuget.org/packages/CSharpInteractive.Templates ) | .NET build project template                        | dotnet new install CSharpInteractive.Templates |
| CSharpInteractive           | [![NuGet](https://img.shields.io/nuget/v/CSharpInteractive)](https://www.nuget.org/packages/CSharpInteractive)                       | A library for use in .NET build projects           | dotnet add package CSharpInteractive           |

## Usage examples

- [Build project example](https://github.com/DevTeam/csharp-interactive/tree/master/Samples/MySampleLib)
- [CSharp Interactive build project](https://github.com/DevTeam/csharp-interactive/tree/master/Build)
- [Pure.DI build project](https://github.com/DevTeam/Pure.DI/tree/master/build)
- [Immutype build project](https://github.com/DevTeam/Immutype/tree/master/Build)
- [Comparison with Cake and Nuke](https://github.com/DevTeam/ci-cd)

## API

- Output, logging and tracing
  - [Write a line to a build log](#write-a-line-to-a-build-log)
  - [Write a line highlighted with "Header" color to a build log](#write-a-line-highlighted-with-"header"-color-to-a-build-log)
  - [Write an empty line to a build log](#write-an-empty-line-to-a-build-log)
  - [Log an error to a build log](#log-an-error-to-a-build-log)
  - [Log a warning to a build log](#log-a-warning-to-a-build-log)
  - [Log information to a build log](#log-information-to-a-build-log)
  - [Log trace information to a build log](#log-trace-information-to-a-build-log)
- Arguments and parameters
  - [Using Args](#using-args)
  - [Using Props](#using-props)
- Microsoft DI
  - [Using the Host property](#using-the-host-property)
  - [Get services](#get-services)
  - [Service collection](#service-collection)
- NuGet
  - [Restore NuGet a package of newest version](#restore-nuget-a-package-of-newest-version)
  - [Restore a NuGet package by a version range for the specified .NET and path](#restore-a-nuget-package-by-a-version-range-for-the-specified-.net-and-path)
- Command Line
  - [Build command lines](#build-command-lines)
  - [Run a command line](#run-a-command-line)
  - [Run a command line asynchronously](#run-a-command-line-asynchronously)
  - [Run and process output](#run-and-process-output)
  - [Run asynchronously in parallel](#run-asynchronously-in-parallel)
  - [Cancellation of asynchronous run](#cancellation-of-asynchronous-run)
  - [Run timeout](#run-timeout)
- Docker CLI
  - [Build a project in a docker container](#build-a-project-in-a-docker-container)
  - [Running in docker](#running-in-docker)
- .NET CLI
  - [Build a project](#build-a-project)
  - [Build a project using MSBuild](#build-a-project-using-msbuild)
  - [Clean a project](#clean-a-project)
  - [Pack a project](#pack-a-project)
  - [Publish a project](#publish-a-project)
  - [Restore a project](#restore-a-project)
  - [Restore local tools](#restore-local-tools)
  - [Run a custom .NET command](#run-a-custom-.net-command)
  - [Run a project](#run-a-project)
  - [Run tests under dotCover](#run-tests-under-dotcover)
  - [Test a project](#test-a-project)
  - [Test a project using the MSBuild VSTest target](#test-a-project-using-the-msbuild-vstest-target)
  - [Test an assembly](#test-an-assembly)
  - [Shuts down build servers](#shuts-down-build-servers)
- TeamCity API
  - [TeamCity integration via service messages](#teamcity-integration-via-service-messages)

### Write a line to a build log



``` CSharp
WriteLine("Hello");
```



### Write an empty line to a build log



``` CSharp
WriteLine();
```



### Write a line highlighted with "Header" color to a build log



``` CSharp
WriteLine("Hello", Header);
```



### Log an error to a build log



``` CSharp
Error("Error info", "Error identifier");
```



### Log a warning to a build log



``` CSharp
Warning("Warning info");
```



### Log information to a build log



``` CSharp
Info("Some info");
```



### Log trace information to a build log



``` CSharp
Trace("Some trace info");
```



### Using Args

_Args_ have got from the script arguments.

``` CSharp
if (Args.Count > 0)
{
    WriteLine(Args[0]);
}

if (Args.Count > 1)
{
    WriteLine(Args[1]);
}
```



### Using Props



``` CSharp
WriteLine(Props["version"]);
WriteLine(Props.Get("configuration", "Release"));

// Some CI/CDs have integration of these properties.
// For example in TeamCity this property with all changes will be available in the next TeamCity steps.
Props["version"] = "1.1.6";
```



### Using the Host property

[_Host_](TeamCity.CSharpInteractive.HostApi/IHost.cs) is actually the provider of all global properties and methods.

``` CSharp
var packages = Host.GetService<INuGet>();
Host.WriteLine("Hello");
```



### Get services

This method might be used to get access to different APIs like [INuGet](TeamCity.CSharpInteractive.HostApi/INuGet.cs) or [ICommandLine](TeamCity.CSharpInteractive.HostApi/ICommandLine.cs).

``` CSharp
GetService<INuGet>();

var serviceProvider = GetService<IServiceProvider>();
serviceProvider.GetService(typeof(INuGet));
```

Besides that, it is possible to get an instance of [System.IServiceProvider](https://docs.microsoft.com/en-US/dotnet/api/system.iserviceprovider) to access APIs.

### Service collection



``` CSharp
public void Run()
{
    var serviceProvider =
        GetService<IServiceCollection>()
            .AddTransient<MyTask>()
            .BuildServiceProvider();

    var myTask = serviceProvider.GetRequiredService<MyTask>();
    var exitCode = myTask.Run();
    exitCode.ShouldBe(0);
}

private class MyTask(ICommandLineRunner runner)
{
    public int? Run() => runner
        .Run(new CommandLine("whoami"))
        .EnsureSuccess()
        .ExitCode;
}

```



### Restore NuGet a package of newest version



``` CSharp
// Adds the namespace "HostApi" to use INuGet
using HostApi;

IEnumerable<NuGetPackage> packages = GetService<INuGet>()
    .Restore(new NuGetRestoreSettings("IoC.Container").WithVersionRange(VersionRange.All));
```



### Restore a NuGet package by a version range for the specified .NET and path



``` CSharp
// Adds the namespace "HostApi" to use INuGet
using HostApi;

var packagesPath = Path.Combine(
    Path.GetTempPath(),
    Guid.NewGuid().ToString()[..4]);

var settings = new NuGetRestoreSettings("IoC.Container")
    .WithVersionRange(VersionRange.Parse("[1.3, 1.3.8)"))
    .WithTargetFrameworkMoniker("net5.0")
    .WithPackagesPath(packagesPath);

IEnumerable<NuGetPackage> packages = GetService<INuGet>().Restore(settings);
```



### Build command lines



``` CSharp
// Adds the namespace "Script.Cmd" to use Command Line API
using HostApi;

// Creates and run a simple command line 
"whoami".AsCommandLine().Run().EnsureSuccess();

// Creates and run a simple command line 
new CommandLine("whoami").Run().EnsureSuccess();

// Creates and run a command line with arguments 
new CommandLine("cmd", "/c", "echo", "Hello").Run();

// Same as previous statement
new CommandLine("cmd", "/c")
    .AddArgs("echo", "Hello")
    .Run()
    .EnsureSuccess();

(new CommandLine("cmd") + "/c" + "echo" + "Hello")
    .Run()
    .EnsureSuccess();

"cmd".AsCommandLine("/c", "echo", "Hello")
    .Run()
    .EnsureSuccess();

("cmd".AsCommandLine() + "/c" + "echo" + "Hello")
    .Run()
    .EnsureSuccess();

// Just builds a command line with multiple environment variables
var cmd = new CommandLine("cmd", "/c", "echo", "Hello")
    .AddVars(("Var1", "val1"), ("var2", "Val2"));

// Same as previous statement
cmd = new CommandLine("cmd") + "/c" + "echo" + "Hello" + ("Var1", "val1") + ("var2", "Val2");

// Builds a command line to run from a specific working directory 
cmd = new CommandLine("cmd", "/c", "echo", "Hello")
    .WithWorkingDirectory("MyDyrectory");

// Builds a command line and replaces all command line arguments
cmd = new CommandLine("cmd", "/c", "echo", "Hello")
    .WithArgs("/c", "echo", "Hello !!!");
```



### Run a command line



``` CSharp
// Adds the namespace "HostApi" to use Command Line API
using HostApi;

GetService<ICommandLineRunner>()
    .Run(new CommandLine("cmd", "/c", "DIR"))
    .EnsureSuccess();

// or the same thing using the extension method
new CommandLine("cmd", "/c", "DIR")
    .Run()
    .EnsureSuccess();

// using operator '+'
var cmd = new CommandLine("cmd") + "/c" + "DIR";
cmd.Run().EnsureSuccess();

// with environment variables
cmd = new CommandLine("cmd") + "/c" + "DIR" + ("MyEnvVar", "Some Value");
cmd.Run().EnsureSuccess();
```



### Run a command line asynchronously



``` CSharp
// Adds the namespace "HostApi" to use Command Line API
using HostApi;

await GetService<ICommandLineRunner>()
    .RunAsync(new CommandLine("cmd", "/C", "DIR"))
    .EnsureSuccess();

// or the same thing using the extension method
var result = await new CommandLine("cmd", "/c", "DIR")
    .RunAsync()
    .EnsureSuccess();
```



### Run and process output



``` CSharp
// Adds the namespace "HostApi" to use Command Line API
using HostApi;

var lines = new List<string>();
var result = new CommandLine("cmd", "/c", "SET")
    .AddVars(("MyEnv", "MyVal"))
    .Run(output => lines.Add(output.Line))
    .EnsureSuccess();

lines.ShouldContain("MyEnv=MyVal");
```



### Run asynchronously in parallel



``` CSharp
// Adds the namespace "HostApi" to use Command Line API
using HostApi;

var task = new CommandLine("cmd", "/c", "DIR")
    .RunAsync()
    .EnsureSuccess();

var result = new CommandLine("cmd", "/c", "SET")
    .Run()
    .EnsureSuccess();

await task;
```



### Cancellation of asynchronous run

Cancellation will destroy the process and its child processes.

``` CSharp
// Adds the namespace "HostApi" to use Command Line API
using HostApi;

var cancellationTokenSource = new CancellationTokenSource();
var task = new CommandLine("cmd", "/c", "TIMEOUT", "/T", "120")
    .RunAsync(default, cancellationTokenSource.Token);

cancellationTokenSource.CancelAfter(TimeSpan.FromMilliseconds(100));
task.IsCompleted.ShouldBeFalse();
```



### Run timeout

If timeout expired a process will be killed.

``` CSharp
// Adds the namespace "HostApi" to use Command Line API
using HostApi;

int? exitCode = new CommandLine("cmd", "/c", "TIMEOUT", "/T", "120")
    .Run(default, TimeSpan.FromMilliseconds(1))
    .EnsureSuccess()
    .ExitCode;

exitCode.HasValue.ShouldBeFalse();
```



### Build a project in a docker container



``` CSharp
// Adds the namespace "HostApi" to use .NET build API and Docker API
using HostApi;

// Creates a base docker command line
var dockerRun = new DockerRun()
    .WithAutoRemove(true)
    .WithInteractive(true)
    .WithImage("mcr.microsoft.com/dotnet/sdk")
    .WithPlatform("linux")
    .WithContainerWorkingDirectory("/MyProjects")
    .AddVolumes((ToAbsoluteLinuxPath(Environment.CurrentDirectory), "/MyProjects"));


// Creates a new library project in a docker container
dockerRun
    .WithCommandLine(new DotNetCustom("new", "classlib", "-n", "MyLib", "--force"))
    .Run()
    .EnsureSuccess();

// Builds the library project in a docker container
var result = dockerRun
    .WithCommandLine(new DotNetBuild().WithProject("MyLib/MyLib.csproj"))
    .Build()
    .EnsureSuccess();

// The "result" variable provides details about a build
result.Errors.Any(message => message.State == BuildMessageState.StdError).ShouldBeFalse();
result.ExitCode.ShouldBe(0);

string ToAbsoluteLinuxPath(string path) =>
    "/" + path.Replace(":", "").Replace('\\', '/');
```



### Running in docker



``` CSharp
// Adds the namespace "HostApi" to use Command Line API and Docker API
using HostApi;

// Creates some command line to run in a docker container
var cmd = new CommandLine("whoami");

// Runs the command line in a docker container
var result = new DockerRun(cmd, "mcr.microsoft.com/dotnet/sdk")
    .WithAutoRemove(true)
    .Run()
    .EnsureSuccess();
```



### Build a project



``` CSharp
// Adds the namespace "HostApi" to use .NET build API
using HostApi;

// Creates a new library project, running a command like: "dotnet new classlib -n MyLib --force"
new DotNetNew("xunit", "-n", "MyLib", "--force")
    .Build()
    .EnsureSuccess();

// Builds the library project, running a command like: "dotnet build" from the directory "MyLib"
var result = new DotNetBuild()
    .WithWorkingDirectory("MyLib")
    .Build()
    .EnsureSuccess();

// The "result" variable provides details about a build
result.Errors.Any(message => message.State == BuildMessageState.StdError).ShouldBeFalse();
result.ExitCode.ShouldBe(0);

// Runs tests in docker
result = new DotNetTest()
    .WithWorkingDirectory("MyLib")
    .Build()
    .EnsureSuccess();

result.ExitCode.ShouldBe(0);
result.Summary.Tests.ShouldBe(1);
result.Tests.Count(test => test.State == TestState.Finished).ShouldBe(1);
```



### Clean a project



``` CSharp
// Adds the namespace "HostApi" to use .NET build API
using HostApi;

// Creates a new library project, running a command like: "dotnet new classlib -n MyLib --force"
var result = new DotNetNew("classlib", "-n", "MyLib", "--force")
    .Build()
    .EnsureSuccess();

result.ExitCode.ShouldBe(0);

// Builds the library project, running a command like: "dotnet build" from the directory "MyLib"
result = new DotNetBuild()
    .WithWorkingDirectory("MyLib")
    .Build()
    .EnsureSuccess();

result.ExitCode.ShouldBe(0);

// Clean the project, running a command like: "dotnet clean" from the directory "MyLib"
result = new DotNetClean()
    .WithWorkingDirectory("MyLib")
    .Build()
    .EnsureSuccess();

// The "result" variable provides details about a build
result.ExitCode.ShouldBe(0);
```



### Run a custom .NET command



``` CSharp
// Adds the namespace "HostApi" to use .NET build API
using HostApi;

// Gets the dotnet version, running a command like: "dotnet --version"
NuGetVersion? version = default;
new DotNetCustom("--version")
    .Run(message => NuGetVersion.TryParse(message.Line, out version))
    .EnsureSuccess();

version.ShouldNotBeNull();
```



### Test a project using the MSBuild VSTest target



``` CSharp
// Adds the namespace "HostApi" to use .NET build API
using HostApi;

// Creates a new test project, running a command like: "dotnet new mstest -n MyTests --force"
var result = new DotNetNew("mstest", "-n", "MyTests", "--force")
    .Build()
    .EnsureSuccess();

result.ExitCode.ShouldBe(0);

// Runs tests via a command like: "dotnet msbuild /t:VSTest" from the directory "MyTests"
result = new MSBuild()
    .WithTarget("VSTest")
    .WithWorkingDirectory("MyTests")
    .Build()
    .EnsureSuccess();

// The "result" variable provides details about a build
result.ExitCode.ShouldBe(0);
result.Summary.Tests.ShouldBe(1);
result.Tests.Count(test => test.State == TestState.Finished).ShouldBe(1);
```



### Pack a project



``` CSharp
// Adds the namespace "HostApi" to use .NET build API
using HostApi;

// Creates a new library project, running a command like: "dotnet new classlib -n MyLib --force"
var result = new DotNetNew("classlib", "-n", "MyLib", "--force")
    .Build()
    .EnsureSuccess();

result.ExitCode.ShouldBe(0);

// Creates a NuGet package of version 1.2.3 for the project, running a command like: "dotnet pack /p:version=1.2.3" from the directory "MyLib"
result = new DotNetPack()
    .WithWorkingDirectory("MyLib")
    .AddProps(("version", "1.2.3"))
    .Build()
    .EnsureSuccess();

result.ExitCode.ShouldBe(0);
```



### Publish a project



``` CSharp
// Adds the namespace "HostApi" to use .NET build API
using HostApi;

// Creates a new library project, running a command like: "dotnet new classlib -n MyLib --force"
var result = new DotNetNew("classlib", "-n", "MyLib", "--force", "-f", "net8.0")
    .Build()
    .EnsureSuccess();

result.ExitCode.ShouldBe(0);

// Publish the project, running a command like: "dotnet publish --framework net6.0" from the directory "MyLib"
result = new DotNetPublish()
    .WithWorkingDirectory("MyLib")
    .WithFramework("net8.0")
    .Build()
    .EnsureSuccess();

result.ExitCode.ShouldBe(0);
```



### Restore a project



``` CSharp
// Adds the namespace "HostApi" to use .NET build API
using HostApi;

// Creates a new library project, running a command like: "dotnet new classlib -n MyLib --force"
var result = new DotNetNew("classlib", "-n", "MyLib", "--force")
    .Build()
    .EnsureSuccess();

result.ExitCode.ShouldBe(0);

// Restore the project, running a command like: "dotnet restore" from the directory "MyLib"
result = new DotNetRestore()
    .WithWorkingDirectory("MyLib")
    .Build()
    .EnsureSuccess();

result.ExitCode.ShouldBe(0);
```



### Run a project



``` CSharp
// Adds the namespace "HostApi" to use .NET build API
using HostApi;

// Creates a new console project, running a command like: "dotnet new console -n MyApp --force"
var result = new DotNetNew("console", "-n", "MyApp", "--force")
    .Build()
    .EnsureSuccess();

result.ExitCode.ShouldBe(0);

// Runs the console project using a command like: "dotnet run" from the directory "MyApp"
var stdOut = new List<string>();
result = new DotNetRun().WithWorkingDirectory("MyApp")
    .Build(message => stdOut.Add(message.Text))
    .EnsureSuccess();

result.ExitCode.ShouldBe(0);

// Checks StdOut
stdOut.ShouldBe(new[] {"Hello, World!"});
```



### Test a project



``` CSharp
// Adds the namespace "HostApi" to use .NET build API
using HostApi;

// Creates a new test project, running a command like: "dotnet new mstest -n MyTests --force"
var result = new DotNetNew("mstest", "-n", "MyTests", "--force")
    .Build()
    .EnsureSuccess();

result.ExitCode.ShouldBe(0);

// Runs tests via a command like: "dotnet test" from the directory "MyTests"
result = new DotNetTest()
    .WithWorkingDirectory("MyTests")
    .Build()
    .EnsureSuccess();

// The "result" variable provides details about a build
result.ExitCode.ShouldBe(0);
result.Summary.Tests.ShouldBe(1);
result.Tests.Count(test => test.State == TestState.Finished).ShouldBe(1);
```



### Run tests under dotCover



``` CSharp
// Adds the namespace "HostApi" to use .NET build API
using HostApi;

// Creates a new test project, running a command like: "dotnet new mstest -n MyTests --force"
new DotNetNew("mstest", "-n", "MyTests", "--force")
    .Run()
    .EnsureSuccess();

// Creates the tool manifest and installs the dotCover tool locally
// It is better to run the following 2 commands manually
// and commit these changes to a source control
new DotNetNew("tool-manifest")
    .Run()
    .EnsureSuccess();

new DotNetCustom("tool", "install", "--local", "JetBrains.dotCover.GlobalTool")
    .Run()
    .EnsureSuccess();

// Creates a test command
var test = new DotNetTest()
    .WithProject("MyTests");

var dotCoverSnapshot = Path.Combine("MyTests", "dotCover.dcvr");
var dotCoverReport = Path.Combine("MyTests", "dotCover.html");
// Modifies the test command by putting "dotCover" in front of all arguments
// to have something like "dotnet dotcover test ..."
// and adding few specific arguments to the end
var testUnderDotCover = test.Customize(cmd =>
    cmd.ClearArgs()
    + "dotcover"
    + cmd.Args
    + $"--dcOutput={dotCoverSnapshot}"
    + "--dcFilters=+:module=TeamCity.CSharpInteractive.HostApi;+:module=dotnet-csi"
    + "--dcAttributeFilters=System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage");

// Runs tests under dotCover via a command like: "dotnet dotcover test ..."
var result = testUnderDotCover
    .Build()
    .EnsureSuccess();

// The "result" variable provides details about a build
result.ExitCode.ShouldBe(0);
result.Tests.Count(i => i.State == TestState.Finished).ShouldBe(1);

// Generates a HTML code coverage report.
new DotNetCustom("dotCover", "report", $"--source={dotCoverSnapshot}", $"--output={dotCoverReport}", "--reportType=HTML")
    .Run().EnsureSuccess();

// Check for a dotCover report
File.Exists(dotCoverReport).ShouldBeTrue();
```



### Restore local tools



``` CSharp
// Adds the namespace "HostApi" to use .NET build API
using HostApi;

var projectDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()[..4]);
Directory.CreateDirectory(projectDir);

// Creates a local tool manifest 
new DotNetNew("tool-manifest")
    .WithWorkingDirectory(projectDir)
    .Run()
    .EnsureSuccess();

// Restore local tools
new DotNetToolRestore()
    .WithWorkingDirectory(projectDir)
    .Run()
    .EnsureSuccess();
```



### Test an assembly



``` CSharp
// Adds the namespace "HostApi" to use .NET build API
using HostApi;

// Creates a new test project, running a command like: "dotnet new mstest -n MyTests --force"
var result = new DotNetNew("mstest", "-n", "MyTests", "--force")
    .Build()
    .EnsureSuccess();

result.ExitCode.ShouldBe(0);

// Builds the test project, running a command like: "dotnet build -c Release" from the directory "MyTests"
result = new DotNetBuild()
    .WithWorkingDirectory("MyTests")
    .WithConfiguration("Release")
    .WithOutput("MyOutput")
    .Build()
    .EnsureSuccess();

result.ExitCode.ShouldBe(0);

// Runs tests via a command like: "dotnet vstest" from the directory "MyTests"
result = new VSTest()
    .AddTestFileNames(Path.Combine("MyOutput", "MyTests.dll"))
    .WithWorkingDirectory("MyTests")
    .Build()
    .EnsureSuccess();

// The "result" variable provides details about a build
result.ExitCode.ShouldBe(0);
result.Summary.Tests.ShouldBe(1);
result.Tests.Count(test => test.State == TestState.Finished).ShouldBe(1);
```



### Build a project using MSBuild



``` CSharp
// Adds the namespace "HostApi" to use .NET build API
using HostApi;

// Creates a new library project, running a command like: "dotnet new classlib -n MyLib --force"
var result = new DotNetNew("classlib", "-n", "MyLib", "--force")
    .Build()
    .EnsureSuccess();

result.ExitCode.ShouldBe(0);

// Builds the library project, running a command like: "dotnet msbuild /t:Build -restore /p:configuration=Release -verbosity=detailed" from the directory "MyLib"
result = new MSBuild()
    .WithWorkingDirectory("MyLib")
    .WithTarget("Build")
    .WithRestore(true)
    .AddProps(("configuration", "Release"))
    .WithVerbosity(DotNetVerbosity.Detailed)
    .Build()
    .EnsureSuccess();

// The "result" variable provides details about a build
result.Errors.Any(message => message.State == BuildMessageState.StdError).ShouldBeFalse();
result.ExitCode.ShouldBe(0);
```



### Shuts down build servers



``` CSharp
// Adds the namespace "HostApi" to use .NET build API
using HostApi;

// Shuts down all build servers that are started from dotnet.
new DotNetBuildServerShutdown()
    .Run()
    .EnsureSuccess();
```



### TeamCity integration via service messages

For more details how to use TeamCity service message API please see [this](https://github.com/JetBrains/TeamCity.ServiceMessages) page. Instead of creating a root message writer like in the following example:
``` CSharp
using JetBrains.TeamCity.ServiceMessages.Write.Special;
using var writer = new TeamCityServiceMessages().CreateWriter(Console.WriteLine);
```
use this statement:
``` CSharp
using JetBrains.TeamCity.ServiceMessages.Write.Special;
using var writer = GetService<ITeamCityWriter>();
```
This sample opens a block _My Tests_ and reports about two tests:

``` CSharp
// Adds a namespace to use ITeamCityWriter
using JetBrains.TeamCity.ServiceMessages.Write.Special;

using var writer = GetService<ITeamCityWriter>();
using (var tests = writer.OpenBlock("My Tests"))
{
    using (var test = tests.OpenTest("Test1"))
    {
        test.WriteStdOutput("Hello");
        test.WriteImage("TestsResults/Test1Screenshot.jpg", "Screenshot");
        test.WriteDuration(TimeSpan.FromMilliseconds(10));
    }

    using (var test = tests.OpenTest("Test2"))
    {
        test.WriteIgnored("Some reason");
    }
}
```

For more information on TeamCity Service Messages, see [this](https://www.jetbrains.com/help/teamcity/service-messages.html) page.

