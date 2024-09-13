// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

namespace HostApi;

using Internal.DotNet;

/// <summary>
/// The dotnet tool restore command finds the tool manifest file that is in scope for the current directory and installs the tools that are listed in it.
/// <example>
/// <code>
/// new DotNetToolRestore()
///     .Run().EnsureSuccess();
/// </code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="AdditionalSources">Adds an additional NuGet package source to use during installation. Feeds are accessed in parallel, not sequentially in some order of precedence. If the same package and version is in multiple feeds, the fastest feed wins.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="DisableParallel">Prevent restoring multiple projects in parallel.</param>
/// <param name="ConfigFile">The NuGet configuration file (nuget.config) to use. If specified, only the settings from this file will be used. If not specified, the hierarchy of configuration files from the current directory will be used.</param>
/// <param name="ToolManifest">Path to the manifest file.</param>
/// <param name="NoCache">Do not cache packages and http requests.</param>
/// <param name="IgnoreFailedSources">Treat package source failures as warnings.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are Quiet, Minimal, Normal, Detailed, and Diagnostic. The default is Minimal. For more information, see LoggerVerbosity.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetToolRestore(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<string> AdditionalSources,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? DisableParallel = default,
    string ConfigFile = "",
    string ToolManifest = "",
    bool? NoCache = default,
    bool? IgnoreFailedSources = default,
    DotNetVerbosity? Verbosity = default,
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetToolRestore(params string[] args)
        : this(args, [], [])
    { }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithArgs("tool", "restore")
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddMSBuildLoggers(host, Verbosity)
            .AddArgs(AdditionalSources.Select(i => ("--add-source", (string?)i)).ToArray())
            .AddArgs(
                ("--configfile", ConfigFile),
                ("--tool-manifest", ToolManifest)
            )
            .AddBooleanArgs(
                ("--disable-parallel", DisableParallel),
                ("--no-cache", NoCache),
                ("--ignore-failed-sources", IgnoreFailedSources)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => "dotnet tool restore".GetShortName(ShortName, ToolManifest);
}