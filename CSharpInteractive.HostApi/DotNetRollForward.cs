// ReSharper disable UnusedMember.Global
namespace HostApi;

/// <summary>
/// Controls how roll forward is applied to the app. 
/// </summary>
public enum DotNetRollForward
{
    /// <summary>
    /// Roll forward to the highest patch version. This disables minor version roll forward. 
    /// </summary>
    LatestPatch,
    
    /// <summary>
    /// Roll forward to the lowest higher minor version, if requested minor version is missing. If the requested minor version is present, then the LatestPatch policy is used.
    /// </summary>
    Minor,
    
    /// <summary>
    /// Roll forward to lowest higher major version, and lowest minor version, if requested major version is missing. If the requested major version is present, then the Minor policy is used.
    /// </summary>
    Major,
    
    /// <summary>
    /// Roll forward to highest minor version, even if requested minor version is present. Intended for component hosting scenarios.
    /// </summary>
    LatestMinor,
    
    /// <summary>
    /// Roll forward to highest major and highest minor version, even if requested major is present. Intended for component hosting scenarios.
    /// </summary>
    LatestMajor,
    
    /// <summary>
    /// Don't roll forward. Only bind to specified version. This policy isn't recommended for general use because it disables the ability to roll forward to the latest patches. This value is only recommended for testing.
    /// </summary>
    Disable
}