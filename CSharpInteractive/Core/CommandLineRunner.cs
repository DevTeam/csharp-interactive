// ReSharper disable ClassNeverInstantiated.Global

namespace CSharpInteractive.Core;

using HostApi;

internal class CommandLineRunner(
    IHost host,
    IProcessRunner processRunner,
    Func<IProcessMonitor> monitorFactory,
    IProcessResultHandler processResultHandler,
    IStartInfoDescription startInfoDescription,
    ICommandLineStatisticsRegistry statisticsRegistry)
    : ICommandLineRunner
{
    public ICommandLineResult Run(ICommandLine commandLine, Action<Output>? handler = default, TimeSpan timeout = default)
    {
        ArgumentNullException.ThrowIfNull(commandLine);
        var processResult = processRunner.Run(new ProcessInfo(commandLine.GetStartInfo(host), monitorFactory(), ProcessInfo.CreateRunId(), handler), timeout);
        processResultHandler.Handle(processResult, handler);
        var commandLineResult = new CommandLineResult(startInfoDescription, processResult.ProcessInfo.StartInfo, processResult.State, processResult.ElapsedMilliseconds, processResult.ExitCode, processResult.Error);
        var info = new CommandLineInfo(commandLineResult, processResult);
        statisticsRegistry.Register(info);
        return commandLineResult;
    }

    public async Task<ICommandLineResult> RunAsync(ICommandLine commandLine, Action<Output>? handler = default, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(commandLine);
        var processResult = await processRunner.RunAsync(new ProcessInfo(commandLine.GetStartInfo(host), monitorFactory(), ProcessInfo.CreateRunId(), handler), cancellationToken);
        processResultHandler.Handle(processResult, handler);
        var commandLineResult = new CommandLineResult(startInfoDescription, processResult.ProcessInfo.StartInfo, processResult.State, processResult.ElapsedMilliseconds, processResult.ExitCode, processResult.Error);
        var info = new CommandLineInfo(commandLineResult, processResult);
        statisticsRegistry.Register(info);
        return commandLineResult;
    }
}