namespace HostApi;

/// <summary>
/// 
/// </summary>
public enum NuGetCertificateAlgorithm
{
    /// <summary>
    /// 
    /// </summary>
    Sha256,
    
    /// <summary>
    /// 
    /// </summary>
    Sha384,
    
    /// <summary>
    /// 
    /// </summary>
    Sha512
}

internal static class NuGetCertificateAlgorithmExtensions
{
    public static string[] ToArgs(this NuGetCertificateAlgorithm? algorithm, string name)
    {
        var algorithmStr = algorithm?.ToString().ToUpperInvariant();
        return algorithmStr is null ? [] : [name, algorithmStr];
    }
}