namespace CSharpInteractive.Core;

internal interface IConsole
{
    void WriteToOut(params (ConsoleColor? color, string output)[] text);

    void WriteToErr(params (ConsoleColor? color, string output)[] text);
}