namespace HostApi;

/// <summary>
/// Provides information about a running of the command line.
/// </summary>
/// <seealso cref="ICommandLineRunner.Run"/>
/// <seealso cref="ICommandLineRunner.RunAsync"/>
/// <seealso cref="IBuildRunner.Build"/>
/// <seealso cref="IBuildRunner.BuildAsync"/>
public interface ICommandLineResult
{
    /// <summary>
    /// Process startup information.
    /// </summary>
    IStartInfo StartInfo { get; }
    
    /// <summary>
    /// The state of the process completion.
    /// </summary>
    ProcessState State { get; }
    
    /// <summary>
    /// Process execution time in ms.
    /// </summary>
    long ElapsedMilliseconds { get; }
    
    /// <summary>
    /// Process exit code. Empty if the process has been cancelled.
    /// </summary>
    int? ExitCode { get; }
    
    /// <summary>
    /// Exception when the process was executed or empty if it was not.
    /// </summary>
    Exception? Error { get; }
}