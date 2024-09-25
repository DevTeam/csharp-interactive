// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedType.Global

namespace HostApi;

using Internal.DotNet;

/// <summary>
/// Executes a custom dotnet command.
/// <example>
/// <code>
/// NuGetVersion? version = default;
/// new DotNetCustom("--version")
///     .Run(message => NuGetVersion.TryParse(message.Line, out version))
///     .EnsureSuccess();
/// </code>
/// </example> 
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Build">Specifies to use of a build logger.</param>
/// <param name="Tests">Specifies to use of a tests logger.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="ShortName">Specifies whether the ability to use build loggers is supported. The default is set to true.</param>
[Target]
public partial record DotNetCustom(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    bool? Build = default,
    bool? Tests = default,
    DotNetVerbosity? Verbosity = default,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetCustom(params string[] args)
        : this(args, [])
    { }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        var cmd = host.CreateCommandLine(ExecutablePath);
        if (Build == true)
        {
            cmd = cmd.AddMSBuildLoggers(host, Verbosity);
        }
        
        if (Tests == true)
        {
            cmd = cmd.AddTestLoggers(host, Array.Empty<string>());
        }
        
        return cmd
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .WithArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() =>
        (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Executes a custom dotnet command.", ShortName, Args.ToArray());
}