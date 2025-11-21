// ReSharper disable UnusedMember.Global
namespace HostApi;

/// <summary>
/// NuGet certificate algorithm.
/// </summary>
public enum NuGetCertificateAlgorithm
{
    /// <summary>
    /// Sha256 
    /// </summary>
    Sha256,
    
    /// <summary>
    /// Sha384
    /// </summary>
    Sha384,
    
    /// <summary>
    /// Sha512
    /// </summary>
    Sha512
}

// ReSharper disable once UnusedType.Global
internal static class NuGetCertificateAlgorithmExtensions
{
    // ReSharper disable once UnusedParameter.Global
    public static string[] ToArgs(this NuGetCertificateAlgorithm? algorithm, string name, string collectionSeparator)
    {
        var algorithmStr = algorithm?.ToString().ToUpperInvariant();
        return algorithmStr is null ? [] : [name, algorithmStr];
    }
}