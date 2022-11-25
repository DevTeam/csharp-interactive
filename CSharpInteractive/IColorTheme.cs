// ReSharper disable UnusedMember.Global
namespace CSharpInteractive;

using HostApi;

internal interface IColorTheme
{
    public ConsoleColor GetConsoleColor(Color color);

    string GetAnsiColor(Color color);
}