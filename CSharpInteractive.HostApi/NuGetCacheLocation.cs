namespace HostApi;

/// <summary>
/// The cache location to list or clear.
/// </summary>
public enum NuGetCacheLocation
{
    /// <summary>
    /// Indicates that the specified operation is applied to all cache types: http-request cache, global packages cache, and the temporary cache.
    /// </summary>
    All,
    
    /// <summary>
    /// Indicates that the specified operation is applied only to the http-request cache. The other cache locations aren't affected.
    /// </summary>
    HttpCache,
    
    /// <summary>
    /// Indicates that the specified operation is applied only to the global packages cache. The other cache locations aren't affected.
    /// </summary>
    GlobalPackages,
    
    /// <summary>
    /// Indicates that the specified operation is applied only to the temporary cache. The other cache locations aren't affected.
    /// </summary>
    Temp
}

internal static class NuGetCacheLocationExtensions
{
    public static string ToArg(this NuGetCacheLocation? cacheLocation) =>
        cacheLocation switch
        {
            NuGetCacheLocation.All => "all",
            NuGetCacheLocation.HttpCache => "http-cache",
            NuGetCacheLocation.GlobalPackages => "global-packages",
            NuGetCacheLocation.Temp => "temp",
            _ => ""
        };
}