// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive.Core;

using HostApi;

internal class CommandLineRunner(
    IHost host,
    IProcessRunner processRunner,
    Func<IProcessMonitor> monitorFactory,
    IProcessResultHandler processResultHandler,
    IStartInfoDescription startInfoDescription)
    : ICommandLineRunner
{
    public ICommandLineResult Run(ICommandLine commandLine, Action<Output>? handler = default, TimeSpan timeout = default)
    {
        var result = processRunner.Run(new ProcessInfo(commandLine.GetStartInfo(host), monitorFactory(), handler), timeout);
        processResultHandler.Handle(result, handler);
        return new CommandLineResult(startInfoDescription, result.StartInfo, result.State, result.ElapsedMilliseconds, result.ExitCode, result.Error);
    }

    public async Task<ICommandLineResult> RunAsync(ICommandLine commandLine, Action<Output>? handler = default, CancellationToken cancellationToken = default)
    {
        var result = await processRunner.RunAsync(new ProcessInfo(commandLine.GetStartInfo(host), monitorFactory(), handler), cancellationToken);
        processResultHandler.Handle(result, handler);
        return new CommandLineResult(startInfoDescription, result.StartInfo, result.State, result.ElapsedMilliseconds, result.ExitCode, result.Error);
    }
}