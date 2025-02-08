// ReSharper disable ClassNeverInstantiated.Global

namespace CSharpInteractive.Core;

using HostApi;
using JetBrains.TeamCity.ServiceMessages.Write.Special;

internal class TeamCityLog<T>(
    ISettings settings,
    ITeamCityWriter teamCityWriter,
    ITeamCityLineFormatter lineFormatter,
    IStatisticsRegistry statisticsRegistry)
    : ILog<T>
{
    public void Error(ErrorId id, params Text[] error)
    {
        var message = error.ToSimpleString();
        statisticsRegistry.Register(StatisticsType.Error, error.WithDefaultColor(Color.Error));
        teamCityWriter.WriteBuildProblem(id.Id, message);
    }

    public void Warning(params Text[] warning)
    {
        var message = warning.ToSimpleString();
        statisticsRegistry.Register(StatisticsType.Warning, warning.WithDefaultColor(Color.Warning));
        teamCityWriter.WriteWarning(message);
    }

    public void Summary(params Text[] summary)
    {
        summary = summary.WithDefaultColor(Color.Highlighted);
        statisticsRegistry.Register(StatisticsType.Summary, summary);
        teamCityWriter.WriteMessage(lineFormatter.Format(summary));
    }

    public void Info(params Text[] message)
    {
        if (settings.VerbosityLevel >= VerbosityLevel.Normal)
        {
            teamCityWriter.WriteMessage(lineFormatter.Format(message.WithDefaultColor(Color.Default)));
        }
    }

    public void Trace(Func<Text[]> traceMessagesFactory, string origin)
    {
        // ReSharper disable once InvertIf
        if (settings.VerbosityLevel >= VerbosityLevel.Diagnostic)
        {
            origin = string.IsNullOrWhiteSpace(origin) ? typeof(T).Name : origin.Trim();
            teamCityWriter.WriteMessage(lineFormatter.Format((new Text($"{origin,-40}") + traceMessagesFactory()).WithDefaultColor(Color.Trace)));
        }
    }
}