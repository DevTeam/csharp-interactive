// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable SwitchStatementHandlesSomeKnownEnumValuesWithDefault
namespace CSharpInteractive;

using NuGet.Common;

[ExcludeFromCodeCoverage]
internal class NuGetLogger(ILog<NuGetLogger> log) : ILogger
{
    public void LogDebug(string data) => log.Trace(() => [new Text(data)], "NuGet");

    public void LogVerbose(string data) => log.Trace(() => [new Text(data)], "NuGet");

    public void LogInformation(string data) => log.Info(data);

    public void LogMinimal(string data) => log.Info([new Text(data)]);

    public void LogWarning(string data) => log.Warning(data);

    public void LogError(string data) => log.Error(ErrorId.NuGet, data);

    public void LogInformationSummary(string data) => log.Trace(() => [new Text(data)], "NuGet");

    public void Log(LogLevel level, string data)
    {
        switch (level)
        {
            case LogLevel.Debug:
                LogDebug(data);
                break;

            case LogLevel.Verbose:
                LogVerbose(data);
                break;

            case LogLevel.Information:
                LogInformation(data);
                break;

            case LogLevel.Minimal:
                LogMinimal(data);
                break;

            case LogLevel.Warning:
                LogWarning(data);
                break;

            case LogLevel.Error:
                LogError(data);
                break;
        }
    }

    public Task LogAsync(LogLevel level, string data)
    {
        Log(level, data);
        return Task.CompletedTask;
    }

    public void Log(ILogMessage message) => Log(message.Level, message.Message);

    public Task LogAsync(ILogMessage message)
    {
        Log(message);
        return Task.CompletedTask;
    }
}