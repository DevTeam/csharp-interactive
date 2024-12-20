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
    [Tag(Tag.Base)] IBuildMessagesProcessor defaultBuildMessagesProcessor,
    [Tag(Tag.Custom)] IBuildMessagesProcessor customBuildMessagesProcessor,
    IProcessResultHandler processResultHandler,
    IStartInfoDescription startInfoDescription,
    ICommandLineStatisticsRegistry statisticsRegistry)
    : IBuildRunner
{
    public IBuildResult Build(ICommandLine commandLine, Action<BuildMessage>? handler = default, TimeSpan timeout = default)
    {
        ArgumentNullException.ThrowIfNull(commandLine);
        var buildContext = buildContextFactory();
        var startInfo = CreateStartInfo(commandLine);
        var processInfo = new ProcessInfo(startInfo, monitorFactory(), ProcessInfo.CreateRunId());
        var info = processInfo;
        processInfo = processInfo.WithHandler(output => Handle(info, handler, output, buildContext));
        var processResult = processRunner.Run(processInfo, timeout);
        processResultHandler.Handle(processResult, handler);
        var buildResult = buildContext.Create(
            new CommandLineResult(startInfoDescription, startInfo, processResult.State, processResult.ElapsedMilliseconds, processResult.ExitCode, processResult.Error));
        var commandLineInfo = new CommandLineInfo(buildResult, processResult);
        statisticsRegistry.Register(commandLineInfo);
        return buildResult;
    }

    public async Task<IBuildResult> BuildAsync(ICommandLine commandLine, Action<BuildMessage>? handler = default, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(commandLine);
        var buildContext = buildContextFactory();
        var startInfo = CreateStartInfo(commandLine);
        var processInfo = new ProcessInfo(startInfo, monitorFactory(), ProcessInfo.CreateRunId());
        var info = processInfo;
        processInfo = processInfo.WithHandler(output => Handle(info, handler, output, buildContext));
        var processResult = await processRunner.RunAsync(processInfo, cancellationToken);
        processResultHandler.Handle(processResult, handler);
        var buildResult = buildContext.Create(
            new CommandLineResult(startInfoDescription, startInfo, processResult.State, processResult.ElapsedMilliseconds, processResult.ExitCode, processResult.Error));
        var commandLineInfo = new CommandLineInfo(buildResult, processResult);
        statisticsRegistry.Register(commandLineInfo);
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

    private void Handle(ProcessInfo processInfo, Action<BuildMessage>? handler, Output output, IBuildContext buildContext)
    {
        var messages = buildOutputProcessor.Convert(output, buildContext).ToList();
        if (handler != default)
        {
            customBuildMessagesProcessor.ProcessMessages(processInfo, output, messages, handler);
        }

        if (!output.Handled)
        {
            defaultBuildMessagesProcessor.ProcessMessages(processInfo, output, messages, EmptyHandler);
        }

        output.Handled = true;
    }

    private static void EmptyHandler(BuildMessage obj)
    { }
}