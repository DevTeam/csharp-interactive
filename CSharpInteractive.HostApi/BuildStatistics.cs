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
        var initialLength = sb.Length;
        AddValue(sb, GetName("error", Errors), Errors, initialLength);
        AddValue(sb, GetName("warning", Warnings), Warnings, initialLength);
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
            // ReSharper disable once InvertIf
            if (FailedTests > 0 || IgnoredTests > 0 || PassedTests > 0)
            {
                sb.Append(": ");
                initialLength = sb.Length;
                AddValue(sb, "failed", FailedTests, initialLength);
                AddValue(sb, "ignored", IgnoredTests, initialLength);
                AddValue(sb, "passed", PassedTests, initialLength);
            }
        }

        return sb.ToString();
    }

    private static void AddValue(StringBuilder sb, string name, int value, int initialLength)
    {
        if (value <= 0) return;
        if (sb.Length > initialLength)
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