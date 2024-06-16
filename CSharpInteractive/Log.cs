// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive;

using HostApi;

[ExcludeFromCodeCoverage]
internal class Log<T>(
    ISettings settings,
    IStdOut stdOut,
    IStdErr stdErr,
    IStatistics statistics)
    : ILog<T>
{
    public void Error(ErrorId id, params Text[] error)
    {
        if (error.Length == 0)
        {
            return;
        }

        statistics.RegisterError(string.Join("", error.Select(i => i.Value)));
        stdErr.WriteLine(GetMessage(error, Color.Default));
    }

    public void Warning(params Text[] warning)
    {
        if (warning.Length == 0)
        {
            return;
        }

        statistics.RegisterWarning(string.Join("", warning.Select(i => i.Value)));
        stdOut.WriteLine(GetMessage(warning, Color.Warning));
    }

    public void Info(params Text[] message)
    {
        if (settings.VerbosityLevel >= VerbosityLevel.Normal)
        {
            stdOut.WriteLine(GetMessage(message, Color.Default));
        }
    }

    public void Trace(Func<Text[]> traceMessagesFactory, string origin)
    {
        // ReSharper disable once InvertIf
        if (settings.VerbosityLevel >= VerbosityLevel.Diagnostic)
        {
            origin = string.IsNullOrWhiteSpace(origin) ? typeof(T).Name : origin.Trim();
            stdOut.WriteLine(GetMessage(new Text($"{origin,-40}") + traceMessagesFactory(), Color.Trace));
        }
    }

    private static Text[] GetMessage(Text[] message, Color defaultColor) => message.WithDefaultColor(defaultColor);
}