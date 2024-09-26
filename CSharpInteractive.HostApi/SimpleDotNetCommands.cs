// ReSharper disable InconsistentNaming
namespace HostApi;

using Internal.DotNet;

/// <summary>
/// Runs source code without any explicit compile or launch commands.
/// <example>
/// <code>
/// var result = new DotNetNew("console", "-n", "MyApp", "--force")
///     .Build().EnsureSuccess();
/// 
/// 
/// new DotNetRun().WithWorkingDirectory("MyApp")
///     .Build().EnsureSuccess();
/// </code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Props">MSBuild options for setting properties.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Project">The project or solution file to operate on. If not specified, the command searches the current directory for one. If more than one solution or project is found, an error is thrown.</param>
/// <param name="Arch">Specifies the target architecture. This is a shorthand syntax for setting the Runtime Identifier (RID), where the provided value is combined with the default RID. For example, on a win-x64 machine, specifying --arch x86 sets the RID to win-x86. If you use this option, don&apos;t use the -r|--runtime option. Available since .NET 6 Preview 7.</param>
/// <param name="Configuration">Defines the build configuration. The default for most projects is Debug, but you can override the build configuration settings in your project.</param>
/// <param name="Framework">Builds and runs the app using the specified framework. The framework must be specified in the project file.</param>
/// <param name="Force">Forces all dependencies to be resolved even if the last restore was successful. Specifying this flag is the same as deleting the project.assets.json file.</param>
/// <param name="LaunchProfile">The name of the launch profile (if any) to use when launching the application. Launch profiles are defined in the launchSettings.json file and are typically called Development, Staging, and Production.</param>
/// <param name="NoBuild">Doesn't build the project before running. It also implicit sets the --no-restore flag.</param>
/// <param name="NoDependencies">When restoring a project with project-to-project (P2P) references, restores the root project and not the references.</param>
/// <param name="NoLaunchProfile">Doesn't try to use launchSettings.json to configure the application.</param>
/// <param name="NoRestore">Adds a package reference without performing a restore preview and compatibility check.</param>
/// <param name="OS">Specifies the target operating system (OS). This is a shorthand syntax for setting the Runtime Identifier (RID), where the provided value is combined with the default RID. For example, on a win-x64 machine, specifying --os linux sets the RID to linux-x64. If you use this option, don&apos;t use the -r|--runtime option. Available since .NET 6.</param>
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
    string Project = "",
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
    string Runtime = "",
    TerminalLogger? TerminalLogger = default,
    DotNetVerbosity? Verbosity = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
            .AddNotEmptyArgs(Project)
            .AddArgs(
                ("--arch", Arch),
                ("--configuration", Configuration),
                ("--framework", Framework),
                ("--launch-profile", LaunchProfile),
                ("--os", OS),
                ("--runtime", Runtime),
                ("--tl", TerminalLogger?.ToString()),
                ("--verbosity", Verbosity?.ToString())
            )
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
    public override string ToString() => "".GetShortName(ShortName, "run", Project);
}

/// <summary>
/// Runs a dotnet application.
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="AdditionalProbingPaths">Paths containing probing policy and assemblies to probe.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="PathToApplication">Specifies the path to an application .dll file to run the application. To run the application means to find and execute the entry point, which in the case of console apps is the Main method. For example, dotnet myapp. dll runs the myapp application.</param>
/// <param name="AdditionalDeps">Path to an additional .deps.json file. A deps.json file contains a list of dependencies, compilation dependencies, and version information used to address assembly conflicts.</param>
/// <param name="FxVersion">Version of the .NET runtime to use to run the application.</param>
/// <param name="RollForward">Controls how roll forward is applied to the app. The SETTING can be one of the following values. If not specified, <see cref="HostApi.RollForward.Minor"/> is the default.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNet(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<string> AdditionalProbingPaths,
    string PathToApplication = "",
    string AdditionalDeps = "",
    string FxVersion = "",
    RollForward? RollForward = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
            .AddNotEmptyArgs(PathToApplication)
            .AddArgs(AdditionalProbingPaths.Select(i => ("--additionalprobingpath", (string?)i)).ToArray())
            .AddArgs(
                ("--additional-deps", AdditionalDeps),
                ("--fx-version", FxVersion),
                ("--roll-forward", RollForward?.ToString())
            )
            .AddBooleanArgs(
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => "".GetShortName(ShortName, PathToApplication);
}

/// <summary>
/// Executes a dotnet application.
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
/// <param name="RollForward">Controls how roll forward is applied to the app. The SETTING can be one of the following values. If not specified, <see cref="HostApi.RollForward.Minor"/> is the default.</param>
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
    RollForward? RollForward = default,
    string RuntimeConfig = "",
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
            .AddNotEmptyArgs(PathToApplication)
            .AddArgs(AdditionalProbingPaths.Select(i => ("--additionalprobingpath", (string?)i)).ToArray())
            .AddArgs(
                ("--depsfile", DepsFile),
                ("--additional-deps", AdditionalDeps),
                ("--fx-version", FxVersion),
                ("--roll-forward", RollForward?.ToString()),
                ("--runtimeconfig", RuntimeConfig)
            )
            .AddBooleanArgs(
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => "".GetShortName(ShortName, "exec", PathToApplication);
}

/// <summary>
/// Adds or updates a package reference in a project file.
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Sources">The URI of the NuGet package source to use during the restore operation.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Project">The project or solution file to operate on. If not specified, the command searches the current directory for one. If more than one solution or project is found, an error is thrown.</param>
/// <param name="PackageName">The package reference to add.</param>
/// <param name="Framework">Adds a package reference only when targeting a specific framework.</param>
/// <param name="NoRestore">Adds a package reference without performing a restore preview and compatibility check.</param>
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
    string PackageName = "",
    string Framework = "",
    bool? NoRestore = default,
    string PackageDirectory = "",
    bool? Prerelease = default,
    string Version = "",
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
            .AddNotEmptyArgs(Project)
            .AddArgs("package")
            .AddNotEmptyArgs(PackageName)
            .AddArgs(Sources.Select(i => ("--source", (string?)i)).ToArray())
            .AddArgs(
                ("--framework", Framework),
                ("--package-directory", PackageDirectory),
                ("--version", Version)
            )
            .AddBooleanArgs(
                ("--no-restore", NoRestore),
                ("--prerelease", Prerelease),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => "".GetShortName(ShortName, "add", Project, "package", PackageName);
}

/// <summary>
/// Lists the package references for a project or solution.
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Frameworks">Displays only the packages applicable for the specified target framework.</param>
/// <param name="Sources">The NuGet sources to use when searching for newer packages. Requires the --outdated or --deprecated option.</param>
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
            .AddNotEmptyArgs(Project)
            .AddArgs("package")
            .AddArgs(Frameworks.Select(i => ("--framework", (string?)i)).ToArray())
            .AddArgs(Sources.Select(i => ("--source", (string?)i)).ToArray())
            .AddArgs(
                ("--config", Config),
                ("--verbosity", Verbosity?.ToString())
            )
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
    public override string ToString() => "".GetShortName(ShortName, "list", Project, "package");
}

/// <summary>
/// Removes package reference from a project file.
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Project">The project or solution file to operate on. If not specified, the command searches the current directory for one. If more than one solution or project is found, an error is thrown.</param>
/// <param name="PackageName">The package reference to add.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetRemovePackage(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    string Project = "",
    string PackageName = "",
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
            .AddNotEmptyArgs(Project)
            .AddArgs("package")
            .AddNotEmptyArgs(PackageName)
            .AddArgs(
            )
            .AddBooleanArgs(
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => "".GetShortName(ShortName, "remove", Project, "package", PackageName);
}

