namespace HostApi;

/// <summary>
/// Command line extensions. Facilitate the use of the command line.
/// </summary>
[ExcludeFromCodeCoverage]
public static class CommandLineTools
{
    /// <summary>
    /// Creates a new command line from the executable file path and arguments.
    /// <example>
    /// <code>
    /// "whoami".AsCommandLine().Run().EnsureSuccess();
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="executable">Path to the executable file.</param>
    /// <param name="args">Command line arguments.</param>
    /// <returns>Created command line.</returns>
    public static CommandLine AsCommandLine(this string executable, params string[] args) =>
        new(executable, args);
    
    /// <summary>
    /// Customizes a command line by overriding its parameters as command line arguments and others.
    /// <example>
    /// <code>
    /// var test = new DotNetTest().WithFilter("Integration!=true");
    ///
    /// 
    /// test.Customize(cmd =>
    ///     cmd.WithArgs("dotcover")
    ///         .AddArgs(cmd.Args)
    ///         .AddArgs(
    ///             $"--dcOutput=dotCover.dcvr",
    ///             "--dcFilters=+:module=MyLib;+:module=MyLib2",
    ///             "--dcAttributeFilters=System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage"))
    ///     .Build().EnsureSuccess();
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="baseCommandLine">The base command for customization.</param>
    /// <param name="customizer">Customization function.</param>
    /// <returns>Customized command line.</returns>
    public static ICommandLine Customize(this ICommandLine baseCommandLine, Func<CommandLine, ICommandLine> customizer) => 
        new CustomCommandLine(baseCommandLine, customizer);

    private class CustomCommandLine(ICommandLine baseCommandLine, Func<CommandLine, ICommandLine> customizer) : ICommandLine
    {
        public IStartInfo GetStartInfo(IHost host)
        {
            if (host == null) throw new ArgumentNullException(nameof(host));
            return customizer(new CommandLine(baseCommandLine.GetStartInfo(host))).GetStartInfo(host);
        }
    }
}