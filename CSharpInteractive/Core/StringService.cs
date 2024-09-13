// ReSharper disable ClassNeverInstantiated.Global

namespace CSharpInteractive.Core;

internal class StringService : IStringService
{
    public string TrimAndUnquote(string quotedString)
    {
        var str = quotedString.Trim();
        if (str.StartsWith('"') && str.EndsWith('"'))
        {
            return str.Substring(1, str.Length - 2);
        }

        return str;
    }
}