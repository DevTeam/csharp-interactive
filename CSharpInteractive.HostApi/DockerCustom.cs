// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedType.Global

namespace HostApi;

using Internal;

/// <summary>
/// The docker custom command is used to execute any docker commands with any arguments.
/// <example>
/// <code>
/// var hasLinuxDocker = false;
/// new DockerCustom("info").Run(output =>
///     {
///         if (output.Line.Contains("OSType: linux"))
///         {
///             hasLinuxDocker = true;
///         }
///     });
/// </code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="ShortName"> Specifies a short name for this operation.</param>
[Target]
public partial record DockerCustom(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DockerCustom(params string[] args)
        : this(args, [])
    { }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return new CommandLine(string.IsNullOrWhiteSpace(ExecutablePath) ? host.GetService<HostComponents>().DockerSettings.DockerExecutablePath : ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .WithArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() =>
        string.IsNullOrWhiteSpace(ShortName)
            ? ((ExecutablePath == string.Empty ? "docker" : Path.GetFileNameWithoutExtension(ExecutablePath)) + " " + Args.FirstOrDefault()).TrimEnd()
            : ShortName;
}