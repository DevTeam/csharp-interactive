namespace CSharpInteractive;

using System.Text;
using HostApi;

[ExcludeFromCodeCoverage]
internal readonly record struct Text(string Value, Color Color)
{
    // ReSharper disable once UnusedMember.Global
    public static readonly Text Empty = new(string.Empty);
    public static readonly Text NewLine = new(System.Environment.NewLine);
    public static readonly Text Space = new(" ");
    public static readonly Text Tab = new("    ");

    public Text(string value)
        // ReSharper disable once IntroduceOptionalParameters.Global
        : this(value, Color.Default)
    { }

    public static implicit operator Text[](Text text) => [text];

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
    
    public static Text[] Join(Text[] text1, Text[] text2)
    {
        var newText = new Text[text1.Length + text2.Length];
        Array.Copy(text1, 0, newText, 0, text1.Length);
        Array.Copy(text2, 0, newText, text1.Length, text2.Length);
        return newText;
    }
}