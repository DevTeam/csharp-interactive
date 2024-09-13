// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

namespace HostApi;

using Internal.DotNet;

/// <summary>
/// The dotnet restore command uses NuGet to restore dependencies as well as project-specific tools that are specified in the project file. In most cases, you don't need to explicitly use the dotnet restore command.
/// <example>
/// <code>
/// new DotNetRestore()
///     .Build().EnsureSuccess();
/// </code>
/// </example>
/// </summary>
/// <param name="Props">MSBuild options for setting properties.</param>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Sources">Specifies the URI of the NuGet package source to use during the restore operation. This setting overrides all the sources specified in the nuget.config files.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Project">Optional path to the project file to restore.</param>
/// <param name="Packages">Specifies the directory for restored packages.</param>
/// <param name="UseCurrentRuntime">Use current runtime as the target runtime.</param>
/// <param name="DisableParallel">Disables restoring multiple projects in parallel.</param>
/// <param name="ConfigFile">The NuGet configuration file (nuget.config) to use. If specified, only the settings from this file will be used. If not specified, the hierarchy of configuration files from the current directory will be used.</param>
/// <param name="NoCache">Specifies to not cache HTTP requests.</param>
/// <param name="IgnoreFailedSources">Only warn about failed sources if there are packages meeting the version requirement.</param>
/// <param name="Force">Forces all dependencies to be resolved even if the last restore was successful. Specifying this flag is the same as deleting the project.assets.json file.</param>
/// <param name="Runtime">Specifies a runtime for the package restore. This is used to restore packages for runtimes not explicitly listed in the &lt;RuntimeIdentifiers&gt; tag in the .csproj file. For a list of Runtime Identifiers (RIDs), see the RID catalog. Provide multiple RIDs by specifying this option multiple times.</param>
/// <param name="NoDependencies">When restoring a project with project-to-project (P2P) references, restores the root project and not the references.</param>
/// <param name="UseLockFile">Enables project lock file to be generated and used with restore.</param>
/// <param name="LockedMode">Don't allow updating project lock file.</param>
/// <param name="LockFilePath">Output location where project lock file is written. By default, this is PROJECT_ROOT\packages.lock.json.</param>
/// <param name="ForceEvaluate">Forces restore to reevaluate all dependencies even if a lock file already exists.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are Quiet, Minimal, Normal, Detailed, and Diagnostic. The default is Minimal. For more information, see LoggerVerbosity.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetRestore(
    IEnumerable<(string name, string value)> Props,
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<string> Sources,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string Project = "",
    string Packages = "",
    bool? UseCurrentRuntime = default,
    bool? DisableParallel = default,
    string ConfigFile = "",
    bool? NoCache = default,
    bool? IgnoreFailedSources = default,
    bool? Force = default,
    string Runtime = "",
    bool? NoDependencies = default,
    bool? UseLockFile = default,
    bool? LockedMode = default,
    string LockFilePath = "",
    bool? ForceEvaluate = default,
    DotNetVerbosity? Verbosity = default,
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetRestore(params string[] args)
        : this([], args, [], [])
    { }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithArgs("restore")
            .AddNotEmptyArgs(Project)
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddMSBuildLoggers(host, Verbosity)
            .AddArgs(Sources.Select(i => ("--source", (string?)i)).ToArray())
            .AddArgs(
                ("--packages", Packages),
                ("--configfile", ConfigFile),
                ("--runtime", Runtime),
                ("--lock-file-path", LockFilePath)
            )
            .AddBooleanArgs(
                ("--use-current-runtime", UseCurrentRuntime),
                ("--disable-parallel", DisableParallel),
                ("--no-cache", NoCache),
                ("--ignore-failed-sources", IgnoreFailedSources),
                ("--force", Force),
                ("--no-dependencies", NoDependencies),
                ("--use-lock-file", UseLockFile),
                ("--locked-mode", LockedMode),
                ("--force-evaluate", ForceEvaluate)
            )
            .AddProps("-p", Props.ToArray())
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => "dotnet restore".GetShortName(ShortName, Project);
}