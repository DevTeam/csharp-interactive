# CSharp Interactive

[![NuGet dotnet-csi](https://img.shields.io/nuget/v/dotnet-csi)](https://www.nuget.org/packages/dotnet-csi)
[![License MIT](https://img.shields.io/github/license/devteam/csharp-interactive)](LICENSE)
[![GitHub build](https://github.com/DevTeam/csharp-interactive/actions/workflows/main.yml/badge.svg)](https://github.com/DevTeam/csharp-interactive/actions/workflows/main.yml)

CSharp Interactive automates .NET builds in C#. Write a console build project, run a `.csx` script, or try commands in an interactive REPL. The Host API covers tasks such as building and testing .NET projects, restoring NuGet packages, and running external commands or Docker containers.

## Quick start

Install the script runner with a supported [.NET SDK](#compatibility).

```sh
dotnet tool install --global dotnet-csi
```

Save this as `hello.csx`.

```csharp
System.Console.WriteLine($"Hello, {Args[0]}!");
```

Run the script with an argument.

```sh
dotnet csi hello.csx World
```

The script prints `Hello, World!`.

<details>
<summary>Watch a demo</summary>

![CSharp Interactive demo](docs/CSharpInteractive.gif)

</details>

## Operating modes

### Build project

A build project is a console application you can add to your solution and debug in your IDE. The `build` template creates `Program.cs` with a Release build step and `Program.csx` that loads the same code.

Run these commands from the directory that contains your solution.

```sh
dotnet new install CSharpInteractive.Templates
dotnet new build -o ./Build --version 1.2.3
dotnet run --project ./Build
```

The `--version` option sets the minimum version of the CSharpInteractive library in the generated project.

With `dotnet-csi` installed, run the same build as a script.

```sh
dotnet csi ./Build/Program.csx
```

To use the Host API in an existing console project, add the library from that project's directory.

```sh
dotnet add package CSharpInteractive
```

### C# script

Pass a `.csx` file or a directory containing a single `.csx` file. Arguments following the script path are available through `Args`. Reference NuGet packages with `#r "nuget: PackageName, 1.2.3"` and load other scripts with `#load "other.csx"`.

<details>
<summary>Command-line options</summary>

```text
dotnet csi [options] [--] [script] [script arguments]
```

`--` ends option parsing. `@file` reads options from a response file.

| Option | Description | Aliases |
|---|---|---|
| `--help` | Show command-line help. | `/?`, `-h`, `/h`, `/help` |
| `--version` | Show the tool version. | `/version` |
| `--source <source>` | Use a NuGet source URL or UNC path. | `-s`, `/s`, `/source` |
| `--property <key=value>` | Set a property exposed through `Props`. | `-p`, `/p`, `/property` |
| `--property:<key=value>` | Set properties in MSBuild style, for example `-p:key1=val1;key2=val2`. | `-p:`, `/p:`, `/property:` |

</details>

### Interactive REPL

Run `dotnet csi` without a script path to evaluate C# statements interactively.

## NuGet packages

| Package | Use it to | Install |
|---|---|---|
| [dotnet-csi](https://www.nuget.org/packages/dotnet-csi) | Run scripts and open the REPL. | `dotnet tool install --global dotnet-csi` |
| [CSharpInteractive](https://www.nuget.org/packages/CSharpInteractive) | Write a console build project with the Host API. | `dotnet add package CSharpInteractive` |
| [CSharpInteractive.Templates](https://www.nuget.org/packages/CSharpInteractive.Templates) | Scaffold a build project with both a console and a script entry point. | `dotnet new install CSharpInteractive.Templates` |

## Compatibility

### Using the packages

| Component | .NET requirements |
|---|---|
| `dotnet-csi` | The stable tool ships builds for .NET 8, 9, and 10. Use a .NET SDK to install it and the runtime matching the installed tool. Select a specific build during installation with `--framework`, for example `--framework net8.0`. |
| `CSharpInteractive` | The stable library targets .NET 8, 9, and 10. The 1.2.4-beta02 package also targets .NET 11. |
| `CSharpInteractive.Templates` | The stable template generates a .NET 10 project by default. The 1.2.4-beta02 template defaults to .NET 11. Use the SDK for the project's target framework. |

### Building CSharp Interactive from source

Use the .NET 11 SDK selected in [global.json](global.json). The repository builds the tool and library for .NET 8, 9, 10, and 11. The full build and compatibility checks use all four versions on Windows, Linux, and macOS. See the [GitHub Actions workflow](.github/workflows/main.yml).

## Usage examples

- [Sample library build](Samples/MySampleLib)
- [CSharp Interactive build project](Build)
- [Pure.DI build project](https://github.com/DevTeam/Pure.DI/tree/master/build)
- [Immutype build project](https://github.com/DevTeam/Immutype/tree/master/Build)
- [Comparison with Cake and Nuke](https://github.com/DevTeam/ci-cd)
