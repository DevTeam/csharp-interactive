namespace HostApi;

/// <summary>
///  The .NET language.
/// </summary>
public enum DotNetLanguage
{
    /// <summary>
    /// C#
    /// </summary>
    CSharp,
    
    /// <summary>
    /// F#
    /// </summary>
    FSharp,
    
    /// <summary>
    /// Visual Basic
    /// </summary>
    VisualBasic,
    
    /// <summary>
    /// SQL
    /// </summary>
    Sql,
    
    /// <summary>
    /// JSON 
    /// </summary>
    Json,
    
    /// <summary>
    /// TypeScript 
    /// </summary>
    TypeScript
}

internal static class DotNetLanguageExtensions
{
    // ReSharper disable once UnusedParameter.Global
    public static string[] ToArgs(this DotNetLanguage? language, string name, string collectionSeparator) =>
        language switch
        {
            DotNetLanguage.CSharp => [name, "\"C#\""],
            DotNetLanguage.FSharp => [name, "\"F#\""],
            DotNetLanguage.VisualBasic => [name, "VB"],
            DotNetLanguage.Sql => [name, "VB"],
            DotNetLanguage.Json => [name, "JSON"],
            DotNetLanguage.TypeScript => [name, "TypeScript"],
            _ => []
        };
}