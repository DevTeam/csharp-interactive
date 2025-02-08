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

        error = error.WithDefaultColor(Color.Error);
        statisticsRegistry.Register(StatisticsType.Error, error);
        stdErr.WriteLine(error);
    }

    public void Warning(params Text[] warning)
    {
        if (warning.Length == 0)
        {
            return;
        }

        warning = warning.WithDefaultColor(Color.Warning);
        statisticsRegistry.Register(StatisticsType.Warning, warning);
        stdOut.WriteLine(warning);
    }

    public void Summary(params Text[] summary)
    {
        if (summary.Length == 0)
        {
            return;
        }

        summary = summary.WithDefaultColor(Color.Highlighted);
        statisticsRegistry.Register(StatisticsType.Summary, summary);
        stdOut.WriteLine(summary);
    }

    public void Info(params Text[] message)
    {
        if (settings.VerbosityLevel >= VerbosityLevel.Normal)
        {
            stdOut.WriteLine(message.WithDefaultColor(Color.Default));
        }
    }

    public void Trace(Func<Text[]> traceMessagesFactory, string origin)
    {
        // ReSharper disable once InvertIf
        if (settings.VerbosityLevel >= VerbosityLevel.Diagnostic)
        {
            origin = string.IsNullOrWhiteSpace(origin) ? typeof(T).Name : origin.Trim();
            stdOut.WriteLine((new Text($"{origin,-40}") + traceMessagesFactory()).WithDefaultColor(Color.Trace));
        }
    }
}