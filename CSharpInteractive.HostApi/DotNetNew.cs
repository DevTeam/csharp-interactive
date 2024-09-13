// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedType.Global

namespace HostApi;

using Internal.DotNet;

/// <summary>
/// The dotnet new command creates a .NET project based on a template.
/// <example>
/// <code>
/// var result = new DotNetNew("classlib", "-n", "MyLib", "--force")
///     .Build().EnsureSuccess();
/// </code>
/// </example> 
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="TemplateName">Specifies a short template name, for example 'console'.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetNew(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    string TemplateName,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="templateName">Specifies a short template name, for example 'console'.</param>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetNew(string templateName, params string[] args)
        : this(args, [], templateName)
    { }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .WithArgs("new", TemplateName)
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() =>
        (ExecutablePath == string.Empty ? "dotnet new" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName(ShortName, TemplateName);
}