// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming
namespace HostApi;

using Internal.DotNet;

/// <summary>
/// Runs a dotnet application.
/// <p>
/// You specify the path to an application .dll file to run the application. To run the application means to find and execute the entry point, which in the case of console apps is the Main method. For example, dotnet myapp.dll runs the myapp application.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet">.NET CLI command</a><br/>
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
            .AddNotEmptyArgs(PathToApplication.ToArg())
            .AddArgs(AdditionalProbingPaths.ToArgs("--additionalprobingpath", ""))
            .AddArgs(AdditionalDeps.ToArgs("--additional-deps", ""))
            .AddArgs(FxVersion.ToArgs("--fx-version", ""))
            .AddArgs(RollForward.ToArgs("--roll-forward", ""))
            .AddBooleanArgs(
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => "".GetShortName(ShortName, PathToApplication.ToArg());
}

/// <summary>
/// Executes a dotnet application.
/// <p>
/// You specify the path to an application .dll file to run the application. To run the application means to find and execute the entry point, which in the case of console apps is the Main method. For example, dotnet myapp.dll runs the myapp application.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet">.NET CLI command</a><br/>
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
    public override string ToString() => "".GetShortName(ShortName, "exec", PathToApplication.ToArg());
}

/// <summary>
/// Adds or updates a package reference in a project file.
/// <p>
/// This command provides a convenient option to add or update a package reference in a project file. When you run the command, there&apos;s a compatibility check to ensure the package is compatible with the frameworks in the project. If the check passes and the package isn&apos;t referenced in the project file, a &lt;PackageReference&gt; element is added to the project file. If the check passes and the package is already referenced in the project file, the &lt;PackageReference&gt; element is updated to the latest compatible version. After the project file is updated, dotnet restore is run.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-add-package">.NET CLI command</a><br/>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Sources">The URI of the NuGet package source to use during this operation.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Project">The project or solution file to operate on. If not specified, the command searches the current directory for one. If more than one solution or project is found, an error is thrown.</param>
/// <param name="PackageName">The package reference to add.</param>
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
            .AddNotEmptyArgs(Project.ToArg())
            .AddArgs("package")
            .AddNotEmptyArgs(PackageName.ToArg())
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
    public override string ToString() => "".GetShortName(ShortName, "add", Project.ToArg(), "package", PackageName.ToArg());
}

/// <summary>
/// Lists the package references for a project or solution.
/// <p>
/// This command provides a convenient option to list all NuGet package references for a specific project or a solution. You first need to build the project in order to have the assets needed for this command to process.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-list-package">.NET CLI command</a><br/>
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
    public override string ToString() => "".GetShortName(ShortName, "list", Project.ToArg(), "package");
}

/// <summary>
/// Removes package reference from a project file.
/// <p>
/// This command provides a convenient option to remove a NuGet package reference from a project.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-remove-package">.NET CLI command</a><br/>
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
            .AddNotEmptyArgs(Project.ToArg())
            .AddArgs("package")
            .AddNotEmptyArgs(PackageName.ToArg())
            .AddBooleanArgs(
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => "".GetShortName(ShortName, "remove", Project.ToArg(), "package", PackageName.ToArg());
}

/// <summary>
/// Adds project-to-project (P2P) references.
/// <p>
/// This command provides a convenient option to add project references to a project. After running the command, the &lt;ProjectReference&gt; elements are added to the project file.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-add-reference">.NET CLI command</a><br/>
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
            .AddNotEmptyArgs(References.ToArray().ToArg())
            .AddArgs(References.ToArgs("--source", ""))
            .AddArgs(Framework.ToArgs("--framework", ""))
            .AddBooleanArgs(
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => "".GetShortName(ShortName, new [] {"add", Project.ToArg(), "reference"}.Concat(References).ToArray());
}

/// <summary>
/// Lists project-to-project references.
/// <p>
/// This command provides a convenient option to list project references for a given project.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-list-reference">.NET CLI command</a><br/>
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
            .AddBooleanArgs(
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => "".GetShortName(ShortName, "list", Project.ToArg());
}

/// <summary>
/// Removes project-to-project (P2P) references.
/// <p>
/// This command provides a convenient option to remove project references from a project.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-remove-reference">.NET CLI command</a><br/>
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
            .AddNotEmptyArgs(References.ToArray().ToArg())
            .AddArgs(References.ToArgs("--source", ""))
            .AddArgs(Framework.ToArgs("--framework", ""))
            .AddBooleanArgs(
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => "".GetShortName(ShortName, new [] {"remove", Project.ToArg(), "reference"}.Concat(References).ToArray());
}

/// <summary>
/// Builds a project and all of its dependencies.
/// <p>
/// This command builds the project and its dependencies into a set of binaries. The binaries include the project's code in Intermediate Language (IL) files with a .dll extension. For executable projects targeting versions earlier than .NET Core 3.0, library dependencies from NuGet are typically NOT copied to the output folder. They're resolved from the NuGet global packages folder at run time. With that in mind, the product of dotnet build isn't ready to be transferred to another machine to run. To create a version of the application that can be deployed, you need to publish it (for example, with the dotnet publish command). For more information, see .NET Application Deployment.
/// </p>
/// <p>
/// For executable projects targeting .NET Core 3.0 and later, library dependencies are copied to the output folder. This means that if there isn't any other publish-specific logic (such as Web projects have), the build output should be deployable.
/// </p>
/// <example>
/// <code>
/// var configuration = Props.Get("configuration", "Release");
/// 
/// 
/// new DotNetBuild().WithConfiguration(configuration)
///     .Build().EnsureSuccess();
/// </code>
/// </example>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-build">.NET CLI command</a><br/>
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
    public override string ToString() => "".GetShortName(ShortName, "build", Project.ToArg());
}

/// <summary>
/// Shuts down build servers that are started from dotnet.
/// <p>
/// By default, all servers are shut down.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-build-server">.NET CLI command</a><br/>
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
    public override string ToString() => "".GetShortName(ShortName, "build-server", "shutdown");
}

/// <summary>
/// Cleans the output of a project.
/// <p>
/// This command cleans the output of the previous build. It's implemented as an MSBuild target, so the project is evaluated when the command is run. Only the outputs created during the build are cleaned. Both intermediate (obj) and final output (bin) folders are cleaned.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-clean">.NET CLI command</a><br/>
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
    public override string ToString() => "".GetShortName(ShortName, "clean", Project.ToArg());
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
    string Format = "",
    string Import = "",
    bool? NoPassword = default,
    bool? Password = default,
    bool? Quiet = default,
    bool? Trust = default,
    bool? Verbose = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
            .AddArgs(Import.ToArgs("--import", ""))
            .AddBooleanArgs(
                ("--check", Check),
                ("--clean", Clean),
                ("--no-password", NoPassword),
                ("--password", Password),
                ("--quiet", Quiet),
                ("--trust", Trust),
                ("--verbose", Verbose),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => "".GetShortName(ShortName, "dev-certs", "https");
}

/// <summary>
/// Creates a new project, configuration file, or solution based on the specified template.
/// <p>
/// This command creates a .NET project or other artifacts based on a template. The command calls the template engine to create the artifacts on disk based on the specified template and options.
/// </p>
/// <example>
/// <code>
/// new DotNetNew()
///     .WithTemplateName("console")
///     .WithName("MyApp")
///     .WithForce(true)
///     .Run().EnsureSuccess();
/// </code>
/// </example>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-new">.NET CLI command</a><br/>
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
    public override string ToString() => "".GetShortName(ShortName, "new", TemplateName.ToArg());
}

/// <summary>
/// Lists available templates to be run using dotnet new.
/// <p>
/// This command lists available templates to use with dotnet new. If the &lt;TEMPLATE_NAME&gt; is specified, lists templates containing the specified name. This option lists only default and installed templates. To find templates in NuGet that you can install locally, use the search command.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-new-list">.NET CLI command</a><br/>
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
    public override string ToString() => "".GetShortName(ShortName, "new", "list", TemplateName.ToArg());
}

/// <summary>
/// Searches for the templates supported by dotnet new on NuGet.org.
/// <p>
/// This command searches for templates supported by dotnet new on NuGet.org. When the &lt;TEMPLATE_NAME&gt; is specified, searches for templates containing the specified name.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-new-search">.NET CLI command</a><br/>
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
    public override string ToString() => "".GetShortName(ShortName, "new", "search", TemplateName.ToArg());
}

/// <summary>
/// Displays template package metadata.
/// <p>
/// This command displays the metadata of the template package from the package name provided. By default, the command searches for the latest available version. If the package is installed locally or is found on the official NuGet website, it also displays the templates that the package contains, otherwise it only displays basic metadata.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-new-details">.NET CLI command</a><br/>
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
    public override string ToString() => "".GetShortName(ShortName, "new", "details", TemplateName.ToArg());
}

/// <summary>
/// Installs a template package.
/// <p>
/// This command installs a template package from the PATH or NUGET_ID provided. If you want to install a specific version or prerelease version of a template package, specify the version in the format &lt;package-name&gt;::&lt;package-version&gt;. By default, dotnet new passes * for the version, which represents the latest stable package version.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-new-install">.NET CLI command</a><br/>
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
    public override string ToString() => "".GetShortName(ShortName, "new", "install", Package.ToArg());
}

/// <summary>
/// Uninstalls a template package.
/// <p>
/// This command uninstalls a template package at the PATH or NUGET_ID provided. When the &lt;PATH|NUGET_ID&gt; value isn&apos;t specified, all currently installed template packages and their associated templates are displayed. When specifying NUGET_ID, don&apos;t include the version number.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-new-uninstall">.NET CLI command</a><br/>
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
    public override string ToString() => "".GetShortName(ShortName, "new", "uninstall", Package.ToArg());
}

/// <summary>
/// Updates installed template packages.
/// <p>
/// This command updates installed template packages. The dotnet new update command with --check-only option checks for available updates for installed template packages without applying them.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-new-update">.NET CLI command</a><br/>
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
    public override string ToString() => "".GetShortName(ShortName, "new", "update");
}

/// <summary>
/// Deletes or unlists a package from the server.
/// <p>
/// This command deletes or unlists a package from the server. For nuget.org, the action is to unlist the package.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-delete">.NET CLI command</a><br/>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="PackageName">Name/ID of the package to delete.</param>
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
    string PackageName = "",
    string PackageVersion = "",
    bool? ForceEnglishOutput = default,
    string ApiKey = "",
    bool? NoServiceEndpoint = default,
    string Source = "",
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
            .AddNotEmptyArgs(PackageName.ToArg())
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
    public override string ToString() => "".GetShortName(ShortName, "nuget", "delete", PackageName.ToArg(), PackageVersion.ToArg());
}

/// <summary>
/// Clears local NuGet resources.
/// <p>
/// This command clears local NuGet resources in the http-request cache, temporary cache, or machine-wide global packages folder.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-locals">.NET CLI command</a><br/>
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
    public override string ToString() => "".GetShortName(ShortName, "nuget", "locals", CacheLocation.ToArg());
}

/// <summary>
/// Lists local NuGet resources.
/// <p>
/// This command lists local NuGet resources in the http-request cache, temporary cache, or machine-wide global packages folder.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-locals">.NET CLI command</a><br/>
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
    public override string ToString() => "".GetShortName(ShortName, "nuget", "locals", CacheLocation.ToArg());
}

/// <summary>
/// Pushes a package to the server and publishes it.
/// <p>
/// This command pushes a package to the server and publishes it. The push command uses server and credential details found in the system's NuGet config file or chain of config files. NuGet's default configuration is obtained by loading %AppData%\NuGet\NuGet.config (Windows) or $HOME/.nuget/NuGet/NuGet.Config (Linux/macOS), then loading any nuget.config or .nuget\nuget.config starting from the root of drive and ending in the current directory.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-push">.NET CLI command</a><br/>
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
    public override string ToString() => "".GetShortName(ShortName, "nuget", "push", Package.ToArg());
}

/// <summary>
/// Add a NuGet source.
/// <p>
/// This command adds a new package source to your NuGet configuration files. When adding multiple package sources, be careful not to introduce a dependency confusion vulnerability.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-add-source">.NET CLI command</a><br/>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ValidAuthenticationTypes">List of valid authentication types for this source. Set this to basic if the server advertises NTLM or Negotiate and your credentials must be sent using the Basic mechanism, for instance when using a PAT with on-premises Azure DevOps Server. Other valid values include negotiate, kerberos, ntlm, and digest, but these values are unlikely to be useful.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="PackageSourcePath">Path to the package source.</param>
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
    string PackageSourcePath = "",
    string ConfigFile = "",
    bool? AllowInsecureConnections = default,
    string Name = "",
    string Password = "",
    bool? StorePasswordInClearText = default,
    string Username = "",
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
            .AddNotEmptyArgs(PackageSourcePath.ToArg())
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
    public override string ToString() => "".GetShortName(ShortName, "nuget", "add", "source", PackageSourcePath.ToArg());
}

/// <summary>
/// Disable a NuGet source.
/// <p>
/// This command disables an existing source in your NuGet configuration files.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-disable-source">.NET CLI command</a><br/>
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
    public override string ToString() => "".GetShortName(ShortName, "nuget", "disable", "source", Name.ToArg());
}

/// <summary>
/// Enable a NuGet source.
/// <p>
/// This command enables an existing source in your NuGet configuration files.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-enable-source">.NET CLI command</a><br/>
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
    public override string ToString() => "".GetShortName(ShortName, "nuget", "enable", "source", Name.ToArg());
}

/// <summary>
/// Lists all configured NuGet sources.
/// <p>
/// This command lists all existing sources from your NuGet configuration files.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-list-source">.NET CLI command</a><br/>
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
    public override string ToString() => "".GetShortName(ShortName, "nuget", "list", "source");
}

/// <summary>
/// Remove a NuGet source.
/// <p>
/// This command removes an existing source from your NuGet configuration files.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-remove-source">.NET CLI command</a><br/>
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
    public override string ToString() => "".GetShortName(ShortName, "nuget", "remove", "source", Name.ToArg());
}

/// <summary>
/// Update a NuGet source.
/// <p>
/// This command updates an existing source in your NuGet configuration files.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-update-source">.NET CLI command</a><br/>
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
    public override string ToString() => "".GetShortName(ShortName, "nuget", "update", "source", Name.ToArg());
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
            .AddNotEmptyArgs(Packages.ToArray().ToArg())
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
    public override string ToString() => "".GetShortName(ShortName, new [] {"nuget", "verify"}.Concat(Packages).ToArray());
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
    public override string ToString() => "".GetShortName(ShortName, "nuget", "trust", "list");
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
    public override string ToString() => "".GetShortName(ShortName, "nuget", "trust", "sync", Name.ToArg());
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
    public override string ToString() => "".GetShortName(ShortName, "nuget", "trust", "remove", Name.ToArg());
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
    public override string ToString() => "".GetShortName(ShortName, "nuget", "trust", "author", Name.ToArg(), Package.ToArg());
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
    public override string ToString() => "".GetShortName(ShortName, "nuget", "trust", "repository", Name.ToArg(), Package.ToArg());
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
    public override string ToString() => "".GetShortName(ShortName, "nuget", "trust", "certificate", Name.ToArg(), Fingerprint.ToArg());
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
    public override string ToString() => "".GetShortName(ShortName, "nuget", "trust", "source", Name.ToArg());
}

/// <summary>
/// Signs all the NuGet packages matching the first argument with a certificate.
/// <p>
/// This command signs all the packages matching the first argument with a certificate. The certificate with the private key can be obtained from a file or from a certificate installed in a certificate store by providing a subject name or a SHA-1 fingerprint. This command requires a certificate root store that is valid for both code signing and timestamping. Also, this command may not be supported on some combinations of operating system and .NET SDK.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-sign">.NET CLI command</a><br/>
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
            .AddNotEmptyArgs(Packages.ToArray().ToArg())
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
    public override string ToString() => "".GetShortName(ShortName, new [] {"nuget", "sign"}.Concat(Packages).ToArray());
}

/// <summary>
/// Shows the dependency graph for a particular package.
/// <p>
/// This command shows the dependency graph for a particular package for a given project or solution. Starting from the .NET 9 SDK, it's possible to pass a NuGet assets file in place of the project file, in order to use the command with projects that can't be restored with the .NET SDK. First, restore the project in Visual Studio, or msbuild.exe. By default the assets file is in the project's obj\ directory, but you can find the location with msbuild.exe path\to\project.proj -getProperty:ProjectAssetsFile. Finally, run dotnet nuget why path\to\project.assets.json SomePackage.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-why">.NET CLI command</a><br/>
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
    public override string ToString() => "".GetShortName(ShortName, "nuget", "why", Project.ToArg(), Package.ToArg());
}

/// <summary>
/// Gets the NuGet configuration settings that will be applied.
/// <p>
/// This command gets the NuGet configuration settings that will be applied from the config section.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-config-get">.NET CLI command</a><br/>
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
public partial record DotNetNuConfigGet(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    string ConfigKey = "ALL",
    bool? ShowPath = default,
    string Directory = "",
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetNuConfigGet(params string[] args)
        : this(args, [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetNuConfigGet()
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
    public override string ToString() => "".GetShortName(ShortName, "nuget", "config", "get", ConfigKey.ToArg());
}

/// <summary>
/// Set the value of a specified NuGet configuration setting.
/// <p>
/// This command sets the values for NuGet configuration settings that will be applied from the config section.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-config-set">.NET CLI command</a><br/>
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
public partial record DotNetNuConfigSet(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    string ConfigKey = "",
    string ConfigValue = "",
    string ConfigFile = "",
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetNuConfigSet(params string[] args)
        : this(args, [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetNuConfigSet()
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
    public override string ToString() => "".GetShortName(ShortName, "nuget", "config", "set", ConfigKey.ToArg(), ConfigValue.ToArg());
}

/// <summary>
/// Removes the key-value pair from a specified NuGet configuration setting.
/// <p>
/// This command unsets the values for NuGet configuration settings that will be applied from the config section.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-config-unset">.NET CLI command</a><br/>
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
public partial record DotNetNuConfigUnset(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    string ConfigKey = "",
    string ConfigFile = "",
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetNuConfigUnset(params string[] args)
        : this(args, [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetNuConfigUnset()
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
    public override string ToString() => "".GetShortName(ShortName, "nuget", "config", "unset", ConfigKey.ToArg());
}

/// <summary>
/// Lists nuget configuration files currently being appplied to a directory.
/// <p>
/// This command lists the paths to all NuGet configuration files that will be applied when invoking NuGet commands in a specific directory.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-config-paths">.NET CLI command</a><br/>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Directory">Specifies the directory to start from when listing configuration files. If not specified, the current directory is used.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetNuConfigPaths(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    string Directory = "",
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetNuConfigPaths(params string[] args)
        : this(args, [])
    {
    }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public DotNetNuConfigPaths()
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
    public override string ToString() => "".GetShortName(ShortName, "nuget", "config", "paths");
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
/// <example>
/// <code>
/// new DotNetPack()
///     .Build().EnsureSuccess();
/// </code>
/// </example>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-pack">.NET CLI command</a><br/>
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
    public override string ToString() => "".GetShortName(ShortName, "pack", Project.ToArg());
}

/// <summary>
/// Searches for a NuGet package.
/// <p>
/// This command searches for a NuGet package.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-package-search">.NET CLI command</a><br/>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Sources">The URI of the NuGet package source to use during this operation.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="SearchTerms">Specifies the search term to filter results. Use this argument to search for packages matching the provided query.</param>
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
    string SearchTerms = "",
    string ConfigFile = "",
    bool? ExactMatch = default,
    DotNetPackageSearchResultFormat? Format = default,
    bool? Prerelease = default,
    int? Skip = default,
    int? Take = default,
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
            .AddArgs("paths")
            .AddNotEmptyArgs(SearchTerms.ToArg())
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
    public override string ToString() => "".GetShortName(ShortName, "package", "search", "paths", SearchTerms.ToArg());
}

/// <summary>
/// Publishes the application and its dependencies to a folder for deployment to a hosting system.
/// <p>
/// This command compiles the application, reads through its dependencies specified in the project file, and publishes the resulting set of files to a directory.
/// </p>
/// <example>
/// <code>
/// new DotNetPublish().AddProps(("PublishDir", ".publish"))
///     .Build().EnsureSuccess();
/// </code>
/// </example>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-publish">.NET CLI command</a><br/>
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
/// <param name="SelfContained">Publishes the .NET runtime with your application so the runtime doesn't need to be installed on the target machine. Default is true if a runtime identifier is specified and the project is an executable project (not a library project). For more information, see .NET application publishing and Publish .NET apps with the .NET CLI.</param>
/// <param name="NoSelfContained">Publishes the application as a framework dependent application. A compatible .NET runtime must be installed on the target machine to run the application. Available since .NET 6 SDK.</param>
/// <param name="Runtime">Publishes the application for a given runtime. For a list of Runtime Identifiers (RIDs), see the RID catalog. For more information, see .NET application publishing and Publish .NET apps with the .NET CLI.</param>
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
    public override string ToString() => "".GetShortName(ShortName, "publish", Project.ToArg());
}

/// <summary>
/// Restores the dependencies and tools of a project.
/// <p>
/// A .NET project typically references external libraries in NuGet packages that provide additional functionality. These external dependencies are referenced in the project file (.csproj or .vbproj). When you run the dotnet restore command, the .NET CLI uses NuGet to look for these dependencies and download them if necessary. It also ensures that all the dependencies required by the project are compatible with each other and that there are no conflicts between them. Once the command is completed, all the dependencies required by the project are available in a local cache and can be used by the .NET CLI to build and run the application.
/// </p>
/// <p>
/// Sometimes, it might be inconvenient to run the implicit NuGet restore with these commands. For example, some automated systems, such as build systems, need to call dotnet restore explicitly to control when the restore occurs so that they can control network usage. To prevent the implicit NuGet restore, you can use the --no-restore flag with any of these commands.
/// </p>
/// <example>
/// <code>
/// new DotNetRestore()
///     .Build().EnsureSuccess();
/// </code>
/// </example>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-restore">.NET CLI command</a><br/>
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
    public override string ToString() => "".GetShortName(ShortName, "restore", Project.ToArg());
}

/// <summary>
/// Runs source code without any explicit compile or launch commands.
/// <p>
/// This command provides a convenient option to run your application from the source code with one command. It's useful for fast iterative development from the command line. The command depends on the dotnet build command to build the code. Any requirements for the build apply to dotnet run as well.
/// </p>
/// <p>
/// To run the application, the dotnet run command resolves the dependencies of the application that are outside of the shared runtime from the NuGet cache. Because it uses cached dependencies, it's not recommended to use dotnet run to run applications in production. Instead, create a deployment using the dotnet publish command and deploy the published output.
/// </p>
/// <example>
/// <code>
/// new DotNetNew()
///     .WithTemplateName("console")
///     .WithName("MyApp")
///     .WithForce(true)
///     .Run().EnsureSuccess();
/// 
/// 
/// new DotNetRun().WithWorkingDirectory("MyApp")
///     .Build().EnsureSuccess();
/// </code>
/// </example>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-run">.NET CLI command</a><br/>
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
/// <param name="NoBuild">Doesn't build the project before running. It also implicit sets the --no-restore flag.</param>
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
    public override string ToString() => "".GetShortName(ShortName, "run");
}

/// <summary>
/// Lists the latest available version of the .NET SDK and .NET Runtime, for each feature band.
/// <p>
/// This command makes it easier to track when new versions of the SDK and Runtimes are available.
/// </p>
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-sdk-check">.NET CLI command</a><br/>
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
    public override string ToString() => "".GetShortName(ShortName, "sdk", "check");
}

/// <summary>
/// Lists all projects in a solution file.
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-sln#list">.NET CLI command</a><br/>
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
    public override string ToString() => "".GetShortName(ShortName, "sln", Solution.ToArg(), "list");
}

/// <summary>
/// Adds one or more projects to the solution file.
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-sln#add">.NET CLI command</a><br/>
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
            .AddNotEmptyArgs(Projects.ToArray().ToArg())
            .AddArgs(SolutionFolder.ToArgs("--solution-folder", ""))
            .AddBooleanArgs(
                ("--in-root", InRoot),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => "".GetShortName(ShortName, new [] {"sln", Solution.ToArg(), "add"}.Concat(Projects).ToArray());
}

/// <summary>
/// Removes a project or multiple projects from the solution file.
/// <br/><a href="https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-sln#remove">.NET CLI command</a><br/>
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
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? Diagnostics = default,
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
            .AddNotEmptyArgs(Projects.ToArray().ToArg())
            .AddBooleanArgs(
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => "".GetShortName(ShortName, new [] {"sln", Solution.ToArg(), "remove"}.Concat(Projects).ToArray());
}


