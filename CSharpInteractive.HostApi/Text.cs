namespace HostApi;

using System.Text;

/// <summary>
/// Represents text with a color.
/// </summary>
/// <param name="Value">Text.</param>
/// <param name="Color">Color of text.</param>
[ExcludeFromCodeCoverage]
[Target]
public readonly record struct Text(string Value = "", Color Color = Color.Default)
{
    /// <summary>
    /// Represents an empty text.
    /// </summary>
    public static readonly Text Empty = new(string.Empty);

    /// <summary>
    /// Represents a new line.
    /// </summary>
    public static readonly Text NewLine = new(Environment.NewLine);

    /// <summary>
    /// Represents a space.
    /// </summary>
    public static readonly Text Space = new(" ");

    /// <summary>
    /// Represents a tab.
    /// </summary>
    public static readonly Text Tab = new("  ");

    /// <summary>
    /// Creates text with the default color.
    /// </summary>
    /// <param name="value">Text.</param>
    public Text(string value)
        // ReSharper disable once IntroduceOptionalParameters.Global
        : this(value, Color.Default)
    { }

    /// <summary>
    /// Implicitly converts a single <see cref="Text"/> instance to an array containing that instance.
    /// </summary>
    /// <param name="text">The text to convert.</param>
    /// <returns>An array containing the single text instance.</returns>
    public static implicit operator Text[](Text text) => [text];

    /// <summary>
    /// Implicitly converts a string to a <see cref="Text"/> instance with default color.
    /// </summary>
    /// <param name="text">The string to convert.</param>
    /// <returns>A Text instance with the specified string value and default color.</returns>
    public static implicit operator Text (string text) => new(text);

    /// <inheritdoc/>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append(Value);
        // ReSharper disable once InvertIf
        if (Color != Color.Default)
        {
            sb.Append('[');
            sb.Append(Color);
            sb.Append(']');
        }

        return sb.ToString();
    }

    /// <summary>
    /// Combines two <see cref="Text"/> instances into an array.
    /// </summary>
    /// <param name="text1">The first text instance.</param>
    /// <param name="text2">The second text instance.</param>
    /// <returns>An array containing both text instances.</returns>
    public static Text[] operator +(Text text1, Text text2)
    {
        var newText = new Text[2];
        newText[0] = text1;
        newText[1] = text2;
        return newText;
    }

    /// <summary>
    /// Appends a <see cref="Text"/> instance to an array of text instances.
    /// </summary>
    /// <param name="text">The array of text instances.</param>
    /// <param name="text2">The text instance to append.</param>
    /// <returns>A new array containing all original text instances with the new text appended at the end.</returns>
    public static Text[] operator +(Text[] text, Text text2)
    {
        var newText = new Text[text.Length + 1];
        Array.Copy(text, 0, newText, 0, text.Length);
        newText[text.Length] = text2;
        return newText;
    }

    /// <summary>
    /// Prepends a <see cref="Text"/> instance to an array of text instances.
    /// </summary>
    /// <param name="text1">The text instance to prepend.</param>
    /// <param name="text">The array of text instances.</param>
    /// <returns>A new array containing the prepended text followed by all original text instances.</returns>
    public static Text[] operator +(Text text1, Text[] text)
    {
        var newText = new Text[text.Length + 1];
        newText[0] = text1;
        Array.Copy(text, 0, newText, 1, text.Length);
        return newText;
    }

    /// <inheritdoc/>
    public bool Equals(Text other) => Value == other.Value && Color == other.Color;

    /// <inheritdoc/>
    public override int GetHashCode() => Value.GetHashCode() ^ 33 + (int)Color;
}