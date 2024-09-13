// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global

namespace HostApi;

/// <summary>
/// State of a build message. Defines the specifics of the build message, what information it contains.
/// </summary>
public enum BuildMessageState
{
    /// <summary>
    /// Service message of a build. 
    /// </summary>
    ServiceMessage,

    /// <summary>
    /// The message that the string was sent to stdOut. 
    /// </summary>
    StdOut,

    /// <summary>
    /// Build warning.
    /// </summary>
    Warning,

    /// <summary>
    /// The message that the string was sent to stdErr.
    /// </summary>
    StdError,

    /// <summary>
    /// Build error.
    /// </summary>
    Failure,

    /// <summary>
    /// Build problem.
    /// </summary>
    BuildProblem,

    /// <summary>
    /// Indicates that the message contains information about test execution.
    /// </summary>
    TestResult
}