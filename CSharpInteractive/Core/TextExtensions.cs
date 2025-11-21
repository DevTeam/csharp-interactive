// ReSharper disable UnusedMember.Global

namespace CSharpInteractive.Core;

using HostApi;

internal static class TextExtensions
{
    private static readonly Text[] SingleTab = [Text.Tab];

    public static string ToSimpleString(this IEnumerable<Text> text) =>
        string.Join("", text.Select(i => i.Value));

    extension(Text[] text)
    {
        public Text[] WithDefaultColor(Color defaultColor)
        {
            var newText = new Text[text.Length];
            for (var i = 0; i < newText.Length; i++)
            {
                var (value, color) = text[i];
                newText[i] = new Text(value, color == Color.Default ? defaultColor : color);
            }

            return newText;
        }

        public Text[] Join(Text[] text2)
        {
            var newText = new Text[text.Length + text2.Length];
            Array.Copy(text, 0, newText, 0, text.Length);
            Array.Copy(text2, 0, newText, text.Length, text2.Length);
            return newText;
        }

        public bool IsEmpty() =>
            text.Length == 0 || text.Sum(i => i.Value.Length) == 0;

        public bool IsEmptyOrWhiteSpace() =>
            text.Length == 0 || text.All(i => string.IsNullOrWhiteSpace(i.Value));

        public Text[] AddPrefix(Func<int, Text[]> prefixSelector)
        {
            var newText = new List<Text>();
            newText.AddRange(prefixSelector(0));
            var index = 1;
            foreach (var item in text)
            {
                newText.Add(item);
                if (item.Value != Text.NewLine.Value)
                {
                    continue;
                }

                newText.AddRange(prefixSelector(index));
                index++;
            }

            return newText.ToArray();
        }

        public Text[] Trim() =>
            TrimStart(
                    TrimStart(text, i => i.TrimStart()).Reverse(),
                    i => i.TrimEnd())
                .Reverse()
                .ToArray();
    }

    extension (Exception error)
    {
        public Text[] ToText()
        {
            var text = new List<Text>();
            var exception = error;
            var prefix = new List<Text>();
            while (exception is not null)
            {
                if (text.Count > 0)
                {
                    text.Add(Text.NewLine);
                }

                text.AddRange(exception.SingleErrorToText().AddPrefix(_ => prefix.ToArray()));
                exception = exception.InnerException;
                prefix.Add(Text.Tab);
            }

            return text.ToArray();
        }

        private Text[] SingleErrorToText()
        {
            var text = new List<Text>();
            text.AddRange($"{error.GetType()}: {error.Message.Trim()}".SplitLines(Color.Error));
            text.Add(Text.NewLine);
            if (error.StackTrace is { } stackTrace)
            {
                text.AddRange(stackTrace.Trim().SplitLines(Color.Details).AddPrefix(_ => SingleTab));
            }

            return text.ToArray();
        }
    }

    public static Text[] SplitLines(this string str, Color color = Color.Default, int maxLines = int.MaxValue)
    {
        var text = new List<Text>();
        var lines = str.Split('\n');
        var counter = 0;
        foreach (var line in lines)
        {
            // ReSharper disable once InvertIf
            var dif = lines.Length - counter;
            if (counter >= maxLines && dif > 0)
            {
                text.Add(Text.NewLine);
                text.Add(new Text($"... +{lines.Length - counter} line{(dif > 1 ? "s" : "")}", Color.Trace));
                break;
            }

            if (text.Count > 0)
            {
                text.Add(Text.NewLine);
            }

            text.Add(new Text(line.TrimEnd(), color));
            counter++;
        }

        return text.ToArray();
    }

    private static IEnumerable<Text> TrimStart(this IEnumerable<Text> text, Func<string, string> trim)
    {
        var started = true;
        foreach (var item in text)
        {
            if (!started)
            {
                if (string.IsNullOrWhiteSpace(item.Value))
                {
                    continue;
                }

                started = true;
                yield return item with {Value = trim(item.Value)};
                continue;
            }

            yield return item;
        }
    }
}