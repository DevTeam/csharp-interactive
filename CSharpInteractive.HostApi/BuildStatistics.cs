namespace HostApi;

using System.Text;

/// <summary>
/// Summary of a build.
/// </summary>
/// <param name="Errors">Number of build errors.</param>
/// <param name="Warnings">Number of build warnings.</param>
/// <param name="Tests">Number of tests.</param>
/// <param name="FailedTests">Number of failed tests.</param>
/// <param name="IgnoredTests">Number of ignored tests.</param>
/// <param name="PassedTests">Number of successful tests.</param>
[ExcludeFromCodeCoverage]
[Target]
public record BuildStatistics(
    // Number of build errors.
    int Errors = default,
    // Number of build warnings.
    int Warnings = default,
    // Number of tests.
    int Tests = default,
    // Number of failed tests.
    int FailedTests = default,
    // Number of ignored tests.
    int IgnoredTests = default,
    // Number of successful tests.
    int PassedTests = default)
{
    /// <summary>
    /// <c>True></c> if the statistic is empty, <c>False</c> if it contains any information.
    /// </summary>
    public bool IsEmpty =>
        Errors == 0
        && Warnings == 0
        && Tests == 0
        && FailedTests == 0
        && IgnoredTests == 0
        && PassedTests == 0;

    /// <inheritdoc />
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