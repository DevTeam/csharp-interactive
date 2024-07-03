namespace CSharpInteractive.Core;

// ReSharper disable once ClassNeverInstantiated.Global
[ExcludeFromCodeCoverage]
internal class ConsoleInOut(
    IConsole console,
    ITextToColorStrings textToColorStrings,
    IColorTheme colorTheme) : IStdOut, IStdErr
{
    void IStdErr.WriteLine(params Text[] line) => WriteStdErr(line + Text.NewLine);

    public void Write(params Text[] text) => console.WriteToOut(text.SelectMany(i => textToColorStrings.Convert(i.Value, colorTheme.GetConsoleColor(i.Color))).ToArray());

    void IStdOut.WriteLine(params Text[] line) => Write(line + Text.NewLine);

    private void WriteStdErr(params Text[] text) => console.WriteToErr(text.Select(i => i.Value).ToArray());
}