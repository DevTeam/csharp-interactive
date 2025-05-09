#  Build automation system for .NET

[![NuGet csi](https://img.shields.io/nuget/v/dotnet-csi?includePreReleases=true)](https://www.nuget.org/packages/dotnet-csi)
![GitHub](https://img.shields.io/github/license/devteam/csharp-interactive)
[<img src="http://teamcity.jetbrains.com/app/rest/builds/buildType:(id:OpenSourceProjects_DevTeam_CScriptInteractive_BuildAndTest)/statusIcon.svg"/>](http://teamcity.jetbrains.com/viewType.html?buildTypeId=OpenSourceProjects_DevTeam_CScriptInteractive_BuildAndTest&guest=1)
![GitHub Build](https://github.com/DevTeam/csharp-interactive/actions/workflows/main.yml/badge.svg)

![](docs/CSharpInteractive.gif)

C# interactive build automation system makes it easy to build .NET projects. It can be part of your solution as a regular .NET console project or run C# scripts without compiling them, or even run in REPL mode - allowing you to run C# interactively.

![](docs/icon.png)

## Key Features

### ✔️ Three Integrated [Execution Modes](#operating-modes)
Flexible interoperability between modes for diverse workflow requirements.
### ✔️ Native Cross-Platform Support
Seamless operation across Windows, Linux, and macOS environments.
### ✔️ Debugging Support
Ability to debug in “.NET build project” mode, allowing developers to efficiently identify and fix problems during the compilation and build process.
### ✔️ Zero Abstraction Constraints
No restrictive abstractions (e.g., Tasks, Targets, DependsOn) to limit script design:
- Pure .NET codebase – no proprietary syntax or hidden layers
- Adheres to industry-standard coding practices for maintainability
### ✔️ Powerful API for building .NET projects
Granular control over builds, tests, and deployments with streamlined project configuration.
### ✔️ Summarised statistics
Consolidated build statistics.

## Operating modes

<details>

<summary>Interactive</summary>

REPL (Read-Eval-Print-Loop) interface for interactive code evaluation. Please see [this page](https://www.nuget.org/packages/dotnet-csi) for installation details.

Launch the tool in the interactive mode:

```Shell
dotnet csi
```

Simply enter C# commands sequentially one line after another and get the result in console output.

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

<summary>.NET build project</summary>

Seamless integration into existing solutions as a standard .NET console project. Please see [this page](https://github.com/DevTeam/csharp-interactive/wiki/Install-the-C%23-script-template) for details on how to install the [project template](https://www.nuget.org/packages/CSharpInteractive.Templates).

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
