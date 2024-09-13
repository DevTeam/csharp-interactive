namespace HostApi;

/// <summary>
/// Represents the state of a test.
/// </summary>
public enum TestState
{
    /// <summary>
    /// The test was finished, but that doesn't mean it was successful. 
    /// </summary>
    Finished,

    /// <summary>
    /// The test was ignored.
    /// </summary>
    Ignored,

    /// <summary>
    /// The test was failed. 
    /// </summary>
    Failed
}