// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMethodReturnValue.Global
// ReSharper disable UnusedMemberInSuper.Global

namespace HostApi;

/// <summary>
/// An abstraction for running builds.
/// <example>
/// <code>
/// var configuration = Props.Get("configuration", "Release");
/// 
///
/// GetService&lt;IBuildRunner&gt;()
///     Build(new DotNetBuild().WithConfiguration(configuration))
///     .EnsureSuccess();
/// </code>
/// </example>
/// </summary>
/// <seealso cref="ICommandLineRunner"/>
public interface IBuildRunner
{
    /// <summary>
    /// Runs a build.
    /// <example>
    /// <code>
    /// var runner = GetService&lt;IBuildRunner&gt;();
    /// runner.Build(new DotNetBuild());
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="commandLine">Command line to build.</param>
    /// <param name="handler">Event handler for build events, optional.</param>
    /// <param name="timeout">Time to wait for build running to complete, optional. By default, waits for the end of build running without limit. If the value is exceeded, the command line process and its children will be cancelled.</param>
    /// <returns>Result of building with the command line.</returns>
    /// <seealso cref="BuildAsync"/>
    /// <seealso cref="BuildMessage"/>
    IBuildResult Build(
        ICommandLine commandLine,
        Action<BuildMessage>? handler = null,
        TimeSpan timeout = default);

    /// <summary>
    /// Runs a build in asynchronous way.
    /// <example>
    /// <code>
    /// var runner = GetService&lt;IBuildRunner&gt;();
    /// await runner.BuildAsync(new DotNetBuild());
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="commandLine">Command line to build.</param>
    /// <param name="handler">Event handler for build events, optional.</param>
    /// <param name="cancellationToken">Propagates notification that a running of a build should be canceled, optional.</param>
    /// <returns>Asynchronous build result using the command line.</returns>
    /// <seealso cref="Build"/>
    /// <seealso cref="Task{T}"/>
    /// <seealso cref="BuildMessage"/>
    /// <seealso cref="CancellationToken"/>
    Task<IBuildResult> BuildAsync(
        ICommandLine commandLine,
        Action<BuildMessage>? handler = null,
        CancellationToken cancellationToken = default);
}