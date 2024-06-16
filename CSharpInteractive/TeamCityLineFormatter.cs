// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
namespace CSharpInteractive;

using System.Text;
using HostApi;

internal class TeamCityLineFormatter(IColorTheme colorTheme) : ITeamCityLineFormatter
{
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
    internal char EscapeSymbol { get; set; } = '\x001B';

    public string Format(params Text[] line)
    {
        var lastColor = Color.Default;
        var sb = new StringBuilder();
        foreach (var (value, color) in line)
        {
            if (color != lastColor && !string.IsNullOrWhiteSpace(value))
            {
                sb.Append($"{EscapeSymbol}[{colorTheme.GetAnsiColor(color)}m");
                lastColor = color;
            }

            sb.Append(value);
        }

        return sb.ToString();
    }
}