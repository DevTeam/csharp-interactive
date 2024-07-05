namespace HostApi;

using System.Text;

[ExcludeFromCodeCoverage]
[Target]
public record BuildStatistics(
    int Errors = default,
    int Warnings = default,
    int Tests = default,
    int FailedTests = default,
    int IgnoredTests = default,
    int PassedTests = default)
{
    public bool IsEmpty =>
        Errors == 0
        && Warnings == 0
        && Tests == 0
        && FailedTests == 0
        && IgnoredTests == 0
        && PassedTests == 0;

    public override string ToString()
    {
        if (IsEmpty)
        {
            return "Empty";
        }

        var sb = new StringBuilder();
        AddValue(sb, GetName("error", Errors), Errors);
        AddValue(sb, GetName("warning", Warnings), Warnings);
        // ReSharper disable once InvertIf
        if (Tests > 0)
        {
            if (sb.Length > 0)
            {
                sb.Append(" and ");
            }
            
            sb.Append(Tests);
            sb.Append(" finished ");
            sb.Append(GetName("test", Tests));
            sb.Append(": ");
            AddValue(sb, "failed", FailedTests, false);
            AddValue(sb, "ignored", IgnoredTests);
            AddValue(sb, "passed", PassedTests);
        }

        return sb.ToString();
    }

    private static void AddValue(StringBuilder sb, string name, int value, bool addSeparator = true)
    {
        if (value <= 0) return;
        if (addSeparator && sb.Length > 0)
        {
            sb.Append(", ");
        }
            
        sb.Append(value);
        sb.Append(' ');
        sb.Append(name);
    }

    private static string GetName(string baseName, int count) =>
        count switch
        {
            1 => baseName,
            _ => baseName + 's'
        };
}