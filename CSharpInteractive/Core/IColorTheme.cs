// ReSharper disable UnusedMember.Global

namespace CSharpInteractive.Core;

using HostApi;

internal interface IColorTheme
{
    public ConsoleColor GetConsoleColor(Color color);

    string GetAnsiColor(Color color);
}