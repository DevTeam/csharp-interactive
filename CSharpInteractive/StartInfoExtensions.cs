namespace CSharpInteractive;

using System.Text;
using HostApi;

internal static class StartInfoExtensions
{
    public static string GetDescription(this IStartInfo? startInfo, int? processId = default)
    {
        var sb = new StringBuilder();
        if (processId.HasValue)
        {
            sb.Append($"{processId.Value:00000}");
        }

        var shortName = startInfo?.ShortName;
        // ReSharper disable once InvertIf
        if (!string.IsNullOrWhiteSpace(shortName))
        {
            if (sb.Length != 0)
            {
                sb.Append(' ');
            }

            sb.Append(shortName.Escape());
        }

        if (sb.Length == 0)
        {
            sb.Append("The");
        }

        return sb.ToString();
    }
    
    public static IEnumerable<Text> GetDescriptionText(this IStartInfo? startInfo, int? processId = default)
    {
        if (processId.HasValue)
        {
            yield return new Text($"{processId.Value:00000}");
        }

        var sb = new StringBuilder();
        var shortName = startInfo?.ShortName;
        // ReSharper disable once InvertIf
        if (!string.IsNullOrWhiteSpace(shortName))
        {
            if (processId.HasValue)
            {
                sb.Append(' ');
            }

            sb.Append(shortName.Escape());
        }

        if (sb.Length == 0)
        {
            sb.Append("The");
        }

        yield return new Text(sb.ToString(), Color.Highlighted);
    }

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