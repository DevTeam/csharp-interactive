namespace HostApi;

/// <summary>
/// Represents the state of a test.
/// </summary>
public enum TestState
{
    // The test was finished, but that doesn't mean it was successful.
    Finished,
    
    // The test was ignored.
    Ignored,
    
    // The test was failed.
    Failed
}