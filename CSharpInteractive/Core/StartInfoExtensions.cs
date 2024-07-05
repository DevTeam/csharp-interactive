namespace CSharpInteractive.Core;

using System.Text;
using HostApi;

internal static class StartInfoExtensions
{
    public static string Escape(this string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return text ?? string.Empty;
        }

        // ReSharper disable once InvertIf
        if (text.Contains(' '))
        {
            var trimmed = text.Trim();
            if (!trimmed.StartsWith('"') && !trimmed.EndsWith('"'))
            {
                return $"\"{text}\"";
            }
        }

        return text;
    }
}