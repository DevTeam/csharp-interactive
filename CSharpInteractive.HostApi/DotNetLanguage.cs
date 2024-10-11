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
    SQL,
    
    /// <summary>
    /// JSON 
    /// </summary>
    JSON,
    
    /// <summary>
    /// TypeScript 
    /// </summary>
    TypeScript
}

internal static class DotNetLanguageExtensions
{
    public static string[] ToArgs(this DotNetLanguage? language, string name) =>
        language switch
        {
            DotNetLanguage.CSharp => [name, "\"C#\""],
            DotNetLanguage.FSharp => [name, "\"F#\""],
            DotNetLanguage.VisualBasic => [name, "VB"],
            DotNetLanguage.SQL => [name, "VB"],
            DotNetLanguage.JSON => [name, "JSON"],
            DotNetLanguage.TypeScript => [name, "TypeScript"],
            _ => []
        };
}