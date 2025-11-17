#  Build automation system for .NET

[![NuGet csi](https://img.shields.io/nuget/v/dotnet-csi?includePreReleases=true)](https://www.nuget.org/packages/dotnet-csi)
![GitHub](https://img.shields.io/github/license/devteam/csharp-interactive)
[<img src="http://teamcity.jetbrains.com/app/rest/builds/buildType:(id:OpenSourceProjects_DevTeam_CScriptInteractive_BuildAndTest)/statusIcon.svg"/>](http://teamcity.jetbrains.com/viewType.html?buildTypeId=OpenSourceProjects_DevTeam_CScriptInteractive_BuildAndTest&guest=1)
![GitHub Build](https://github.com/DevTeam/csharp-interactive/actions/workflows/main.yml/badge.svg)

![](docs/CSharpInteractive.gif)

C# interactive build automation system makes it easy to build .NET projects. It can be part of your solution as a regular .NET console project or run C# scripts without compiling them, or even run in REPL mode - allowing you to run C# interactively.

![](docs/icon.png)

## Key Features

✔️ Three Integrated Execution Modes
  - Flexible interoperability between modes (#operating-modes) to adapt to diverse workflow requirements.

✔️ Native Cross-Platform Support
  - Seamlessly operates on Windows, Linux, and macOS without compatibility compromises.

✔️ Integrated Debugging Capabilities
  - Debug ".NET build projects" directly to identify and resolve issues during compilation and builds.

✔️ Zero Abstraction Overhead
  - Eliminates restrictive constructs (e.g., Tasks/Targets):
  - Pure .NET codebase – no proprietary syntax or hidden layers
  - Industry-standard practices ensure long-term maintainability

✔️ Granular Build Control API
  - Precise management of builds, tests, and deployments with streamlined project configuration.

✔️ Consolidated Build Analytics
  - Actionable insights through summarized statistics.

## Operating modes

<details>

<summary>.NET build project</summary>

Seamless integration into existing solutions as a standard .NET console project.

### Typical .NET console project

1. Create the Project  
  Initialize a new console application using .NET CLI:
  ```bash
  dotnet new console -n MyBuildApp
  cd MyConsoleApp
  ```  

2. Install Essential Package  
  Add the [CSharpInteractive](https://www.nuget.org/packages/CSharpInteractive) NuGet package for scripting capabilities:
  ```bash
  dotnet add package CSharpInteractive
  ```  

### Creating a Build Automation Project from the _build_ template
1. Install the Template  
  First, install the `CSharpInteractive.Templates` NuGet package as a dotnet template:
  ```bash
  dotnet new install CSharpInteractive.Templates
  ```  

*[Detailed installation guide](https://github.com/DevTeam/csharp-interactive/wiki/Install-the-C%23-script-template)*

2. Generate the Project  
  Create a new project named **Build** using the template:
  ```bash
  dotnet new build -o ./Build
  ```  

### Dual Execution Modes

The generated project supports two run methods:

- **Compiled Application** (via `Program.cs`):
  ```bash
  dotnet run --project ./Build
  ```  
- **Script Execution** (via `Program.csx`):
  ```bash
  dotnet csi ./Build
  ```

</details>

<details>

<summary>Running C# script</summary>

Direct execution of C# scripts without prior compilation:
- Zero-Compilation Workflow - execute .csx files directly using Roslyn scripting engines, bypassing traditional dotnet build steps for rapid iteration.
- Cross-Platform Scripting
  - Windows - integrate with PowerShell/PowerShell Core automation pipelines
  - Linux/macOS - combine with bash/zsh scripts via shebang directives (#!/usr/bin/env dotnet-script)
  - Dependency Management - resolve NuGet packages in scripts via #r "nuget: PackageName/Version" syntax, with local cache optimization.

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

</details>

<details>

<summary>Interactive</summary>

REPL (Read-Eval-Print-Loop) interface for interactive code evaluation. Please see [this page](https://www.nuget.org/packages/dotnet-csi) for installation details.

Launch the tool in the interactive mode:

```Shell
dotnet csi
```

Simply enter C# commands sequentially one line after another and get the result in console output.

</details>

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
  - [Writing a line to a build log](#writing-a-line-to-a-build-log)
  - [Writing a line highlighted with "Header" color to a build log](#writing-a-line-highlighted-with-"header"-color-to-a-build-log)
  - [Writing an empty line to a build log](#writing-an-empty-line-to-a-build-log)
  - [Registering errors in the build log](#registering-errors-in-the-build-log)
  - [Registering warnings in the build log](#registering-warnings-in-the-build-log)
  - [Registering a summary in the build log](#registering-a-summary-in-the-build-log)
  - [Registering information in the build log](#registering-information-in-the-build-log)
  - [Registering trace information in the build log](#registering-trace-information-in-the-build-log)
- Arguments and parameters
  - [Command line arguments](#command-line-arguments)
  - [Using properties](#using-properties)
- Microsoft DI
  - [Using the Host property](#using-the-host-property)
  - [Getting services](#getting-services)
  - [Using service collections](#using-service-collections)
- NuGet
  - [Restoring a NuGet package of newest version](#restoring-a-nuget-package-of-newest-version)
  - [Restoring a NuGet package by a version range for the specified .NET and path](#restoring-a-nuget-package-by-a-version-range-for-the-specified-.net-and-path)
- Command Line
  - [Building custom command lines](#building-custom-command-lines)
  - [Running a command line](#running-a-command-line)
  - [Running a command line asynchronously](#running-a-command-line-asynchronously)
  - [Running and analyzing an output](#running-and-analyzing-an-output)
  - [Running asynchronously in parallel](#running-asynchronously-in-parallel)
  - [Cancellation of asynchronous run](#cancellation-of-asynchronous-run)
  - [Running with timeout](#running-with-timeout)
- Docker CLI
  - [Building a project in a docker container](#building-a-project-in-a-docker-container)
  - [Running in docker](#running-in-docker)
- .NET CLI
  - [Adding a NuGet package](#adding-a-nuget-package)
  - [Adding a NuGet source](#adding-a-nuget-source)
  - [Adding a project reference](#adding-a-project-reference)
  - [Adding projects to the solution file](#adding-projects-to-the-solution-file)
  - [Adding project-to-project (P2P) references](#adding-project-to-project-(p2p)-references)
  - [Building a project](#building-a-project)
  - [Building a project using MSBuild](#building-a-project-using-msbuild)
  - [Cleaning a project](#cleaning-a-project)
  - [Clearing the specified NuGet cache type](#clearing-the-specified-nuget-cache-type)
  - [Creating a new project, configuration file, or solution based on the specified template](#creating-a-new-project,-configuration-file,-or-solution-based-on-the-specified-template)
  - [Deleting a NuGet package to the server](#deleting-a-nuget-package-to-the-server)
  - [Disabling a NuGet source](#disabling-a-nuget-source)
  - [Displaing template package metadata](#displaing-template-package-metadata)
  - [Enabling a NuGet source](#enabling-a-nuget-source)
  - [Enabling or disabling workload-set update mode](#enabling-or-disabling-workload-set-update-mode)
  - [Executing a dotnet application](#executing-a-dotnet-application)
  - [Fixing (non code style) analyzer issues](#fixing-(non-code-style)-analyzer-issues)
  - [Fixing code style issues](#fixing-code-style-issues)
  - [Formatting a code](#formatting-a-code)
  - [Getting a value of a specified NuGet configuration setting](#getting-a-value-of-a-specified-nuget-configuration-setting)
  - [Installing a template package](#installing-a-template-package)
  - [Installing optional workloads](#installing-optional-workloads)
  - [Installing the .NET local tools that are in scope for the current directory](#installing-the-.net-local-tools-that-are-in-scope-for-the-current-directory)
  - [Installing the specified .NET tool](#installing-the-specified-.net-tool)
  - [Installing workloads needed for a project or a solution](#installing-workloads-needed-for-a-project-or-a-solution)
  - [Invoking a local tool](#invoking-a-local-tool)
  - [Packing a code into a NuGet package](#packing-a-code-into-a-nuget-package)
  - [Printing a dependency graph for NuGet package](#printing-a-dependency-graph-for-nuget-package)
  - [Printing all .NET tools of the specified type currently installed](#printing-all-.net-tools-of-the-specified-type-currently-installed)
  - [Printing all configured NuGet sources](#printing-all-configured-nuget-sources)
  - [Printing all projects in a solution file](#printing-all-projects-in-a-solution-file)
  - [Printing available templates to be run using dotnet new](#printing-available-templates-to-be-run-using-dotnet-new)
  - [Printing installed workloads](#printing-installed-workloads)
  - [Printing nuget configuration files currently being applied to a directory](#printing-nuget-configuration-files-currently-being-applied-to-a-directory)
  - [Printing NuGet packages for a project](#printing-nuget-packages-for-a-project)
  - [Printing project references for a project](#printing-project-references-for-a-project)
  - [Printing the latest available version of the .NET SDK and .NET Runtime, for each feature band](#printing-the-latest-available-version-of-the-.net-sdk-and-.net-runtime,-for-each-feature-band)
  - [Printing the location of the specified NuGet cache type](#printing-the-location-of-the-specified-nuget-cache-type)
  - [Publishing an application and its dependencies to a folder for deployment to a hosting system](#publishing-an-application-and-its-dependencies-to-a-folder-for-deployment-to-a-hosting-system)
  - [Pushing a NuGet package to the server](#pushing-a-nuget-package-to-the-server)
  - [Removing a NuGet package](#removing-a-nuget-package)
  - [Removing a project or multiple projects from the solution file](#removing-a-project-or-multiple-projects-from-the-solution-file)
  - [Removing an existing source from your NuGet configuration files](#removing-an-existing-source-from-your-nuget-configuration-files)
  - [Repairing workloads installations](#repairing-workloads-installations)
  - [Restoring the dependencies and tools of a project](#restoring-the-dependencies-and-tools-of-a-project)
  - [Running a .NET application](#running-a-.net-application)
  - [Running a custom .NET command](#running-a-custom-.net-command)
  - [Running source code without any explicit compile or launch commands](#running-source-code-without-any-explicit-compile-or-launch-commands)
  - [Running tests from the specified assemblies](#running-tests-from-the-specified-assemblies)
  - [Searching all .NET tools that are published to NuGet](#searching-all-.net-tools-that-are-published-to-nuget)
  - [Searching for a NuGet package](#searching-for-a-nuget-package)
  - [Searching for optional workloads](#searching-for-optional-workloads)
  - [Searching for the templates](#searching-for-the-templates)
  - [Setting the value of a specified NuGet configuration setting](#setting-the-value-of-a-specified-nuget-configuration-setting)
  - [Signing with certificate](#signing-with-certificate)
  - [Storing the specified assemblies in the runtime package store.](#storing-the-specified-assemblies-in-the-runtime-package-store.)
  - [Testing a project using the MSBuild VSTest target](#testing-a-project-using-the-msbuild-vstest-target)
  - [Testing from the specified project](#testing-from-the-specified-project)
  - [Uninstalling a specified workload](#uninstalling-a-specified-workload)
  - [Uninstalling a template package](#uninstalling-a-template-package)
  - [Uninstalling the specified .NET tool](#uninstalling-the-specified-.net-tool)
  - [Unsetting the value of a specified NuGet configuration setting](#unsetting-the-value-of-a-specified-nuget-configuration-setting)
  - [Updating a NuGet source](#updating-a-nuget-source)
  - [Updating installed template packages](#updating-installed-template-packages)
  - [Updating installed workloads](#updating-installed-workloads)
  - [Updating the specified .NET tool](#updating-the-specified-.net-tool)
  - [Working with development certificates](#working-with-development-certificates)
  - [Running C# script](#running-c#-script)
  - [Shutting down build servers](#shutting-down-build-servers)
- TeamCity API
  - [TeamCity integration via service messages](#teamcity-integration-via-service-messages)

## Output, logging and tracing

### Writing a line to a build log

``` CSharp
WriteLine("Hello");
```

### Writing a line highlighted with "Header" color to a build log

``` CSharp
WriteLine("Hello", Header);
WriteLine("Hello ".WithColor(Header), "world!");
```

### Writing an empty line to a build log

``` CSharp
WriteLine();
```

### Registering errors in the build log

``` CSharp
Error("Error info");
Error("Error info", "Error identifier");
Error("Error: ".WithColor(), "datails".WithColor(Color.Details));
```

### Registering warnings in the build log

``` CSharp
Warning("Warning info");
Warning("Warning ", "info".WithColor(Color.Details));
```

### Registering a summary in the build log

``` CSharp
Summary("Summary message");
Summary("Summary ", "message".WithColor(Color.Details));
```

### Registering information in the build log

``` CSharp
Info("Some info");
Info("Some ", "info".WithColor(Color.Details));
```

### Registering trace information in the build log

``` CSharp
Trace("Some trace info");
Trace("Some trace ", "info".WithColor(Color.Details));
```

## Arguments and parameters

### Command line arguments
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

### Using properties

``` CSharp
WriteLine(Props["version"]);
WriteLine(Props.Get("configuration", "Release"));

// Some CI/CDs have integration of these properties.
// For example in TeamCity this property with all changes will be available in the next TeamCity steps.
Props["version"] = "1.1.6";
```

## Microsoft DI

### Using the Host property
[_Host_](TeamCity.CSharpInteractive.HostApi/IHost.cs) is actually the provider of all global properties and methods.
``` CSharp
var packages = Host.GetService<INuGet>();
Host.WriteLine("Hello");
```

### Getting services
This method might be used to get access to different APIs like [INuGet](TeamCity.CSharpInteractive.HostApi/INuGet.cs) or [ICommandLine](TeamCity.CSharpInteractive.HostApi/ICommandLine.cs).
``` CSharp
GetService<INuGet>();

var serviceProvider = GetService<IServiceProvider>();
serviceProvider.GetService(typeof(INuGet));
```
Besides that, it is possible to get an instance of [System.IServiceProvider](https://docs.microsoft.com/en-US/dotnet/api/system.iserviceprovider) to access APIs.
### Using service collections

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

## NuGet

### Restoring a NuGet package of newest version

``` CSharp
using HostApi;

IEnumerable<NuGetPackage> packages = GetService<INuGet>()
    .Restore(new NuGetRestoreSettings("IoC.Container").WithVersionRange(VersionRange.All));
```

### Restoring a NuGet package by a version range for the specified .NET and path

``` CSharp
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

## Command Line

### Building custom command lines

``` CSharp
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
    .Run().EnsureSuccess();

(new CommandLine("cmd") + "/c" + "echo" + "Hello")
    .Run().EnsureSuccess();

"cmd".AsCommandLine("/c", "echo", "Hello")
    .Run().EnsureSuccess();

("cmd".AsCommandLine() + "/c" + "echo" + "Hello")
    .Run().EnsureSuccess();

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

### Running a command line

``` CSharp
using HostApi;

GetService<ICommandLineRunner>()
    .Run(new CommandLine("cmd", "/c", "DIR")).EnsureSuccess();

// or the same thing using the extension method
new CommandLine("cmd", "/c", "DIR")
    .Run().EnsureSuccess();

// using operator '+'
var cmd = new CommandLine("cmd") + "/c" + "DIR";
cmd.Run().EnsureSuccess();

// with environment variables
cmd = new CommandLine("cmd") + "/c" + "DIR" + ("MyEnvVar", "Some Value");
cmd.Run().EnsureSuccess();
```

### Running a command line asynchronously

``` CSharp
using HostApi;

await GetService<ICommandLineRunner>()
    .RunAsync(new CommandLine("cmd", "/C", "DIR")).EnsureSuccess();

// or the same thing using the extension method
var result = await new CommandLine("cmd", "/c", "DIR")
    .RunAsync().EnsureSuccess();
```

### Running and analyzing an output

``` CSharp
using HostApi;

var lines = new List<string>();
var result = new CommandLine("cmd", "/c", "SET")
    .AddVars(("MyEnv", "MyVal"))
    .Run(output => lines.Add(output.Line)).EnsureSuccess();

lines.ShouldContain("MyEnv=MyVal");
```

### Running asynchronously in parallel

``` CSharp
using HostApi;

var task = new CommandLine("cmd", "/c", "DIR")
    .RunAsync().EnsureSuccess();

var result = new CommandLine("cmd", "/c", "SET")
    .Run().EnsureSuccess();

await task;
```

### Cancellation of asynchronous run
Cancellation will destroy the process and its child processes.
``` CSharp
using HostApi;

var cancellationTokenSource = new CancellationTokenSource();
var task = new CommandLine("cmd", "/c", "TIMEOUT", "/T", "120")
    .RunAsync(null, cancellationTokenSource.Token);

cancellationTokenSource.CancelAfter(TimeSpan.FromMilliseconds(100));
task.IsCompleted.ShouldBeFalse();
```

### Running with timeout
If timeout expired a process will be killed.
``` CSharp
using HostApi;

var exitCode = new CommandLine("cmd", "/c", "TIMEOUT", "/T", "120")
    .Run(null, TimeSpan.FromMilliseconds(1))
    .ExitCode;
```

## Docker CLI

### Building a project in a docker container

``` CSharp
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
    .Run().EnsureSuccess();

// Builds the library project in a docker container
var result = dockerRun
    .WithCommandLine(new DotNetBuild().WithProject("MyLib/MyLib.csproj"))
    .Build().EnsureSuccess();

string ToAbsoluteLinuxPath(string path) =>
    "/" + path.Replace(":", "").Replace('\\', '/');
```

### Running in docker

``` CSharp
using HostApi;

// Creates some command line to run in a docker container
var cmd = new CommandLine("whoami");

// Runs the command line in a docker container
var result = new DockerRun(cmd, "mcr.microsoft.com/dotnet/sdk")
    .WithAutoRemove(true)
    .Run().EnsureSuccess();
```

## .NET CLI

### Adding a NuGet package

``` CSharp
using HostApi;

var result = new DotNetAddPackage()
    .WithWorkingDirectory("MyLib")
    .WithPackage("Pure.DI")
    .Run().EnsureSuccess();
```

### Adding a NuGet source

``` CSharp
using HostApi;

new DotNetNuGetAddSource()
    .WithName("TestSource")
    .WithSource(source)
    .Run().EnsureSuccess();
```

### Adding a project reference

``` CSharp
using HostApi;

new DotNetAddReference()
    .WithProject(Path.Combine("MyTests", "MyTests.csproj"))
    .WithReferences(Path.Combine("MyLib", "MyLib.csproj"))
    .Run().EnsureSuccess();

new DotNetRemoveReference()
    .WithProject(Path.Combine("MyTests", "MyTests.csproj"))
    .WithReferences(Path.Combine("MyLib", "MyLib.csproj"))
    .Run().EnsureSuccess();

var lines = new List<string>();
new DotNetListReference()
    .WithProject(Path.Combine("MyTests", "MyTests.csproj"))
    .Run(output => lines.Add(output.Line));

lines.Any(i => i.Contains("MyLib.csproj")).ShouldBeFalse();
```

### Adding projects to the solution file

``` CSharp
using HostApi;

new DotNetNew()
    .WithTemplateName("sln")
    .WithName("MySolution")
    .WithForce(true)
    .Run().EnsureSuccess();

new DotNetSlnAdd()
    .WithSolution("MySolution.sln")
    .AddProjects(
        Path.Combine("MyLib", "MyLib.csproj"),
        Path.Combine("MyTests", "MyTests.csproj"))
    .Run().EnsureSuccess();
```

### Adding project-to-project (P2P) references

``` CSharp
using HostApi;

var result = new DotNetAddReference()
    .WithProject(Path.Combine("MyTests", "MyTests.csproj"))
    .WithReferences(Path.Combine("MyLib", "MyLib.csproj"))
    .Run().EnsureSuccess();

```

### Building a project

``` CSharp
using HostApi;

var messages = new List<BuildMessage>();
var result = new DotNetBuild()
    .WithWorkingDirectory("MyTests")
    .Build(message => messages.Add(message)).EnsureSuccess();

result.Errors.Any(message => message.State == BuildMessageState.StdError).ShouldBeFalse(result.ToString());
result.ExitCode.ShouldBe(0, result.ToString());
```

### Building a project using MSBuild

``` CSharp
using HostApi;

// Creates a new library project, running a command like: "dotnet new classlib -n MyLib --force"
new DotNetNew()
    .WithTemplateName("classlib")
    .WithName("MyLib")
    .WithForce(true)
    .Build().EnsureSuccess();

// Builds the library project, running a command like: "dotnet msbuild /t:Build -restore /p:configuration=Release -verbosity=detailed" from the directory "MyLib"
var result = new MSBuild()
    .WithWorkingDirectory("MyLib")
    .WithTarget("Build")
    .WithRestore(true)
    .AddProps(("configuration", "Release"))
    .WithVerbosity(DotNetVerbosity.Detailed)
    .Build().EnsureSuccess();

// The "result" variable provides details about a build
result.Errors.Any(message => message.State == BuildMessageState.StdError).ShouldBeFalse(result.ToString());
result.ExitCode.ShouldBe(0, result.ToString());
```

### Cleaning a project

``` CSharp
using HostApi;

// Clean the project, running a command like: "dotnet clean" from the directory "MyLib"
new DotNetClean()
    .WithWorkingDirectory("MyLib")
    .Build().EnsureSuccess();
```

### Clearing the specified NuGet cache type

``` CSharp
using HostApi;

new DotNetNuGetLocalsClear()
    .WithCacheLocation(NuGetCacheLocation.Temp)
    .Run().EnsureSuccess();
```

### Creating a new project, configuration file, or solution based on the specified template

``` CSharp
using HostApi;

new DotNetNew()
    .WithTemplateName("classlib")
    .WithName("MyLib")
    .WithForce(true)
    .Run().EnsureSuccess();
```

### Deleting a NuGet package to the server

``` CSharp
using HostApi;

new DotNetNuGetDelete()
    .WithPackage("MyLib")
    .WithPackageVersion("1.0.0")
    .WithSource(repoUrl)
    .Run().EnsureSuccess();
```

### Disabling a NuGet source

``` CSharp
using HostApi;

new DotNetNuGetDisableSource()
    .WithName("TestSource")
    .Run().EnsureSuccess();
```

### Displaing template package metadata

``` CSharp
using HostApi;

new DotNetNewDetails()
    .WithTemplateName("CSharpInteractive.Templates")
    .Run().EnsureSuccess();
```

### Enabling a NuGet source

``` CSharp
using HostApi;

new DotNetNuGetEnableSource()
    .WithName("TestSource")
    .Run().EnsureSuccess();
```

### Enabling or disabling workload-set update mode

``` CSharp
using HostApi;

new DotNetWorkloadConfig()
    .WithUpdateMode(DotNetWorkloadUpdateMode.WorkloadSet)
    .Run().EnsureSuccess();
```

### Executing a dotnet application

``` CSharp
using HostApi;
new DotNetExec()
    .WithPathToApplication(Path.Combine(path, "MyApp.dll"))
    .Run().EnsureSuccess();
```

### Fixing (non code style) analyzer issues

``` CSharp
using HostApi;

new DotNetFormatAnalyzers()
    .WithWorkingDirectory("MyLib")
    .WithProject("MyLib.csproj")
    .AddDiagnostics("CA1831", "CA1832")
    .WithSeverity(DotNetFormatSeverity.Warning)
    .Run().EnsureSuccess();
```

### Fixing code style issues

``` CSharp
using HostApi;

new DotNetFormatStyle()
    .WithWorkingDirectory("MyLib")
    .WithProject("MyLib.csproj")
    .AddDiagnostics("IDE0005", "IDE0006")
    .WithSeverity(DotNetFormatSeverity.Information)
    .Run().EnsureSuccess();
```

### Formatting a code

``` CSharp
using HostApi;

new DotNetFormat()
    .WithWorkingDirectory("MyLib")
    .WithProject("MyLib.csproj")
    .AddDiagnostics("IDE0005", "IDE0006")
    .AddIncludes(".", "./tests")
    .AddExcludes("./obj")
    .WithSeverity(DotNetFormatSeverity.Information)
    .Run().EnsureSuccess();
```

### Getting a value of a specified NuGet configuration setting

``` CSharp
using HostApi;

string? repositoryPath = null;
new DotNetNuGetConfigGet()
    .WithConfigKey("repositoryPath")
    .Run(output => repositoryPath = output.Line).EnsureSuccess();
```

### Installing a template package

``` CSharp
using HostApi;

new DotNetNewInstall()
    .WithPackage("Pure.DI.Templates")
    .Run().EnsureSuccess();
```

### Installing optional workloads

``` CSharp
using HostApi;

new DotNetWorkloadInstall()
    .AddWorkloads("aspire")
    .Run().EnsureSuccess();
```

### Installing the .NET local tools that are in scope for the current directory

``` CSharp
using HostApi;

// Creates a local tool manifest 
new DotNetNew()
    .WithTemplateName("tool-manifest")
    .Run().EnsureSuccess();

new DotNetToolRestore()
    .Run().EnsureSuccess();
```

### Installing the specified .NET tool

``` CSharp
using HostApi;

new DotNetToolInstall()
    .WithLocal(true)
    .WithPackage("dotnet-csi")
    .WithVersion("1.1.2")
    .Run().EnsureSuccess();
```

### Installing workloads needed for a project or a solution

``` CSharp
using HostApi;

new DotNetWorkloadRestore()
    .WithProject(Path.Combine("MyLib", "MyLib.csproj"))
    .Run().EnsureSuccess();
```

### Invoking a local tool

``` CSharp
using HostApi;

var script = Path.GetTempFileName();
File.WriteAllText(script, "Console.WriteLine($\"Hello, {Args[0]}!\");");

var stdOut = new List<string>();
new DotNetToolRun()
    .WithCommandName("dotnet-csi")
    .AddArgs(script)
    .AddArgs("World")
    .Run(output => stdOut.Add(output.Line))
    .EnsureSuccess();

// Checks stdOut
stdOut.Contains("Hello, World!").ShouldBeTrue();
```

### Packing a code into a NuGet package

``` CSharp
using HostApi;

// Creates a NuGet package of version 1.2.3 for the project
new DotNetPack()
    .WithWorkingDirectory("MyLib")
    .WithOutput(path)
    .AddProps(("version", "1.2.3"))
    .Build().EnsureSuccess();
```

### Printing a dependency graph for NuGet package

``` CSharp
using HostApi;

new DotNetNuGetWhy()
    .WithProject(Path.Combine("MyLib", "MyLib.csproj"))
    .WithPackage("MyLib.1.2.3.nupkg")
    .Run().EnsureSuccess();
```

### Printing all .NET tools of the specified type currently installed

``` CSharp
using HostApi;

new DotNetToolList()
    .WithLocal(true)
    .Run().EnsureSuccess();

new DotNetToolList()
    .WithGlobal(true)
    .Run().EnsureSuccess();
```

### Printing all configured NuGet sources

``` CSharp
using HostApi;

new DotNetNuGetListSource()
    .WithFormat(NuGetListFormat.Short)
    .Run().EnsureSuccess();
```

### Printing all projects in a solution file

``` CSharp
using HostApi;

var lines = new List<string>();
new DotNetSlnList()
    .WithSolution("MySolution.sln")
    .Run(output => lines.Add(output.Line))
    .EnsureSuccess();
```

### Printing available templates to be run using dotnet new

``` CSharp
using HostApi;

new DotNetNewList()
    .Run().EnsureSuccess();
```

### Printing installed workloads

``` CSharp
using HostApi;

new DotNetWorkloadList()
    .Run().EnsureSuccess();
```

### Printing nuget configuration files currently being applied to a directory

``` CSharp
using HostApi;

var configPaths = new List<string>();
new DotNetNuGetConfigPaths()
    .Run(output => configPaths.Add(output.Line)).EnsureSuccess();
```

### Printing NuGet packages for a project

``` CSharp
using HostApi;

new DotNetAddPackage()
    .WithWorkingDirectory("MyLib")
    .WithPackage("Pure.DI")
    .Run().EnsureSuccess();

var lines = new List<string>();
new DotNetListPackage()
    .WithProject(Path.Combine("MyLib", "MyLib.csproj"))
    .WithVerbosity(DotNetVerbosity.Minimal)
    .Run(output => lines.Add(output.Line));

lines.Any(i => i.Contains("Pure.DI")).ShouldBeTrue();
```

### Printing project references for a project

``` CSharp
using HostApi;

new DotNetAddReference()
    .WithProject(Path.Combine("MyTests", "MyTests.csproj"))
    .WithReferences(Path.Combine("MyLib", "MyLib.csproj"))
    .Run().EnsureSuccess();

var lines = new List<string>();
new DotNetListReference()
    .WithProject(Path.Combine("MyTests", "MyTests.csproj"))
    .Run(output => lines.Add(output.Line));

lines.Any(i => i.Contains("MyLib.csproj")).ShouldBeTrue();
```

### Printing the latest available version of the .NET SDK and .NET Runtime, for each feature band

``` CSharp
using HostApi;

var sdks = new List<Sdk>();
new DotNetSdkCheck()
    .Run(output =>
    {
        if (output.Line.StartsWith("Microsoft."))
        {
            var data = output.Line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (data.Length >= 2)
            {
                sdks.Add(new Sdk(data[0], NuGetVersion.Parse(data[1])));
            }
        }
    })
    .EnsureSuccess();

sdks.Count.ShouldBeGreaterThan(0);

record Sdk(string Name, NuGetVersion Version);
```

### Printing the location of the specified NuGet cache type

``` CSharp
using HostApi;

new DotNetNuGetLocalsList()
    .WithCacheLocation(NuGetCacheLocation.GlobalPackages)
    .Run().EnsureSuccess();
```

### Publishing an application and its dependencies to a folder for deployment to a hosting system

``` CSharp
using HostApi;

new DotNetPublish()
    .WithWorkingDirectory("MyLib")
    .WithFramework(framework)
    .WithOutput("bin")
    .Build().EnsureSuccess();
```

### Pushing a NuGet package to the server

``` CSharp
using HostApi;

new DotNetNuGetPush()
    .WithWorkingDirectory("MyLib")
    .WithPackage(Path.Combine("packages", "MyLib.1.0.0.nupkg"))
    .WithSource(repoUrl)
    .Run().EnsureSuccess();
```

### Removing a NuGet package

``` CSharp
using HostApi;

new DotNetAddPackage()
    .WithWorkingDirectory("MyLib")
    .WithPackage("Pure.DI")
    .Run().EnsureSuccess();

new DotNetRemovePackage()
    .WithWorkingDirectory("MyLib")
    .WithPackage("Pure.DI")
    .Run().EnsureSuccess();
```

### Removing a project or multiple projects from the solution file

``` CSharp
using HostApi;

new DotNetSlnRemove()
    .WithSolution("MySolution.sln")
    .AddProjects(
        Path.Combine("MyLib", "MyLib.csproj"))
    .Run().EnsureSuccess();
```

### Removing an existing source from your NuGet configuration files

``` CSharp
using HostApi;

new DotNetNuGetRemoveSource()
    .WithName("TestSource")
    .Run().EnsureSuccess(); 
```

### Repairing workloads installations

``` CSharp
using HostApi;

new DotNetWorkloadRepair()
    .Run().EnsureSuccess();
```

### Restoring the dependencies and tools of a project

``` CSharp
using HostApi;

new DotNetRestore()
    .WithProject(Path.Combine("MyLib", "MyLib.csproj"))
    .Build().EnsureSuccess();
```

### Running a .NET application

``` CSharp
// Adds the namespace "HostApi" to use .NET build API
using HostApi;

new DotNet()
    .WithPathToApplication(Path.Combine(path, "MyApp.dll"))
    .Run().EnsureSuccess();
```

### Running a custom .NET command

``` CSharp
using HostApi;

// Gets the dotnet version, running a command like: "dotnet --version"
NuGetVersion? version = null;
new DotNetCustom("--version")
    .Run(message => NuGetVersion.TryParse(message.Line, out version))
    .EnsureSuccess();

version.ShouldNotBeNull();
```

### Running source code without any explicit compile or launch commands

``` CSharp
using HostApi;

var stdOut = new List<string>();
new DotNetRun()
    .WithProject(Path.Combine("MyApp", "MyApp.csproj"))
    .Build(message => stdOut.Add(message.Text))
    .EnsureSuccess();
```

### Running tests from the specified assemblies

``` CSharp
using HostApi;

// Runs tests
var result = new VSTest()
    .AddTestFileNames(Path.Combine("bin", "MyTests.dll"))
    .WithWorkingDirectory(path)
    .Build().EnsureSuccess();

// The "result" variable provides details about build and tests
result.ExitCode.ShouldBe(0, result.ToString());
result.Summary.Tests.ShouldBe(1, result.ToString());
result.Tests.Count(test => test.State == TestState.Finished).ShouldBe(1, result.ToString());
```

### Searching all .NET tools that are published to NuGet

``` CSharp
using HostApi;

new DotNetToolSearch()
    .WithPackage("dotnet-csi")
    .WithDetail(true)
    .Run().EnsureSuccess();
```

### Searching for a NuGet package

``` CSharp
using System.Text;
using System.Text.Json;
using HostApi;

var packagesJson = new StringBuilder();
new DotNetPackageSearch()
    .WithSearchTerm("Pure.DI")
    .WithFormat(DotNetPackageSearchResultFormat.Json)
    .Run(output => packagesJson.AppendLine(output.Line)).EnsureSuccess();

var result = JsonSerializer.Deserialize<Result>(
    packagesJson.ToString(),
    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

result.ShouldNotBeNull();
result.SearchResult.SelectMany(i => i.Packages).Count(i => i.Id == "Pure.DI").ShouldBe(1);

record Result(int Version, IReadOnlyCollection<Source> SearchResult);

record Source(string SourceName, IReadOnlyCollection<Package> Packages);

record Package(
    string Id,
    string LatestVersion,
    int TotalDownloads,
    string Owners);
```

### Searching for optional workloads

``` CSharp
using HostApi;

new DotNetWorkloadSearch()
    .WithSearchString("maui")
    .Run().EnsureSuccess();
```

### Searching for the templates

``` CSharp
using HostApi;

new DotNetNewSearch()
    .WithTemplateName("build")
    .Run().EnsureSuccess();
```

### Setting the value of a specified NuGet configuration setting

``` CSharp
using HostApi;

new DotNetNuGetConfigSet()
    .WithConfigFile(configFile)
    .WithConfigKey("repositoryPath")
    .WithConfigValue("MyValue")
    .Run().EnsureSuccess();
```

### Signing with certificate

``` CSharp
using HostApi;

new DotNetNuGetSign()
    .AddPackages("MyLib.1.2.3.nupkg")
    .WithCertificatePath("certificate.pfx")
    .WithCertificatePassword("Abc")
    .WithTimestampingServer("http://timestamp.digicert.com/")
    .Run().EnsureSuccess();
```

### Storing the specified assemblies in the runtime package store.

``` CSharp
using HostApi;

new DotNetStore()
    .AddManifests(Path.Combine("MyLib", "MyLib.csproj"))
    .WithFramework("net8.0")
    .WithRuntime("win-x64")
    .Build();

```

### Testing a project using the MSBuild VSTest target

``` CSharp
using HostApi;

// Runs tests via a command
var result = new MSBuild()
    .WithTarget("VSTest")
    .WithWorkingDirectory("MyTests")
    .Build().EnsureSuccess();

// The "result" variable provides details about a build
result.ExitCode.ShouldBe(0, result.ToString());
result.Summary.Tests.ShouldBe(1, result.ToString());
result.Tests.Count(test => test.State == TestState.Finished).ShouldBe(1, result.ToString());
```

### Testing from the specified project

``` CSharp
using HostApi;

// Runs tests
var result = new DotNetTest()
    .WithProject("MyTests")
    .Build().EnsureSuccess();
```

### Uninstalling a specified workload

``` CSharp
using HostApi;

new DotNetWorkloadUninstall()
    .AddWorkloads("aspire")
    .Run().EnsureSuccess();
```

### Uninstalling a template package

``` CSharp
using HostApi;

new DotNetNewUninstall()
    .WithPackage("Pure.DI.Templates")
    .Run();
```

### Uninstalling the specified .NET tool

``` CSharp
using HostApi;

new DotNetToolUninstall()
    .WithPackage("dotnet-csi")
    .Run().EnsureSuccess();
```

### Unsetting the value of a specified NuGet configuration setting

``` CSharp
using HostApi;

new DotNetNuGetConfigUnset()
    .WithConfigKey("repositoryPath")
    .Run().EnsureSuccess();
```

### Updating a NuGet source

``` CSharp
using HostApi;

new DotNetNuGetUpdateSource()
    .WithName("TestSource")
    .WithSource(newSource)
    .Run().EnsureSuccess();
```

### Updating installed template packages

``` CSharp
using HostApi;

new DotNetNewUpdate()
    .Run().EnsureSuccess();
```

### Updating installed workloads

``` CSharp
using HostApi;

new DotNetWorkloadUpdate()
    .Run().EnsureSuccess();
```

### Updating the specified .NET tool

``` CSharp
using HostApi;

new DotNetToolUpdate()
    .WithLocal(true)
    .WithPackage("dotnet-csi")
    .WithVersion("1.1.2")
    .Run().EnsureSuccess();
```

### Working with development certificates

``` CSharp
using HostApi;

// Create a certificate, trust it, and export it to a PEM file.
new DotNetDevCertsHttps()
    .WithExportPath("certificate.pem")
    .WithTrust(true)
    .WithFormat(DotNetCertificateFormat.Pem)
    .WithPassword("Abc")
    .Run().EnsureSuccess();
```

### Running C# script

``` CSharp
using HostApi;

var script = Path.GetTempFileName();
File.WriteAllText(script, "Console.WriteLine($\"Hello, {Args[0]}!\");");

var stdOut = new List<string>();
new DotNetCsi()
    .WithScript(script)
    .AddArgs("World")
    .Run(output => stdOut.Add(output.Line))
    .EnsureSuccess();

// Checks stdOut
stdOut.Contains("Hello, World!").ShouldBeTrue();
```

### Shutting down build servers

``` CSharp
using HostApi;

// Shuts down all build servers that are started from dotnet.
new DotNetBuildServerShutdown()
    .Run().EnsureSuccess();
```

## TeamCity API

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
