// ReSharper disable UnusedMember.Global
namespace HostApi;

/// <summary>
/// An abstraction for running command lines.
/// <example>
/// <code>
/// GetService&lt;ICommandLineRunner&gt;()
///     .Run(new CommandLine("cmd", "/c", "DIR"))
///     .EnsureSuccess();
/// </code>
/// </example>
/// </summary>
/// <seealso cref="IBuildRunner"/>
public interface ICommandLineRunner
{
    /// <summary>
    /// Runs a command line.
    /// <example>
    /// <code>
    /// var runner = GetService&lt;ICommandLineRunner&gt;();
    /// runner.Run(new CommandLine("whoami"));
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="commandLine">Command line to run.</param>
    /// <param name="handler">Event handler for running command line events, optional.</param>
    /// <param name="timeout">Time to wait for command line running to complete, optional. By default, waits for the end of running without limit. If the value is exceeded, the command line process and its children will be cancelled.</param>
    /// <returns>The result of a command line running.</returns>
    /// <seealso cref="RunAsync"/>
    /// <seealso cref="Output"/>
    ICommandLineResult Run(
        ICommandLine commandLine,
        Action<Output>? handler = default,
        TimeSpan timeout = default);

    /// <summary>
    /// Runs a command line in asynchronous way.
    /// <example>
    /// <code>
    /// var runner = GetService&lt;ICommandLineRunner&gt;();
    /// await runner.RunAsync(new CommandLine("whoami"));
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="commandLine">Command line to run.</param>
    /// <param name="handler">Event handler for running command line events, optional.</param>
    /// <param name="cancellationToken">Propagates notification that a running should be canceled, optional.</param>
    /// <returns>The result of an asynchronous command line running.</returns>
    /// <seealso cref="Run"/>
    /// <seealso cref="Task{T}"/>
    /// <seealso cref="Output"/>
    /// <seealso cref="CancellationToken"/>
    Task<ICommandLineResult> RunAsync(
        ICommandLine commandLine,
        Action<Output>? handler = default,
        CancellationToken cancellationToken = default);
}