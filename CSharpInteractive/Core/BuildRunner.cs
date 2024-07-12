// ReSharper disable InconsistentNaming
// ReSharper disable UnusedType.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable StringLiteralTypo
// ReSharper disable InvertIf
// ReSharper disable UseDeconstructionOnParameter
namespace CSharpInteractive.Core;

using HostApi;
using Pure.DI;

internal class BuildRunner(
    IProcessRunner processRunner,
    IHost host,
    ITeamCityContext teamCityContext,
    Func<IBuildContext> buildContextFactory,
    IBuildOutputProcessor buildOutputProcessor,
    Func<IProcessMonitor> monitorFactory,
    [Tag("default")] IBuildMessagesProcessor defaultBuildMessagesProcessor,
    [Tag("custom")] IBuildMessagesProcessor customBuildMessagesProcessor,
    IProcessResultHandler processResultHandler,
    IStartInfoDescription startInfoDescription,
    ICommandLineStatisticsRegistry statisticsRegistry)
    : IBuildRunner
{
    public IBuildResult Run(ICommandLine commandLine, Action<BuildMessage>? handler = default, TimeSpan timeout = default)
    {
        ArgumentNullException.ThrowIfNull(commandLine);
        var buildContext = buildContextFactory();
        var startInfo = CreateStartInfo(commandLine);
        var processInfo = new ProcessInfo(startInfo, monitorFactory(), output => Handle(handler, output, buildContext));
        var processResult = processRunner.Run(processInfo, timeout);
        processResultHandler.Handle(processResult, handler);
        var buildResult = buildContext.Create(
            new CommandLineResult(startInfoDescription, startInfo, processResult.State, processResult.ElapsedMilliseconds, processResult.ExitCode, processResult.Error));
        statisticsRegistry.Register(new CommandLineInfo(buildResult, processResult));
        return buildResult;
    }

    public async Task<IBuildResult> RunAsync(ICommandLine commandLine, Action<BuildMessage>? handler = default, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(commandLine);
        var buildContext = buildContextFactory();
        var startInfo = CreateStartInfo(commandLine);
        var processInfo = new ProcessInfo(startInfo, monitorFactory(), output => Handle(handler, output, buildContext));
        var processResult = await processRunner.RunAsync(processInfo, cancellationToken);
        processResultHandler.Handle(processResult, handler);
        var buildResult = buildContext.Create(
            new CommandLineResult(startInfoDescription, startInfo, processResult.State, processResult.ElapsedMilliseconds, processResult.ExitCode, processResult.Error));
        statisticsRegistry.Register(new CommandLineInfo(buildResult, processResult));
        return buildResult;
    }

    private IStartInfo CreateStartInfo(ICommandLine commandLine)
    {
        try
        {
            teamCityContext.TeamCityIntegration = true;
            return commandLine.GetStartInfo(host);
        }
        finally
        {
            teamCityContext.TeamCityIntegration = false;
        }
    }

    private void Handle(Action<BuildMessage>? handler, Output output, IBuildContext buildContext)
    {
        var messages = buildOutputProcessor.Convert(output, buildContext).ToList();
        if (handler != default)
        {
            customBuildMessagesProcessor.ProcessMessages(output, messages, handler);
        }
        
        if (!output.Handled)
        {
            defaultBuildMessagesProcessor.ProcessMessages(output, messages, EmptyHandler);
        }
        
        output.Handled = true;
    }

    private static void EmptyHandler(BuildMessage obj) { }
}