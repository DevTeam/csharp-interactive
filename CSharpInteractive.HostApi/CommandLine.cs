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
/// <param name="ExecutablePath">Specifies the application executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the application to be started.</param>
/// <param name="Args">Specifies the set of command line arguments to use when starting the application.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ShortName">Specifies a short name for this command line.</param>
/// <seealso cref="ICommandLineRunner.Run"/>
/// <seealso cref="ICommandLineRunner.RunAsync"/>
/// <seealso cref="IBuildRunner.Build"/>
/// <seealso cref="IBuildRunner.BuildAsync"/>
[Target]
[DebuggerTypeProxy(typeof(CommandLineDebugView))]
public partial record CommandLine(
    string ExecutablePath,
    string WorkingDirectory,
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    string ShortName = "")
    : IStartInfo
{

    /// <summary>
    /// Creates a new command line.
    /// </summary>
    /// <param name="executablePath">Path to the executable file.</param>
    /// <param name="args">Command line arguments.</param>
    public CommandLine(string executablePath, params string[] args)
        : this(executablePath, string.Empty, args, [])
    { }

    internal CommandLine(IStartInfo startInfo)
        : this(startInfo.ExecutablePath, startInfo.WorkingDirectory, startInfo.Args, startInfo.Vars, startInfo.ShortName)
    { }

    /// <inheritdoc/>
    public string ShortName
    {
        get => !string.IsNullOrWhiteSpace(field)
            ? field
            : Path.GetFileNameWithoutExtension(ExecutablePath);
    } = ShortName;

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host) =>
        host != null ? this : throw new ArgumentNullException(nameof(host));

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