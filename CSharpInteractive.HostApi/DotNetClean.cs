// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

namespace HostApi;

using Internal.DotNet;

/// <summary>
/// The dotnet clean command cleans the output of the previous build. It's implemented as an MSBuild target, so the project is evaluated when the command is run. Only the outputs created during the build are cleaned. Both intermediate (obj) and final output (bin) folders are cleaned.
/// <example>
/// <code>
/// new DotNetClean()
///     .WithVerbosity(DotNetVerbosity.Quiet)
///     .Build().EnsureSuccess();
/// </code>
/// </example>
/// </summary>
/// <param name="Props">MSBuild options for setting properties.</param>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Project">The MSBuild project or solution to clean. If a project or solution file is not specified, MSBuild searches the current working directory for a file that has a file extension that ends in proj or sln, and uses that file.</param>
/// <param name="Framework">The framework that was specified at build time. The framework must be defined in the project file. If you specified the framework at build time, you must specify the framework when cleaning.</param>
/// <param name="Runtime">Cleans the output folder of the specified runtime. This is used when a self-contained deployment was created.</param>
/// <param name="Configuration">Defines the build configuration. The default for most projects is Debug, but you can override the build configuration settings in your project. This option is only required when cleaning if you specified it during build time.</param>
/// <param name="Output">The directory that contains the build artifacts to clean. Specify the -f|--framework &lt;FRAMEWORK&gt; switch with the output directory switch if you specified the framework when the project was built.</param>
/// <param name="NoLogo">Doesn't display the startup banner or the copyright message. Available since .NET Core 3.0 SDK.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are Quiet, Minimal, Normal, Detailed, and Diagnostic. The default is Minimal. For more information, see LoggerVerbosity.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetClean(
    IEnumerable<(string name, string value)> Props,
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string Project = "",
    string Framework = "",
    string Runtime = "",
    string Configuration = "",
    string Output = "",
    bool? NoLogo = default,
    DotNetVerbosity? Verbosity = default,
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetClean(params string[] args)
        : this([], args, [])
    { }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithArgs("clean")
            .AddNotEmptyArgs(Project)
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddMSBuildLoggers(host, Verbosity)
            .AddArgs(
                ("--output", Output),
                ("--framework", Framework),
                ("--runtime", Runtime),
                ("--configuration", Configuration),
                ("--verbosity", Verbosity?.ToString().ToLowerInvariant())
            )
            .AddBooleanArgs(
                ("--nologo", NoLogo)
            )
            .AddProps("-p", Props.ToArray())
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => "dotnet clean".GetShortName(ShortName, Project);
}