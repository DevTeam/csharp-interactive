// ReSharper disable UnusedMember.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable HeapView.PossibleBoxingAllocation

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

    public void WriteLine<T>(T line, Color color = Color.Default) => stdOut.WriteLine(new Text(line?.ToString() ?? string.Empty, color));

    public void Error(string? error, string? errorId = default)
    {
        if (error != default)
        {
            log.Error(new ErrorId(errorId ?? "Unknown"), error);
        }
    }

    public void Warning(string? warning)
    {
        if (warning != default)
        {
            log.Warning(warning);
        }
    }

    public void Info(string? text)
    {
        if (text != default)
        {
            log.Info(text);
        }
    }

    public void Trace(string? trace, string? origin = default)
    {
        if (trace != default)
        {
            log.Trace(() => [new Text(trace)], origin ?? string.Empty);
        }
    }

    public T GetService<T>() => Composition.Shared.Root.GetService<T>()!;
}