// ReSharper disable UnusedMember.Global
namespace HostApi;

/// <summary>
/// Terminal logger modes.
/// </summary>
public enum DotNetTerminalLogger
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