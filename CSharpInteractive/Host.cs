// ReSharper disable RedundantUsingDirective
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable CheckNamespace

using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Text;
using CSharpInteractive;
using CSharpInteractive.Core;
using HostApi;
using NuGet.Versioning;
// ReSharper disable HeapView.PossibleBoxingAllocation

/// <summary>
/// API host.
/// </summary>
[ExcludeFromCodeCoverage]
[SuppressMessage("Design", "CA1050:Declare types in namespaces")]
#if APPLICATION
public static class Host
#else
public static class Components
#endif
{
    private static readonly Root Root = Composition.Shared.Root;
    private static readonly IHost CurHost = Root.Host;

#if APPLICATION
    private static readonly IDisposable FinishToken;

    static Host()
    {
        if (AppDomain.CurrentDomain.GetAssemblies().Any(i => string.Equals("dotnet-csi", i.GetName().Name, StringComparison.InvariantCultureIgnoreCase)))
        {
            FinishToken = Disposable.Empty;
            return;
        }

        Root.Info.ShowHeader();
        FinishToken = Disposable.Create(Root.ExitTracker.Track(), Root.StatisticsRegistry.Start());
        AppDomain.CurrentDomain.ProcessExit += OnCurrentDomainOnProcessExit;
        AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
    }

    private static void OnCurrentDomainOnProcessExit(object? o, EventArgs eventArgs) =>
        Finish();

    internal static void Finish()
    {
        try
        {
            FinishToken.Dispose();
            Composition.Shared.Dispose();
        }
        catch (Exception ex)
        {
            try
            {
                Root.Log.Error(ErrorId.Unhandled, ex);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }

    private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        try
        {
            if (e.ExceptionObject is Exception error)
            {
                Root.Log.Error(ErrorId.Exception, error);
            }
            else
            {
                Root.Log.Error(ErrorId.Exception, [new Text(e.ExceptionObject.ToString() ?? "Unhandled exception.", Color.Error)]);
            }

            Root.ExitTracker.Exit(1);
        }
        catch
        {
            // ignored
        }
    }
#endif

    /// <summary>
    /// List of command line arguments.
    /// <example>
    /// <code>
    /// Info(&quot;First argument: &quot; + (Args.Count &gt; 0 ? Args[0] : &quot;empty&quot;));
    /// </code>
    /// </example>
    /// </summary>
    public static IReadOnlyList<string> Args => CurHost.Args;

    /// <summary>
    /// Set of properties.
    /// <example>
    /// <code>
    /// Info(&quot;Version: &quot; + Props.Get(&quot;version&quot;, &quot;1.0.0&quot;));
    /// Props[&quot;version&quot;] = &quot;1.0.1&quot;;
    /// </code>
    /// </example>
    /// </summary>
    /// <seealso cref="IProperties.TryGetValue"/>
    /// <seealso cref="IProperties.this"/>
    public static IProperties Props => CurHost.Props;

    /// <summary>
    /// Writes an empty line to stdOut.
    /// <example>
    /// <code>
    /// WriteLine("Hello");
    /// </code>
    /// </example>
    /// </summary>
    public static void WriteLine() => CurHost.WriteLine();

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
    public static void WriteLine<T>(T line, Color color = Color.Default) => CurHost.WriteLine(line, color);

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
    public static void Error(string? error, string? errorId = null) => CurHost.Error(error, errorId);

    /// <summary>
    /// Writes a warning to stdOut. This warning will affect the summary run statistics.
    /// <example>
    /// <code>
    /// Warning("Warning");
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="warning">Warning message.</param>
    public static void Warning(string? warning) => CurHost.Warning(warning);

    /// <summary>
    /// Writes an information message to stdOut.
    /// <example>
    /// <code>
    /// Info("Some info");
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="text">Information message.</param>
    public static void Info(string? text) => CurHost.Info(text);

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
    public static void Trace(string? trace, string? origin = null) => CurHost.Trace(trace, origin);

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
    // ReSharper disable once UnusedMethodReturnValue.Global
    public static T GetService<T>() => CurHost.GetService<T>();

    /// <summary>
    /// Runs a command line.
    /// <example>
    /// <code>
    /// new CommandLine("whoami").Run();
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="commandLine">Command line to run.</param>
    /// <param name="handler">Event handler for running command line events, optional.</param>
    /// <param name="timeout">Time to wait for command line running to complete, optional. By default, waits for the end of running without limit. If the value is exceeded, the command line process and its children will be cancelled.</param>
    /// <returns>The result of a command line running.</returns>
    /// <seealso cref="RunAsync"/>
    /// <seealso cref="Output"/>
    public static ICommandLineResult Run(
        this ICommandLine commandLine,
        Action<Output>? handler = null,
        TimeSpan timeout = default)
    {
        ArgumentNullException.ThrowIfNull(commandLine);
        return Root.CommandLineRunner.Run(commandLine, handler, timeout);
    }

    /// <summary>
    /// Runs a command line in asynchronous way.
    /// <example>
    /// <code>
    /// await new CommandLine("whoami").RunAsync();
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
    public static Task<ICommandLineResult> RunAsync(
        this ICommandLine commandLine,
        Action<Output>? handler = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(commandLine);
        return Root.CommandLineRunner.RunAsync(commandLine, handler, cancellationToken);
    }

    /// <summary>
    /// Runs a build.
    /// <example>
    /// <code>
    /// new DotNetBuild().Build();
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="commandLine">Command line to build.</param>
    /// <param name="handler">Event handler for build events, optional.</param>
    /// <param name="timeout">Time to wait for build running to complete, optional. By default, waits for the end of build running without limit. If the value is exceeded, the command line process and its children will be cancelled.</param>
    /// <returns>Result of building with the command line.</returns>
    /// <seealso cref="BuildAsync"/>
    /// <seealso cref="BuildMessage"/>
    public static IBuildResult Build(
        this ICommandLine commandLine,
        Action<BuildMessage>? handler = null,
        TimeSpan timeout = default)
    {
        ArgumentNullException.ThrowIfNull(commandLine);
        return Root.BuildRunner.Build(commandLine, handler, timeout);
    }

    /// <summary>
    /// Runs a build in asynchronous way.
    /// <example>
    /// <code>
    /// await new DotNetBuild().BuildAsync();
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="commandLine">Command line to build.</param>
    /// <param name="handler">Event handler for build events.</param>
    /// <param name="cancellationToken">Propagates notification that a running of a build should be canceled, optional.</param>
    /// <returns>Asynchronous build result using the command line.</returns>
    /// <seealso cref="Build"/>
    /// <seealso cref="Task{T}"/>
    /// <seealso cref="BuildMessage"/>
    /// <seealso cref="CancellationToken"/>
    public static Task<IBuildResult> BuildAsync(
        this ICommandLine commandLine,
        Action<BuildMessage>? handler = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(commandLine);
        return Root.BuildRunner.BuildAsync(commandLine, handler, cancellationToken);
    }

    /// <summary>
    /// Ensures that the command line or build was completed successfully.
    /// </summary>
    /// <param name="result">Enumeration of results to be verified.</param>
    /// <param name="isSuccess">Function to check the result for success, optional. Returns True if successful, False if not successful and empty if warning.</param>
    /// <param name="failureExitCode">If unsuccessful, terminates with the specified exit code, which is optional and defaults to 1.</param>
    /// <typeparam name="TResult">Result type. Should implement the <see cref="ICommandLineResult"/> interface.</typeparam>
    /// <returns>Simply returns the passed result, for possible later use in a call chain.</returns>
    /// <seealso cref="ICommandLineResult"/>
    /// <seealso cref="IBuildResult"/>
    /// <seealso cref="ICommandLineRunner.Run"/>
    /// <seealso cref="IBuildRunner.Build"/>
    public static IEnumerable<TResult> EnsureSuccess<TResult>(
        this IEnumerable<TResult> result,
        Func<TResult, bool?>? isSuccess = null,
        int? failureExitCode = 1)
        where TResult: ICommandLineResult
    {
        ArgumentNullException.ThrowIfNull(result);
        return EnsureSuccess<IEnumerable<TResult>, TResult>(result, isSuccess, failureExitCode);
    }

    /// <summary>
    /// Ensures that the command line or build was completed successfully in asynchronous way.
    /// </summary>
    /// <param name="result">Enumeration of results to be verified.</param>
    /// <param name="isSuccess">Function to check the result for success, optional. Returns True if successful, False if not successful and empty if warning.</param>
    /// <param name="failureExitCode">If unsuccessful, terminates with the specified exit code, which is optional and defaults to 1.</param>
    /// <typeparam name="TResult">Result type. Should implement the <see cref="ICommandLineResult"/> interface.</typeparam>
    /// <returns>Simply returns the passed result, for possible later use in a call chain.</returns>
    /// <seealso cref="ICommandLineResult"/>
    /// <seealso cref="IBuildResult"/>
    /// <seealso cref="ICommandLineRunner.RunAsync"/>
    /// <seealso cref="IBuildRunner.BuildAsync"/>
    /// <seealso cref="Task{T}"/>
    public static async Task<IEnumerable<TResult>> EnsureSuccess<TResult>(
        this Task<IEnumerable<TResult>> result,
        Func<TResult, bool?>? isSuccess = null,
        int? failureExitCode = 1)
        where TResult: ICommandLineResult
    {
        ArgumentNullException.ThrowIfNull(result);
        return EnsureSuccess<IEnumerable<TResult>, TResult>(await result, isSuccess, failureExitCode);
    }

    /// <summary>
    /// Ensures that the command line or build was completed successfully.
    /// </summary>
    /// <param name="result">Enumeration of results to be verified.</param>
    /// <param name="isSuccess">Function to check the result for success, optional. Returns True if successful, False if not successful and empty if warning.</param>
    /// <param name="failureExitCode">If unsuccessful, terminates with the specified exit code, which is optional and defaults to 1.</param>
    /// <typeparam name="TResult">Result type. Should implement the <see cref="ICommandLineResult"/> interface.</typeparam>
    /// <returns>Simply returns the passed result, for possible later use in a call chain.</returns>
    /// <seealso cref="ICommandLineResult"/>
    /// <seealso cref="IBuildResult"/>
    /// <seealso cref="ICommandLineRunner.Run"/>
    /// <seealso cref="IBuildRunner.Build"/>
    public static TResult[] EnsureSuccess<TResult>(
        this TResult[] result,
        Func<TResult, bool?>? isSuccess = null,
        int? failureExitCode = 1)
        where TResult: ICommandLineResult
    {
        ArgumentNullException.ThrowIfNull(result);
        return EnsureSuccess<TResult[], TResult>(result, isSuccess, failureExitCode);
    }

    /// <summary>
    /// Ensures that the command line or build was completed successfully.
    /// </summary>
    /// <param name="result">Enumeration of results to be verified.</param>
    /// <param name="isSuccess">Function to check the result for success, optional. Returns True if successful, False if not successful and empty if warning.</param>
    /// <param name="failureExitCode">If unsuccessful, terminates with the specified exit code, which is optional and defaults to 1.</param>
    /// <typeparam name="TResult">Result type. Should implement the <see cref="ICommandLineResult"/> interface.</typeparam>
    /// <returns>Simply returns the passed result, for possible later use in a call chain.</returns>
    /// <seealso cref="ICommandLineResult"/>
    /// <seealso cref="IBuildResult"/>
    /// <seealso cref="ICommandLineRunner.RunAsync"/>
    /// <seealso cref="IBuildRunner.BuildAsync"/>
    /// <seealso cref="Task{T}"/>
    public static async Task<TResult[]> EnsureSuccess<TResult>(
        this Task<TResult[]> result,
        Func<TResult, bool?>? isSuccess = null,
        int? failureExitCode = 1)
        where TResult: ICommandLineResult
    {
        ArgumentNullException.ThrowIfNull(result);
        return EnsureSuccess<TResult[], TResult>(await result, isSuccess, failureExitCode);
    }

    /// <summary>
    /// Ensures that the command line or build was completed successfully.
    /// <example>
    /// <code>
    /// new DotNetBuild()
    ///     .Build()
    ///     .EnsureSuccess();
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="result">Enumeration of results to be verified.</param>
    /// <param name="isSuccess">Function to check the result for success, optional. Returns True if successful, False if not successful and empty if warning.</param>
    /// <param name="failureExitCode">If unsuccessful, terminates with the specified exit code, which is optional and defaults to 1.</param>
    /// <typeparam name="TResult">Result type. Should implement the <see cref="ICommandLineResult"/> interface.</typeparam>
    /// <returns>Simply returns the passed result, for possible later use in a call chain.</returns>
    /// <seealso cref="ICommandLineResult"/>
    /// <seealso cref="IBuildResult"/>
    /// <seealso cref="ICommandLineRunner.Run"/>
    /// <seealso cref="IBuildRunner.Build"/>
    public static TResult EnsureSuccess<TResult>(
        this TResult result,
        Func<TResult, bool?>? isSuccess = null,
        int? failureExitCode = 1)
        where TResult: ICommandLineResult
    {
        ArgumentNullException.ThrowIfNull(result);
        return EnsureSuccess([result], isSuccess, failureExitCode).First();
    }

    /// <summary>
    /// Ensures that the command line or build was completed successfully.
    /// <example>
    /// <code>
    /// await new DotNetBuild()
    ///     .BuildAsync()
    ///     .EnsureSuccess();
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="result">Enumeration of results to be verified.</param>
    /// <param name="isSuccess">Function to check the result for success, optional. Returns True if successful, False if not successful and empty if warning.</param>
    /// <param name="failureExitCode">If unsuccessful, terminates with the specified exit code, which is optional and defaults to 1.</param>
    /// <typeparam name="TResult">Result type. Should implement the <see cref="ICommandLineResult"/> interface.</typeparam>
    /// <returns>Simply returns the passed result, for possible later use in a call chain.</returns>
    /// <seealso cref="ICommandLineResult"/>
    /// <seealso cref="IBuildResult"/>
    /// <seealso cref="ICommandLineRunner.RunAsync"/>
    /// <seealso cref="IBuildRunner.BuildAsync"/>
    /// <seealso cref="Task{T}"/>
    public static async Task<TResult> EnsureSuccess<TResult>(
        this Task<TResult> result,
        Func<TResult, bool?>? isSuccess = null,
        int? failureExitCode = 1)
        where TResult: ICommandLineResult
    {
        ArgumentNullException.ThrowIfNull(result);
        return EnsureSuccess(await result, isSuccess, failureExitCode);
    }

    /// <summary>
    /// Tries to get a property by its key.
    /// <example>
    /// <code>
    /// if (Props.TryGet("configuration", out configuration))
    /// {
    ///     // do something
    /// }
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="properties">A set of properties of type <see cref="IProperties"/>.</param>
    /// <param name="key">Property key.</param>
    /// <param name="value">The resulting property converted to type <typeparamref name="T"/>.</param>
    /// <typeparam name="T">Property type.</typeparam>
    /// <returns><c>True</c> if the property is obtained successfully, otherwise <c>False</c>.</returns>
    /// <seealso cref="IProperties"/>
    /// <seealso cref="IProperties.TryGetValue"/>
    /// <seealso cref="IProperties.get_Item"/>
    public static bool TryGet<T>(
        this IProperties properties,
        string key,
        [MaybeNullWhen(false)] out T value)
    {
        ArgumentNullException.ThrowIfNull(properties);
        ArgumentNullException.ThrowIfNull(key);
        if (properties.TryGetValue(key, out var valStr))
        {
            if (typeof(T) == typeof(string))
            {
                value = (T)(object)valStr;
                return true;
            }

            var converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter.CanConvertFrom(typeof(string)))
            {
                var nullableValue = converter.ConvertFrom(valStr);
                if (!Equals(nullableValue, null))
                {
                    try
                    {
                        value = (T)nullableValue;
                        return true;
                    }
                    catch (InvalidCastException)
                    { }
                }
            }
        }

        value = default;
        return false;
    }

    /// <summary>
    /// Gets a property by its key if it is defined and convertible to type <typeparamref name="T"/>, or returns a default value.
    /// <example>
    /// <code>
    /// var configuration = Props.Get("configuration", "Release");
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="properties">A set of properties of type <see cref="IProperties"/></param>
    /// <param name="key">Property key.</param>
    /// <param name="defaultValue">Default value.</param>
    /// <typeparam name="T">Property type.</typeparam>
    /// <returns>The resulting property converted to type <typeparamref name="T"/>.</returns>
    /// <seealso cref="IProperties"/>
    /// <seealso cref="IProperties.TryGetValue"/>
    /// <seealso cref="IProperties.get_Item"/>
    public static T Get<T>(
        this IProperties properties,
        string key,
        T defaultValue)
    {
        ArgumentNullException.ThrowIfNull(properties);
        ArgumentNullException.ThrowIfNull(key);
        return properties.TryGet<T>(key, out var value)
            ? value
            : defaultValue;
    }

    private static T EnsureSuccess<T, TResult>(
        T result,
        Func<TResult, bool?>? isSuccess = null,
        int? failureExitCode = 1)
        where T: IEnumerable<TResult>
        where TResult: ICommandLineResult
    {
        ArgumentNullException.ThrowIfNull(result);
        isSuccess ??= r => r is ISuccessDeterminant successDeterminant ? successDeterminant.IsSuccess : true;
        var hasError = false;
        foreach (var nextResult in result)
        {
            switch (isSuccess(nextResult))
            {
                case true:
                    break;

                case null:
                    Root.Log.Warning($"{nextResult}.");
                    break;

                case false:
                    hasError = true;
                    Root.Log.Error(ErrorId.Build, $"{nextResult}.");
                    break;
            }
        }

        if (hasError && failureExitCode is { } exitCode)
        {
            Root.ExitTracker.Exit(exitCode);
        }

        return result;
    }
}