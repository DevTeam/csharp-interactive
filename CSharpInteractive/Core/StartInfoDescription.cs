// ReSharper disable ClassNeverInstantiated.Global

namespace CSharpInteractive.Core;

using System.Text;
using HostApi;

internal class StartInfoDescription : IStartInfoDescription
{
    public string GetDescription(IStartInfo? startInfo, int? processId = null)
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
            sb.Append("The process");
        }

        return sb.ToString();
    }

    public IEnumerable<Text> GetDescriptionText(IStartInfo? startInfo, int? processId = null)
    {
        if (processId.HasValue)
        {
            yield return new Text($"{processId.Value:00000}", Color.Header);
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
            sb.Append("The process");
        }

        yield return new Text(sb.ToString(), Color.Highlighted);
    }
}