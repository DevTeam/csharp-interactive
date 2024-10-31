// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming
namespace HostApi;
using Internal.DotNet;
using Internal;

/// <summary>
/// Runs a dotnet application.
/// <p>
/// You specify the path to an application .dll file to run the application. To run the application means to find and execute the entry point, which in the case of console apps is the Main method. For example, dotnet myapp.dll runs the myapp application.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet">.NET CLI command</a><br/>
/// <example>
///<code>
/// // Adds the namespace "HostApi" to use .NET build API
/// using HostApi;
/// 
/// new DotNet()
///     .WithPathToApplication(Path.Combine(path, "MyApp.dll"))
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="AdditionalProbingPaths">Paths containing probing policy and assemblies to probe.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="PathToApplication">Specifies the path to an application .dll file to run the application. To run the application means to find and execute the entry point, which in the case of console apps is the Main method. For example, dotnet myapp. dll runs the myapp application.</param>
/// <param name="AdditionalDeps">Path to an additional .deps.json file. A deps.json file contains a list of dependencies, compilation dependencies, and version information used to address assembly conflicts.</param>
/// <param name="FxVersion">Version of the .NET runtime to use to run the application.</param>
/// <param name="RollForward">Controls how roll forward is applied to the app. The SETTING can be one of the following values. If not specified, <see cref="HostApi.DotNetRollForward.Minor"/> is the default.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="Info">Prints out detailed information about a .NET installation and the machine environment, such as the current operating system, and commit SHA of the .NET version.</param>
/// <param name="Version">Prints out the version of the .NET SDK used by dotnet commands, which may be affected by a global.json file.</param>
/// <param name="ListRuntimes">Prints out a list of the installed .NET runtimes. An x86 version of the SDK lists only x86 runtimes, and an x64 version of the SDK lists only x64 runtimes.</param>
/// <param name="ListSdks">Prints out a list of the installed .NET SDKs.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNet(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<string> AdditionalProbingPaths,
    string PathToApplication = "",
    string AdditionalDeps = "",
    string FxVersion = "",
    DotNetRollForward? RollForward = default,
    bool? Diagnostics = default,
    bool? Info = default,
    bool? Version = default,
    bool? ListRuntimes = default,
    bool? ListSdks = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNet(params string[] args)
        : this(args, [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNet()
        : this([], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddNotEmptyArgs(PathToApplication.ToArg())
            .AddArgs(AdditionalProbingPaths.ToArgs("--additionalprobingpath", ""))
            .AddArgs(AdditionalDeps.ToArgs("--additional-deps", ""))
            .AddArgs(FxVersion.ToArgs("--fx-version", ""))
            .AddArgs(RollForward.ToArgs("--roll-forward", ""))
            .AddBooleanArgs(
                ("--diagnostics", Diagnostics),
                ("--info", Info),
                ("--version", Version),
                ("--list-runtimes", ListRuntimes),
                ("--list-sdks", ListSdks)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Runs a dotnet application.", ShortName, PathToApplication.ToArg());
}

/// <summary>
/// Executes a dotnet application.
/// <p>
/// You specify the path to an application .dll file to run the application. To run the application means to find and execute the entry point, which in the case of console apps is the Main method. For example, dotnet myapp.dll runs the myapp application.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// new DotNetExec()
///     .WithPathToApplication(Path.Combine(path, "MyApp.dll"))
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="AdditionalProbingPaths">Paths containing probing policy and assemblies to probe.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="PathToApplication">Specifies the path to an application .dll file to run the application. To run the application means to find and execute the entry point, which in the case of console apps is the Main method. For example, dotnet myapp. dll runs the myapp application.</param>
/// <param name="DepsFile">Path to a deps.json file. A deps.json file is a configuration file that contains information about dependencies necessary to run the application. This file is generated by the .NET SDK.</param>
/// <param name="AdditionalDeps">Path to an additional .deps.json file. A deps.json file contains a list of dependencies, compilation dependencies, and version information used to address assembly conflicts.</param>
/// <param name="FxVersion">Version of the .NET runtime to use to run the application.</param>
/// <param name="RollForward">Controls how roll forward is applied to the app. The SETTING can be one of the following values. If not specified, <see cref="HostApi.DotNetRollForward.Minor"/> is the default.</param>
/// <param name="RuntimeConfig">Path to a runtimeconfig.json file. A runtimeconfig.json file contains run-time settings and is typically named &lt;applicationname&gt;.runtimeconfig.json.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetExec(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<string> AdditionalProbingPaths,
    string PathToApplication = "",
    string DepsFile = "",
    string AdditionalDeps = "",
    string FxVersion = "",
    DotNetRollForward? RollForward = default,
    string RuntimeConfig = "",
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetExec(params string[] args)
        : this(args, [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetExec()
        : this([], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("exec")
            .AddNotEmptyArgs(PathToApplication.ToArg())
            .AddArgs(AdditionalProbingPaths.ToArgs("--additionalprobingpath", ""))
            .AddArgs(DepsFile.ToArgs("--depsfile", ""))
            .AddArgs(AdditionalDeps.ToArgs("--additional-deps", ""))
            .AddArgs(FxVersion.ToArgs("--fx-version", ""))
            .AddArgs(RollForward.ToArgs("--roll-forward", ""))
            .AddArgs(RuntimeConfig.ToArgs("--runtimeconfig", ""))
            .AddBooleanArgs(
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Executes a dotnet application.", ShortName, "exec", PathToApplication.ToArg());
}

/// <summary>
/// Adds or updates a package reference in a project file.
/// <p>
/// This command provides a convenient option to add or update a package reference in a project file. When you run the command, there&apos;s a compatibility check to ensure the package is compatible with the frameworks in the project. If the check passes and the package isn&apos;t referenced in the project file, a &lt;PackageReference&gt; element is added to the project file. If the check passes and the package is already referenced in the project file, the &lt;PackageReference&gt; element is updated to the latest compatible version. After the project file is updated, dotnet restore is run.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-add-package">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// var result = new DotNetAddPackage()
///     .WithWorkingDirectory("MyLib")
///     .WithPackage("Pure.DI")
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Sources">The URI of the NuGet package source to use during this operation.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Project">The project or solution file to operate on. If not specified, the command searches the current directory for one. If more than one solution or project is found, an error is thrown.</param>
/// <param name="Package">The package reference to add.</param>
/// <param name="Framework">Adds a package reference only when targeting a specific framework.</param>
/// <param name="NoRestore">Doesn't execute an implicit restore when running the command.</param>
/// <param name="PackageDirectory">The directory where to restore the packages. The default package restore location is %userprofile%\.nuget\packages on Windows and ~/.nuget/packages on macOS and Linux.</param>
/// <param name="Prerelease">Allows prerelease packages to be installed. Available since .NET Core 5 SDK.</param>
/// <param name="Version">Version of the package</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetAddPackage(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<string> Sources,
    string Project = "",
    string Package = "",
    string Framework = "",
    bool? NoRestore = default,
    string PackageDirectory = "",
    bool? Prerelease = default,
    string Version = "",
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetAddPackage(params string[] args)
        : this(args, [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetAddPackage()
        : this([], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("add")
            .AddNotEmptyArgs(Project.ToArg())
            .AddArgs("package")
            .AddNotEmptyArgs(Package.ToArg())
            .AddArgs(Sources.ToArgs("--source", ""))
            .AddArgs(Framework.ToArgs("--framework", ""))
            .AddArgs(PackageDirectory.ToArgs("--package-directory", ""))
            .AddArgs(Version.ToArgs("--version", ""))
            .AddBooleanArgs(
                ("--no-restore", NoRestore),
                ("--prerelease", Prerelease),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Adds or updates a package reference in a project file.", ShortName, "add", Project.ToArg(), "package", Package.ToArg());
}

/// <summary>
/// Lists the package references for a project or solution.
/// <p>
/// This command provides a convenient option to list all NuGet package references for a specific project or a solution. You first need to build the project in order to have the assets needed for this command to process.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-list-package">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetAddPackage()
///     .WithWorkingDirectory("MyLib")
///     .WithPackage("Pure.DI")
///     .Run().EnsureSuccess();
/// 
/// var lines = new List&lt;string&gt;();
/// new DotNetListPackage()
///     .WithProject(Path.Combine("MyLib", "MyLib.csproj"))
///     .WithVerbosity(DotNetVerbosity.Minimal)
///     .Run(output =&gt; lines.Add(output.Line));
/// 
/// lines.Any(i =&gt; i.Contains("Pure.DI")).ShouldBeTrue();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Frameworks">Displays only the packages applicable for the specified target framework.</param>
/// <param name="Sources">The URI of the NuGet package source to use during this operation.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Project">The project or solution file to operate on. If not specified, the command searches the current directory for one. If more than one solution or project is found, an error is thrown.</param>
/// <param name="Config">The NuGet sources to use when searching for newer packages. Requires the --outdated option.</param>
/// <param name="Deprecated">Displays packages that have been deprecated.</param>
/// <param name="HighestMinor">Considers only the packages with a matching major version number when searching for newer packages. Requires the --outdated or --deprecated option.</param>
/// <param name="HighestPatch">Considers only the packages with a matching major and minor version numbers when searching for newer packages. Requires the --outdated or --deprecated option.</param>
/// <param name="IncludePrerelease">Considers packages with prerelease versions when searching for newer packages. Requires the --outdated or --deprecated option.</param>
/// <param name="IncludeTransitive">Lists transitive packages, in addition to the top-level packages. When specifying this option, you get a list of packages that the top-level packages depend on.</param>
/// <param name="Outdated">Lists packages that have newer versions available.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetListPackage(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<string> Frameworks,
    IEnumerable<string> Sources,
    string Project = "",
    string Config = "",
    bool? Deprecated = default,
    bool? HighestMinor = default,
    bool? HighestPatch = default,
    bool? IncludePrerelease = default,
    bool? IncludeTransitive = default,
    bool? Outdated = default,
    DotNetVerbosity? Verbosity = default,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetListPackage(params string[] args)
        : this(args, [], [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetListPackage()
        : this([], [], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("list")
            .AddNotEmptyArgs(Project.ToArg())
            .AddArgs("package")
            .AddArgs(Frameworks.ToArgs("--framework", ""))
            .AddArgs(Sources.ToArgs("--source", ""))
            .AddArgs(Config.ToArgs("--config", ""))
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddBooleanArgs(
                ("--deprecated", Deprecated),
                ("--highest-minor", HighestMinor),
                ("--highest-patch", HighestPatch),
                ("--include-prerelease", IncludePrerelease),
                ("--include-transitive", IncludeTransitive),
                ("--outdated", Outdated),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Lists the package references for a project or solution.", ShortName, "list", Project.ToArg(), "package");
}

/// <summary>
/// Removes package reference from a project file.
/// <p>
/// This command provides a convenient option to remove a NuGet package reference from a project.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-remove-package">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetAddPackage()
///     .WithWorkingDirectory("MyLib")
///     .WithPackage("Pure.DI")
///     .Run().EnsureSuccess();
/// 
/// new DotNetRemovePackage()
///     .WithWorkingDirectory("MyLib")
///     .WithPackage("Pure.DI")
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Project">The project or solution file to operate on. If not specified, the command searches the current directory for one. If more than one solution or project is found, an error is thrown.</param>
/// <param name="Package">The package reference to add.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetRemovePackage(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    string Project = "",
    string Package = "",
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetRemovePackage(params string[] args)
        : this(args, [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetRemovePackage()
        : this([], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("remove")
            .AddNotEmptyArgs(Project.ToArg())
            .AddArgs("package")
            .AddNotEmptyArgs(Package.ToArg())
            .AddBooleanArgs(
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Removes package reference from a project file.", ShortName, "remove", Project.ToArg(), "package", Package.ToArg());
}

/// <summary>
/// Adds project-to-project (P2P) references.
/// <p>
/// This command provides a convenient option to add project references to a project. After running the command, the &lt;ProjectReference&gt; elements are added to the project file.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-add-reference">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// var result = new DotNetAddReference()
///     .WithProject(Path.Combine("MyTests", "MyTests.csproj"))
///     .WithReferences(Path.Combine("MyLib", "MyLib.csproj"))
///     .Run().EnsureSuccess();
/// 
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="References">Project-to-project (P2P) references to add. Specify one or more projects. Glob patterns are supported on Unix/Linux-based systems.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Project">The project or solution file to operate on. If not specified, the command searches the current directory for one. If more than one solution or project is found, an error is thrown.</param>
/// <param name="Framework">Adds project references only when targeting a specific framework using the TFM format.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetAddReference(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<string> References,
    string Project = "",
    string Framework = "",
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetAddReference(params string[] args)
        : this(args, [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetAddReference()
        : this([], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("add")
            .AddNotEmptyArgs(Project.ToArg())
            .AddArgs("reference")
            .AddNotEmptyArgs(References.ToArray())
            .AddArgs(Framework.ToArgs("--framework", ""))
            .AddBooleanArgs(
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Adds project-to-project (P2P) references.", ShortName, new [] {"add", Project.ToArg(), "reference"}.Concat(References).ToArray());
}

/// <summary>
/// Lists project-to-project references.
/// <p>
/// This command provides a convenient option to list project references for a given project.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-list-reference">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetAddReference()
///     .WithProject(Path.Combine("MyTests", "MyTests.csproj"))
///     .WithReferences(Path.Combine("MyLib", "MyLib.csproj"))
///     .Run().EnsureSuccess();
/// 
/// var lines = new List&lt;string&gt;();
/// new DotNetListReference()
///     .WithProject(Path.Combine("MyTests", "MyTests.csproj"))
///     .Run(output =&gt; lines.Add(output.Line));
/// 
/// lines.Any(i =&gt; i.Contains("MyLib.csproj")).ShouldBeTrue();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Project">The project or solution file to operate on. If not specified, the command searches the current directory for one. If more than one solution or project is found, an error is thrown.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetListReference(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    string Project = "",
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetListReference(params string[] args)
        : this(args, [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetListReference()
        : this([], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("list")
            .AddNotEmptyArgs(Project.ToArg())
            .AddArgs("reference")
            .AddBooleanArgs(
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Lists project-to-project references.", ShortName, "list", Project.ToArg(), "reference");
}

/// <summary>
/// Removes project-to-project (P2P) references.
/// <p>
/// This command provides a convenient option to remove project references from a project.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-remove-reference">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetAddReference()
///     .WithProject(Path.Combine("MyTests", "MyTests.csproj"))
///     .WithReferences(Path.Combine("MyLib", "MyLib.csproj"))
///     .Run().EnsureSuccess();
/// 
/// new DotNetRemoveReference()
///     .WithProject(Path.Combine("MyTests", "MyTests.csproj"))
///     .WithReferences(Path.Combine("MyLib", "MyLib.csproj"))
///     .Run().EnsureSuccess();
/// 
/// var lines = new List&lt;string&gt;();
/// new DotNetListReference()
///     .WithProject(Path.Combine("MyTests", "MyTests.csproj"))
///     .Run(output =&gt; lines.Add(output.Line));
/// 
/// lines.Any(i =&gt; i.Contains("MyLib.csproj")).ShouldBeFalse();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="References">Project-to-project (P2P) references to remove. You can specify one or multiple projects. Glob patterns are supported on Unix/Linux based terminals.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Project">The project or solution file to operate on. If not specified, the command searches the current directory for one. If more than one solution or project is found, an error is thrown.</param>
/// <param name="Framework">Removes the reference only when targeting a specific framework using the TFM format.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetRemoveReference(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<string> References,
    string Project = "",
    string Framework = "",
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetRemoveReference(params string[] args)
        : this(args, [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetRemoveReference()
        : this([], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("remove")
            .AddNotEmptyArgs(Project.ToArg())
            .AddArgs("reference")
            .AddNotEmptyArgs(References.ToArray())
            .AddArgs(Framework.ToArgs("--framework", ""))
            .AddBooleanArgs(
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Removes project-to-project (P2P) references.", ShortName, new [] {"remove", Project.ToArg(), "reference"}.Concat(References).ToArray());
}

/// <summary>
/// Builds a project and all of its dependencies.
/// <p>
/// This command builds the project and its dependencies into a set of binaries. The binaries include the project's code in Intermediate Language (IL) files with a .dll extension. For executable projects targeting versions earlier than .NET Core 3.0, library dependencies from NuGet are typically NOT copied to the output folder. They're resolved from the NuGet global packages folder at run time. With that in mind, the product of dotnet build isn't ready to be transferred to another machine to run. To create a version of the application that can be deployed, you need to publish it (for example, with the dotnet publish command).
/// </p>
/// <p>
/// For executable projects targeting .NET Core 3.0 and later, library dependencies are copied to the output folder. This means that if there isn't any other publish-specific logic (such as Web projects have), the build output should be deployable.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-build">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// var messages = new List&lt;BuildMessage&gt;();
/// var result = new DotNetBuild()
///     .WithWorkingDirectory("MyTests")
///     .Build(message =&gt; messages.Add(message)).EnsureSuccess();
/// 
/// result.Errors.Any(message =&gt; message.State == BuildMessageState.StdError).ShouldBeFalse(result.ToString());
/// result.ExitCode.ShouldBe(0, result.ToString());
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Props">MSBuild options for setting properties.</param>
/// <param name="Sources">The URI of the NuGet package source to use during this operation.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Project">The project or solution file to operate on. If not specified, the command searches the current directory for one. If more than one solution or project is found, an error is thrown.</param>
/// <param name="Arch">Specifies the target architecture. This is a shorthand syntax for setting the Runtime Identifier (RID), where the provided value is combined with the default RID. For example, on a win-x64 machine, specifying --arch x86 sets the RID to win-x86. If you use this option, don&apos;t use the -r|--runtime option. Available since .NET 6 Preview 7.</param>
/// <param name="ArtifactsPath">All build output files from the executed command will go in subfolders under the specified path, separated by project.</param>
/// <param name="Configuration">Defines the build configuration. The default for most projects is Debug, but you can override the build configuration settings in your project.</param>
/// <param name="DisableBuildServers">Forces the command to ignore any persistent build servers. This option provides a consistent way to disable all use of build caching, which forces a build from scratch. A build that doesn't rely on caches is useful when the caches might be corrupted or incorrect for some reason. Available since .NET 7 SDK.</param>
/// <param name="Framework">Builds and runs the app using the specified framework. The framework must be specified in the project file.</param>
/// <param name="Force">Forces all dependencies to be resolved even if the last restore was successful. Specifying this flag is the same as deleting the project.assets.json file.</param>
/// <param name="NoDependencies">When restoring a project with project-to-project (P2P) references, restores the root project and not the references.</param>
/// <param name="NoIncremental">Marks the build as unsafe for incremental build. This flag turns off incremental compilation and forces a clean rebuild of the project's dependency graph.</param>
/// <param name="NoRestore">Doesn't execute an implicit restore when running the command.</param>
/// <param name="NoLogo">Doesn't display the startup banner or the copyright message.</param>
/// <param name="NoSelfContained">Publishes the application as a framework dependent application. A compatible .NET runtime must be installed on the target machine to run the application. Available since .NET 6 SDK.</param>
/// <param name="Output">Directory in which to place the built binaries. If not specified, the default path is ./bin/&lt;configuration&gt;/&lt;framework&gt;/. For projects with multiple target frameworks (via the TargetFrameworks property), you also need to define --framework when you specify this option.</param>
/// <param name="OS">Specifies the target operating system (OS). This is a shorthand syntax for setting the Runtime Identifier (RID), where the provided value is combined with the default RID. For example, on a win-x64 machine, specifying --os linux sets the RID to linux-x64. If you use this option, don&apos;t use the -r|--runtime option. Available since .NET 6.</param>
/// <param name="Runtime">Specifies the target runtime to restore packages for. For a list of Runtime Identifiers (RIDs), see the RID catalog. -r short option available since .NET Core 3.0 SDK.</param>
/// <param name="SelfContained">Publishes the .NET runtime with the application so the runtime doesn't need to be installed on the target machine. The default is true if a runtime identifier is specified. Available since .NET 6.</param>
/// <param name="TerminalLogger">Specifies whether the terminal logger should be used for the build output.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="UseCurrentRuntime">Sets the RuntimeIdentifier to a platform portable RuntimeIdentifier based on the one of your machine. This happens implicitly with properties that require a RuntimeIdentifier, such as SelfContained, PublishAot, PublishSelfContained, PublishSingleFile, and PublishReadyToRun. If the property is set to false, that implicit resolution will no longer occur.</param>
/// <param name="VersionSuffix">Sets the value of the $(VersionSuffix) property to use when building the project. This only works if the $(Version) property isn't set. Then, $(Version) is set to the $(VersionPrefix) combined with the $(VersionSuffix), separated by a dash.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetBuild(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<(string name, string value)> Props,
    IEnumerable<string> Sources,
    string Project = "",
    string Arch = "",
    string ArtifactsPath = "",
    string Configuration = "",
    bool? DisableBuildServers = default,
    string Framework = "",
    bool? Force = default,
    bool? NoDependencies = default,
    bool? NoIncremental = default,
    bool? NoRestore = default,
    bool? NoLogo = default,
    bool? NoSelfContained = default,
    string Output = "",
    string OS = "",
    string Runtime = "",
    bool? SelfContained = default,
    DotNetTerminalLogger? TerminalLogger = default,
    DotNetVerbosity? Verbosity = default,
    bool? UseCurrentRuntime = default,
    string VersionSuffix = "",
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetBuild(params string[] args)
        : this(args, [], [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetBuild()
        : this([], [], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("build")
            .AddNotEmptyArgs(Project.ToArg())
            .AddMSBuildLoggers(host, Verbosity)
            .AddArgs(Sources.ToArgs("--source", ""))
            .AddArgs(Arch.ToArgs("--arch", ""))
            .AddArgs(ArtifactsPath.ToArgs("--artifacts-path", ""))
            .AddArgs(Configuration.ToArgs("--configuration", ""))
            .AddArgs(Framework.ToArgs("--framework", ""))
            .AddArgs(Output.ToArgs("--output", ""))
            .AddArgs(OS.ToArgs("--os", ""))
            .AddArgs(Runtime.ToArgs("--runtime", ""))
            .AddArgs(TerminalLogger.ToArgs("--tl", ""))
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddArgs(VersionSuffix.ToArgs("--version-suffix", ""))
            .AddBooleanArgs(
                ("--disable-build-servers", DisableBuildServers),
                ("--force", Force),
                ("--no-dependencies", NoDependencies),
                ("--no-incremental", NoIncremental),
                ("--no-restore", NoRestore),
                ("--nologo", NoLogo),
                ("--no-self-contained", NoSelfContained),
                ("--self-contained", SelfContained),
                ("--use-current-runtime", UseCurrentRuntime),
                ("--diagnostics", Diagnostics)
            )
            .AddProps("--property", Props.ToArray())
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Builds a project and all of its dependencies.", ShortName, "build", Project.ToArg());
}

/// <summary>
/// Shuts down build servers that are started from dotnet.
/// <p>
/// By default, all servers are shut down.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-build-server">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// // Shuts down all build servers that are started from dotnet.
/// new DotNetBuildServerShutdown()
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Servers">Shuts down build servers that are started from dotnet. By default, all servers are shut down.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetBuildServerShutdown(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<DotNetBuildServer> Servers,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetBuildServerShutdown(params string[] args)
        : this(args, [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetBuildServerShutdown()
        : this([], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("build-server")
            .AddArgs("shutdown")
            .AddArgs(Servers.ToArgs("", ""))
            .AddBooleanArgs(
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Shuts down build servers that are started from dotnet.", ShortName, "build-server", "shutdown");
}

/// <summary>
/// Cleans the output of a project.
/// <p>
/// This command cleans the output of the previous build. It's implemented as an MSBuild target, so the project is evaluated when the command is run. Only the outputs created during the build are cleaned. Both intermediate (obj) and final output (bin) folders are cleaned.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-clean">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// // Clean the project, running a command like: "dotnet clean" from the directory "MyLib"
/// new DotNetClean()
///     .WithWorkingDirectory("MyLib")
///     .Build().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Props">MSBuild options for setting properties.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Project">The project or solution file to operate on. If not specified, the command searches the current directory for one. If more than one solution or project is found, an error is thrown.</param>
/// <param name="ArtifactsPath">All build output files from the executed command will go in subfolders under the specified path, separated by project.</param>
/// <param name="Configuration">Defines the build configuration. The default for most projects is Debug, but you can override the build configuration settings in your project.</param>
/// <param name="Framework">Builds and runs the app using the specified framework. The framework must be specified in the project file.</param>
/// <param name="NoLogo">Doesn't display the startup banner or the copyright message.</param>
/// <param name="Output">Directory in which to place the built binaries. If not specified, the default path is ./bin/&lt;configuration&gt;/&lt;framework&gt;/. For projects with multiple target frameworks (via the TargetFrameworks property), you also need to define --framework when you specify this option.</param>
/// <param name="Runtime">Specifies the target runtime to restore packages for. For a list of Runtime Identifiers (RIDs), see the RID catalog. -r short option available since .NET Core 3.0 SDK.</param>
/// <param name="TerminalLogger">Specifies whether the terminal logger should be used for the build output.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetClean(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<(string name, string value)> Props,
    string Project = "",
    string ArtifactsPath = "",
    string Configuration = "",
    string Framework = "",
    bool? NoLogo = default,
    string Output = "",
    string Runtime = "",
    DotNetTerminalLogger? TerminalLogger = default,
    DotNetVerbosity? Verbosity = default,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetClean(params string[] args)
        : this(args, [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetClean()
        : this([], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("clean")
            .AddNotEmptyArgs(Project.ToArg())
            .AddMSBuildLoggers(host, Verbosity)
            .AddArgs(ArtifactsPath.ToArgs("--artifacts-path", ""))
            .AddArgs(Configuration.ToArgs("--configuration", ""))
            .AddArgs(Framework.ToArgs("--framework", ""))
            .AddArgs(Output.ToArgs("--output", ""))
            .AddArgs(Runtime.ToArgs("--runtime", ""))
            .AddArgs(TerminalLogger.ToArgs("--tl", ""))
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddBooleanArgs(
                ("--nologo", NoLogo),
                ("--diagnostics", Diagnostics)
            )
            .AddProps("--property", Props.ToArray())
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Cleans the output of a project.", ShortName, "clean", Project.ToArg());
}

/// <summary>
/// Generates a self-signed certificate to enable HTTPS use in development.
/// <p>
/// This command manages a self-signed certificate to enable HTTPS use in local web app development. Its main functions are:
/// <br/>- Generating a certificate for use with HTTPS endpoints during development.
/// <br/>- Trusting the generated certificate on the local machine.
/// <br/>- Removing the generated certificate from the local machine.
/// <br/>- Exporting a certificate in various formats so that it can be used by other tools.
/// <br/>- Importing an existing certificate generated by the tool into the local machine.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-dev-certs">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// // Create a certificate, trust it, and export it to a PEM file.
/// new DotNetDevCertsHttps()
///     .WithExportPath("certificate.pem")
///     .WithTrust(true)
///     .WithFormat(DotNetCertificateFormat.Pem)
///     .WithPassword("Abc")
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Check">Checks for the existence of the development certificate but doesn't perform any action. Use this option with the --trust option to check if the certificate is not only valid but also trusted.</param>
/// <param name="Clean">Removes all HTTPS development certificates from the certificate store by using the .NET certificate store API. Doesn't remove any physical files that were created by using the --export-path option. On macOS in .NET 7.0, the dotnet dev-certs command creates the certificate on a path on disk, and the clean operation removes that certificate file.</param>
/// <param name="ExportPath">Exports the certificate to a file so that it can be used by other tools. Specify the full path to the exported certificate file, including the file name.</param>
/// <param name="Format">When used with --export-path, specifies the format of the exported certificate file. Valid values are PFX and PEM, case-insensitive. PFX is the default. The file format is independent of the file name extension. For example, if you specify --format pfx and --export-path ./cert.pem, you'll get a file named cert.pem in PFX format.</param>
/// <param name="Import">Imports the provided HTTPS development certificate into the local machine. Requires that you also specify the --clean option, which clears out any existing HTTPS developer certificates.</param>
/// <param name="NoPassword">Doesn't use a password for the key when exporting a certificate to PEM format files. The key file is exported in plain text. This option is not applicable to PFX files and is intended for internal testing use only.</param>
/// <param name="Password">Specifies the password to use.</param>
/// <param name="Quiet">Display warnings and errors only.</param>
/// <param name="Trust">rusts the certificate on the local machine. If this option isn't specified, the certificate is added to the certificate store but not to a trusted list. When combined with the --check option, validates that the certificate is trusted.</param>
/// <param name="Verbose">Display debug information.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetDevCertsHttps(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    bool? Check = default,
    bool? Clean = default,
    string ExportPath = "",
    DotNetCertificateFormat? Format = default,
    bool? Import = default,
    bool? NoPassword = default,
    string Password = "",
    bool? Quiet = default,
    bool? Trust = default,
    bool? Verbose = default,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetDevCertsHttps(params string[] args)
        : this(args, [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetDevCertsHttps()
        : this([], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("dev-certs")
            .AddArgs("https")
            .AddArgs(ExportPath.ToArgs("--export-path", ""))
            .AddArgs(Format.ToArgs("--format", ""))
            .AddArgs(Password.ToArgs("--password", ""))
            .AddBooleanArgs(
                ("--check", Check),
                ("--clean", Clean),
                ("--import", Import),
                ("--no-password", NoPassword),
                ("--quiet", Quiet),
                ("--trust", Trust),
                ("--verbose", Verbose),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Generates a self-signed certificate to enable HTTPS use in development.", ShortName, "dev-certs", "https");
}

/// <summary>
/// Formats code to match editorconfig settings.
/// <p>
/// This command formats a code that applies style preferences and static analysis recommendations to a project or solution. Preferences will be read from an .editorconfig file, if present, otherwise a default set of preferences will be used. For more information, see the EditorConfig documentation.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-format">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetFormat()
///     .WithWorkingDirectory("MyLib")
///     .WithProject("MyLib.csproj")
///     .AddDiagnostics("IDE0005", "IDE0006")
///     .AddIncludes(".", "./tests")
///     .AddExcludes("./obj")
///     .WithSeverity(DotNetFormatSeverity.Information)
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Diagnostics">A list of diagnostic IDs to use as a filter when fixing code style or third-party issues. Default value is whichever IDs are listed in the .editorconfig file. For a list of built-in analyzer rule IDs that you can specify, see the list of IDs for code-analysis style rules.</param>
/// <param name="Includes">A list of relative file or folder paths to include in formatting. The default is all files in the solution or project.</param>
/// <param name="Excludes">A space-separated list of relative file or folder paths to exclude from formatting. The default is none.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Project">The MSBuild project or solution to run code formatting on. If a project or solution file is not specified, MSBuild searches the current working directory for a file that has a file extension that ends in proj or sln, and uses that file.</param>
/// <param name="Severity">The minimum severity of diagnostics to fix. Allowed values are <c>Information</c>, <c>Warning</c>, and <c>Error</c>. The default value is <c>Warning</c>.</param>
/// <param name="NoRestore">Doesn't execute an implicit restore when running the command.</param>
/// <param name="VerifyNoChanges">Verifies that no formatting changes would be performed. Terminates with a non zero exit code if any files would have been formatted.</param>
/// <param name="IncludeGenerated">Formats files generated by the SDK.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="BinaryLog">Logs all project or solution load information to a binary log file.</param>
/// <param name="Report">Produces a JSON report in the specified directory.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetFormat(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<string> Diagnostics,
    IEnumerable<string> Includes,
    IEnumerable<string> Excludes,
    string Project = "",
    DotNetFormatSeverity? Severity = default,
    bool? NoRestore = default,
    bool? VerifyNoChanges = default,
    bool? IncludeGenerated = default,
    DotNetVerbosity? Verbosity = default,
    string BinaryLog = "",
    string Report = "",
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetFormat(params string[] args)
        : this(args, [], [], [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetFormat()
        : this([], [], [], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("format")
            .AddNotEmptyArgs(Project.ToArg())
            .AddArgs(Diagnostics.ToArgs("--diagnostics", " "))
            .AddArgs(Includes.ToArgs("--include", " "))
            .AddArgs(Excludes.ToArgs("--exclude", " "))
            .AddArgs(Severity.ToArgs("--severity", ""))
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddArgs(BinaryLog.ToArgs("--binarylog", ""))
            .AddArgs(Report.ToArgs("--report", ""))
            .AddBooleanArgs(
                ("--no-restore", NoRestore),
                ("--verify-no-changes", VerifyNoChanges),
                ("--include-generated", IncludeGenerated)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Formats code to match editorconfig settings.", ShortName, "format", Project.ToArg());
}

/// <summary>
/// Formats code to match EditorConfig settings for code style.
/// <p>
/// This command only runs formatting rules associated with code style formatting. For a complete list of formatting options that you can specify in your editorconfig file.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-format#whitespace">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetFormatStyle()
///     .WithWorkingDirectory("MyLib")
///     .WithProject("MyLib.csproj")
///     .AddDiagnostics("IDE0005", "IDE0006")
///     .WithSeverity(DotNetFormatSeverity.Information)
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Diagnostics">A list of diagnostic IDs to use as a filter when fixing code style issues. Default value is whichever IDs are listed in the .editorconfig file. For a list of built-in code style analyzer rule IDs that you can specify, see the list of IDs for code-analysis style rules.</param>
/// <param name="Includes">A list of relative file or folder paths to include in formatting. The default is all files in the solution or project.</param>
/// <param name="Excludes">A space-separated list of relative file or folder paths to exclude from formatting. The default is none.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Project">The MSBuild project or solution to run code formatting on. If a project or solution file is not specified, MSBuild searches the current working directory for a file that has a file extension that ends in proj or sln, and uses that file.</param>
/// <param name="Folder">Treat the <c>Project</c> argument as a path to a simple folder of code files.</param>
/// <param name="Severity">The minimum severity of diagnostics to fix. Allowed values are <c>Information</c>, <c>Warning</c>, and <c>Error</c>. The default value is <c>Warning</c>.</param>
/// <param name="NoRestore">Doesn't execute an implicit restore when running the command.</param>
/// <param name="VerifyNoChanges">Verifies that no formatting changes would be performed. Terminates with a non zero exit code if any files would have been formatted.</param>
/// <param name="IncludeGenerated">Formats files generated by the SDK.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="BinaryLog">Logs all project or solution load information to a binary log file.</param>
/// <param name="Report">Produces a JSON report in the specified directory.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetFormatStyle(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<string> Diagnostics,
    IEnumerable<string> Includes,
    IEnumerable<string> Excludes,
    string Project = "",
    bool? Folder = default,
    DotNetFormatSeverity? Severity = default,
    bool? NoRestore = default,
    bool? VerifyNoChanges = default,
    bool? IncludeGenerated = default,
    DotNetVerbosity? Verbosity = default,
    string BinaryLog = "",
    string Report = "",
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetFormatStyle(params string[] args)
        : this(args, [], [], [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetFormatStyle()
        : this([], [], [], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("format")
            .AddArgs("style")
            .AddNotEmptyArgs(Project.ToArg())
            .AddArgs(Diagnostics.ToArgs("--diagnostics", " "))
            .AddArgs(Includes.ToArgs("--include", " "))
            .AddArgs(Excludes.ToArgs("--exclude", " "))
            .AddArgs(Severity.ToArgs("--severity", ""))
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddArgs(BinaryLog.ToArgs("--binarylog", ""))
            .AddArgs(Report.ToArgs("--report", ""))
            .AddBooleanArgs(
                ("--folder", Folder),
                ("--no-restore", NoRestore),
                ("--verify-no-changes", VerifyNoChanges),
                ("--include-generated", IncludeGenerated)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Formats code to match EditorConfig settings for code style.", ShortName, "format", "style", Project.ToArg());
}

/// <summary>
/// Formats code to match editorconfig settings for analyzers (excluding code style rules).
/// <p>
/// This command only runs formatting rules associated with analyzers. For a list of analyzer rules that you can specify in your editorconfig file, see Quality rules.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-format#analyzers">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetFormatAnalyzers()
///     .WithWorkingDirectory("MyLib")
///     .WithProject("MyLib.csproj")
///     .AddDiagnostics("CA1831", "CA1832")
///     .WithSeverity(DotNetFormatSeverity.Warning)
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Diagnostics">A list of diagnostic IDs to use as a filter when fixing non code style issues. Default value is whichever IDs are listed in the .editorconfig file. For a list of built-in analyzer rule IDs that you can specify, see the list of IDs for quality rules. For third-party analyzers refer to their documentation.</param>
/// <param name="Includes">A list of relative file or folder paths to include in formatting. The default is all files in the solution or project.</param>
/// <param name="Excludes">A space-separated list of relative file or folder paths to exclude from formatting. The default is none.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Project">The MSBuild project or solution to run code formatting on. If a project or solution file is not specified, MSBuild searches the current working directory for a file that has a file extension that ends in proj or sln, and uses that file.</param>
/// <param name="Severity">The minimum severity of diagnostics to fix. Allowed values are <c>Information</c>, <c>Warning</c>, and <c>Error</c>. The default value is <c>Warning</c>.</param>
/// <param name="NoRestore">Doesn't execute an implicit restore when running the command.</param>
/// <param name="VerifyNoChanges">Verifies that no formatting changes would be performed. Terminates with a non zero exit code if any files would have been formatted.</param>
/// <param name="IncludeGenerated">Formats files generated by the SDK.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="BinaryLog">Logs all project or solution load information to a binary log file.</param>
/// <param name="Report">Produces a JSON report in the specified directory.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetFormatAnalyzers(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<string> Diagnostics,
    IEnumerable<string> Includes,
    IEnumerable<string> Excludes,
    string Project = "",
    DotNetFormatSeverity? Severity = default,
    bool? NoRestore = default,
    bool? VerifyNoChanges = default,
    bool? IncludeGenerated = default,
    DotNetVerbosity? Verbosity = default,
    string BinaryLog = "",
    string Report = "",
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetFormatAnalyzers(params string[] args)
        : this(args, [], [], [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetFormatAnalyzers()
        : this([], [], [], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("format")
            .AddArgs("analyzers")
            .AddNotEmptyArgs(Project.ToArg())
            .AddArgs(Diagnostics.ToArgs("--diagnostics", " "))
            .AddArgs(Includes.ToArgs("--include", " "))
            .AddArgs(Excludes.ToArgs("--exclude", " "))
            .AddArgs(Severity.ToArgs("--severity", ""))
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddArgs(BinaryLog.ToArgs("--binarylog", ""))
            .AddArgs(Report.ToArgs("--report", ""))
            .AddBooleanArgs(
                ("--no-restore", NoRestore),
                ("--verify-no-changes", VerifyNoChanges),
                ("--include-generated", IncludeGenerated)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Formats code to match editorconfig settings for analyzers (excluding code style rules).", ShortName, "format", "analyzers", Project.ToArg());
}

/// <summary>
/// Creates a new project, configuration file, or solution based on the specified template.
/// <p>
/// This command creates a .NET project or other artifacts based on a template. The command calls the template engine to create the artifacts on disk based on the specified template and options.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-new">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetNew()
///     .WithTemplateName("classlib")
///     .WithName("MyLib")
///     .WithForce(true)
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="TemplateName">The template to instantiate when the command is invoked. Each template might have specific options you can pass.</param>
/// <param name="DryRun">Displays a summary of what would happen if the given command were run if it would result in a template creation. Available since .NET Core 2.2 SDK.</param>
/// <param name="Force">Forces content to be generated even if it would change existing files. This is required when the template chosen would override existing files in the output directory.</param>
/// <param name="Language">The language of the template to create. The language accepted varies by the template (see defaults in the arguments section). Not valid for some templates.</param>
/// <param name="Name">The name for the created output. If no name is specified, the name of the current directory is used.</param>
/// <param name="Framework">Specifies the target framework. It expects a target framework moniker (TFM). Examples: "net6.0", "net7.0-macos". This value will be reflected in the project file.</param>
/// <param name="NoUpdateCheck">Disables checking for template package updates when instantiating a template. Available since .NET SDK 6.0.100.</param>
/// <param name="Output">Location to place the generated output. The default is the current directory.</param>
/// <param name="NoRestore">Doesn't execute an implicit restore when running the command.</param>
/// <param name="Project">The project that the template is added to. This project is used for context evaluation. If not specified, the project in the current or parent directories will be used. Available since .NET SDK 7.0.100.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetNew(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    string TemplateName = "",
    bool? DryRun = default,
    bool? Force = default,
    DotNetLanguage? Language = default,
    string Name = "",
    string Framework = "",
    bool? NoUpdateCheck = default,
    string Output = "",
    bool? NoRestore = default,
    string Project = "",
    DotNetVerbosity? Verbosity = default,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetNew(params string[] args)
        : this(args, [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetNew()
        : this([], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("new")
            .AddNotEmptyArgs(TemplateName.ToArg())
            .AddArgs(Language.ToArgs("--language", ""))
            .AddArgs(Name.ToArgs("--name", ""))
            .AddArgs(Framework.ToArgs("--framework", ""))
            .AddArgs(Output.ToArgs("--output", ""))
            .AddArgs(Project.ToArgs("--project", ""))
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddBooleanArgs(
                ("--dry-run", DryRun),
                ("--force", Force),
                ("-no-update-check", NoUpdateCheck),
                ("--no-restore", NoRestore),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Creates a new project, configuration file, or solution based on the specified template.", ShortName, "new", TemplateName.ToArg());
}

/// <summary>
/// Lists available templates to be run using dotnet new.
/// <p>
/// This command lists available templates to use with dotnet new. If the &lt;TEMPLATE_NAME&gt; is specified, lists templates containing the specified name. This option lists only default and installed templates. To find templates in NuGet that you can install locally, use the search command.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-new-list">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetNewList()
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Columns">Columns to display in the output.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="TemplateName">If the argument is specified, only the templates containing TEMPLATE_NAME in template name or short name will be shown.</param>
/// <param name="Author">Filters templates based on template author. Partial match is supported. Available since .NET SDK 5.0.300.</param>
/// <param name="ColumnsAll">Displays all columns in the output. Available since .NET SDK 5.0.300.</param>
/// <param name="IgnoreConstraints">Disables checking if the template meets the constraints to be run. Available since .NET SDK 7.0.100.</param>
/// <param name="Language">Filters templates based on language supported by the template. The language accepted varies by the template. Not valid for some templates.</param>
/// <param name="Output">Location to place the generated output. The default is the current directory. For the list command, it might be necessary to specify the output directory to correctly evaluate constraints for the template. Available since .NET SDK 7.0.100.</param>
/// <param name="Project">The project that the template is added to. For the list command, it might be needed to specify the project the template is being added to to correctly evaluate constraints for the template. Available since .NET SDK 7.0.100.</param>
/// <param name="Tag">Filters templates based on template tags. To be selected, a template must have at least one tag that exactly matches the criteria. Available since .NET SDK 5.0.300.</param>
/// <param name="Type">Filters templates based on template type.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetNewList(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<DotNetNewListColumn> Columns,
    string TemplateName = "",
    string Author = "",
    bool? ColumnsAll = default,
    bool? IgnoreConstraints = default,
    DotNetLanguage? Language = default,
    string Output = "",
    string Project = "",
    string Tag = "",
    DotNetTemplateType? Type = default,
    DotNetVerbosity? Verbosity = default,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetNewList(params string[] args)
        : this(args, [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetNewList()
        : this([], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("new")
            .AddArgs("list")
            .AddNotEmptyArgs(TemplateName.ToArg())
            .AddArgs(Columns.ToArgs("--columns", ""))
            .AddArgs(Author.ToArgs("--author", ""))
            .AddArgs(Language.ToArgs("--language", ""))
            .AddArgs(Output.ToArgs("--output", ""))
            .AddArgs(Project.ToArgs("--project", ""))
            .AddArgs(Tag.ToArgs("--tag", ""))
            .AddArgs(Type.ToArgs("--type", ""))
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddBooleanArgs(
                ("--columns-all", ColumnsAll),
                ("--ignore-constraints", IgnoreConstraints),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Lists available templates to be run using dotnet new.", ShortName, "new", "list", TemplateName.ToArg());
}

/// <summary>
/// Searches for the templates supported by dotnet new on NuGet.org.
/// <p>
/// This command searches for templates supported by dotnet new on NuGet.org. When the &lt;TEMPLATE_NAME&gt; is specified, searches for templates containing the specified name.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-new-search">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetNewSearch()
///     .WithTemplateName("build")
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Columns">Columns to display in the output.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="TemplateName">If the argument is specified, only the templates containing TEMPLATE_NAME in template name or short name will be shown.</param>
/// <param name="Author">Filters templates based on template author. Partial match is supported. Available since .NET SDK 5.0.300.</param>
/// <param name="ColumnsAll">Displays all columns in the output. Available since .NET SDK 5.0.300.</param>
/// <param name="Language">Filters templates based on language supported by the template.</param>
/// <param name="Package">Filters templates based on NuGet package ID. A partial match is supported.</param>
/// <param name="Tag">Filters templates based on template tags. To be selected, a template must have at least one tag that exactly matches the criteria.</param>
/// <param name="Type">Filters templates based on template type.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetNewSearch(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<DotNetNewListColumn> Columns,
    string TemplateName = "",
    string Author = "",
    bool? ColumnsAll = default,
    DotNetLanguage? Language = default,
    string Package = "",
    string Tag = "",
    DotNetTemplateType? Type = default,
    DotNetVerbosity? Verbosity = default,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetNewSearch(params string[] args)
        : this(args, [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetNewSearch()
        : this([], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("new")
            .AddArgs("search")
            .AddNotEmptyArgs(TemplateName.ToArg())
            .AddArgs(Columns.ToArgs("--columns", ""))
            .AddArgs(Author.ToArgs("--author", ""))
            .AddArgs(Language.ToArgs("--language", ""))
            .AddArgs(Package.ToArgs("--package", ""))
            .AddArgs(Tag.ToArgs("--tag", ""))
            .AddArgs(Type.ToArgs("--type", ""))
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddBooleanArgs(
                ("--columns-all", ColumnsAll),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Searches for the templates supported by dotnet new on NuGet.org.", ShortName, "new", "search", TemplateName.ToArg());
}

/// <summary>
/// Displays template package metadata.
/// <p>
/// This command displays the metadata of the template package from the package name provided. By default, the command searches for the latest available version. If the package is installed locally or is found on the official NuGet website, it also displays the templates that the package contains, otherwise it only displays basic metadata.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-new-details">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetNewDetails()
///     .WithTemplateName("CSharpInteractive.Templates")
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Sources">The URI of the NuGet package source to use during this operation.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="TemplateName">If the argument is specified, only the templates containing TEMPLATE_NAME in template name or short name will be shown.</param>
/// <param name="Force">Forces all dependencies to be resolved even if the last restore was successful. Specifying this flag is the same as deleting the project.assets.json file.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetNewDetails(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<string> Sources,
    string TemplateName = "",
    bool? Force = default,
    DotNetVerbosity? Verbosity = default,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetNewDetails(params string[] args)
        : this(args, [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetNewDetails()
        : this([], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("new")
            .AddArgs("details")
            .AddNotEmptyArgs(TemplateName.ToArg())
            .AddArgs(Sources.ToArgs("--add-source", ""))
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddBooleanArgs(
                ("--force", Force),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Displays template package metadata.", ShortName, "new", "details", TemplateName.ToArg());
}

/// <summary>
/// Installs a template package.
/// <p>
/// This command installs a template package from the PATH or NUGET_ID provided. If you want to install a specific version or prerelease version of a template package, specify the version in the format &lt;package-name&gt;::&lt;package-version&gt;. By default, dotnet new passes * for the version, which represents the latest stable package version.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-new-install">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetNewInstall()
///     .WithPackage("Pure.DI.Templates")
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Sources">The URI of the NuGet package source to use during this operation.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Package">The folder on the file system or the NuGet package identifier to install the template package from. dotnet new attempts to install the NuGet package from the NuGet sources available for the current working directory and the sources specified via the --add-source option. If you want to install a specific version or prerelease version of a template package from NuGet source, specify the version in the format &lt;package-name&gt;::&lt;package-version&gt;.</param>
/// <param name="Force">Forces all dependencies to be resolved even if the last restore was successful. Specifying this flag is the same as deleting the project.assets.json file.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetNewInstall(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<string> Sources,
    string Package = "",
    bool? Force = default,
    DotNetVerbosity? Verbosity = default,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetNewInstall(params string[] args)
        : this(args, [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetNewInstall()
        : this([], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("new")
            .AddArgs("install")
            .AddNotEmptyArgs(Package.ToArg())
            .AddArgs(Sources.ToArgs("--add-source", ""))
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddBooleanArgs(
                ("--force", Force),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Installs a template package.", ShortName, "new", "install", Package.ToArg());
}

/// <summary>
/// Uninstalls a template package.
/// <p>
/// This command uninstalls a template package at the PATH or NUGET_ID provided. When the &lt;PATH|NUGET_ID&gt; value isn&apos;t specified, all currently installed template packages and their associated templates are displayed. When specifying NUGET_ID, don&apos;t include the version number.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-new-uninstall">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetNewUninstall()
///     .WithPackage("Pure.DI.Templates")
///     .Run();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Package">The folder on the file system or the NuGet package identifier the package was installed from. Note that the version for the NuGet package should not be specified.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetNewUninstall(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    string Package = "",
    DotNetVerbosity? Verbosity = default,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetNewUninstall(params string[] args)
        : this(args, [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetNewUninstall()
        : this([], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("new")
            .AddArgs("uninstall")
            .AddNotEmptyArgs(Package.ToArg())
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddBooleanArgs(
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Uninstalls a template package.", ShortName, "new", "uninstall", Package.ToArg());
}

/// <summary>
/// Updates installed template packages.
/// <p>
/// This command updates installed template packages. The dotnet new update command with --check-only option checks for available updates for installed template packages without applying them.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-new-update">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetNewUpdate()
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Sources">The URI of the NuGet package source to use during this operation.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="CheckOnly">Only checks for updates and displays the template packages to be updated, without applying any updates.</param>
/// <param name="DryRun">Only checks for updates and displays the template packages to be updated, without applying any updates.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetNewUpdate(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<string> Sources,
    bool? CheckOnly = default,
    bool? DryRun = default,
    DotNetVerbosity? Verbosity = default,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetNewUpdate(params string[] args)
        : this(args, [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetNewUpdate()
        : this([], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("new")
            .AddArgs("update")
            .AddArgs(Sources.ToArgs("--add-source", ""))
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddBooleanArgs(
                ("--check-only", CheckOnly),
                ("--dry-run", DryRun),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Updates installed template packages.", ShortName, "new", "update");
}

/// <summary>
/// Deletes or unlists a package from the server.
/// <p>
/// This command deletes or unlists a package from the server. For nuget.org, the action is to unlist the package.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-delete">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetNuGetDelete()
///     .WithPackage("MyLib")
///     .WithPackageVersion("1.0.0")
///     .WithSource(repoUrl)
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Package">Name/ID of the package to delete.</param>
/// <param name="PackageVersion">Version of the package to delete.</param>
/// <param name="ForceEnglishOutput">Forces the application to run using an invariant, English-based culture.</param>
/// <param name="ApiKey">The API key for the server.</param>
/// <param name="NoServiceEndpoint">Doesn't append "api/v2/package" to the source URL.</param>
/// <param name="Source">Specifies the server URL. NuGet identifies a UNC or local folder source and simply copies the file there instead of pushing it using HTTP.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetNuGetDelete(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    string Package = "",
    string PackageVersion = "",
    bool? ForceEnglishOutput = default,
    string ApiKey = "",
    bool? NoServiceEndpoint = default,
    string Source = "",
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetNuGetDelete(params string[] args)
        : this(args, [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetNuGetDelete()
        : this([], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("nuget")
            .AddArgs("delete")
            .AddNotEmptyArgs(Package.ToArg())
            .AddNotEmptyArgs(PackageVersion.ToArg())
            .AddArgs("--non-interactive")
            .AddArgs(ApiKey.ToArgs("--api-key", ""))
            .AddArgs(Source.ToArgs("--source", ""))
            .AddBooleanArgs(
                ("--force-english-output", ForceEnglishOutput),
                ("--no-service-endpoint", NoServiceEndpoint),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Deletes or unlists a package from the server.", ShortName, "nuget", "delete", Package.ToArg(), PackageVersion.ToArg());
}

/// <summary>
/// Clears local NuGet resources.
/// <p>
/// This command clears local NuGet resources in the http-request cache, temporary cache, or machine-wide global packages folder.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-locals">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetNuGetLocalsClear()
///     .WithCacheLocation(NuGetCacheLocation.Temp)
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="CacheLocation">The cache location to clear.</param>
/// <param name="ForceEnglishOutput">Forces the application to run using an invariant, English-based culture.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetNuGetLocalsClear(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    NuGetCacheLocation? CacheLocation = default,
    bool? ForceEnglishOutput = default,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetNuGetLocalsClear(params string[] args)
        : this(args, [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetNuGetLocalsClear()
        : this([], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("nuget")
            .AddArgs("locals")
            .AddNotEmptyArgs(CacheLocation.ToArg())
            .AddArgs("--clear")
            .AddBooleanArgs(
                ("--force-english-output", ForceEnglishOutput),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Clears local NuGet resources.", ShortName, "nuget", "locals", CacheLocation.ToArg());
}

/// <summary>
/// Displays the location of the specified cache type.
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-locals">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetNuGetLocalsList()
///     .WithCacheLocation(NuGetCacheLocation.GlobalPackages)
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="CacheLocation">The cache location to list.</param>
/// <param name="ForceEnglishOutput">Forces the application to run using an invariant, English-based culture.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetNuGetLocalsList(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    NuGetCacheLocation? CacheLocation = default,
    bool? ForceEnglishOutput = default,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetNuGetLocalsList(params string[] args)
        : this(args, [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetNuGetLocalsList()
        : this([], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("nuget")
            .AddArgs("locals")
            .AddNotEmptyArgs(CacheLocation.ToArg())
            .AddArgs("--list")
            .AddBooleanArgs(
                ("--force-english-output", ForceEnglishOutput),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Displays the location of the specified cache type.", ShortName, "nuget", "locals", CacheLocation.ToArg());
}

/// <summary>
/// Pushes a package to the server and publishes it.
/// <p>
/// This command pushes a package to the server and publishes it. The push command uses server and credential details found in the system's NuGet config file or chain of config files. NuGet's default configuration is obtained by loading %AppData%\NuGet\NuGet.config (Windows) or $HOME/.nuget/NuGet/NuGet.Config (Linux/macOS), then loading any nuget.config or .nuget\nuget.config starting from the root of drive and ending in the current directory.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-push">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetNuGetPush()
///     .WithWorkingDirectory("MyLib")
///     .WithPackage(Path.Combine("packages", "MyLib.1.0.0.nupkg"))
///     .WithSource(repoUrl)
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Package">Specifies the file path to the package to be pushed.</param>
/// <param name="DisableBuffering">Disables buffering when pushing to an HTTP(S) server to reduce memory usage.</param>
/// <param name="ForceEnglishOutput">Forces the application to run using an invariant, English-based culture.</param>
/// <param name="ApiKey">The API key for the server.</param>
/// <param name="NoSymbols">Doesn't push symbols (even if present).</param>
/// <param name="NoServiceEndpoint">Doesn't append "api/v2/package" to the source URL.</param>
/// <param name="Source">Specifies the server URL. NuGet identifies a UNC or local folder source and simply copies the file there instead of pushing it using HTTP.</param>
/// <param name="SkipDuplicate">When pushing multiple packages to an HTTP(S) server, treats any 409 Conflict response as a warning so that other pushes can continue.</param>
/// <param name="SymbolApiKey">The API key for the symbol server.</param>
/// <param name="SymbolSource">Specifies the symbol server URL.</param>
/// <param name="Timeout">Specifies the timeout for pushing to a server in seconds. Defaults to 300 seconds (5 minutes). Specifying 0 applies the default value.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetNuGetPush(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    string Package = "",
    bool? DisableBuffering = default,
    bool? ForceEnglishOutput = default,
    string ApiKey = "",
    bool? NoSymbols = default,
    bool? NoServiceEndpoint = default,
    string Source = "",
    bool? SkipDuplicate = default,
    string SymbolApiKey = "",
    string SymbolSource = "",
    int? Timeout = default,
    DotNetVerbosity? Verbosity = default,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetNuGetPush(params string[] args)
        : this(args, [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetNuGetPush()
        : this([], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("nuget")
            .AddArgs("push")
            .AddNotEmptyArgs(Package.ToArg())
            .AddArgs(ApiKey.ToArgs("--api-key", ""))
            .AddArgs(Source.ToArgs("--source", ""))
            .AddArgs(SymbolApiKey.ToArgs("--symbol-api-key", ""))
            .AddArgs(SymbolSource.ToArgs("--symbol-source", ""))
            .AddArgs(Timeout.ToArgs("--timeout", ""))
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddBooleanArgs(
                ("--disable-buffering", DisableBuffering),
                ("--force-english-output", ForceEnglishOutput),
                ("--no-symbols", NoSymbols),
                ("--no-service-endpoint", NoServiceEndpoint),
                ("--skip-duplicate", SkipDuplicate),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Pushes a package to the server and publishes it.", ShortName, "nuget", "push", Package.ToArg());
}

/// <summary>
/// Add a NuGet source.
/// <p>
/// This command adds a new package source to your NuGet configuration files. When adding multiple package sources, be careful not to introduce a dependency confusion vulnerability.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-add-source">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetNuGetAddSource()
///     .WithName("TestSource")
///     .WithSource(source)
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ValidAuthenticationTypes">List of valid authentication types for this source. Set this to basic if the server advertises NTLM or Negotiate and your credentials must be sent using the Basic mechanism, for instance when using a PAT with on-premises Azure DevOps Server. Other valid values include negotiate, kerberos, ntlm, and digest, but these values are unlikely to be useful.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Source">Path to the package source.</param>
/// <param name="ConfigFile">The NuGet configuration file (nuget.config) to use. If specified, only the settings from this file will be used. If not specified, the hierarchy of configuration files from the current directory will be used.</param>
/// <param name="AllowInsecureConnections">Allows HTTP connections for adding or updating packages. This method is not secure. Available since .NET 9 SDK.</param>
/// <param name="Name">Name of the source.</param>
/// <param name="Password">Password to be used when connecting to an authenticated source.</param>
/// <param name="StorePasswordInClearText">Enables storing portable package source credentials by disabling password encryption. Storing passwords in clear text is strongly discouraged.</param>
/// <param name="Username">Username to be used when connecting to an authenticated source.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetNuGetAddSource(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<NuGetAuthenticationType> ValidAuthenticationTypes,
    string Source = "",
    string ConfigFile = "",
    bool? AllowInsecureConnections = default,
    string Name = "",
    string Password = "",
    bool? StorePasswordInClearText = default,
    string Username = "",
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetNuGetAddSource(params string[] args)
        : this(args, [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetNuGetAddSource()
        : this([], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("nuget")
            .AddArgs("add")
            .AddArgs("source")
            .AddNotEmptyArgs(Source.ToArg())
            .AddArgs(ValidAuthenticationTypes.ToArgs("--valid-authentication-types", ""))
            .AddArgs(ConfigFile.ToArgs("--configfile", ""))
            .AddArgs(Name.ToArgs("--name", ""))
            .AddArgs(Password.ToArgs("--password", ""))
            .AddArgs(Username.ToArgs("--username", ""))
            .AddBooleanArgs(
                ("--allow-insecure-connections", AllowInsecureConnections),
                ("--store-password-in-clear-text", StorePasswordInClearText),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Add a NuGet source.", ShortName, "nuget", "add", "source", Source.ToArg());
}

/// <summary>
/// Disable a NuGet source.
/// <p>
/// This command disables an existing source in your NuGet configuration files.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-disable-source">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetNuGetDisableSource()
///     .WithName("TestSource")
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Name">Name of the source.</param>
/// <param name="ConfigFile">The NuGet configuration file (nuget.config) to use. If specified, only the settings from this file will be used. If not specified, the hierarchy of configuration files from the current directory will be used.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetNuGetDisableSource(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    string Name = "",
    string ConfigFile = "",
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetNuGetDisableSource(params string[] args)
        : this(args, [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetNuGetDisableSource()
        : this([], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("nuget")
            .AddArgs("disable")
            .AddArgs("source")
            .AddNotEmptyArgs(Name.ToArg())
            .AddArgs(ConfigFile.ToArgs("--configfile", ""))
            .AddBooleanArgs(
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Disable a NuGet source.", ShortName, "nuget", "disable", "source", Name.ToArg());
}

/// <summary>
/// Enable a NuGet source.
/// <p>
/// This command enables an existing source in your NuGet configuration files.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-enable-source">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetNuGetEnableSource()
///     .WithName("TestSource")
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Name">Name of the source.</param>
/// <param name="ConfigFile">The NuGet configuration file (nuget.config) to use. If specified, only the settings from this file will be used. If not specified, the hierarchy of configuration files from the current directory will be used.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetNuGetEnableSource(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    string Name = "",
    string ConfigFile = "",
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetNuGetEnableSource(params string[] args)
        : this(args, [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetNuGetEnableSource()
        : this([], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("nuget")
            .AddArgs("enable")
            .AddArgs("source")
            .AddNotEmptyArgs(Name.ToArg())
            .AddArgs(ConfigFile.ToArgs("--configfile", ""))
            .AddBooleanArgs(
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Enable a NuGet source.", ShortName, "nuget", "enable", "source", Name.ToArg());
}

/// <summary>
/// Lists all configured NuGet sources.
/// <p>
/// This command lists all existing sources from your NuGet configuration files.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-list-source">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetNuGetListSource()
///     .WithFormat(NuGetListFormat.Short)
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="ConfigFile">The NuGet configuration file (nuget.config) to use. If specified, only the settings from this file will be used. If not specified, the hierarchy of configuration files from the current directory will be used.</param>
/// <param name="Format">The format of the list command output: Detailed (the default) and Short.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetNuGetListSource(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    string ConfigFile = "",
    NuGetListFormat? Format = default,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetNuGetListSource(params string[] args)
        : this(args, [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetNuGetListSource()
        : this([], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("nuget")
            .AddArgs("list")
            .AddArgs("source")
            .AddArgs(ConfigFile.ToArgs("--configfile", ""))
            .AddArgs(Format.ToArgs("--format", ""))
            .AddBooleanArgs(
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Lists all configured NuGet sources.", ShortName, "nuget", "list", "source");
}

/// <summary>
/// Remove a NuGet source.
/// <p>
/// This command removes an existing source from your NuGet configuration files.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-remove-source">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetNuGetRemoveSource()
///     .WithName("TestSource")
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Name">Name of the source.</param>
/// <param name="ConfigFile">The NuGet configuration file (nuget.config) to use. If specified, only the settings from this file will be used. If not specified, the hierarchy of configuration files from the current directory will be used.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetNuGetRemoveSource(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    string Name = "",
    string ConfigFile = "",
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetNuGetRemoveSource(params string[] args)
        : this(args, [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetNuGetRemoveSource()
        : this([], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("nuget")
            .AddArgs("remove")
            .AddArgs("source")
            .AddNotEmptyArgs(Name.ToArg())
            .AddArgs(ConfigFile.ToArgs("--configfile", ""))
            .AddBooleanArgs(
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Remove a NuGet source.", ShortName, "nuget", "remove", "source", Name.ToArg());
}

/// <summary>
/// Update a NuGet source.
/// <p>
/// This command updates an existing source in your NuGet configuration files.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-update-source">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetNuGetUpdateSource()
///     .WithName("TestSource")
///     .WithSource(newSource)
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ValidAuthenticationTypes">List of valid authentication types for this source. Set this to basic if the server advertises NTLM or Negotiate and your credentials must be sent using the Basic mechanism, for instance when using a PAT with on-premises Azure DevOps Server. Other valid values include negotiate, kerberos, ntlm, and digest, but these values are unlikely to be useful.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Name">Name of the source.</param>
/// <param name="ConfigFile">The NuGet configuration file (nuget.config) to use. If specified, only the settings from this file will be used. If not specified, the hierarchy of configuration files from the current directory will be used.</param>
/// <param name="Password">Password to be used when connecting to an authenticated source.</param>
/// <param name="Source">Path to the package source.</param>
/// <param name="StorePasswordInClearText">Enables storing portable package source credentials by disabling password encryption. Storing passwords in clear text is strongly discouraged.</param>
/// <param name="Username">Username to be used when connecting to an authenticated source.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetNuGetUpdateSource(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<NuGetAuthenticationType> ValidAuthenticationTypes,
    string Name = "",
    string ConfigFile = "",
    string Password = "",
    string Source = "",
    bool? StorePasswordInClearText = default,
    string Username = "",
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetNuGetUpdateSource(params string[] args)
        : this(args, [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetNuGetUpdateSource()
        : this([], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("nuget")
            .AddArgs("update")
            .AddArgs("source")
            .AddNotEmptyArgs(Name.ToArg())
            .AddArgs(ValidAuthenticationTypes.ToArgs("--valid-authentication-types", ""))
            .AddArgs(ConfigFile.ToArgs("--configfile", ""))
            .AddArgs(Password.ToArgs("--password", ""))
            .AddArgs(Source.ToArgs("--source", ""))
            .AddArgs(Username.ToArgs("--username", ""))
            .AddBooleanArgs(
                ("--store-password-in-clear-text", StorePasswordInClearText),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Update a NuGet source.", ShortName, "nuget", "update", "source", Name.ToArg());
}

/// <summary>
/// Verifies a signed NuGet package.
/// <p>
/// This command verifies a signed NuGet package. This command requires a certificate root store that is valid for both code signing and timestamping. Also, this command may not be supported on some combinations of operating system and .NET SDK.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-verify">.NET CLI command</a><br/>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Packages">Specifies the file path to the package(s) to be verified.</param>
/// <param name="Fingerprints">Verify that the signer certificate matches with one of the specified SHA256 fingerprints. This option can be supplied multiple times to provide multiple fingerprints.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="All">Specifies that all verifications possible should be performed on the package(s). By default, only signatures are verified.</param>
/// <param name="ConfigFile">The NuGet configuration file (nuget.config) to use. If specified, only the settings from this file will be used. If not specified, the hierarchy of configuration files from the current directory will be used.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetNuGetVerify(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<string> Packages,
    IEnumerable<string> Fingerprints,
    bool? All = default,
    string ConfigFile = "",
    DotNetVerbosity? Verbosity = default,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetNuGetVerify(params string[] args)
        : this(args, [], [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetNuGetVerify()
        : this([], [], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("nuget")
            .AddArgs("verify")
            .AddNotEmptyArgs(Packages.ToArray())
            .AddArgs(Packages.ToArgs("", ""))
            .AddArgs(Fingerprints.ToArgs("--certificate-fingerprint", ""))
            .AddArgs(ConfigFile.ToArgs("--configfile", ""))
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddBooleanArgs(
                ("--all", All),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Verifies a signed NuGet package.", ShortName, new [] {"nuget", "verify"}.Concat(Packages).ToArray());
}

/// <summary>
/// Lists all the trusted signers in the configuration.
/// <p>
/// This option will include all the certificates (with fingerprint and fingerprint algorithm) each signer has. If a certificate has a preceding [U], it means that certificate entry has allowUntrustedRoot set as true.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-trust#list">.NET CLI command</a><br/>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="ConfigFile">The NuGet configuration file (nuget.config) to use. If specified, only the settings from this file will be used. If not specified, the hierarchy of configuration files from the current directory will be used.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetNuGetTrustList(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    string ConfigFile = "",
    DotNetVerbosity? Verbosity = default,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetNuGetTrustList(params string[] args)
        : this(args, [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetNuGetTrustList()
        : this([], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("nuget")
            .AddArgs("trust")
            .AddArgs("list")
            .AddArgs(ConfigFile.ToArgs("--configfile", ""))
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddBooleanArgs(
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Lists all the trusted signers in the configuration.", ShortName, "nuget", "trust", "list");
}

/// <summary>
/// Deletes the current list of certificates and replaces them with an up-to-date list from the repository.
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-trust#sync">.NET CLI command</a><br/>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Name">The name of the existing trusted signer to sync.</param>
/// <param name="ConfigFile">The NuGet configuration file (nuget.config) to use. If specified, only the settings from this file will be used. If not specified, the hierarchy of configuration files from the current directory will be used.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetNuGetTrustSync(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    string Name = "",
    string ConfigFile = "",
    DotNetVerbosity? Verbosity = default,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetNuGetTrustSync(params string[] args)
        : this(args, [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetNuGetTrustSync()
        : this([], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("nuget")
            .AddArgs("trust")
            .AddArgs("sync")
            .AddNotEmptyArgs(Name.ToArg())
            .AddArgs(ConfigFile.ToArgs("--configfile", ""))
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddBooleanArgs(
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Deletes the current list of certificates and replaces them with an up-to-date list from the repository.", ShortName, "nuget", "trust", "sync", Name.ToArg());
}

/// <summary>
/// Removes any trusted signers that match the given name.
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-trust#sync">.NET CLI command</a><br/>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Name">The name of the existing trusted signer to remove.</param>
/// <param name="ConfigFile">The NuGet configuration file (nuget.config) to use. If specified, only the settings from this file will be used. If not specified, the hierarchy of configuration files from the current directory will be used.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetNuGetTrustRemove(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    string Name = "",
    string ConfigFile = "",
    DotNetVerbosity? Verbosity = default,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetNuGetTrustRemove(params string[] args)
        : this(args, [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetNuGetTrustRemove()
        : this([], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("nuget")
            .AddArgs("trust")
            .AddArgs("remove")
            .AddNotEmptyArgs(Name.ToArg())
            .AddArgs(ConfigFile.ToArgs("--configfile", ""))
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddBooleanArgs(
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Removes any trusted signers that match the given name.", ShortName, "nuget", "trust", "remove", Name.ToArg());
}

/// <summary>
/// Adds a trusted signer with the given name, based on the author signature of the package.
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-trust#author">.NET CLI command</a><br/>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Name">The name of the trusted signer to add. If NAME already exists in the configuration, the signature is appended.</param>
/// <param name="Package">The given PACKAGE should be a local path to the signed .nupkg file.</param>
/// <param name="AllowUntrustedRoot">Specifies if the certificate for the trusted signer should be allowed to chain to an untrusted root. This is not recommended.</param>
/// <param name="ConfigFile">The NuGet configuration file (nuget.config) to use. If specified, only the settings from this file will be used. If not specified, the hierarchy of configuration files from the current directory will be used.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetNuGetTrustAuthor(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    string Name = "",
    string Package = "",
    bool? AllowUntrustedRoot = default,
    string ConfigFile = "",
    DotNetVerbosity? Verbosity = default,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetNuGetTrustAuthor(params string[] args)
        : this(args, [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetNuGetTrustAuthor()
        : this([], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("nuget")
            .AddArgs("trust")
            .AddArgs("author")
            .AddNotEmptyArgs(Name.ToArg())
            .AddNotEmptyArgs(Package.ToArg())
            .AddArgs(ConfigFile.ToArgs("--configfile", ""))
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddBooleanArgs(
                ("--allow-untrusted-root", AllowUntrustedRoot),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Adds a trusted signer with the given name, based on the author signature of the package.", ShortName, "nuget", "trust", "author", Name.ToArg(), Package.ToArg());
}

/// <summary>
/// Adds a trusted signer with the given name, based on the repository signature or countersignature of a signed package.
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-trust#repository">.NET CLI command</a><br/>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Owners">List of trusted owners to further restrict the trust of a repository.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Name">The name of the trusted signer to add. If NAME already exists in the configuration, the signature is appended.</param>
/// <param name="Package">The given PACKAGE should be a local path to the signed .nupkg file.</param>
/// <param name="AllowUntrustedRoot">Specifies if the certificate for the trusted signer should be allowed to chain to an untrusted root. This is not recommended.</param>
/// <param name="ConfigFile">The NuGet configuration file (nuget.config) to use. If specified, only the settings from this file will be used. If not specified, the hierarchy of configuration files from the current directory will be used.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetNuGetTrustRepository(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<string> Owners,
    string Name = "",
    string Package = "",
    bool? AllowUntrustedRoot = default,
    string ConfigFile = "",
    DotNetVerbosity? Verbosity = default,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetNuGetTrustRepository(params string[] args)
        : this(args, [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetNuGetTrustRepository()
        : this([], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("nuget")
            .AddArgs("trust")
            .AddArgs("repository")
            .AddNotEmptyArgs(Name.ToArg())
            .AddNotEmptyArgs(Package.ToArg())
            .AddArgs(Owners.ToArgs("--owners", ","))
            .AddArgs(ConfigFile.ToArgs("--configfile", ""))
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddBooleanArgs(
                ("--allow-untrusted-root", AllowUntrustedRoot),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Adds a trusted signer with the given name, based on the repository signature or countersignature of a signed package.", ShortName, "nuget", "trust", "repository", Name.ToArg(), Package.ToArg());
}

/// <summary>
/// Adds a trusted signer with the given name, based on a certificate fingerprint.
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-trust#certificate">.NET CLI command</a><br/>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Name">The name of the trusted signer to add. If a trusted signer with the given name already exists, the certificate item is added to that signer. Otherwise a trusted author is created with a certificate item from the given certificate information.</param>
/// <param name="Fingerprint">The fingerprint of the certificate.</param>
/// <param name="Algorithm">Specifies the hash algorithm used to calculate the certificate fingerprint. Defaults to SHA256. Values supported are SHA256, SHA384 and SHA512.</param>
/// <param name="AllowUntrustedRoot">Specifies if the certificate for the trusted signer should be allowed to chain to an untrusted root. This is not recommended.</param>
/// <param name="ConfigFile">The NuGet configuration file (nuget.config) to use. If specified, only the settings from this file will be used. If not specified, the hierarchy of configuration files from the current directory will be used.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetNuGetTrustCertificate(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    string Name = "",
    string Fingerprint = "",
    NuGetCertificateAlgorithm? Algorithm = default,
    bool? AllowUntrustedRoot = default,
    string ConfigFile = "",
    DotNetVerbosity? Verbosity = default,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetNuGetTrustCertificate(params string[] args)
        : this(args, [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetNuGetTrustCertificate()
        : this([], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("nuget")
            .AddArgs("trust")
            .AddArgs("certificate")
            .AddNotEmptyArgs(Name.ToArg())
            .AddNotEmptyArgs(Fingerprint.ToArg())
            .AddArgs(Algorithm.ToArgs("--algorithm", ""))
            .AddArgs(ConfigFile.ToArgs("--configfile", ""))
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddBooleanArgs(
                ("--allow-untrusted-root", AllowUntrustedRoot),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Adds a trusted signer with the given name, based on a certificate fingerprint.", ShortName, "nuget", "trust", "certificate", Name.ToArg(), Fingerprint.ToArg());
}

/// <summary>
/// Adds a trusted signer based on a given package source.
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-trust#source">.NET CLI command</a><br/>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Owners">List of trusted owners to further restrict the trust of a repository.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Name">The name of the trusted signer to add. If only &lt;NAME&gt; is provided without --&lt;source-url&gt;, the package source from your NuGet configuration files with the same name is added to the trusted list. If &lt;NAME&gt; already exists in the configuration, the package source is appended to it.</param>
/// <param name="ConfigFile">The NuGet configuration file (nuget.config) to use. If specified, only the settings from this file will be used. If not specified, the hierarchy of configuration files from the current directory will be used.</param>
/// <param name="SourceUrl">If a source-url is provided, it must be a v3 package source URL (like https://api.nuget.org/v3/index.json). Other package source types are not supported.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetNuGetTrustSource(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<string> Owners,
    string Name = "",
    string ConfigFile = "",
    string SourceUrl = "",
    DotNetVerbosity? Verbosity = default,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetNuGetTrustSource(params string[] args)
        : this(args, [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetNuGetTrustSource()
        : this([], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("nuget")
            .AddArgs("trust")
            .AddArgs("source")
            .AddNotEmptyArgs(Name.ToArg())
            .AddArgs(Owners.ToArgs("--owners", ","))
            .AddArgs(ConfigFile.ToArgs("--configfile", ""))
            .AddArgs(SourceUrl.ToArgs("--source-url", ""))
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddBooleanArgs(
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Adds a trusted signer based on a given package source.", ShortName, "nuget", "trust", "source", Name.ToArg());
}

/// <summary>
/// Signs the NuGet packages with a certificate.
/// <p>
/// This command signs all the packages matching the first argument with a certificate. The certificate with the private key can be obtained from a file or from a certificate installed in a certificate store by providing a subject name or a SHA-1 fingerprint. This command requires a certificate root store that is valid for both code signing and timestamping. Also, this command may not be supported on some combinations of operating system and .NET SDK.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-sign">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetNuGetSign()
///     .AddPackages("MyLib.1.2.3.nupkg")
///     .WithCertificatePath("certificate.pfx")
///     .WithCertificatePassword("Abc")
///     .WithTimestampingServer("http://timestamp.digicert.com/")
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Packages">Specifies the file path to the packages to be signed.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="CertificatePath">Specifies the file path to the certificate to be used in signing the package. This option currently supports only PKCS12 (PFX) files that contain the certificate's private key.</param>
/// <param name="CertificateStoreName">Specifies the name of the X.509 certificate store to use to search for the certificate. Defaults to "My", the X.509 certificate store for personal certificates. This option should be used when specifying the certificate via --certificate-subject-name or --certificate-fingerprint options.</param>
/// <param name="CertificateStoreLocation">Specifies the name of the X.509 certificate store use to search for the certificate. Defaults to "CurrentUser", the X.509 certificate store used by the current user. This option should be used when specifying the certificate via --certificate-subject-name or --certificate-fingerprint options.</param>
/// <param name="CertificateSubjectName">Specifies the subject name of the certificate used to search a local certificate store for the certificate. The search is a case-insensitive string comparison using the supplied value, which finds all certificates with the subject name containing that string, regardless of other subject values. The certificate store can be specified by --certificate-store-name and --certificate-store-location options. This option currently supports only a single matching certificate in the result. If there are multiple matching certificates in the result, or no matching certificate in the result, the sign command will fail.</param>
/// <param name="CertificateFingerprint">Specifies the fingerprint of the certificate used to search a local certificate store for the certificate. Starting with .NET 9, this option can be used to specify the SHA-1, SHA-256, SHA-384, or SHA-512 fingerprint of the certificate. However, a NU3043 warning is raised when a SHA-1 certificate fingerprint is used because it is no longer considered secure. All the previous versions of the .NET SDK continue to accept only SHA-1 certificate fingerprint.</param>
/// <param name="CertificatePassword">Specifies the certificate password, if needed. If a certificate is password protected but no password is provided, the sign command will fail.</param>
/// <param name="HashAlgorithm">Hash algorithm to be used to sign the package. Defaults to SHA256. Possible values are SHA256, SHA384, and SHA512.</param>
/// <param name="Output">Specifies the directory where the signed package should be saved. If this option isn't specified, by default the original package is overwritten by the signed package.</param>
/// <param name="Overwrite">Indicate that the current signature should be overwritten. By default the command will fail if the package already has a signature.</param>
/// <param name="TimestampHashAlgorithm">Hash algorithm to be used by the RFC 3161 timestamp server. Defaults to SHA256.</param>
/// <param name="TimestampingServer">URL to an RFC 3161 timestamping server.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetNuGetSign(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<string> Packages,
    string CertificatePath = "",
    string CertificateStoreName = "",
    string CertificateStoreLocation = "",
    string CertificateSubjectName = "",
    string CertificateFingerprint = "",
    string CertificatePassword = "",
    NuGetCertificateAlgorithm? HashAlgorithm = default,
    string Output = "",
    bool? Overwrite = default,
    NuGetCertificateAlgorithm? TimestampHashAlgorithm = default,
    string TimestampingServer = "",
    DotNetVerbosity? Verbosity = default,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetNuGetSign(params string[] args)
        : this(args, [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetNuGetSign()
        : this([], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("nuget")
            .AddArgs("sign")
            .AddNotEmptyArgs(Packages.ToArray())
            .AddArgs(CertificatePath.ToArgs("--certificate-path", ""))
            .AddArgs(CertificateStoreName.ToArgs("--certificate-store-name", ""))
            .AddArgs(CertificateStoreLocation.ToArgs("--certificate-store-location", ""))
            .AddArgs(CertificateSubjectName.ToArgs("--certificate-subject-name", ""))
            .AddArgs(CertificateFingerprint.ToArgs("--certificate-fingerprint", ""))
            .AddArgs(CertificatePassword.ToArgs("--certificate-password", ""))
            .AddArgs(HashAlgorithm.ToArgs("--hash-algorithm", ""))
            .AddArgs(Output.ToArgs("--output", ""))
            .AddArgs(TimestampHashAlgorithm.ToArgs("--timestamp-hash-algorithm", ""))
            .AddArgs(TimestampingServer.ToArgs("--timestamper", ""))
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddBooleanArgs(
                ("--overwrite", Overwrite),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Signs the NuGet packages with a certificate.", ShortName, new [] {"nuget", "sign"}.Concat(Packages).ToArray());
}

/// <summary>
/// Shows the dependency graph for a particular package.
/// <p>
/// This command shows the dependency graph for a particular package for a given project or solution. Starting from the .NET 9 SDK, it's possible to pass a NuGet assets file in place of the project file, in order to use the command with projects that can't be restored with the .NET SDK. First, restore the project in Visual Studio, or msbuild.exe. By default the assets file is in the project's obj\ directory, but you can find the location with msbuild.exe path\to\project.proj -getProperty:ProjectAssetsFile. Finally, run dotnet nuget why path\to\project.assets.json SomePackage.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-why">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetNuGetWhy()
///     .WithProject(Path.Combine("MyLib", "MyLib.csproj"))
///     .WithPackage("MyLib.1.2.3.nupkg")
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Frameworks">The target frameworks for which dependency graphs are shown.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Project">The project or solution file to operate on. If a directory is specified, the command searches the directory for a project or solution file. If more than one project or solution is found, an error is thrown.</param>
/// <param name="Package">The package name to look up in the dependency graph.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetNuGetWhy(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<string> Frameworks,
    string Project = "",
    string Package = "",
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetNuGetWhy(params string[] args)
        : this(args, [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetNuGetWhy()
        : this([], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("nuget")
            .AddArgs("why")
            .AddNotEmptyArgs(Project.ToArg())
            .AddNotEmptyArgs(Package.ToArg())
            .AddArgs(Frameworks.ToArgs("--framework", ""))
            .AddBooleanArgs(
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Shows the dependency graph for a particular package.", ShortName, "nuget", "why", Project.ToArg(), Package.ToArg());
}

/// <summary>
/// Gets the NuGet configuration settings that will be applied.
/// <p>
/// This command gets the NuGet configuration settings that will be applied from the config section.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-config-get">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// string? repositoryPath = default;
/// new DotNetNuGetConfigGet()
///     .WithConfigKey("repositoryPath")
///     .Run(output =&gt; repositoryPath = output.Line).EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="ConfigKey"><c>ALL</c> is default value, gets all merged NuGet configuration settings from multiple NuGet configuration files that will be applied, when invoking NuGet command from the working directory path. Otherwise gets the effective value of the specified configuration settings of the config section.</param>
/// <param name="ShowPath">Indicate that the NuGet configuration file path will be shown beside the configuration settings.</param>
/// <param name="Directory">Specifies the directory to start from when listing configuration files. If not specified, the current directory is used.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetNuGetConfigGet(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    string ConfigKey = "ALL",
    bool? ShowPath = default,
    string Directory = "",
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetNuGetConfigGet(params string[] args)
        : this(args, [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetNuGetConfigGet()
        : this([], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("nuget")
            .AddArgs("config")
            .AddArgs("get")
            .AddNotEmptyArgs(ConfigKey.ToArg())
            .AddArgs(Directory.ToArgs("--working-directory", ""))
            .AddBooleanArgs(
                ("--show-path", ShowPath),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Gets the NuGet configuration settings that will be applied.", ShortName, "nuget", "config", "get", ConfigKey.ToArg());
}

/// <summary>
/// Set the value of a specified NuGet configuration setting.
/// <p>
/// This command sets the values for NuGet configuration settings that will be applied from the config section.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-config-set">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetNuGetConfigSet()
///     .WithConfigFile(configFile)
///     .WithConfigKey("repositoryPath")
///     .WithConfigValue("MyValue")
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="ConfigKey">The key of the settings that are to be set.</param>
/// <param name="ConfigValue">The value of the settings that are to be set.</param>
/// <param name="ConfigFile">The NuGet configuration file (nuget.config) to use. If specified, only the settings from this file will be used. If not specified, the hierarchy of configuration files from the current directory will be used.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetNuGetConfigSet(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    string ConfigKey = "",
    string ConfigValue = "",
    string ConfigFile = "",
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetNuGetConfigSet(params string[] args)
        : this(args, [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetNuGetConfigSet()
        : this([], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("nuget")
            .AddArgs("config")
            .AddArgs("set")
            .AddNotEmptyArgs(ConfigKey.ToArg())
            .AddNotEmptyArgs(ConfigValue.ToArg())
            .AddArgs(ConfigFile.ToArgs("--configfile", ""))
            .AddBooleanArgs(
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Set the value of a specified NuGet configuration setting.", ShortName, "nuget", "config", "set", ConfigKey.ToArg(), ConfigValue.ToArg());
}

/// <summary>
/// Removes the key-value pair from a specified NuGet configuration setting.
/// <p>
/// This command unsets the values for NuGet configuration settings that will be applied from the config section.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-config-unset">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetNuGetConfigUnset()
///     .WithConfigKey("repositoryPath")
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="ConfigKey">The key of the settings that are to be removed.</param>
/// <param name="ConfigFile">The NuGet configuration file (nuget.config) to use. If specified, only the settings from this file will be used. If not specified, the hierarchy of configuration files from the current directory will be used.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetNuGetConfigUnset(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    string ConfigKey = "",
    string ConfigFile = "",
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetNuGetConfigUnset(params string[] args)
        : this(args, [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetNuGetConfigUnset()
        : this([], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("nuget")
            .AddArgs("config")
            .AddArgs("unset")
            .AddNotEmptyArgs(ConfigKey.ToArg())
            .AddArgs(ConfigFile.ToArgs("--configfile", ""))
            .AddBooleanArgs(
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Removes the key-value pair from a specified NuGet configuration setting.", ShortName, "nuget", "config", "unset", ConfigKey.ToArg());
}

/// <summary>
/// Lists nuget configuration files currently being applied to a directory.
/// <p>
/// This command lists the paths to all NuGet configuration files that will be applied when invoking NuGet commands in a specific directory.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-config-paths">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// var configPaths = new List&lt;string&gt;();
/// new DotNetNuGetConfigPaths()
///     .Run(output =&gt; configPaths.Add(output.Line)).EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Directory">Specifies the directory to start from when listing configuration files. If not specified, the current directory is used.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetNuGetConfigPaths(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    string Directory = "",
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetNuGetConfigPaths(params string[] args)
        : this(args, [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetNuGetConfigPaths()
        : this([], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("nuget")
            .AddArgs("config")
            .AddArgs("paths")
            .AddArgs(Directory.ToArgs("--working-directory", ""))
            .AddBooleanArgs(
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Lists nuget configuration files currently being applied to a directory.", ShortName, "nuget", "config", "paths");
}

/// <summary>
/// Packs the code into a NuGet package.
/// <p>
/// The dotnet pack command builds the project and creates NuGet packages. The result of this command is a NuGet package (that is, a .nupkg file).
/// </p>
/// <p>
/// NuGet dependencies of the packed project are added to the .nuspec file, so they're properly resolved when the package is installed. If the packed project has references to other projects, the other projects aren't included in the package. Currently, you must have a package per project if you have project-to-project dependencies.
/// </p>
/// <p>
/// By default, dotnet pack builds the project first. If you wish to avoid this behavior, pass the --no-build option. This option is often useful in Continuous Integration (CI) build scenarios where you know the code was previously built.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-pack">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// // Creates a NuGet package of version 1.2.3 for the project
/// new DotNetPack()
///     .WithWorkingDirectory("MyLib")
///     .WithOutput(path)
///     .AddProps(("version", "1.2.3"))
///     .Build().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Props">MSBuild options for setting properties.</param>
/// <param name="Sources">The URI of the NuGet package source to use during this operation.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Project">The project or solution to pack. It's either a path to a csproj, vbproj, or fsproj file, or to a solution file or directory. If not specified, the command searches the current directory for a project or solution file.</param>
/// <param name="ArtifactsPath">All build output files from the executed command will go in subfolders under the specified path, separated by project.</param>
/// <param name="Configuration">Defines the build configuration. The default for most projects is Debug, but you can override the build configuration settings in your project.</param>
/// <param name="Force">Forces all dependencies to be resolved even if the last restore was successful. Specifying this flag is the same as deleting the project.assets.json file.</param>
/// <param name="IncludeSource">Includes the debug symbols NuGet packages in addition to the regular NuGet packages in the output directory. The sources files are included in the src folder within the symbols package.</param>
/// <param name="IncludeSymbols">Includes the debug symbols NuGet packages in addition to the regular NuGet packages in the output directory.</param>
/// <param name="NoBuild">Doesn't build the project before packing. It also implicitly sets the --no-restore flag.</param>
/// <param name="NoDependencies">When restoring a project with project-to-project (P2P) references, restores the root project and not the references.</param>
/// <param name="NoIncremental">Marks the build as unsafe for incremental build. This flag turns off incremental compilation and forces a clean rebuild of the project's dependency graph.</param>
/// <param name="NoRestore">Doesn't execute an implicit restore when running the command.</param>
/// <param name="NoLogo">Doesn't display the startup banner or the copyright message.</param>
/// <param name="NoSelfContained">Publishes the application as a framework dependent application. A compatible .NET runtime must be installed on the target machine to run the application. Available since .NET 6 SDK.</param>
/// <param name="Output">Directory in which to place the built binaries. If not specified, the default path is ./bin/&lt;configuration&gt;/&lt;framework&gt;/. For projects with multiple target frameworks (via the TargetFrameworks property), you also need to define --framework when you specify this option.</param>
/// <param name="OS">Specifies the target operating system (OS). This is a shorthand syntax for setting the Runtime Identifier (RID), where the provided value is combined with the default RID. For example, on a win-x64 machine, specifying --os linux sets the RID to linux-x64. If you use this option, don&apos;t use the -r|--runtime option. Available since .NET 6.</param>
/// <param name="Runtime">Specifies the target runtime to restore packages for. For a list of Runtime Identifiers (RIDs), see the RID catalog. -r short option available since .NET Core 3.0 SDK.</param>
/// <param name="SelfContained">Publishes the .NET runtime with the application so the runtime doesn't need to be installed on the target machine. The default is true if a runtime identifier is specified. Available since .NET 6.</param>
/// <param name="TerminalLogger">Specifies whether the terminal logger should be used for the build output.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="UseCurrentRuntime">Sets the RuntimeIdentifier to a platform portable RuntimeIdentifier based on the one of your machine. This happens implicitly with properties that require a RuntimeIdentifier, such as SelfContained, PublishAot, PublishSelfContained, PublishSingleFile, and PublishReadyToRun. If the property is set to false, that implicit resolution will no longer occur.</param>
/// <param name="VersionSuffix">Sets the value of the $(VersionSuffix) property to use when building the project. This only works if the $(Version) property isn't set. Then, $(Version) is set to the $(VersionPrefix) combined with the $(VersionSuffix), separated by a dash.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetPack(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<(string name, string value)> Props,
    IEnumerable<string> Sources,
    string Project = "",
    string ArtifactsPath = "",
    string Configuration = "",
    bool? Force = default,
    string IncludeSource = "",
    string IncludeSymbols = "",
    bool? NoBuild = default,
    bool? NoDependencies = default,
    bool? NoIncremental = default,
    bool? NoRestore = default,
    bool? NoLogo = default,
    bool? NoSelfContained = default,
    string Output = "",
    string OS = "",
    string Runtime = "",
    bool? SelfContained = default,
    DotNetTerminalLogger? TerminalLogger = default,
    DotNetVerbosity? Verbosity = default,
    bool? UseCurrentRuntime = default,
    string VersionSuffix = "",
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetPack(params string[] args)
        : this(args, [], [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetPack()
        : this([], [], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("pack")
            .AddNotEmptyArgs(Project.ToArg())
            .AddMSBuildLoggers(host, Verbosity)
            .AddArgs(Sources.ToArgs("--source", ""))
            .AddArgs(ArtifactsPath.ToArgs("--artifacts-path", ""))
            .AddArgs(Configuration.ToArgs("--configuration", ""))
            .AddArgs(IncludeSource.ToArgs("--include-source", ""))
            .AddArgs(IncludeSymbols.ToArgs("--include-symbols", ""))
            .AddArgs(Output.ToArgs("--output", ""))
            .AddArgs(OS.ToArgs("--os", ""))
            .AddArgs(Runtime.ToArgs("--runtime", ""))
            .AddArgs(TerminalLogger.ToArgs("--tl", ""))
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddArgs(VersionSuffix.ToArgs("--version-suffix", ""))
            .AddBooleanArgs(
                ("--force", Force),
                ("--no-build", NoBuild),
                ("--no-dependencies", NoDependencies),
                ("--no-incremental", NoIncremental),
                ("--no-restore", NoRestore),
                ("--nologo", NoLogo),
                ("--no-self-contained", NoSelfContained),
                ("--self-contained", SelfContained),
                ("--use-current-runtime", UseCurrentRuntime),
                ("--diagnostics", Diagnostics)
            )
            .AddProps("--property", Props.ToArray())
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Packs the code into a NuGet package.", ShortName, "pack", Project.ToArg());
}

/// <summary>
/// Searches for a NuGet package.
/// <p>
/// This command searches for a NuGet package.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-package-search">.NET CLI command</a><br/>
/// <example>
///<code>
/// using System.Text;
/// using System.Text.Json;
/// using HostApi;
/// 
/// var packagesJson = new StringBuilder();
/// new DotNetPackageSearch()
///     .WithSearchTerm("Pure.DI")
///     .WithFormat(DotNetPackageSearchResultFormat.Json)
///     .Run(output =&gt; packagesJson.AppendLine(output.Line)).EnsureSuccess();
/// 
/// var result = JsonSerializer.Deserialize&lt;Result&gt;(
///     packagesJson.ToString(),
///     new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
/// 
/// result.ShouldNotBeNull();
/// result.SearchResult.SelectMany(i =&gt; i.Packages).Count(i =&gt; i.Id == "Pure.DI").ShouldBe(1);
/// 
/// record Result(int Version, IReadOnlyCollection&lt;Source&gt; SearchResult);
/// 
/// record Source(string SourceName, IReadOnlyCollection&lt;Package&gt; Packages);
/// 
/// record Package(
///     string Id,
///     string LatestVersion,
///     int TotalDownloads,
///     string Owners);
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Sources">The URI of the NuGet package source to use during this operation.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="SearchTerm">Specifies the search term to filter results. Use this argument to search for packages matching the provided query.</param>
/// <param name="ConfigFile">The NuGet configuration file (nuget.config) to use. If specified, only the settings from this file will be used. If not specified, the hierarchy of configuration files from the current directory will be used.</param>
/// <param name="ExactMatch">This option narrows the search to only include packages whose IDs exactly match the specified search term, effectively filtering out any partial matches. It provides a concise list of all available versions for the identified package. Causes --take and --skip options to be ignored. Utilize this option to display all available versions of a specified package.</param>
/// <param name="Format">The format options are table and json. The default is table.</param>
/// <param name="Prerelease">Allow prerelease packages to be shown.</param>
/// <param name="Skip">The number of results to skip, for pagination. The default value is <c>0</c>.</param>
/// <param name="Take">The number of results to return. The default value is <c>20</c>.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetPackageSearch(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<string> Sources,
    string SearchTerm = "",
    string ConfigFile = "",
    bool? ExactMatch = default,
    DotNetPackageSearchResultFormat? Format = default,
    bool? Prerelease = default,
    int? Skip = default,
    int? Take = default,
    DotNetVerbosity? Verbosity = default,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetPackageSearch(params string[] args)
        : this(args, [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetPackageSearch()
        : this([], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("package")
            .AddArgs("search")
            .AddNotEmptyArgs(SearchTerm.ToArg())
            .AddArgs(Sources.ToArgs("--source", ""))
            .AddArgs(ConfigFile.ToArgs("--configfile", ""))
            .AddArgs(Format.ToArgs("--format", ""))
            .AddArgs(Skip.ToArgs("--skip", ""))
            .AddArgs(Take.ToArgs("--take", ""))
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddBooleanArgs(
                ("--exact-match", ExactMatch),
                ("--prerelease", Prerelease),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Searches for a NuGet package.", ShortName, "package", "search", SearchTerm.ToArg());
}

/// <summary>
/// Publishes the application and its dependencies to a folder for deployment to a hosting system.
/// <p>
/// This command compiles the application, reads through its dependencies specified in the project file, and publishes the resulting set of files to a directory.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-publish">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetPublish()
///     .WithWorkingDirectory("MyLib")
///     .WithFramework("net8.0")
///     .WithOutput("bin")
///     .Build().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Props">MSBuild options for setting properties.</param>
/// <param name="Sources">The URI of the NuGet package source to use during this operation.</param>
/// <param name="Manifests">Specifies one or several target manifests to use to trim the set of packages published with the app. The manifest file is part of the output of the dotnet store command.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Project">The project or solution file to operate on. If not specified, the command searches the current directory for one. If more than one solution or project is found, an error is thrown.</param>
/// <param name="Arch">Specifies the target architecture. This is a shorthand syntax for setting the Runtime Identifier (RID), where the provided value is combined with the default RID. For example, on a win-x64 machine, specifying --arch x86 sets the RID to win-x86. If you use this option, don&apos;t use the -r|--runtime option. Available since .NET 6 Preview 7.</param>
/// <param name="ArtifactsPath">All build output files from the executed command will go in subfolders under the specified path, separated by project.</param>
/// <param name="Configuration">Defines the build configuration. The default for most projects is Debug, but you can override the build configuration settings in your project.</param>
/// <param name="DisableBuildServers">Forces the command to ignore any persistent build servers. This option provides a consistent way to disable all use of build caching, which forces a build from scratch. A build that doesn't rely on caches is useful when the caches might be corrupted or incorrect for some reason. Available since .NET 7 SDK.</param>
/// <param name="Framework">Builds and runs the app using the specified framework. The framework must be specified in the project file.</param>
/// <param name="Force">Forces all dependencies to be resolved even if the last restore was successful. Specifying this flag is the same as deleting the project.assets.json file.</param>
/// <param name="NoBuild">Doesn't build the project before publishing. It also implicitly sets the --no-restore flag.</param>
/// <param name="NoDependencies">When restoring a project with project-to-project (P2P) references, restores the root project and not the references.</param>
/// <param name="NoLogo">Doesn't display the startup banner or the copyright message.</param>
/// <param name="NoRestore">Doesn't execute an implicit restore when running the command.</param>
/// <param name="Output">Specifies the path for the output directory. If not specified, it defaults to [project_file_folder]/bin/[configuration]/[framework]/publish/ for a framework-dependent executable and cross-platform binaries. It defaults to [project_file_folder]/bin/[configuration]/[framework]/[runtime]/publish/ for a self-contained executable. In a web project, if the output folder is in the project folder, successive dotnet publish commands result in nested output folders. For example, if the project folder is myproject, and the publish output folder is myproject/publish, and you run dotnet publish twice, the second run puts content files such as .config and .json files in myproject/publish/publish. To avoid nesting publish folders, specify a publish folder that isn't directly under the project folder, or exclude the publish folder from the project.</param>
/// <param name="OS">Specifies the target operating system (OS). This is a shorthand syntax for setting the Runtime Identifier (RID), where the provided value is combined with the default RID. For example, on a win-x64 machine, specifying --os linux sets the RID to linux-x64. If you use this option, don&apos;t use the -r|--runtime option. Available since .NET 6.</param>
/// <param name="SelfContained">Publishes the .NET runtime with your application so the runtime doesn't need to be installed on the target machine. Default is true if a runtime identifier is specified and the project is an executable project (not a library project).</param>
/// <param name="NoSelfContained">Publishes the application as a framework dependent application. A compatible .NET runtime must be installed on the target machine to run the application. Available since .NET 6 SDK.</param>
/// <param name="Runtime">Publishes the application for a given runtime. For a list of Runtime Identifiers (RIDs), see the RID catalog.</param>
/// <param name="TerminalLogger">Specifies whether the terminal logger should be used for the build output.</param>
/// <param name="UseCurrentRuntime">Sets the RuntimeIdentifier to a platform portable RuntimeIdentifier based on the one of your machine. This happens implicitly with properties that require a RuntimeIdentifier, such as SelfContained, PublishAot, PublishSelfContained, PublishSingleFile, and PublishReadyToRun. If the property is set to false, that implicit resolution will no longer occur.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="VersionSuffix">Sets the value of the $(VersionSuffix) property to use when building the project. This only works if the $(Version) property isn't set. Then, $(Version) is set to the $(VersionPrefix) combined with the $(VersionSuffix), separated by a dash.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetPublish(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<(string name, string value)> Props,
    IEnumerable<string> Sources,
    IEnumerable<string> Manifests,
    string Project = "",
    string Arch = "",
    string ArtifactsPath = "",
    string Configuration = "",
    bool? DisableBuildServers = default,
    string Framework = "",
    bool? Force = default,
    bool? NoBuild = default,
    bool? NoDependencies = default,
    bool? NoLogo = default,
    bool? NoRestore = default,
    string Output = "",
    string OS = "",
    bool? SelfContained = default,
    bool? NoSelfContained = default,
    string Runtime = "",
    DotNetTerminalLogger? TerminalLogger = default,
    bool? UseCurrentRuntime = default,
    DotNetVerbosity? Verbosity = default,
    string VersionSuffix = "",
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetPublish(params string[] args)
        : this(args, [], [], [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetPublish()
        : this([], [], [], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("publish")
            .AddNotEmptyArgs(Project.ToArg())
            .AddMSBuildLoggers(host, Verbosity)
            .AddArgs(Sources.ToArgs("--source", ""))
            .AddArgs(Manifests.ToArgs("--manifest", ""))
            .AddArgs(Arch.ToArgs("--arch", ""))
            .AddArgs(ArtifactsPath.ToArgs("--artifacts-path", ""))
            .AddArgs(Configuration.ToArgs("--configuration", ""))
            .AddArgs(Framework.ToArgs("--framework", ""))
            .AddArgs(Output.ToArgs("--output", ""))
            .AddArgs(OS.ToArgs("--os", ""))
            .AddArgs(Runtime.ToArgs("--runtime", ""))
            .AddArgs(TerminalLogger.ToArgs("--tl", ""))
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddArgs(VersionSuffix.ToArgs("--version-suffix", ""))
            .AddBooleanArgs(
                ("--disable-build-servers", DisableBuildServers),
                ("--force", Force),
                ("--no-build", NoBuild),
                ("--no-dependencies", NoDependencies),
                ("--nologo", NoLogo),
                ("--no-restore", NoRestore),
                ("--self-contained", SelfContained),
                ("--no-self-contained", NoSelfContained),
                ("--use-current-runtime", UseCurrentRuntime),
                ("--diagnostics", Diagnostics)
            )
            .AddProps("--property", Props.ToArray())
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Publishes the application and its dependencies to a folder for deployment to a hosting system.", ShortName, "publish", Project.ToArg());
}

/// <summary>
/// Restores the dependencies and tools of a project.
/// <p>
/// A .NET project typically references external libraries in NuGet packages that provide additional functionality. These external dependencies are referenced in the project file (.csproj or .vbproj). When you run the dotnet restore command, the .NET CLI uses NuGet to look for these dependencies and download them if necessary. It also ensures that all the dependencies required by the project are compatible with each other and that there are no conflicts between them. Once the command is completed, all the dependencies required by the project are available in a local cache and can be used by the .NET CLI to build and run the application.
/// </p>
/// <p>
/// Sometimes, it might be inconvenient to run the implicit NuGet restore with these commands. For example, some automated systems, such as build systems, need to call dotnet restore explicitly to control when the restore occurs so that they can control network usage. To prevent the implicit NuGet restore, you can use the --no-restore flag with any of these commands.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-restore">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetRestore()
///     .WithProject(Path.Combine("MyLib", "MyLib.csproj"))
///     .Build().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Props">MSBuild options for setting properties.</param>
/// <param name="Sources">The URI of the NuGet package source to use during this operation.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Project">The project or solution file to operate on. If not specified, the command searches the current directory for one. If more than one solution or project is found, an error is thrown.</param>
/// <param name="Arch">Specifies the target architecture. This is a shorthand syntax for setting the Runtime Identifier (RID), where the provided value is combined with the default RID. For example, on a win-x64 machine, specifying --arch x86 sets the RID to win-x86. If you use this option, don&apos;t use the -r|--runtime option. Available since .NET 6 Preview 7.</param>
/// <param name="ConfigFile">The NuGet configuration file (nuget.config) to use. If specified, only the settings from this file will be used. If not specified, the hierarchy of configuration files from the current directory will be used.</param>
/// <param name="DisableBuildServers">Forces the command to ignore any persistent build servers. This option provides a consistent way to disable all use of build caching, which forces a build from scratch. A build that doesn't rely on caches is useful when the caches might be corrupted or incorrect for some reason. Available since .NET 7 SDK.</param>
/// <param name="DisableParallel">Disables restoring multiple projects in parallel.</param>
/// <param name="Force">Forces all dependencies to be resolved even if the last restore was successful. Specifying this flag is the same as deleting the project.assets.json file.</param>
/// <param name="ForceEvaluate">Forces restore to reevaluate all dependencies even if a lock file already exists.</param>
/// <param name="IgnoreFailedSources">Only warn about failed sources if there are packages meeting the version requirement.</param>
/// <param name="LockFilePath">Output location where project lock file is written. By default, this is PROJECT_ROOT\packages.lock.json.</param>
/// <param name="LockedMode">Don't allow updating project lock file.</param>
/// <param name="NoCache">Specifies to not cache HTTP requests.</param>
/// <param name="NoDependencies">When restoring a project with project-to-project (P2P) references, restores the root project and not the references.</param>
/// <param name="Packages">Specifies the directory for restored packages.</param>
/// <param name="Runtime">Specifies the target runtime to restore packages for. For a list of Runtime Identifiers (RIDs), see the RID catalog. -r short option available since .NET Core 3.0 SDK.</param>
/// <param name="TerminalLogger">Specifies whether the terminal logger should be used for the build output.</param>
/// <param name="UseCurrentRuntime">Sets the RuntimeIdentifier to a platform portable RuntimeIdentifier based on the one of your machine. This happens implicitly with properties that require a RuntimeIdentifier, such as SelfContained, PublishAot, PublishSelfContained, PublishSingleFile, and PublishReadyToRun. If the property is set to false, that implicit resolution will no longer occur.</param>
/// <param name="UseLockFile">Enables project lock file to be generated and used with restore.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetRestore(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<(string name, string value)> Props,
    IEnumerable<string> Sources,
    string Project = "",
    string Arch = "",
    string ConfigFile = "",
    bool? DisableBuildServers = default,
    bool? DisableParallel = default,
    bool? Force = default,
    bool? ForceEvaluate = default,
    bool? IgnoreFailedSources = default,
    string LockFilePath = "",
    bool? LockedMode = default,
    bool? NoCache = default,
    bool? NoDependencies = default,
    string Packages = "",
    string Runtime = "",
    DotNetTerminalLogger? TerminalLogger = default,
    bool? UseCurrentRuntime = default,
    bool? UseLockFile = default,
    DotNetVerbosity? Verbosity = default,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetRestore(params string[] args)
        : this(args, [], [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetRestore()
        : this([], [], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("restore")
            .AddNotEmptyArgs(Project.ToArg())
            .AddMSBuildLoggers(host, Verbosity)
            .AddArgs(Sources.ToArgs("--source", ""))
            .AddArgs(Arch.ToArgs("--arch", ""))
            .AddArgs(ConfigFile.ToArgs("--configfile", ""))
            .AddArgs(LockFilePath.ToArgs("--lock-file-path", ""))
            .AddArgs(Packages.ToArgs("--packages", ""))
            .AddArgs(Runtime.ToArgs("--runtime", ""))
            .AddArgs(TerminalLogger.ToArgs("--tl", ""))
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddBooleanArgs(
                ("--disable-build-servers", DisableBuildServers),
                ("--disable-parallel", DisableParallel),
                ("--force", Force),
                ("--force-evaluate", ForceEvaluate),
                ("--ignore-failed-sources", IgnoreFailedSources),
                ("--locked-mode", LockedMode),
                ("--no-cache", NoCache),
                ("--no-dependencies", NoDependencies),
                ("--use-current-runtime", UseCurrentRuntime),
                ("--use-lock-file", UseLockFile),
                ("--diagnostics", Diagnostics)
            )
            .AddProps("--property", Props.ToArray())
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Restores the dependencies and tools of a project.", ShortName, "restore", Project.ToArg());
}

/// <summary>
/// Runs source code without any explicit compile or launch commands.
/// <p>
/// This command provides a convenient option to run your application from the source code with one command. It's useful for fast iterative development from the command line. The command depends on the dotnet build command to build the code. Any requirements for the build apply to dotnet run as well.
/// </p>
/// <p>
/// To run the application, the dotnet run command resolves the dependencies of the application that are outside of the shared runtime from the NuGet cache. Because it uses cached dependencies, it's not recommended to use dotnet run to run applications in production. Instead, create a deployment using the dotnet publish command and deploy the published output.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-run">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// var stdOut = new List&lt;string&gt;();
/// new DotNetRun()
///     .WithProject(Path.Combine("MyApp", "MyApp.csproj"))
///     .Build(message =&gt; stdOut.Add(message.Text))
///     .EnsureSuccess();
/// 
/// // Checks stdOut
/// stdOut.ShouldBe(new[] {"Hello, World!"});
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Props">MSBuild options for setting properties.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Arch">Specifies the target architecture. This is a shorthand syntax for setting the Runtime Identifier (RID), where the provided value is combined with the default RID. For example, on a win-x64 machine, specifying --arch x86 sets the RID to win-x86. If you use this option, don&apos;t use the -r|--runtime option. Available since .NET 6 Preview 7.</param>
/// <param name="Configuration">Defines the build configuration. The default for most projects is Debug, but you can override the build configuration settings in your project.</param>
/// <param name="Framework">Builds and runs the app using the specified framework. The framework must be specified in the project file.</param>
/// <param name="Force">Forces all dependencies to be resolved even if the last restore was successful. Specifying this flag is the same as deleting the project.assets.json file.</param>
/// <param name="LaunchProfile">The name of the launch profile (if any) to use when launching the application. Launch profiles are defined in the launchSettings.json file and are typically called <c>Development</c>, <c>Staging</c>, and <c>Production</c>.</param>
/// <param name="NoBuild">Doesn't build the project before running this command.</param>
/// <param name="NoDependencies">When restoring a project with project-to-project (P2P) references, restores the root project and not the references.</param>
/// <param name="NoLaunchProfile">Doesn't try to use launchSettings.json to configure the application.</param>
/// <param name="NoRestore">Doesn't execute an implicit restore when running the command.</param>
/// <param name="OS">Specifies the target operating system (OS). This is a shorthand syntax for setting the Runtime Identifier (RID), where the provided value is combined with the default RID. For example, on a win-x64 machine, specifying --os linux sets the RID to linux-x64. If you use this option, don&apos;t use the -r|--runtime option. Available since .NET 6.</param>
/// <param name="Project">Specifies the path of the project file to run (folder name or full path). If not specified, it defaults to the current directory.</param>
/// <param name="Runtime">Specifies the target runtime to restore packages for. For a list of Runtime Identifiers (RIDs), see the RID catalog. -r short option available since .NET Core 3.0 SDK.</param>
/// <param name="TerminalLogger">Specifies whether the terminal logger should be used for the build output.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetRun(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<(string name, string value)> Props,
    string Arch = "",
    string Configuration = "",
    string Framework = "",
    bool? Force = default,
    string LaunchProfile = "",
    bool? NoBuild = default,
    bool? NoDependencies = default,
    bool? NoLaunchProfile = default,
    bool? NoRestore = default,
    string OS = "",
    string Project = "",
    string Runtime = "",
    DotNetTerminalLogger? TerminalLogger = default,
    DotNetVerbosity? Verbosity = default,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetRun(params string[] args)
        : this(args, [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetRun()
        : this([], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("run")
            .AddArgs(Arch.ToArgs("--arch", ""))
            .AddArgs(Configuration.ToArgs("--configuration", ""))
            .AddArgs(Framework.ToArgs("--framework", ""))
            .AddArgs(LaunchProfile.ToArgs("--launch-profile", ""))
            .AddArgs(OS.ToArgs("--os", ""))
            .AddArgs(Project.ToArgs("--project", ""))
            .AddArgs(Runtime.ToArgs("--runtime", ""))
            .AddArgs(TerminalLogger.ToArgs("--tl", ""))
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddBooleanArgs(
                ("--force", Force),
                ("--no-build", NoBuild),
                ("--no-dependencies", NoDependencies),
                ("--no-launch-profile", NoLaunchProfile),
                ("--no-restore", NoRestore),
                ("--diagnostics", Diagnostics)
            )
            .AddProps("--property", Props.ToArray())
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Runs source code without any explicit compile or launch commands.", ShortName, "run");
}

/// <summary>
/// Lists the latest available version of the .NET SDK and .NET Runtime, for each feature band.
/// <p>
/// This command makes it easier to track when new versions of the SDK and Runtimes are available.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-sdk-check">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// var sdks = new List&lt;Sdk&gt;();
/// new DotNetSdkCheck()
///     .Run(output =&gt;
///     {
///         if (output.Line.StartsWith("Microsoft."))
///         {
///             var data = output.Line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
///             if (data.Length &gt;= 2)
///             {
///                 sdks.Add(new Sdk(data[0], NuGetVersion.Parse(data[1])));
///             }
///         }
///     })
///     .EnsureSuccess();
/// 
/// sdks.Count.ShouldBeGreaterThan(0);
/// 
/// record Sdk(string Name, NuGetVersion Version);
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetSdkCheck(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetSdkCheck(params string[] args)
        : this(args, [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetSdkCheck()
        : this([], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("sdk")
            .AddArgs("check")
            .AddBooleanArgs(
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Lists the latest available version of the .NET SDK and .NET Runtime, for each feature band.", ShortName, "sdk", "check");
}

/// <summary>
/// Lists all projects in a solution file.
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-sln#list">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// var lines = new List&lt;string&gt;();
/// new DotNetSlnList()
///     .WithSolution("NySolution.sln")
///     .Run(output =&gt; lines.Add(output.Line))
///     .EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Solution">The solution file to use. If this argument is omitted, the command searches the current directory for one. If it finds no solution file or multiple solution files, the command fails.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetSlnList(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    string Solution = "",
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetSlnList(params string[] args)
        : this(args, [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetSlnList()
        : this([], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("sln")
            .AddNotEmptyArgs(Solution.ToArg())
            .AddArgs("list")
            .AddBooleanArgs(
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Lists all projects in a solution file.", ShortName, "sln", Solution.ToArg(), "list");
}

/// <summary>
/// Adds one or more projects to the solution file.
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-sln#add">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetNew()
///     .WithTemplateName("sln")
///     .WithName("NySolution")
///     .WithForce(true)
///     .Run().EnsureSuccess();
/// 
/// new DotNetSlnAdd()
///     .WithSolution("NySolution.sln")
///     .AddProjects(
///         Path.Combine("MyLib", "MyLib.csproj"),
///         Path.Combine("MyTests", "MyTests.csproj"))
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Projects">The path to the project or projects to add to the solution. Unix/Linux shell globbing pattern expansions are processed correctly by the dotnet sln command.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Solution">The solution file to use. If this argument is omitted, the command searches the current directory for one. If it finds no solution file or multiple solution files, the command fails.</param>
/// <param name="InRoot">Places the projects in the root of the solution, rather than creating a solution folder. Can't be used with -s|--solution-folder.</param>
/// <param name="SolutionFolder">The destination solution folder path to add the projects to. Can't be used with --in-root.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetSlnAdd(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<string> Projects,
    string Solution = "",
    bool? InRoot = default,
    string SolutionFolder = "",
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetSlnAdd(params string[] args)
        : this(args, [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetSlnAdd()
        : this([], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("sln")
            .AddNotEmptyArgs(Solution.ToArg())
            .AddArgs("add")
            .AddNotEmptyArgs(Projects.ToArray())
            .AddArgs(SolutionFolder.ToArgs("--solution-folder", ""))
            .AddBooleanArgs(
                ("--in-root", InRoot),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Adds one or more projects to the solution file.", ShortName, new [] {"sln", Solution.ToArg(), "add"}.Concat(Projects).ToArray());
}

/// <summary>
/// Removes a project or multiple projects from the solution file.
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-sln#remove">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetSlnRemove()
///     .WithSolution("NySolution.sln")
///     .AddProjects(
///         Path.Combine("MyLib", "MyLib.csproj"))
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Projects">The path to the project or projects to add to the solution. Unix/Linux shell globbing pattern expansions are processed correctly by the dotnet sln command.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Solution">The solution file to use. If this argument is omitted, the command searches the current directory for one. If it finds no solution file or multiple solution files, the command fails.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetSlnRemove(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<string> Projects,
    string Solution = "",
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetSlnRemove(params string[] args)
        : this(args, [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetSlnRemove()
        : this([], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("sln")
            .AddNotEmptyArgs(Solution.ToArg())
            .AddArgs("remove")
            .AddNotEmptyArgs(Projects.ToArray())
            .AddBooleanArgs(
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Removes a project or multiple projects from the solution file.", ShortName, new [] {"sln", Solution.ToArg(), "remove"}.Concat(Projects).ToArray());
}

/// <summary>
/// Stores the specified assemblies in the runtime package store.
/// <p>
/// This command stores the specified assemblies in the runtime package store. By default, assemblies are optimized for the target runtime and framework.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-store">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetStore()
///     .AddManifests(Path.Combine("MyLib", "MyLib.csproj"))
///     .WithFramework("net8.0")
///     .WithRuntime("win-x64")
///     .Build();
/// 
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Manifests">The package store manifest file is an XML file that contains the list of packages to store. The format of the manifest file is compatible with the SDK-style project format. So, a project file that references the desired packages can be used with the -m|--manifest option to store assemblies in the runtime package store.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Framework">Specifies the .NET SDK version. This option enables you to select a specific framework version beyond the framework specified by the -f|--framework option.</param>
/// <param name="Runtime">Specifies the target runtime to restore packages for. For a list of Runtime Identifiers (RIDs), see the RID catalog. -r short option available since .NET Core 3.0 SDK.</param>
/// <param name="FrameworkVersion">Specifies the .NET SDK version. This option enables you to select a specific framework version beyond the framework specified by the -f|--framework option.</param>
/// <param name="Output">Specifies the path to the runtime package store. If not specified, it defaults to the store subdirectory of the user profile .NET installation directory.</param>
/// <param name="SkipOptimization">Skips the optimization phase.</param>
/// <param name="SkipSymbols">Skips symbol generation. Currently, you can only generate symbols on Windows and Linux.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="Directory">The working directory used by the command. If not specified, it uses the obj subdirectory of the current directory.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetStore(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<string> Manifests,
    string Framework = "",
    string Runtime = "",
    string FrameworkVersion = "",
    string Output = "",
    bool? SkipOptimization = default,
    bool? SkipSymbols = default,
    DotNetVerbosity? Verbosity = default,
    string Directory = "",
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetStore(params string[] args)
        : this(args, [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetStore()
        : this([], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("store")
            .AddArgs(Manifests.ToArgs("--manifest", ""))
            .AddArgs(Framework.ToArgs("--framework", ""))
            .AddArgs(Runtime.ToArgs("--runtime", ""))
            .AddArgs(FrameworkVersion.ToArgs("--framework-version", ""))
            .AddArgs(Output.ToArgs("--output", ""))
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddArgs(Directory.ToArgs("--working-dir", ""))
            .AddBooleanArgs(
                ("--skip-optimization", SkipOptimization),
                ("--skip-symbols", SkipSymbols),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Stores the specified assemblies in the runtime package store.", ShortName, "store");
}

/// <summary>
/// .NET test driver used to execute unit tests.
/// <p>
/// This command is used to execute unit tests in a given solution. The dotnet test command builds the solution and runs a test host application for each test project in the solution using VSTest. The test host executes tests in the given project using a test framework, for example: MSTest, NUnit, or xUnit, and reports the success or failure of each test. If all tests are successful, the test runner returns 0 as an exit code; otherwise if any test fails, it returns 1.
/// </p>
/// <p>
/// For multi-targeted projects, tests are run for each targeted framework. The test host and the unit test framework are packaged as NuGet packages and are restored as ordinary dependencies for the project. Starting with the .NET 9 SDK, these tests are run in parallel by default. To disable parallel execution, set the TestTfmsInParallel MSBuild property to false.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-test">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// // Runs tests
/// var result = new DotNetTest()
///     .WithWorkingDirectory("MyTests")
///     .Build().EnsureSuccess();
/// 
/// // The "result" variable provides details about build and tests
/// result.ExitCode.ShouldBe(0, result.ToString());
/// result.Summary.Tests.ShouldBe(1, result.ToString());
/// result.Tests.Count(test =&gt; test.State == TestState.Finished).ShouldBe(1, result.ToString());
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Props">MSBuild options for setting properties.</param>
/// <param name="Environments">Sets the value of an environment variable. Creates the variable if it does not exist, overrides if it does exist. Use of this option will force the tests to be run in an isolated process.</param>
/// <param name="Loggers">Specifies a logger for test results and optionally switches for the logger.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Project">Path to the test project.<br/>Or path to the solution.<br/>Or path to a directory that contains a project or a solution.<br/>Or path to a test project .dll file.<br/>Or path to a test project .exe file.<br/>If not specified, the effect is the same as using the DIRECTORY argument to specify the current directory.</param>
/// <param name="TestAdapterPath">Path to a directory to be searched for additional test adapters. Only .dll files with suffix .TestAdapter.dll are inspected. If not specified, the directory of the test .dll is searched.</param>
/// <param name="Arch">Specifies the target architecture. This is a shorthand syntax for setting the Runtime Identifier (RID), where the provided value is combined with the default RID. For example, on a win-x64 machine, specifying --arch x86 sets the RID to win-x86. If you use this option, don&apos;t use the -r|--runtime option. Available since .NET 6 Preview 7.</param>
/// <param name="ArtifactsPath">All build output files from the executed command will go in subfolders under the specified path, separated by project.</param>
/// <param name="Blame">Runs the tests in blame mode. This option is helpful in isolating problematic tests that cause the test host to crash. When a crash is detected, it creates a sequence file in TestResults/&lt;Guid&gt;/&lt;Guid&gt;_Sequence.xml that captures the order of tests that were run before the crash.&lt;br/&gt;This option does not create a memory dump and is not helpful when the test is hanging.</param>
/// <param name="BlameCrash">Runs the tests in blame mode and collects a crash dump when the test host exits unexpectedly. This option depends on the version of .NET used, the type of error, and the operating system.<br/>For exceptions in managed code, a dump will be automatically collected on .NET 5.0 and later versions. It will generate a dump for testhost or any child process that also ran on .NET 5.0 and crashed. Crashes in native code will not generate a dump. This option works on Windows, macOS, and Linux.</param>
/// <param name="BlameCrashDumpType">The type of crash dump to be collected. Supported dump types are full (default), and mini.</param>
/// <param name="BlameCrashCollectAlways">Collects a crash dump on expected as well as unexpected test host exit.</param>
/// <param name="BlameHang">Run the tests in blame mode and collects a hang dump when a test exceeds the given timeout.</param>
/// <param name="BlameHangDumpType">The type of crash dump to be collected. It should be full, mini, or none. When none is specified, test host is terminated on timeout, but no dump is collected.</param>
/// <param name="BlameHangTimeout">Per-test timeout, after which a hang dump is triggered and the test host process and all of its child processes are dumped and terminated.</param>
/// <param name="Configuration">Defines the build configuration. The default for most projects is Debug, but you can override the build configuration settings in your project.</param>
/// <param name="Collect">Enables data collector for the test run.</param>
/// <param name="Diag">Enables diagnostic mode for the test platform and writes diagnostic messages to the specified file and to files next to it. The process that is logging the messages determines which files are created, such as *.host_&amp;lt;date&amp;gt;.txt for test host log, and *.datacollector_&amp;lt;date&amp;gt;.txt for data collector log.</param>
/// <param name="Framework">Builds and runs the app using the specified framework. The framework must be specified in the project file.</param>
/// <param name="Filter">Filters tests in the current project using the given expression. Only tests that match the filter expression are run.</param>
/// <param name="NoBuild">Doesn't build the project before running this command.</param>
/// <param name="NoLogo">Doesn't display the startup banner or the copyright message.</param>
/// <param name="NoRestore">Doesn't execute an implicit restore when running the command.</param>
/// <param name="Output">Directory in which to place the built binaries. If not specified, the default path is ./bin/&lt;configuration&gt;/&lt;framework&gt;/. For projects with multiple target frameworks (via the TargetFrameworks property), you also need to define --framework when you specify this option.</param>
/// <param name="OS">Specifies the target operating system (OS). This is a shorthand syntax for setting the Runtime Identifier (RID), where the provided value is combined with the default RID. For example, on a win-x64 machine, specifying --os linux sets the RID to linux-x64. If you use this option, don&apos;t use the -r|--runtime option. Available since .NET 6.</param>
/// <param name="ResultsDirectory">The directory where the test results are going to be placed. If the specified directory doesn't exist, it's created. The default is TestResults in the directory that contains the project file.</param>
/// <param name="Runtime">The target runtime to test for.</param>
/// <param name="Settings">The .runsettings file to use for running the tests. The TargetPlatform element (x86|x64) has no effect for dotnet test. To run tests that target x86, install the x86 version of .NET Core. The bitness of the dotnet.exe that is on the path is what will be used for running tests.</param>
/// <param name="ListTests">List the discovered tests instead of running the tests.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetTest(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<(string name, string value)> Props,
    IEnumerable<(string name, string value)> Environments,
    IEnumerable<string> Loggers,
    string Project = "",
    string TestAdapterPath = "",
    string Arch = "",
    string ArtifactsPath = "",
    bool? Blame = default,
    bool? BlameCrash = default,
    DotNetBlameDumpType? BlameCrashDumpType = default,
    bool? BlameCrashCollectAlways = default,
    bool? BlameHang = default,
    DotNetBlameDumpType? BlameHangDumpType = default,
    TimeSpan? BlameHangTimeout = default,
    string Configuration = "",
    string Collect = "",
    string Diag = "",
    string Framework = "",
    string Filter = "",
    bool? NoBuild = default,
    bool? NoLogo = default,
    bool? NoRestore = default,
    string Output = "",
    string OS = "",
    string ResultsDirectory = "",
    string Runtime = "",
    string Settings = "",
    bool? ListTests = default,
    DotNetVerbosity? Verbosity = default,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetTest(params string[] args)
        : this(args, [], [], [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetTest()
        : this([], [], [], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        var components = host.GetService<HostComponents>();
        var virtualContext = components.VirtualContext;
        var settings = components.DotNetSettings;
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("test")
            .AddNotEmptyArgs(Project.ToArg())
            .AddMSBuildLoggers(host, Verbosity)
            .AddTestLoggers(host, Loggers)
            .AddArgs(Environments.ToArgs("--environment", "="))
            .AddArgs(Loggers.ToArgs("--logger ", ""))
            .AddArgs("--test-adapter-path", $"{string.Join(";", new[] {TestAdapterPath, virtualContext.Resolve(settings.DotNetVSTestLoggerDirectory)}.Where(i => !string.IsNullOrWhiteSpace(i)))}")
            .AddArgs(Arch.ToArgs("--arch", ""))
            .AddArgs(ArtifactsPath.ToArgs("--artifacts-path", ""))
            .AddArgs(BlameCrashDumpType.ToArgs("--blame-crash-dump-type", ""))
            .AddArgs(BlameHangDumpType.ToArgs("--blame-hang-dump-type", ""))
            .AddArgs(BlameHangTimeout.ToArgs("--blame-hang-timeout", ""))
            .AddArgs(Configuration.ToArgs("--configuration", ""))
            .AddArgs(Collect.ToArgs("--collect", ""))
            .AddArgs(Diag.ToArgs("--diag", ""))
            .AddArgs(Framework.ToArgs("--framework", ""))
            .AddArgs(Filter.ToArgs("--filter", ""))
            .AddArgs(Output.ToArgs("--output", ""))
            .AddArgs(OS.ToArgs("--os", ""))
            .AddArgs(ResultsDirectory.ToArgs("--results-directory", ""))
            .AddArgs(Runtime.ToArgs("--runtime", ""))
            .AddArgs(Settings.ToArgs("--settings", ""))
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddBooleanArgs(
                ("--blame", Blame),
                ("--blame-crash", BlameCrash),
                ("--blame-crash-collect-always", BlameCrashCollectAlways),
                ("--blame-hang", BlameHang),
                ("--no-build", NoBuild),
                ("--nologo", NoLogo),
                ("--no-restore", NoRestore),
                ("--list-tests", ListTests),
                ("--diagnostics", Diagnostics)
            )
            .AddProps("--property", Props.ToArray())
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName(".NET test driver used to execute unit tests.", ShortName, "test", Project.ToArg());
}

/// <summary>
/// Installs the specified .NET tool on your machine.
/// <p>
/// This command provides a way for you to install .NET tools on your machine.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-tool-install">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetToolInstall()
///     .WithLocal(true)
///     .WithPackage("dotnet-csi")
///     .WithVersion("1.1.2")
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Sources">Adds an additional NuGet package source to use during installation. Feeds are accessed in parallel, not sequentially in some order of precedence. If the same package and version is in multiple feeds, the fastest feed wins.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Package">Name/ID of the NuGet package that contains the .NET tool to install.</param>
/// <param name="AllowDowngrade">Allow package downgrade when installing or updating a .NET tool package. Suppresses the warning, "The requested version x.x.x is lower than existing version x.x.x."</param>
/// <param name="Arch">Specifies the target architecture. This is a shorthand syntax for setting the Runtime Identifier (RID), where the provided value is combined with the default RID. For example, on a win-x64 machine, specifying --arch x86 sets the RID to win-x86. If you use this option, don&apos;t use the -r|--runtime option. Available since .NET 6 Preview 7.</param>
/// <param name="ConfigFile">The NuGet configuration file (nuget.config) to use. If specified, only the settings from this file will be used. If not specified, the hierarchy of configuration files from the current directory will be used.</param>
/// <param name="CreateManifestIfNeeded">Applies to local tools. Available starting with .NET 8 SDK. To find a manifest, the search algorithm searches up the directory tree for dotnet-tools.json or a .config folder that contains a dotnet-tools.json file. If a tool-manifest can't be found and the --create-manifest-if-needed option is set to false, the CannotFindAManifestFile error occurs. If a tool-manifest can't be found and the --create-manifest-if-needed option is set to true, the tool creates a manifest automatically.</param>
/// <param name="DisableParallel">Disables restoring multiple projects in parallel.</param>
/// <param name="Framework">Specifies the target framework to install the tool for. By default, the .NET SDK tries to choose the most appropriate target framework.</param>
/// <param name="Global">Specifies that the installation is user wide. Can't be combined with the --tool-path option. Omitting both --global and --tool-path specifies a local tool installation.</param>
/// <param name="IgnoreFailedSources">Treat package source failures as warnings.</param>
/// <param name="Local">Update the tool and the local tool manifest. Can't be combined with the --global option or the --tool-path option.</param>
/// <param name="NoCache">Specifies to not cache HTTP requests.</param>
/// <param name="Prerelease">Include prerelease packages.</param>
/// <param name="ToolManifest">Path to the manifest file.</param>
/// <param name="ToolPath">Specifies the location where to install the Global Tool. PATH can be absolute or relative. If PATH doesn't exist, the command tries to create it. Omitting both --global and --tool-path specifies a local tool installation.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="Version">The version of the tool to install. By default, the latest stable package version is installed. Use this option to install preview or older versions of the tool.<br/>Starting with .NET 8.0, --version Major.Minor.Patch refers to a specific major/minor/patch version, including unlisted versions. To get the latest version of a certain major/minor version instead, use --version Major.Minor.*.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetToolInstall(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<string> Sources,
    string Package = "",
    bool? AllowDowngrade = default,
    string Arch = "",
    string ConfigFile = "",
    bool? CreateManifestIfNeeded = default,
    bool? DisableParallel = default,
    string Framework = "",
    bool? Global = default,
    bool? IgnoreFailedSources = default,
    bool? Local = default,
    bool? NoCache = default,
    bool? Prerelease = default,
    string ToolManifest = "",
    string ToolPath = "",
    DotNetVerbosity? Verbosity = default,
    string Version = "",
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetToolInstall(params string[] args)
        : this(args, [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetToolInstall()
        : this([], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("tool")
            .AddArgs("install")
            .AddNotEmptyArgs(Package.ToArg())
            .AddArgs(Sources.ToArgs("--add-source", ""))
            .AddArgs(Arch.ToArgs("--arch", ""))
            .AddArgs(ConfigFile.ToArgs("--configfile", ""))
            .AddArgs(Framework.ToArgs("--framework", ""))
            .AddArgs(ToolManifest.ToArgs("--tool-manifest", ""))
            .AddArgs(ToolPath.ToArgs("--tool-path", ""))
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddArgs(Version.ToArgs("--version", ""))
            .AddBooleanArgs(
                ("--allow-downgrade", AllowDowngrade),
                ("--create-manifest-if-needed", CreateManifestIfNeeded),
                ("--disable-parallel", DisableParallel),
                ("--global", Global),
                ("--ignore-failed-sources", IgnoreFailedSources),
                ("--local", Local),
                ("--no-cache", NoCache),
                ("--prerelease", Prerelease),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Installs the specified .NET tool on your machine.", ShortName, "tool", "install", Package.ToArg());
}

/// <summary>
/// Lists all .NET tools of the specified type currently installed on your machine.
/// <p>
/// This command provides a way for you to list .NET global, tool-path, or local tools installed on your machine. The command lists the package name, version installed, and the tool command.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-tool-list">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetToolList()
///     .WithLocal(true)
///     .Run().EnsureSuccess();
/// 
/// new DotNetToolList()
///     .WithGlobal(true)
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Package">Lists the tool that has the supplied package ID if the tool is installed. Can be used in conjunction with options. Provides a way to check if a specific tool was installed. If no tool with the specified package ID is found, the command lists headings with no detail rows.</param>
/// <param name="Global">Lists user-wide global tools. Can't be combined with the --tool-path option. Omitting both --global and --tool-path lists local tools.</param>
/// <param name="Local">Lists local tools for the current directory. Can't be combined with the --global or --tool-path options. Omitting both --global and --tool-path lists local tools even if --local is not specified.</param>
/// <param name="ToolPath">Specifies a custom location where to find global tools. PATH can be absolute or relative. Can't be combined with the --global option. Omitting both --global and --tool-path lists local tools.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetToolList(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    string Package = "",
    bool? Global = default,
    bool? Local = default,
    string ToolPath = "",
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetToolList(params string[] args)
        : this(args, [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetToolList()
        : this([], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("tool")
            .AddArgs("list")
            .AddNotEmptyArgs(Package.ToArg())
            .AddArgs(ToolPath.ToArgs("--tool-path", ""))
            .AddBooleanArgs(
                ("--global", Global),
                ("--local", Local),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Lists all .NET tools of the specified type currently installed on your machine.", ShortName, "tool", "list", Package.ToArg());
}

/// <summary>
/// Installs the .NET local tools that are in scope for the current directory.
/// <p>
/// This command finds the tool manifest file that is in scope for the current directory and installs the tools that are listed in it. For information about manifest files, see Install a local tool and Invoke a local tool.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-tool-restore">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// // Creates a local tool manifest
/// new DotNetNew()
///     .WithTemplateName("tool-manifest")
///     .Run().EnsureSuccess();
/// 
/// new DotNetToolRestore()
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Sources">Adds an additional NuGet package source to use during installation. Feeds are accessed in parallel, not sequentially in some order of precedence. If the same package and version is in multiple feeds, the fastest feed wins.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="ConfigFile">The NuGet configuration file (nuget.config) to use. If specified, only the settings from this file will be used. If not specified, the hierarchy of configuration files from the current directory will be used.</param>
/// <param name="ToolManifest">Path to the manifest file.</param>
/// <param name="DisableParallel">Disables restoring multiple projects in parallel.</param>
/// <param name="IgnoreFailedSources">Treat package source failures as warnings.</param>
/// <param name="NoCache">Specifies to not cache HTTP requests.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetToolRestore(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<string> Sources,
    string ConfigFile = "",
    string ToolManifest = "",
    bool? DisableParallel = default,
    bool? IgnoreFailedSources = default,
    bool? NoCache = default,
    DotNetVerbosity? Verbosity = default,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetToolRestore(params string[] args)
        : this(args, [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetToolRestore()
        : this([], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("tool")
            .AddArgs("restore")
            .AddArgs(Sources.ToArgs("--add-source", ""))
            .AddArgs(ConfigFile.ToArgs("--configfile", ""))
            .AddArgs(ToolManifest.ToArgs("--tool-manifest", ""))
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddBooleanArgs(
                ("--disable-parallel", DisableParallel),
                ("--ignore-failed-sources", IgnoreFailedSources),
                ("--no-cache", NoCache),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Installs the .NET local tools that are in scope for the current directory.", ShortName, "tool", "restore");
}

/// <summary>
/// Invokes a local tool.
/// <p>
/// This command searches tool manifest files that are in scope for the current directory. When it finds a reference to the specified tool, it runs the tool.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-tool-run">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// var script = Path.GetTempFileName();
/// File.WriteAllText(script, "Console.WriteLine($\"Hello, {Args[0]}!\");");
/// 
/// var stdOut = new List&lt;string&gt;();
/// new DotNetToolRun()
///     .WithCommandName("dotnet-csi")
///     .AddArgs(script)
///     .AddArgs("World")
///     .Run(output =&gt; stdOut.Add(output.Line))
///     .EnsureSuccess();
/// 
/// // Checks stdOut
/// stdOut.Contains("Hello, World!").ShouldBeTrue();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="CommandName">The command name of the tool to run.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetToolRun(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    string CommandName = "",
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetToolRun(params string[] args)
        : this(args, [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetToolRun()
        : this([], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("tool")
            .AddArgs("run")
            .AddNotEmptyArgs(CommandName.ToArg())
            .AddBooleanArgs(
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Invokes a local tool.", ShortName, "tool", "run", CommandName.ToArg());
}

/// <summary>
/// Searches all .NET tools that are published to NuGet.
/// <p>
/// This command  provides a way for you to search NuGet for tools that can be used as .NET global, tool-path, or local tools. The command searches the tool names and metadata such as titles, descriptions, and tags.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-tool-search">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetToolSearch()
///     .WithPackage("dotnet-csi")
///     .WithDetail(true)
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Package">Name/description of the NuGet package.</param>
/// <param name="Detail">Shows detailed results from the query.</param>
/// <param name="Prerelease">Includes pre-release packages.</param>
/// <param name="Skip">Specifies the number of query results to skip. Used for pagination.</param>
/// <param name="Take">Specifies the number of query results to show. Used for pagination.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetToolSearch(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    string Package = "",
    bool? Detail = default,
    bool? Prerelease = default,
    int? Skip = default,
    int? Take = default,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetToolSearch(params string[] args)
        : this(args, [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetToolSearch()
        : this([], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("tool")
            .AddArgs("search")
            .AddNotEmptyArgs(Package.ToArg())
            .AddArgs(Skip.ToArgs("--skip", ""))
            .AddArgs(Take.ToArgs("--take", ""))
            .AddBooleanArgs(
                ("--detail", Detail),
                ("--prerelease", Prerelease),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Searches all .NET tools that are published to NuGet.", ShortName, "tool", "search", Package.ToArg());
}

/// <summary>
/// Uninstalls the specified .NET tool from your machine.
/// <p>
/// This command provides a way for you to uninstall .NET tools from your machine.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-tool-uninstall">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetToolUninstall()
///     .WithPackage("dotnet-csi")
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Package">Name/ID of the NuGet package that contains the .NET tool to uninstall. You can find the package name using the dotnet tool list command.</param>
/// <param name="Global">Specifies that the tool to be removed is from a user-wide installation. Can't be combined with the --tool-path option. Omitting both --global and --tool-path specifies that the tool to be removed is a local tool.</param>
/// <param name="ToolPath">Specifies the location where to uninstall the tool. PATH can be absolute or relative. Can't be combined with the --global option. Omitting both --global and --tool-path specifies that the tool to be removed is a local tool.</param>
/// <param name="ToolManifest">Specifies the manifest file that the tool is to be removed from. PATH can be absolute or relative. Can't be combined with the --global option.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetToolUninstall(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    string Package = "",
    bool? Global = default,
    string ToolPath = "",
    string ToolManifest = "",
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetToolUninstall(params string[] args)
        : this(args, [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetToolUninstall()
        : this([], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("tool")
            .AddArgs("uninstall")
            .AddNotEmptyArgs(Package.ToArg())
            .AddArgs(ToolPath.ToArgs("--tool-path", ""))
            .AddArgs(ToolManifest.ToArgs("--tool-manifest", ""))
            .AddBooleanArgs(
                ("--global", Global),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Uninstalls the specified .NET tool from your machine.", ShortName, "tool", "uninstall", Package.ToArg());
}

/// <summary>
/// Updates the specified .NET tool on your machine.
/// <p>
/// This command provides a way for you to update .NET tools on your machine to the latest stable version of the package. The command uninstalls and reinstalls a tool, effectively updating it. 
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-tool-update">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetToolUpdate()
///     .WithLocal(true)
///     .WithPackage("dotnet-csi")
///     .WithVersion("1.1.2")
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Sources">Adds an additional NuGet package source to use during installation. Feeds are accessed in parallel, not sequentially in some order of precedence. If the same package and version is in multiple feeds, the fastest feed wins.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Package">Name/ID of the NuGet package that contains the .NET global tool to update. You can find the package name using the dotnet tool list command.</param>
/// <param name="AllowDowngrade">Allow package downgrade when installing or updating a .NET tool package. Suppresses the warning, "The requested version x.x.x is lower than existing version x.x.x."</param>
/// <param name="ConfigFile">The NuGet configuration file (nuget.config) to use. If specified, only the settings from this file will be used. If not specified, the hierarchy of configuration files from the current directory will be used.</param>
/// <param name="DisableParallel">Disables restoring multiple projects in parallel.</param>
/// <param name="Framework">Specifies the target framework to update the tool for.</param>
/// <param name="Global">Specifies that the update is for a user-wide tool. Can't be combined with the --tool-path option. Omitting both --global and --tool-path specifies that the tool to be updated is a local tool.</param>
/// <param name="IgnoreFailedSources">Treat package source failures as warnings.</param>
/// <param name="Local">Update the tool and the local tool manifest. Can't be combined with the --global option or the --tool-path option.</param>
/// <param name="NoCache">Specifies to not cache HTTP requests.</param>
/// <param name="Prerelease">Include prerelease packages.</param>
/// <param name="ToolManifest">Path to the manifest file.</param>
/// <param name="ToolPath">Specifies the location where the global tool is installed. PATH can be absolute or relative. Can't be combined with the --global option. Omitting both --global and --tool-path specifies that the tool to be updated is a local tool.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="Version">The version range of the tool package to update to. This cannot be used to downgrade versions, you must uninstall newer versions first.<br/>Starting in .NET 8.0, --version Major.Minor.Patch refers to a specific major.minor.patch version, including unlisted versions. To get the latest version of a certain major.minor version instead, use --version Major.Minor.*.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetToolUpdate(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<string> Sources,
    string Package = "",
    bool? AllowDowngrade = default,
    string ConfigFile = "",
    bool? DisableParallel = default,
    string Framework = "",
    bool? Global = default,
    bool? IgnoreFailedSources = default,
    bool? Local = default,
    bool? NoCache = default,
    bool? Prerelease = default,
    string ToolManifest = "",
    string ToolPath = "",
    DotNetVerbosity? Verbosity = default,
    string Version = "",
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetToolUpdate(params string[] args)
        : this(args, [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetToolUpdate()
        : this([], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("tool")
            .AddArgs("update")
            .AddNotEmptyArgs(Package.ToArg())
            .AddArgs(Sources.ToArgs("--add-source", ""))
            .AddArgs(ConfigFile.ToArgs("--configfile", ""))
            .AddArgs(Framework.ToArgs("--framework", ""))
            .AddArgs(ToolManifest.ToArgs("--tool-manifest", ""))
            .AddArgs(ToolPath.ToArgs("--tool-path", ""))
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddArgs(Version.ToArgs("--version", ""))
            .AddBooleanArgs(
                ("--allow-downgrade", AllowDowngrade),
                ("--disable-parallel", DisableParallel),
                ("--global", Global),
                ("--ignore-failed-sources", IgnoreFailedSources),
                ("--local", Local),
                ("--no-cache", NoCache),
                ("--prerelease", Prerelease),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Updates the specified .NET tool on your machine.", ShortName, "tool", "update", Package.ToArg());
}

/// <summary>
/// Provides information about the available workload commands and installed workloads.
/// <p>
/// This command provides commands for working with .NET workloads.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-workload">.NET CLI command</a><br/>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Version">Displays the current workload set version.</param>
/// <param name="Info">Prints out detailed information about installed workloads, including their installation source, manifest version, manifest path, and install type.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetWorkload(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    bool? Version = default,
    bool? Info = default,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetWorkload(params string[] args)
        : this(args, [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetWorkload()
        : this([], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("workload")
            .AddBooleanArgs(
                ("--version", Version),
                ("--info", Info),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Provides information about the available workload commands and installed workloads.", ShortName, "workload");
}

/// <summary>
/// Enables or disables workload-set update mode.
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-workload-config">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetWorkloadConfig()
///     .WithUpdateMode(DotNetWorkloadUpdateMode.WorkloadSet)
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="UpdateMode">Controls whether updates look for workload set versions or individual manifest versions. To display the current mode, specify this option without an argument.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetWorkloadConfig(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    DotNetWorkloadUpdateMode? UpdateMode = default,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetWorkloadConfig(params string[] args)
        : this(args, [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetWorkloadConfig()
        : this([], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("workload")
            .AddArgs("config")
            .AddArgs(UpdateMode.ToArgs("--update-mode", ""))
            .AddBooleanArgs(
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Enables or disables workload-set update mode.", ShortName, "workload", "config");
}

/// <summary>
/// Installs optional workloads.
/// <p>
/// This command installs one or more optional workloads. Optional workloads can be installed on top of the .NET SDK to provide support for various application types, such as .NET MAUI and Blazor WebAssembly AOT.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-workload-install">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetWorkloadInstall()
///     .AddWorkloads("aspire")
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Workloads">The workload ID or multiple IDs to install. Use <c>dotnet workload search</c> to learn what workloads are available.</param>
/// <param name="Sources">The URI of the NuGet package source to use during this operation.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="ConfigFile">The NuGet configuration file (nuget.config) to use. If specified, only the settings from this file will be used. If not specified, the hierarchy of configuration files from the current directory will be used.</param>
/// <param name="DisableParallel">Disables restoring multiple projects in parallel.</param>
/// <param name="IgnoreFailedSources">Treat package source failures as warnings.</param>
/// <param name="IncludePreviews">Allows prerelease workload manifests.</param>
/// <param name="NoCache">Specifies to not cache HTTP requests.</param>
/// <param name="SkipManifestUpdate">Skip updating the workload manifests. The workload manifests define what assets and versions need to be installed for each workload.</param>
/// <param name="TempDir">Specify the temporary directory used to download and extract NuGet packages (must be secure).</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="Version">The workload set version to install or update to. If you specify the workload-set version in global.json, you can't use the --version option to specify the workload-set version. To make it possible to use the --version option in that case, run the command outside of the path containing the global.json file. Available since 8.0.400 SDK.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetWorkloadInstall(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<string> Workloads,
    IEnumerable<string> Sources,
    string ConfigFile = "",
    bool? DisableParallel = default,
    bool? IgnoreFailedSources = default,
    bool? IncludePreviews = default,
    bool? NoCache = default,
    bool? SkipManifestUpdate = default,
    string TempDir = "",
    DotNetVerbosity? Verbosity = default,
    bool? Version = default,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetWorkloadInstall(params string[] args)
        : this(args, [], [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetWorkloadInstall()
        : this([], [], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("workload")
            .AddArgs("install")
            .AddNotEmptyArgs(Workloads.ToArray())
            .AddArgs(Sources.ToArgs("--source", ""))
            .AddArgs(ConfigFile.ToArgs("--configfile", ""))
            .AddArgs(TempDir.ToArgs("--temp-dir", ""))
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddBooleanArgs(
                ("--disable-parallel", DisableParallel),
                ("--ignore-failed-sources", IgnoreFailedSources),
                ("--include-previews", IncludePreviews),
                ("--no-cache", NoCache),
                ("--skip-manifest-update", SkipManifestUpdate),
                ("--version", Version),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Installs optional workloads.", ShortName, new [] {"workload", "install"}.Concat(Workloads).ToArray());
}

/// <summary>
/// Lists installed workloads.
/// <p>
/// This command lists all installed workloads.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-workload-list">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetWorkloadList()
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetWorkloadList(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    DotNetVerbosity? Verbosity = default,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetWorkloadList(params string[] args)
        : this(args, [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetWorkloadList()
        : this([], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("workload")
            .AddArgs("list")
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddBooleanArgs(
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Lists installed workloads.", ShortName, "workload", "list");
}

/// <summary>
/// Repairs workloads installations.
/// <p>
/// This command reinstalls all installed workloads. Workloads are made up of multiple workload packs and it's possible to get into a state where some installed successfully but others didn't.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-workload-repair">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetWorkloadRepair()
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Sources">The URI of the NuGet package source to use during this operation.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="ConfigFile">The NuGet configuration file (nuget.config) to use. If specified, only the settings from this file will be used. If not specified, the hierarchy of configuration files from the current directory will be used.</param>
/// <param name="DisableParallel">Disables restoring multiple projects in parallel.</param>
/// <param name="IgnoreFailedSources">Treat package source failures as warnings.</param>
/// <param name="IncludePreviews">Allows prerelease workload manifests.</param>
/// <param name="NoCache">Specifies to not cache HTTP requests.</param>
/// <param name="TempDir">Specify the temporary directory used to download and extract NuGet packages (must be secure).</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetWorkloadRepair(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<string> Sources,
    string ConfigFile = "",
    bool? DisableParallel = default,
    bool? IgnoreFailedSources = default,
    bool? IncludePreviews = default,
    bool? NoCache = default,
    string TempDir = "",
    DotNetVerbosity? Verbosity = default,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetWorkloadRepair(params string[] args)
        : this(args, [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetWorkloadRepair()
        : this([], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("workload")
            .AddArgs("repair")
            .AddArgs(Sources.ToArgs("--source", ""))
            .AddArgs(ConfigFile.ToArgs("--configfile", ""))
            .AddArgs(TempDir.ToArgs("--temp-dir", ""))
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddBooleanArgs(
                ("--disable-parallel", DisableParallel),
                ("--ignore-failed-sources", IgnoreFailedSources),
                ("--include-previews", IncludePreviews),
                ("--no-cache", NoCache),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Repairs workloads installations.", ShortName, "workload", "repair");
}

/// <summary>
/// Installs workloads needed for a project or a solution.
/// <p>
/// This command analyzes a project or solution to determine which workloads it needs, then installs any workloads that are missing.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-workload-restore">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetWorkloadRestore()
///     .WithProject(Path.Combine("MyLib", "MyLib.csproj"))
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Sources">The URI of the NuGet package source to use during this operation.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Project">The project or solution file to install workloads for. If a file is not specified, the command searches the current directory for one.</param>
/// <param name="ConfigFile">The NuGet configuration file (nuget.config) to use. If specified, only the settings from this file will be used. If not specified, the hierarchy of configuration files from the current directory will be used.</param>
/// <param name="DisableParallel">Disables restoring multiple projects in parallel.</param>
/// <param name="IgnoreFailedSources">Treat package source failures as warnings.</param>
/// <param name="IncludePreviews">Allows prerelease workload manifests.</param>
/// <param name="NoCache">Specifies to not cache HTTP requests.</param>
/// <param name="SkipManifestUpdate">Skip updating the workload manifests. The workload manifests define what assets and versions need to be installed for each workload.</param>
/// <param name="TempDir">Specify the temporary directory used to download and extract NuGet packages (must be secure).</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="Version">The workload set version to install or update to. If you specify the workload-set version in global.json, you can't use the --version option to specify the workload-set version. To make it possible to use the --version option in that case, run the command outside of the path containing the global.json file. Available since 8.0.400 SDK.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetWorkloadRestore(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<string> Sources,
    string Project = "",
    string ConfigFile = "",
    bool? DisableParallel = default,
    bool? IgnoreFailedSources = default,
    bool? IncludePreviews = default,
    bool? NoCache = default,
    bool? SkipManifestUpdate = default,
    string TempDir = "",
    DotNetVerbosity? Verbosity = default,
    bool? Version = default,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetWorkloadRestore(params string[] args)
        : this(args, [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetWorkloadRestore()
        : this([], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("workload")
            .AddArgs("restore")
            .AddNotEmptyArgs(Project.ToArg())
            .AddArgs(Sources.ToArgs("--source", ""))
            .AddArgs(ConfigFile.ToArgs("--configfile", ""))
            .AddArgs(TempDir.ToArgs("--temp-dir", ""))
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddBooleanArgs(
                ("--disable-parallel", DisableParallel),
                ("--ignore-failed-sources", IgnoreFailedSources),
                ("--include-previews", IncludePreviews),
                ("--no-cache", NoCache),
                ("--skip-manifest-update", SkipManifestUpdate),
                ("--version", Version),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Installs workloads needed for a project or a solution.", ShortName, "workload", "restore", Project.ToArg());
}

/// <summary>
/// Searches for optional workloads.
/// <p>
/// This command lists available workloads. You can filter the list by specifying all or part of the workload ID you're looking for.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-workload-search">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetWorkloadSearch()
///     .WithSearchString("maui")
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="SearchString">The workload ID to search for, or part of it. For example, if you specify <c>maui</c>, the command lists all of the workload IDs that have <c>maui</c> in their workload ID.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetWorkloadSearch(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    string SearchString = "",
    DotNetVerbosity? Verbosity = default,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetWorkloadSearch(params string[] args)
        : this(args, [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetWorkloadSearch()
        : this([], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("workload")
            .AddArgs("search")
            .AddNotEmptyArgs(SearchString.ToArg())
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddBooleanArgs(
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Searches for optional workloads.", ShortName, "workload", "search", SearchString.ToArg());
}

/// <summary>
/// Uninstalls a specified workload.
/// <p>
/// This command uninstalls one or more workloads.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-workload-uninstall">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetWorkloadUninstall()
///     .AddWorkloads("aspire")
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Workloads">The workload ID or multiple IDs to uninstall.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetWorkloadUninstall(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<string> Workloads,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetWorkloadUninstall(params string[] args)
        : this(args, [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetWorkloadUninstall()
        : this([], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("workload")
            .AddArgs("uninstall")
            .AddNotEmptyArgs(Workloads.ToArray())
            .AddBooleanArgs(
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Uninstalls a specified workload.", ShortName, new [] {"workload", "uninstall"}.Concat(Workloads).ToArray());
}

/// <summary>
/// Updates installed workloads.
/// <p>
/// This command updates all installed workloads to the newest available versions. It queries Nuget.org for updated workload manifests. It then updates local manifests, downloads new versions of the installed workloads, and removes all old versions of each workload. When the command is in workload-set update mode, workloads are updated according to the workload-set version, not the latest version of each individual workload.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-workload-update">.NET CLI command</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// new DotNetWorkloadUpdate()
///     .Run().EnsureSuccess();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Sources">The URI of the NuGet package source to use during this operation.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="AdvertisingManifestsOnly">Downloads advertising manifests but doesn't update any workloads.</param>
/// <param name="ConfigFile">The NuGet configuration file (nuget.config) to use. If specified, only the settings from this file will be used. If not specified, the hierarchy of configuration files from the current directory will be used.</param>
/// <param name="DisableParallel">Disables restoring multiple projects in parallel.</param>
/// <param name="FromPreviousSdk">Include workloads installed with previous SDK versions in the update.</param>
/// <param name="IgnoreFailedSources">Treat package source failures as warnings.</param>
/// <param name="IncludePreviews">Allows prerelease workload manifests.</param>
/// <param name="NoCache">Specifies to not cache HTTP requests.</param>
/// <param name="TempDir">Specify the temporary directory used to download and extract NuGet packages (must be secure).</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="Version">The workload set version to install or update to. If you specify the workload-set version in global.json, you can't use the --version option to specify the workload-set version. To make it possible to use the --version option in that case, run the command outside of the path containing the global.json file. Available since 8.0.400 SDK.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetWorkloadUpdate(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<string> Sources,
    bool? AdvertisingManifestsOnly = default,
    string ConfigFile = "",
    bool? DisableParallel = default,
    bool? FromPreviousSdk = default,
    bool? IgnoreFailedSources = default,
    bool? IncludePreviews = default,
    bool? NoCache = default,
    string TempDir = "",
    DotNetVerbosity? Verbosity = default,
    bool? Version = default,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetWorkloadUpdate(params string[] args)
        : this(args, [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetWorkloadUpdate()
        : this([], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("workload")
            .AddArgs("update")
            .AddArgs(Sources.ToArgs("--source", ""))
            .AddArgs(ConfigFile.ToArgs("--configfile", ""))
            .AddArgs(TempDir.ToArgs("--temp-dir", ""))
            .AddArgs(Verbosity.ToArgs("--verbosity", ""))
            .AddBooleanArgs(
                ("--advertising-manifests-only", AdvertisingManifestsOnly),
                ("--disable-parallel", DisableParallel),
                ("--from-previous-sdk", FromPreviousSdk),
                ("--ignore-failed-sources", IgnoreFailedSources),
                ("--include-previews", IncludePreviews),
                ("--no-cache", NoCache),
                ("--version", Version),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Updates installed workloads.", ShortName, "workload", "update");
}

/// <summary>
/// Runs a C# script.
/// <br/><a href="https://github.com/DevTeam/csharp-interactive">C# interactive</a><br/>
/// <example>
///<code>
/// using HostApi;
/// 
/// var script = Path.GetTempFileName();
/// File.WriteAllText(script, "Console.WriteLine($\"Hello, {Args[0]}!\");");
/// 
/// var stdOut = new List&lt;string&gt;();
/// new DotNetCsi()
///     .WithScript(script)
///     .AddArgs("World")
///     .Run(output =&gt; stdOut.Add(output.Line))
///     .EnsureSuccess();
/// 
/// // Checks stdOut
/// stdOut.Contains("Hello, World!").ShouldBeTrue();
///</code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Props">MSBuild options for setting properties.</param>
/// <param name="Sources">The URI of the NuGet package source to use during this operation.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Script">Script to execute.</param>
/// <param name="Version">Prints out the version.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetCsi(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<(string name, string value)> Props,
    IEnumerable<string> Sources,
    string Script = "",
    bool? Version = default,
    bool? Diagnostics = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetCsi(params string[] args)
        : this(args, [], [], [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetCsi()
        : this([], [], [], [])
    {
    }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs("csi")
            .AddNotEmptyArgs(Script.ToArg())
            .AddArgs(Sources.ToArgs("--source", ""))
            .AddBooleanArgs(
                ("--version", Version),
                ("--diagnostics", Diagnostics)
            )
            .AddProps("--property", Props.ToArray())
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Runs a C# script.", ShortName, "csi", Script.ToArg());
}
