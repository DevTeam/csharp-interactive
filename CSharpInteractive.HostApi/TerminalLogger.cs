namespace HostApi;

/// <summary>
/// Terminal logger modes.
/// </summary>
public enum TerminalLogger
{
    /// <summary>
    /// First verifies the environment before enabling terminal logging.
    /// </summary>
    Auto,
    
    /// <summary>
    /// Skips the environment check and enables terminal logging.
    /// </summary>
    On,
    
    /// <summary>
    /// Skips the environment check and uses the default console logger.
    /// </summary>
    Off   
}