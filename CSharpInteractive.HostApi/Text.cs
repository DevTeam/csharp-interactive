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
    // ReSharper disable once UnusedMember.Global
    public static readonly Text Empty = new(string.Empty);
    public static readonly Text NewLine = new(Environment.NewLine);
    public static readonly Text Space = new(" ");
    public static readonly Text Tab = new("  ");

    /// <summary>
    /// Creates text with the default color.
    /// </summary>
    /// <param name="value">Text.</param>
    public Text(string value)
        // ReSharper disable once IntroduceOptionalParameters.Global
        : this(value, Color.Default)
    { }

    public static implicit operator Text[](Text text) => [text];

    public static implicit operator Text (string text) => new(text);

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

    public static Text[] operator +(Text text1, Text text2)
    {
        var newText = new Text[2];
        newText[0] = text1;
        newText[1] = text2;
        return newText;
    }

    public static Text[] operator +(Text[] text, Text text2)
    {
        var newText = new Text[text.Length + 1];
        Array.Copy(text, 0, newText, 0, text.Length);
        newText[text.Length] = text2;
        return newText;
    }

    public static Text[] operator +(Text text1, Text[] text)
    {
        var newText = new Text[text.Length + 1];
        newText[0] = text1;
        Array.Copy(text, 0, newText, 1, text.Length);
        return newText;
    }

    public bool Equals(Text other) => Value == other.Value && Color == other.Color;

    public override int GetHashCode() => Value.GetHashCode() ^ 33 + (int)Color;
}