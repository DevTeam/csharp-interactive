// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive.Core;

using HostApi;

internal class CommandLineRunner(
    IHost host,
    IProcessRunner processRunner,
    Func<IProcessMonitor> monitorFactory,
    IProcessResultHandler processResultHandler)
    : ICommandLineRunner
{
    public int? Run(ICommandLine commandLine, Action<Output>? handler = default, TimeSpan timeout = default)
    {
        var result = processRunner.Run(new ProcessInfo(commandLine.GetStartInfo(host), monitorFactory(), handler), timeout);
        processResultHandler.Handle(result, handler);
        return result.ExitCode;
    }

    public async Task<int?> RunAsync(ICommandLine commandLine, Action<Output>? handler = default, CancellationToken cancellationToken = default)
    {
        var result = await processRunner.RunAsync(new ProcessInfo(commandLine.GetStartInfo(host), monitorFactory(), handler), cancellationToken);
        processResultHandler.Handle(result, handler);
        return result.ExitCode;
    }
}