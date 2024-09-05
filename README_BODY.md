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
