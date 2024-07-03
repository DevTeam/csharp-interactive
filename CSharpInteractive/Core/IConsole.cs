namespace CSharpInteractive.Core;

internal interface IConsole
{
    void WriteToOut(params (ConsoleColor? color, string output)[] text);

    void WriteToErr(params string[] text);
}