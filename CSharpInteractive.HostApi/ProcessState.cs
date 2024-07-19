namespace HostApi;

/// <summary>
/// The state of the process completion.
/// </summary>
public enum ProcessState
{
    /// <summary>
    /// The process has been completed.
    /// </summary>
    Finished,
    
    /// <summary>
    /// The process failed to start.
    /// </summary>
    FailedToStart,
    
    /// <summary>
    /// The process has been cancelled.
    /// </summary>
    Canceled
}