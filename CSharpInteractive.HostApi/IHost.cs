// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMemberInSuper.Global

namespace HostApi;

using System.Diagnostics.Contracts;

/// <summary>
/// An abstraction for API host.
/// <example>
/// <code>
/// var host = GetService&lt;IHost&gt;();
/// host.WriteLine("Hello!");
/// </code>
/// </example>
/// </summary>
public interface IHost
{
    /// <summary>
    /// List of command line arguments.
    /// <example>
    /// <code>
    /// Info(&quot;First argument: &quot; + (Args.Count &gt; 0 ? Args[0] : &quot;empty&quot;));
    /// </code>
    /// </example>
    /// </summary>
    IReadOnlyList<string> Args { get; }

    /// <summary>
    /// Set of properties.
    /// <example>
    /// <code>
    /// Info(&quot;Version: &quot; + Props.Get(&quot;version&quot;, &quot;1.0.0&quot;));
    /// Props[&quot;version&quot;] = &quot;1.0.1&quot;;
    /// </code>
    /// </example>
    /// </summary>
    /// <seealso cref="IProperties"/>
    /// <seealso cref="IProperties.TryGetValue"/>
    /// <seealso cref="IProperties.this"/>
    IProperties Props { get; }

    /// <summary>
    /// Writes an empty line to stdOut.
    /// <example>
    /// <code>
    /// WriteLine("Hello");
    /// </code>
    /// </example>
    /// </summary>
    void WriteLine();

    /// <summary>
    /// Writes a line to stdOut.
    /// <example>
    /// <code>
    /// WriteLine("Hello !");
    /// WriteLine("Hello !!!", Color.Highlighted);
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="line">Any value that will be converted to a line.</param>
    /// <param name="color">Line color, optional.</param>
    /// <typeparam name="T">The type of the value to be converted to a line.</typeparam>
    void WriteLine<T>(T line, Color color = Color.Default);

    /// <summary>
    /// Writes a line to stdOut.
    /// <example>
    /// <code>
    /// WriteLine("Hello !".WithColor(Color.Highlighted));
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="line">Any value that will be converted to a line.</param>
    void WriteLine(params Text[] line);

    /// <summary>
    /// Writes an error to stdErr. This error will affect the summary run statistics.
    /// <example>
    /// <code>
    /// Error("Error details");
    /// Error("Error details", "ERR327");
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="error">Error message.</param>
    /// <param name="errorId">Unique error identifier, optional.</param>
    void Error(string? error, string? errorId = null);

    /// <summary>
    /// Writes an error to stdErr. This error will affect the summary run statistics.
    /// <example>
    /// <code>
    /// Error(new Text("Some "), new Text("error", Color.Error));
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="error">Error message.</param>
    void Error(params Text[] error);

    /// <summary>
    /// Writes an error to stdErr. This error will affect the summary run statistics.
    /// <example>
    /// <code>
    /// Error("ERR327", new Text("Some "), new Text("error", Color.Error));
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="errorId">Unique error identifier, optional.</param>
    /// <param name="error">Error message.</param>
    void Error(string errorId, params Text[] error);

    /// <summary>
    /// Writes a warning to stdOut. This warning will affect the summary run statistics.
    /// <example>
    /// <code>
    /// Warning("Warning");
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="warning">Warning message.</param>
    void Warning(string? warning);

    /// <summary>
    /// Writes a warning to stdOut. This warning will affect the summary run statistics.
    /// <example>
    /// <code>
    /// Warning(new Text("Some "), new Text("warning", Color.Warning));
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="warning">Warning message.</param>
    void Warning(params Text[] warning);

    /// <summary>
    /// Writes a summary message to stdOut.
    /// <example>
    /// <code>
    /// Info("Some summary");
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="summary">Summary message.</param>
    void Summary(string? summary);

    /// <summary>
    /// Writes a summary message to stdOut.
    /// <example>
    /// <code>
    /// Summary(new Text("Some "), new Text("summary", Color.Highlighted));
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="summary">Summary message.</param>
    void Summary(params Text[] summary);

    /// <summary>
    /// Writes an information message to stdOut.
    /// <example>
    /// <code>
    /// Info("Some info");
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="text">Information message.</param>
    void Info(string? text);

    /// <summary>
    /// Writes an information message to stdOut.
    /// <example>
    /// <code>
    /// Ingo(new Text("Some "), new Text("info", Color.Highlighted));
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="text">Information message.</param>
    void Info(params Text[] text);

    /// <summary>
    /// Writes a trace message to stdOut for the appropriate logging level.
    /// <example>
    /// <code>
    /// Trace("Trace message");
    /// </code>
    /// </example>
    /// <example>
    /// When run as a script:
    /// <code>
    /// #l Diagnostic
    /// Trace("Tracing details.");
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="trace">Trace message.</param>
    /// <param name="origin">Source of the trace message, optional.</param>
    void Trace(string? trace, string? origin = null);

    /// <summary>
    /// Writes a trace message to stdOut for the appropriate logging level.
    /// <example>
    /// <code>
    /// Trace(new Text("Trace message", Color.Details));
    /// </code>
    /// </example>
    /// <example>
    /// When run as a script:
    /// <code>
    /// #l Diagnostic
    /// Trace("Tracing ", "details".WithColor(Color.Details));
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="trace">Trace message.</param>
    void Trace(params Text[] trace);

    /// <summary>
    /// Provides an instance of a service by its type.
    /// <example>
    /// <code>
    /// var nuget = GetService&lt;INuGet&gt;();
    /// </code>
    /// </example>
    /// </summary>
    /// <typeparam name="T">Type of service.</typeparam>
    /// <returns>Service instance.</returns>
    /// <seealso cref="IHost"/>
    /// <seealso cref="ICommandLineRunner"/>
    /// <seealso cref="IBuildRunner"/>
    /// <seealso cref="INuGet"/>
    /// <seealso cref="IServiceProvider"/>
    [Pure]
    T GetService<T>();
}