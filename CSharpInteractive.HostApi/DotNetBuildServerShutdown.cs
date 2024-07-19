// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedType.Global
// ReSharper disable StringLiteralTypo
namespace HostApi;

using Internal.DotNet;

/// <summary>
/// The dotnet build server command is used to shut down build servers that are started from dotnet.
/// <example>
/// <code>
/// new DotNetBuildServerShutdown()
///     .Run().EnsureSuccess();
/// </code>
/// </example> 
/// </summary>
[Target]
public partial record DotNetBuildServerShutdown(
    // Specifies the set of command line arguments to use when starting the tool.
    IEnumerable<string> Args,
    // Specifies the set of environment variables that apply to this process and its child processes.
    IEnumerable<(string name, string value)> Vars,
    // Build servers to shut down. By default, all servers are shut down. 
    IEnumerable<DotNetBuildServer> Servers,
    // Overrides the tool executable path.
    string ExecutablePath = "",
    // Specifies the working directory for the tool to be started.
    string WorkingDirectory = "",
    // Specifies a short name for this operation.
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetBuildServerShutdown(params string[] args)
        : this(args, [], [])
    { }
    
    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .WithArgs("build-server", "shutdown")
            .AddArgs(GetServerArg().ToArray())
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => "dotnet build-server shutdown".GetShortName(ShortName);
    
    private IEnumerable<string> GetServerArg() =>
        Servers.Select(server => server switch
        {
            DotNetBuildServer.MSBuild => "--msbuild",
            DotNetBuildServer.VbCsCompiler => "--vbcscompiler",
            DotNetBuildServer.Razor => "--razor",
            _ => throw new ArgumentOutOfRangeException()
        });
}