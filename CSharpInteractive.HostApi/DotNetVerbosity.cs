// ReSharper disable UnusedMember.Global
namespace HostApi;

/// <summary>
/// Represents a verbosity level of the output.
/// </summary>
public enum DotNetVerbosity
{
    /// <summary>
    /// Quiet verbosity.    
    /// </summary>
    Quiet,
    
    /// <summary>
    /// Minimal verbosity.
    /// </summary>
    Minimal,
    
    /// <summary>
    /// Normal verbosity.
    /// </summary>
    Normal,
    
    /// <summary>
    /// Detailed verbosity.
    /// </summary>
    Detailed,
    
    /// <summary>
    /// Diagnostic verbosity.
    /// </summary>
    Diagnostic
}