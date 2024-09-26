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
/// <param name="TemplateName">A short name of the template to create, for example 'console'.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Output">Location to place the generated output.</param>
/// <param name="Name">The name for the output being created. If no name is specified, the name of the output directory is used.</param>
/// <param name="DryRun">Displays a summary of what would happen if the given command line were run if it would result in a template creation.</param>
/// <param name="Force">Forces content to be generated even if it would change existing files.</param>
/// <param name="NoUpdateCheck">Disables checking for the template package updates when instantiating a template.</param>
/// <param name="Project">The project that should be used for context evaluation.</param>
/// <param name="Verbosity">Sets the verbosity level. Allowed values are quiet, minimal, normal, and diagnostic. Default is normal.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetNew(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    string TemplateName,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string Output = "",
    string Name = "",
    bool? DryRun = default,
    bool? Force = default,
    bool? NoUpdateCheck = default,
    string Project = "",
    DotNetVerbosity? Verbosity = default,
    bool? Diagnostics = default,
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
            .AddArgs(
                ("--output", Output),
                ("--name", Name),
                ("--project", Project),
                ("--verbosity", Verbosity?.ToString().ToLowerInvariant())
            )
            .AddBooleanArgs(
                ("--dry-run", DryRun),
                ("--force", Force),
                ("--no-update-check", NoUpdateCheck),
                ("--diagnostics", Diagnostics)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() =>
        (ExecutablePath == string.Empty ? "dotnet new" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName(ShortName, TemplateName);
}