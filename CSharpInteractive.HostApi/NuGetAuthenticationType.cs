// ReSharper disable UnusedMember.Global
namespace HostApi;

/// <summary>
/// NuGet authentication types.
/// </summary>
public enum NuGetAuthenticationType
{
    /// <summary>
    /// Basic 
    /// </summary>
    Basic,
    
    /// <summary>
    /// Negotiate
    /// </summary>
    Negotiate,
    
    /// <summary>
    /// Kerberos 
    /// </summary>
    Kerberos,
    
    /// <summary>
    /// Ntlm 
    /// </summary>
    Ntlm,
    
    /// <summary>
    /// Digest 
    /// </summary>
    Digest
}

// ReSharper disable once UnusedType.Global
internal static class NuGetAuthenticationTypeExtensions
{
    // ReSharper disable once UnusedParameter.Global
    public static string[] ToArgs(this IEnumerable<NuGetAuthenticationType> authenticationTypes, string name, string collectionSeparator)
    {
        var authenticationTypesStr = string.Join(",", authenticationTypes.Select(i => i.ToString().ToLowerInvariant()));
        return string.IsNullOrWhiteSpace(authenticationTypesStr) ? [] : [name, authenticationTypesStr];
    }
}