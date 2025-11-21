// ReSharper disable HeapView.PossibleBoxingAllocation
namespace HostApi;

/// <summary>
/// Text extensions.
/// </summary>
public static partial class TextExtensions
{
    /// <summary>
    /// Converts an object to text with color.
    /// </summary>
    /// <param name="it">The object to convert.</param>
    /// <param name="color">Text color.</param>
    /// <returns></returns>
    public static Text WithColor<T>(this T it, Color color = Color.Default) => new(it?.ToString() ?? "", color);
}