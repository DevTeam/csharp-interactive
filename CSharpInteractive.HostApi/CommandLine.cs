// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable ReturnTypeCanBeEnumerable.Global
namespace HostApi;

using System.Diagnostics;
using System.Text;

/// <summary>
/// Runs an arbitrary executable with arguments and environment variables from the working directory.
/// <example>
/// <code>
/// var cmd = new CommandLine("whoami");
/// cmd.Run().EnsureSuccess();
/// </code>
/// </example>
/// </summary>
/// <seealso cref="ICommandLineRunner.Run"/>
/// <seealso cref="ICommandLineRunner.RunAsync"/>
/// <seealso cref="IBuildRunner.Build"/>
/// <seealso cref="IBuildRunner.BuildAsync"/>
[Target]
[DebuggerTypeProxy(typeof(CommandLineDebugView))]
public partial record CommandLine(
    // Specifies the application executable path.
    string ExecutablePath,
    // Specifies the working directory for the application to be started.
    string WorkingDirectory,
    // Specifies the set of command line arguments to use when starting the application.
    IEnumerable<string> Args,
    // Specifies the set of environment variables that apply to this process and its child processes.
    IEnumerable<(string name, string value)> Vars,
    // Specifies a short name for this command line.
    string ShortName = "")
    : IStartInfo
{
    private readonly string _shortName = ShortName;

    /// <summary>
    /// Creates a new command line.
    /// </summary>
    /// <param name="executablePath">Path to the executable file.</param>
    /// <param name="args">Command line arguments.</param>
    public CommandLine(string executablePath, params string[] args)
        : this(executablePath, string.Empty, args, Array.Empty<(string name, string value)>())
    { }

    internal CommandLine(IStartInfo startInfo)
        : this(startInfo.ExecutablePath, startInfo.WorkingDirectory, startInfo.Args, startInfo.Vars, startInfo.ShortName)
    { }

    /// <inheritdoc/>
    public string ShortName => 
        !string.IsNullOrWhiteSpace(_shortName)
            ? _shortName
            : Path.GetFileNameWithoutExtension(ExecutablePath);

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return this;
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        var sb = new StringBuilder();
        if (!string.IsNullOrWhiteSpace(ShortName))
        {
            sb.Append(ShortName);
            sb.Append(": ");
        }

        sb.Append(Escape(ExecutablePath));
        foreach (var arg in Args)
        {
            sb.Append(' ');
            sb.Append(Escape(arg));
        }

        return sb.ToString();
    }

    private static string Escape(string text) => 
        !text.TrimStart().StartsWith("\"") && text.Contains(' ')
            ? $"\"{text}\""
            : text;

    internal class CommandLineDebugView(IStartInfo startInfo)
    {
        public string ShortName => startInfo.ShortName;

        public string ExecutablePath => startInfo.ExecutablePath;

        public string WorkingDirectory => startInfo.WorkingDirectory;

        [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
        public IEnumerable<string> Args => startInfo.Args;

        [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
        public IEnumerable<(string name, string value)> Vars => startInfo.Vars;
    }
}