// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedType.Global
namespace HostApi;

using Internal.DotNet;

/// <summary>
/// The dotnet custom command is used to execute any dotnet commands with any arguments.
/// <example>
/// <code>
/// NuGetVersion? version = default;
/// new DotNetCustom("--version")
///     .Run(message => NuGetVersion.TryParse(message.Line, out version))
///     .EnsureSuccess();
/// </code>
/// </example> 
/// </summary>
[Target]
public partial record DotNetCustom(
    // Specifies the set of command line arguments to use when starting the tool.
    IEnumerable<string> Args,
    // Specifies the set of environment variables that apply to this process and its child processes.
    IEnumerable<(string name, string value)> Vars,
    // Overrides the tool executable path.
    string ExecutablePath = "",
    // Specifies the working directory for the tool to be started.
    string WorkingDirectory = "",
    // Specifies whether the ability to use build loggers is supported. The default is set to true.
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
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .WithArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => 
        (ExecutablePath == string.Empty
            ? "dotnet"
            : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName(ShortName, Args.FirstOrDefault()?? string.Empty);
}