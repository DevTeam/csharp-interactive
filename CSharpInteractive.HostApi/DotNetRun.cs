// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming
namespace HostApi;

using Internal.DotNet;

/// <summary>
/// The dotnet run command provides a convenient option to run your application from the source code with one command. It's useful for fast iterative development from the command line. The command depends on the dotnet build command to build the code. Any requirements for the build, such as that the project must be restored first, apply to dotnet run as well.
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
/// <param name="Props">MSBuild options for setting properties.</param>
/// <param name="Args">Specifies the set of command line arguments to use when starting the application.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Framework">Builds and runs the app using the specified framework. The framework must be specified in the project file.</param>
/// <param name="Configuration">Defines the build configuration. The default for most projects is Debug, but you can override the build configuration settings in your project.</param>
/// <param name="Runtime">Specifies the target runtime to restore packages for. For a list of Runtime Identifiers (RIDs), see the RID catalog. -r short option available since .NET Core 3.0 SDK.</param>
/// <param name="Project">Specifies the path of the project file to run (folder name or full path). If not specified, it defaults to the current directory.</param>
/// <param name="LaunchProfile">The name of the launch profile (if any) to use when launching the application. Launch profiles are defined in the launchSettings.json file and are typically called Development, Staging, and Production. For more information, see Working with multiple environments.</param>
/// <param name="NoLaunchProfile">Doesn't try to use launchSettings.json to configure the application.</param>
/// <param name="NoBuild">Doesn't build the project before running. It also implicit sets the --no-restore flag.</param>
/// <param name="NoRestore">Doesn't execute an implicit restore when running the command.</param>
/// <param name="NoDependencies">When restoring a project with project-to-project (P2P) references, restores the root project and not the references.</param>
/// <param name="Force">Forces all dependencies to be resolved even if the last restore was successful. Specifying this flag is the same as deleting the project.assets.json file.</param>
/// <param name="Arch">Specifies the target architecture. This is a shorthand syntax for setting the Runtime Identifier (RID), where the provided value is combined with the default RID. For example, on a win-x64 machine, specifying --arch x86 sets the RID to win-x86. If you use this option, don&apos;t use the -r|--runtime option. Available since .NET 6 Preview 7.</param>
/// <param name="OS">Specifies the target operating system (OS). This is a shorthand syntax for setting the Runtime Identifier (RID), where the provided value is combined with the default RID. For example, on a win-x64 machine, specifying --os linux sets the RID to linux-x64. If you use this option, don&apos;t use the -r|--runtime option. Available since .NET 6.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are Quiet, Minimal, Normal, Detailed, and Diagnostic. The default is Minimal. For more information, see LoggerVerbosity.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
/// <seealso cref="DotNetNew"/>
[Target]
public partial record DotNetRun(
    IEnumerable<(string name, string value)> Props,
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string Framework = "",
    string Configuration = "",
    string Runtime = "",
    string Project = "",
    string LaunchProfile = "",
    bool? NoLaunchProfile = default,
    bool? NoBuild = default,
    bool? NoRestore = default,
    bool? NoDependencies = default,
    bool? Force = default,
    string Arch = "",
    string OS = "",
    DotNetVerbosity? Verbosity = default,
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetRun(params string[] args)
        : this([], args, [])
    { }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithArgs("run")
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs(
                ("--framework", Framework),
                ("--configuration", Configuration),
                ("--runtime", Runtime),
                ("--project", Project),
                ("--launch-profile", LaunchProfile),
                ("--verbosity", Verbosity?.ToString().ToLowerInvariant()),
                ("--arch", Arch),
                ("--os", OS)
            )
            .AddBooleanArgs(
                ("--no-launch-profile", NoLaunchProfile),
                ("--no-build", NoBuild),
                ("--no-restore", NoRestore),
                ("--no-dependencies", NoDependencies),
                ("--force", Force)
            )
            .AddProps("--property", Props.ToArray())
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => "dotnet run".GetShortName(ShortName, Project);
}