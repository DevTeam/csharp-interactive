namespace HostApi;

/// <summary>
/// The UnityPlayer command is used to launch Unity Players from the command line and pass in arguments to change how the Player executes. To use the information on this page, you need to know how to use your operating system’s command-line interface to launch applications and run command-line arguments.
/// <example>
/// <code>
/// new UnityPlayer().Run();
/// </code>
/// </example>
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="ShortName"> Specifies a short name for this operation.</param>
[Target]
public partial record UnityPlayer(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string ShortName = "")
{
    public IStartInfo GetStartInfo(IHost host)
    {
        throw new NotImplementedException();
    }
}