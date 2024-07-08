// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedType.Global
namespace HostApi;

using Internal;

/// <summary>
/// The docker custom command is used to execute any docker commands with any arguments.
/// </summary>
[Target]
public partial record DockerCustom(
    // Specifies the set of command line arguments to use when starting the tool.
    IEnumerable<string> Args,
    // Specifies the set of environment variables that apply to this process and its child processes.
    IEnumerable<(string name, string value)> Vars,
    // Overrides the tool executable path.
    string ExecutablePath = "",
    // Specifies the working directory for the tool to be started.
    string WorkingDirectory = "",
    // Specifies a short name for this operation.
    string ShortName = "")
{
    public DockerCustom(params string[] args)
        : this(args, [])
    { }

    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return new CommandLine(string.IsNullOrWhiteSpace(ExecutablePath) ? host.GetService<HostComponents>().DockerSettings.DockerExecutablePath : ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .WithArgs(Args.ToArray());
    }

    public override string ToString() => string.IsNullOrWhiteSpace(ShortName) ? ((ExecutablePath == string.Empty ? "docker" : Path.GetFileNameWithoutExtension(ExecutablePath)) + " " + Args.FirstOrDefault()).TrimEnd() : ShortName;
}