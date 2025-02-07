// ReSharper disable ClassNeverInstantiated.Global

namespace CSharpInteractive.Core;

using HostApi;

[ExcludeFromCodeCoverage]
internal class Log<T>(
    ISettings settings,
    IStdOut stdOut,
    IStdErr stdErr,
    IStatisticsRegistry statisticsRegistry)
    : ILog<T>
{
    public void Error(ErrorId id, params Text[] error)
    {
        if (error.Length == 0)
        {
            return;
        }

        statisticsRegistry.RegisterError(error);
        stdErr.WriteLine(error);
    }

    public void Warning(params Text[] warning)
    {
        if (warning.Length == 0)
        {
            return;
        }

        statisticsRegistry.RegisterWarning(warning);
        stdOut.WriteLine(warning);
    }

    public void Summary(params Text[] summary)
    {
        if (summary.Length == 0)
        {
            return;
        }

        statisticsRegistry.RegisterSummary(summary);
        stdOut.WriteLine(GetMessage(summary, Color.Highlighted));
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