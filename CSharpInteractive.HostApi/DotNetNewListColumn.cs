// ReSharper disable UnusedMember.Global
namespace HostApi;

/// <summary>
/// 
/// </summary>
public enum DotNetNewListColumn
{
    /// <summary>
    /// A comma-separated list of languages supported by the template.
    /// </summary>
    Language,
    
    /// <summary>
    /// The list of template tags.
    /// </summary>
    Tags,
    
    /// <summary>
    /// The template author.
    /// </summary>
    Author,
    
    /// <summary>
    /// The template type: project or item.
    /// </summary>
    Type 
}

// ReSharper disable once UnusedType.Global
internal static class DotNetNewListColumnExtensions
{
    // ReSharper disable once UnusedParameter.Global
    public static string[] ToArgs(this IEnumerable<DotNetNewListColumn> columns, string name, string collectionSeparator)
    {
        var columnsStr = string.Join(",", columns.Select(i => i.ToString()));
        return string.IsNullOrWhiteSpace(columnsStr) ? [] : [name, columnsStr];
    }
}