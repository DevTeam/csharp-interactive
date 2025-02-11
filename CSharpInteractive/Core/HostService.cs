// ReSharper disable UnusedMember.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable HeapView.PossibleBoxingAllocation
// ReSharper disable RedundantExplicitParamsArrayCreation
namespace CSharpInteractive.Core;

using HostApi;
using Microsoft.Extensions.DependencyInjection;

internal class HostService(
    ILog<HostService> log,
    ISettings settings,
    IStdOut stdOut,
    IProperties properties)
    : IHost
{
    public IReadOnlyList<string> Args
    {
        get
        {
            var settingsScriptArguments = settings.ScriptArguments;
            return settingsScriptArguments;
        }
    }

    public IProperties Props { get; } = properties;

    public void WriteLine() => stdOut.WriteLine();

    public void WriteLine<T>(T line, Color color = Color.Default) =>
        stdOut.WriteLine(new Text(line?.ToString() ?? string.Empty, color));

    public void WriteLine(params Text[] line) => stdOut.WriteLine(line);

    public void Error(string? error, string? errorId = null)
    {
        if (error != null)
        {
            log.Error(new ErrorId(errorId ?? "Unknown"), error);
        }
    }

    public void Error(params Text[] error) => log.Error(new ErrorId("Unknown"), error);

    public void Error(string errorId, params Text[] error) => log.Error(new ErrorId("Unknown"), error);

    public void Warning(string? warning)
    {
        if (warning != null)
        {
            log.Warning(warning);
        }
    }

    public void Warning(params Text[] warning) => log.Warning(warning);

    public void Summary(string? summary)
    {
        if (summary != null)
        {
            log.Summary(summary);
        }
    }

    public void Summary(params Text[] summary) => log.Summary(summary);

    public void Info(string? text)
    {
        if (text != null)
        {
            log.Info(text);
        }
    }

    public void Info(params Text[] text) => log.Info(text);

    public void Trace(string? trace, string? origin = null)
    {
        if (trace != null)
        {
            log.Trace(() => [new Text(trace)], origin ?? string.Empty);
        }
    }

    public void Trace(Text[] trace) => log.Trace(() => trace, string.Empty);

    public T GetService<T>() => Composition.Shared.Root.GetService<T>()!;
}