
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
  - [Adding a NuGet package](#adding-a-nuget-package)
  - [Adding a NuGet package](#adding-a-nuget-package)
  - [Adding a NuGet source](#adding-a-nuget-source)
  - [Adding a project reference](#adding-a-project-reference)
  - [Adding projects to the solution file](#adding-projects-to-the-solution-file)
  - [Adding projects to the solution file](#adding-projects-to-the-solution-file)
  - [Build a project](#build-a-project)
  - [Build a project using MSBuild](#build-a-project-using-msbuild)
  - [Clean a project](#clean-a-project)
  - [Clearing the specified NuGet cache type](#clearing-the-specified-nuget-cache-type)
  - [Creating a new project from template](#creating-a-new-project-from-template)
  - [Deleting a NuGet package to the server](#deleting-a-nuget-package-to-the-server)
  - [Disabling a NuGet source](#disabling-a-nuget-source)
  - [Display template package metadata](#display-template-package-metadata)
  - [Enabling a NuGet source](#enabling-a-nuget-source)
  - [Enabling or disabling workload-set update mode](#enabling-or-disabling-workload-set-update-mode)
  - [Execute a dotnet application](#execute-a-dotnet-application)
  - [Fix (non code style) analyzer issues](#fix-(non-code-style)-analyzer-issues)
  - [Fix code style issues](#fix-code-style-issues)
  - [Format a code](#format-a-code)
  - [Gets the value of a specified NuGet configuration setting](#gets-the-value-of-a-specified-nuget-configuration-setting)
  - [Installing a template package](#installing-a-template-package)
  - [Installing optional workloads](#installing-optional-workloads)
  - [Installing the .NET local tools that are in scope for the current directory](#installing-the-.net-local-tools-that-are-in-scope-for-the-current-directory)
  - [Installing the specified .NET tool](#installing-the-specified-.net-tool)
  - [Installing the specified .NET tool](#installing-the-specified-.net-tool)
  - [Installing workloads needed for a project or a solution](#installing-workloads-needed-for-a-project-or-a-solution)
  - [Invoking a local tool](#invoking-a-local-tool)
  - [List available templates](#list-available-templates)
  - [NuGet package listing](#nuget-package-listing)
  - [Packing the code into a NuGet package](#packing-the-code-into-a-nuget-package)
  - [Printing all .NET tools of the specified type currently installed](#printing-all-.net-tools-of-the-specified-type-currently-installed)
  - [Printing all configured NuGet sources](#printing-all-configured-nuget-sources)
  - [Printing all projects in a solution file](#printing-all-projects-in-a-solution-file)
  - [Printing installed workloads](#printing-installed-workloads)
  - [Printing nuget configuration files currently being applied to a directory](#printing-nuget-configuration-files-currently-being-applied-to-a-directory)
  - [Printing the latest available version of the .NET SDK and .NET Runtime, for each feature band](#printing-the-latest-available-version-of-the-.net-sdk-and-.net-runtime,-for-each-feature-band)
  - [Printing the location of the specified NuGet cache type](#printing-the-location-of-the-specified-nuget-cache-type)
  - [Project reference listing](#project-reference-listing)
  - [Publishing the application and its dependencies to a folder for deployment to a hosting system](#publishing-the-application-and-its-dependencies-to-a-folder-for-deployment-to-a-hosting-system)
  - [Pushing a NuGet package to the server](#pushing-a-nuget-package-to-the-server)
  - [Removing a NuGet package](#removing-a-nuget-package)
  - [Removing a NuGet source.](#removing-a-nuget-source.)
  - [Repairing workloads installations](#repairing-workloads-installations)
  - [Restoring the dependencies and tools of a project](#restoring-the-dependencies-and-tools-of-a-project)
  - [Run a custom .NET command](#run-a-custom-.net-command)
  - [Run a dotnet application](#run-a-dotnet-application)
  - [Run tests under dotCover](#run-tests-under-dotcover)
  - [Running source code without any explicit compile or launch commands](#running-source-code-without-any-explicit-compile-or-launch-commands)
  - [Running tests from the specified assemblies](#running-tests-from-the-specified-assemblies)
  - [Searche for the templates](#searche-for-the-templates)
  - [Searching all .NET tools that are published to NuGet](#searching-all-.net-tools-that-are-published-to-nuget)
  - [Searching for a NuGet package](#searching-for-a-nuget-package)
  - [Searching for optional workloads](#searching-for-optional-workloads)
  - [Sets the value of a specified NuGet configuration setting](#sets-the-value-of-a-specified-nuget-configuration-setting)
  - [Show the dependency graph for NuGet package](#show-the-dependency-graph-for-nuget-package)
  - [Signing with certificate](#signing-with-certificate)
  - [Storing the specified assemblies in the runtime package store.](#storing-the-specified-assemblies-in-the-runtime-package-store.)
  - [Test a project](#test-a-project)
  - [Test a project using the MSBuild VSTest target](#test-a-project-using-the-msbuild-vstest-target)
  - [Uninstalling a specified workload](#uninstalling-a-specified-workload)
  - [Uninstalling a template package](#uninstalling-a-template-package)
  - [Uninstalling the specified .NET tool](#uninstalling-the-specified-.net-tool)
  - [Unsets the value of a specified NuGet configuration setting](#unsets-the-value-of-a-specified-nuget-configuration-setting)
  - [Updating a NuGet source](#updating-a-nuget-source)
  - [Updating installed template packages](#updating-installed-template-packages)
  - [Updating installed workloads](#updating-installed-workloads)
  - [Working with development certificates](#working-with-development-certificates)
  - [Run C# script](#run-c#-script)
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
using HostApi;

IEnumerable<NuGetPackage> packages = GetService<INuGet>()
    .Restore(new NuGetRestoreSettings("IoC.Container").WithVersionRange(VersionRange.All));
```



### Restore a NuGet package by a version range for the specified .NET and path



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



### Build command lines



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



### Run a command line



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



### Run a command line asynchronously



``` CSharp
using HostApi;

await GetService<ICommandLineRunner>()
    .RunAsync(new CommandLine("cmd", "/C", "DIR")).EnsureSuccess();

// or the same thing using the extension method
var result = await new CommandLine("cmd", "/c", "DIR")
    .RunAsync().EnsureSuccess();
```



### Run and process output



``` CSharp
using HostApi;

var lines = new List<string>();
var result = new CommandLine("cmd", "/c", "SET")
    .AddVars(("MyEnv", "MyVal"))
    .Run(output => lines.Add(output.Line)).EnsureSuccess();

lines.ShouldContain("MyEnv=MyVal");
```



### Run asynchronously in parallel



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
    .RunAsync(default, cancellationTokenSource.Token);

cancellationTokenSource.CancelAfter(TimeSpan.FromMilliseconds(100));
task.IsCompleted.ShouldBeFalse();
```



### Run timeout

If timeout expired a process will be killed.

``` CSharp
using HostApi;

var exitCode = new CommandLine("cmd", "/c", "TIMEOUT", "/T", "120")
    .Run(default, TimeSpan.FromMilliseconds(1))
    .ExitCode;
```



### Build a project in a docker container



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



### Adding a NuGet package



``` CSharp
using HostApi;

var result = new DotNetAddPackage()
    .WithWorkingDirectory("MyLib")
    .WithPackage("Pure.DI")
    .Run().EnsureSuccess();
```



### Adding a NuGet package



``` CSharp
using HostApi;

var result = new DotNetAddReference()
    .WithProject(Path.Combine("MyTests", "MyTests.csproj"))
    .WithReferences(Path.Combine("MyLib", "MyLib.csproj"))
    .Run().EnsureSuccess();

```



### Build a project



``` CSharp
using HostApi;

var messages = new List<BuildMessage>();
var result = new DotNetBuild()
    .WithWorkingDirectory("MyTests")
    .Build(message => messages.Add(message)).EnsureSuccess();

result.Errors.Any(message => message.State == BuildMessageState.StdError).ShouldBeFalse(result.ToString());
result.ExitCode.ShouldBe(0, result.ToString());
```



### Clean a project



``` CSharp
using HostApi;

// Clean the project, running a command like: "dotnet clean" from the directory "MyLib"
new DotNetClean()
    .WithWorkingDirectory("MyLib")
    .Build().EnsureSuccess();
```



### Run a custom .NET command



``` CSharp
using HostApi;

// Gets the dotnet version, running a command like: "dotnet --version"
NuGetVersion? version = default;
new DotNetCustom("--version")
    .Run(message => NuGetVersion.TryParse(message.Line, out version))
    .EnsureSuccess();

version.ShouldNotBeNull();
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



### Execute a dotnet application



``` CSharp
using HostApi;
new DotNetExec()
    .WithPathToApplication(Path.Combine(path, "MyApp.dll"))
    .Run().EnsureSuccess();
```



### Fix (non code style) analyzer issues



``` CSharp
using HostApi;

new DotNetFormatAnalyzers()
    .WithWorkingDirectory("MyLib")
    .WithProject("MyLib.csproj")
    .AddDiagnostics("CA1831", "CA1832")
    .WithSeverity(DotNetFormatSeverity.Warning)
    .Run().EnsureSuccess();
```



### Format a code



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



### Fix code style issues



``` CSharp
using HostApi;

new DotNetFormatStyle()
    .WithWorkingDirectory("MyLib")
    .WithProject("MyLib.csproj")
    .AddDiagnostics("IDE0005", "IDE0006")
    .WithSeverity(DotNetFormatSeverity.Information)
    .Run().EnsureSuccess();
```



### NuGet package listing



``` CSharp
using HostApi;

new DotNetAddPackage()
    .WithWorkingDirectory("MyLib")
    .WithPackage("Pure.DI")
    .Run().EnsureSuccess();

var lines = new List<string>();
new DotNetListPackage()
    .WithWorkingDirectory("MyLib")
    .WithVerbosity(DotNetVerbosity.Minimal)
    .Run(output => lines.Add(output.Line));

lines.Any(i => i.Contains("Pure.DI")).ShouldBeTrue();
```



### Project reference listing



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



### Test a project using the MSBuild VSTest target



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



### Display template package metadata



``` CSharp
using HostApi;

new DotNetNewDetails()
    .WithTemplateName("CSharpInteractive.Templates")
    .Run().EnsureSuccess();
```



### Installing a template package



``` CSharp
using HostApi;

new DotNetNewInstall()
    .WithPackage("Pure.DI.Templates")
    .Run().EnsureSuccess();
```



### Creating a new project from template



``` CSharp
using HostApi;

new DotNetNewList()
    .Run().EnsureSuccess();
```



### List available templates



``` CSharp
using HostApi;

new DotNetNew()
    .WithTemplateName("classlib")
    .WithName("MyLib")
    .WithForce(true)
    .Run().EnsureSuccess();
```



### Searche for the templates



``` CSharp
using HostApi;

new DotNetNewSearch()
    .WithTemplateName("build")
    .Run().EnsureSuccess();
```



### Uninstalling a template package



``` CSharp
using HostApi;

new DotNetNewUninstall()
    .WithPackage("Pure.DI.Templates")
    .Run();
```



### Updating installed template packages



``` CSharp
using HostApi;

new DotNetNewUpdate()
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



### Gets the value of a specified NuGet configuration setting



``` CSharp
using HostApi;

string? repositoryPath = default;
new DotNetNuGetConfigGet()
    .WithConfigKey("repositoryPath")
    .Run(output => repositoryPath = output.Line).EnsureSuccess();
```



### Printing nuget configuration files currently being applied to a directory



``` CSharp
using HostApi;

var configPaths = new List<string>();
new DotNetNuGetConfigPaths()
    .Run(output => configPaths.Add(output.Line)).EnsureSuccess();
```



### Sets the value of a specified NuGet configuration setting



``` CSharp
using HostApi;

new DotNetNuGetConfigSet()
    .WithConfigFile(configFile)
    .WithConfigKey("repositoryPath")
    .WithConfigValue("MyValue")
    .Run().EnsureSuccess();
```



### Unsets the value of a specified NuGet configuration setting



``` CSharp
using HostApi;

new DotNetNuGetConfigUnset()
    .WithConfigKey("repositoryPath")
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



### Enabling a NuGet source



``` CSharp
using HostApi;

new DotNetNuGetEnableSource()
    .WithName("TestSource")
    .Run().EnsureSuccess();
```



### Printing all configured NuGet sources



``` CSharp
using HostApi;

new DotNetNuGetListSource()
    .WithFormat(NuGetListFormat.Short)
    .Run().EnsureSuccess();
```



### Clearing the specified NuGet cache type



``` CSharp
using HostApi;

new DotNetNuGetLocalsClear()
    .WithCacheLocation(NuGetCacheLocation.Temp)
    .Run().EnsureSuccess();
```



### Printing the location of the specified NuGet cache type



``` CSharp
using HostApi;

new DotNetNuGetLocalsList()
    .WithCacheLocation(NuGetCacheLocation.GlobalPackages)
    .Run().EnsureSuccess();
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



### Removing a NuGet source.



``` CSharp
using HostApi;

new DotNetNuGetRemoveSource()
    .WithName("TestSource")
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



### Updating a NuGet source



``` CSharp
using HostApi;

new DotNetNuGetUpdateSource()
    .WithName("TestSource")
    .WithSource(newSource)
    .Run().EnsureSuccess();
```



### Show the dependency graph for NuGet package



``` CSharp
using HostApi;

new DotNetNuGetWhy()
    .WithProject(Path.Combine("MyLib", "MyLib.csproj"))
    .WithPackage("MyLib.1.2.3.nupkg")
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



### Packing the code into a NuGet package



``` CSharp
using HostApi;

// Creates a NuGet package of version 1.2.3 for the project
new DotNetPack()
    .WithWorkingDirectory("MyLib")
    .WithOutput(path)
    .AddProps(("version", "1.2.3"))
    .Build().EnsureSuccess();
```



### Publishing the application and its dependencies to a folder for deployment to a hosting system



``` CSharp
using HostApi;

new DotNetPublish()
    .WithWorkingDirectory("MyLib")
    .WithFramework("net8.0")
    .WithOutput("bin")
    .Build().EnsureSuccess();
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



### Restoring the dependencies and tools of a project



``` CSharp
using HostApi;

new DotNetRestore()
    .WithProject(Path.Combine("MyLib", "MyLib.csproj"))
    .Build().EnsureSuccess();
```



### Running source code without any explicit compile or launch commands



``` CSharp
using HostApi;

var stdOut = new List<string>();
new DotNetRun()
    .WithProject(Path.Combine("MyApp", "MyApp.csproj"))
    .Build(message => stdOut.Add(message.Text))
    .EnsureSuccess();

// Checks stdOut
stdOut.ShouldBe(new[] {"Hello, World!"});
```



### Run a dotnet application



``` CSharp
// Adds the namespace "HostApi" to use .NET build API
using HostApi;

new DotNet()
    .WithPathToApplication(Path.Combine(path, "MyApp.dll"))
    .Run().EnsureSuccess();
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



### Adding projects to the solution file



``` CSharp
using HostApi;

new DotNetNew()
    .WithTemplateName("sln")
    .WithName("NySolution")
    .WithForce(true)
    .Run().EnsureSuccess();

new DotNetSlnAdd()
    .WithSolution("NySolution.sln")
    .AddProjects(
        Path.Combine("MyLib", "MyLib.csproj"),
        Path.Combine("MyTests", "MyTests.csproj"))
    .Run().EnsureSuccess();
```



### Printing all projects in a solution file



``` CSharp
using HostApi;

var lines = new List<string>();
new DotNetSlnList()
    .WithSolution("NySolution.sln")
    .Run(output => lines.Add(output.Line))
    .EnsureSuccess();
```



### Adding projects to the solution file



``` CSharp
using HostApi;

new DotNetSlnRemove()
    .WithSolution("NySolution.sln")
    .AddProjects(
        Path.Combine("MyLib", "MyLib.csproj"))
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



### Test a project



``` CSharp
using HostApi;

// Runs tests
var result = new DotNetTest()
    .WithWorkingDirectory("MyTests")
    .Build().EnsureSuccess();

// The "result" variable provides details about build and tests
result.ExitCode.ShouldBe(0, result.ToString());
result.Summary.Tests.ShouldBe(1, result.ToString());
result.Tests.Count(test => test.State == TestState.Finished).ShouldBe(1, result.ToString());
```



### Run tests under dotCover



``` CSharp
using HostApi;

new DotNetToolInstall()
    .WithLocal(true)
    .WithPackage("JetBrains.dotCover.GlobalTool")
    .Run().EnsureSuccess();

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

// Runs tests under dotCover
var result = testUnderDotCover
    .Build().EnsureSuccess();

// The "result" variable provides details about a build
result.ExitCode.ShouldBe(0, result.ToString());
result.Tests.Count(i => i.State == TestState.Finished).ShouldBe(1, result.ToString());

// Generates a HTML code coverage report.
new DotNetCustom("dotCover", "report", $"--source={dotCoverSnapshot}", $"--output={dotCoverReport}", "--reportType=HTML")
    .Run().EnsureSuccess();
```



### Installing the specified .NET tool



``` CSharp
using HostApi;

new DotNetToolInstall()
    .WithLocal(true)
    .WithPackage("dotnet-csi")
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



### Searching all .NET tools that are published to NuGet



``` CSharp
using HostApi;

new DotNetToolSearch()
    .WithPackage("dotnet-csi")
    .WithDetail(true)
    .Run().EnsureSuccess();
```



### Uninstalling the specified .NET tool



``` CSharp
using HostApi;

new DotNetToolUninstall()
    .WithPackage("dotnet-csi")
    .Run().EnsureSuccess();
```



### Installing the specified .NET tool



``` CSharp
using HostApi;

new DotNetToolUpdate()
    .WithLocal(true)
    .WithPackage("dotnet-csi")
    .Run().EnsureSuccess();
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



### Enabling or disabling workload-set update mode



``` CSharp
using HostApi;

new DotNetWorkloadConfig()
    .WithUpdateMode(DotNetWorkloadUpdateMode.WorkloadSet)
    .Run().EnsureSuccess();
```



### Installing optional workloads



``` CSharp
using HostApi;

new DotNetWorkloadInstall()
    .AddWorkloads("aspire")
    .Run().EnsureSuccess();
```



### Printing installed workloads



``` CSharp
using HostApi;

new DotNetWorkloadList()
    .Run().EnsureSuccess();
```



### Repairing workloads installations



``` CSharp
using HostApi;

new DotNetWorkloadRepair()
    .Run().EnsureSuccess();
```



### Installing workloads needed for a project or a solution



``` CSharp
using HostApi;

new DotNetWorkloadRestore()
    .WithProject(Path.Combine("MyLib", "MyLib.csproj"))
    .Run().EnsureSuccess();
```



### Searching for optional workloads



``` CSharp
using HostApi;

new DotNetWorkloadSearch()
    .WithSearchString("maui")
    .Run().EnsureSuccess();
```



### Uninstalling a specified workload



``` CSharp
using HostApi;

new DotNetWorkloadUninstall()
    .AddWorkloads("aspire")
    .Run().EnsureSuccess();
```



### Updating installed workloads



``` CSharp
using HostApi;

new DotNetWorkloadUpdate()
    .Run().EnsureSuccess();
```



### Build a project using MSBuild



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



### Shuts down build servers



``` CSharp
using HostApi;

// Shuts down all build servers that are started from dotnet.
new DotNetBuildServerShutdown()
    .Run().EnsureSuccess();
```



### Run C# script



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

