// ReSharper disable InconsistentNaming
// ReSharper disable UnusedType.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable StringLiteralTypo
// ReSharper disable InvertIf
// ReSharper disable UseDeconstructionOnParameter
namespace CSharpInteractive;

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
    IProcessResultHandler processResultHandler)
    : IBuildRunner
{

    public IBuildResult Run(ICommandLine commandLine, Action<BuildMessage>? handler = default, TimeSpan timeout = default)
    {
        var buildContext = buildContextFactory();
        var startInfo = CreateStartInfo(commandLine);
        var processInfo = new ProcessInfo(startInfo, monitorFactory(), output => Handle(handler, output, buildContext));
        var result = processRunner.Run(processInfo, timeout);
        processResultHandler.Handle(result, handler);
        return buildContext.Create(startInfo, result.ExitCode);
    }

    public async Task<IBuildResult> RunAsync(ICommandLine commandLine, Action<BuildMessage>? handler = default, CancellationToken cancellationToken = default)
    {
        var buildContext = buildContextFactory();
        var startInfo = CreateStartInfo(commandLine);
        var processInfo = new ProcessInfo(startInfo, monitorFactory(), output => Handle(handler, output, buildContext));
        var result = await processRunner.RunAsync(processInfo, cancellationToken);
        processResultHandler.Handle(result, handler);
        return buildContext.Create(startInfo, result.ExitCode);
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

    private void Handle(Action<BuildMessage>? handler, in Output output, IBuildContext buildContext)
    {
        var messages = buildOutputProcessor.Convert(output, buildContext);
        if (handler != default)
        {
            customBuildMessagesProcessor.ProcessMessages(output, messages, handler);
        }
        else
        {
            defaultBuildMessagesProcessor.ProcessMessages(output, messages, EmptyHandler);
        }
    }

    private static void EmptyHandler(BuildMessage obj) { }
}