namespace HostApi;

/// <summary>
/// The severity of diagnostics.
/// </summary>
public enum DotNetFormatSeverity
{
    /// <summary>
    /// Info
    /// </summary>
    Information,
    
    /// <summary>
    /// Warn
    /// </summary>
    Warning,
    
    /// <summary>
    /// Error
    /// </summary>
    Error 
}

// ReSharper disable once UnusedType.Global
internal static class DotNetFormatSeverityExtensions
{
    // ReSharper disable once UnusedParameter.Global
    // ReSharper disable once UnusedMember.Global
    public static string[] ToArgs(this DotNetFormatSeverity? severity, string name, string collectionSeparator) =>
        severity switch
        {
            DotNetFormatSeverity.Information => [name, "info"],
            DotNetFormatSeverity.Warning => [name, "warn"],
            DotNetFormatSeverity.Error => [name, "error"],
            _ => []
        };
}