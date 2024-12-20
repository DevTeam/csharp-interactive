// ReSharper disable ClassNeverInstantiated.Global

namespace CSharpInteractive.Core;

using System.Diagnostics;
using HostApi;
using JetBrains.TeamCity.ServiceMessages.Write.Special;
using Pure.DI;

internal class ProcessInFlowRunner(
    [Tag(Tag.Base)] IProcessRunner baseProcessRunner,
    ICISettings ciSettings,
    ITeamCityWriter teamCityWriter,
    IFlowContext flowContext)
    : IProcessRunner
{
    public ProcessResult Run(ProcessInfo processInfo, TimeSpan timeout)
    {
        using var flow = CreateFlow();
        return baseProcessRunner.Run(processInfo.WithStartInfo(WrapInFlow(processInfo.StartInfo)), timeout);
    }

    public async Task<ProcessResult> RunAsync(ProcessInfo processInfo, CancellationToken cancellationToken)
    {
        using var flow = CreateFlow();
        var result = await baseProcessRunner.RunAsync(processInfo.WithStartInfo(WrapInFlow(processInfo.StartInfo)), cancellationToken);
        return result;
    }

    private IStartInfo WrapInFlow(IStartInfo startInfo) =>
        ciSettings.CIType == CIType.TeamCity
            ? new StartInfoInFlow(startInfo, flowContext.CurrentFlowId)
            : startInfo;

    private IDisposable CreateFlow() =>
        ciSettings.CIType == CIType.TeamCity ? teamCityWriter.OpenFlow() : Disposable.Empty;

    [DebuggerTypeProxy(typeof(CommandLine.CommandLineDebugView))]
    private class StartInfoInFlow(IStartInfo baseStartIfo, string flowId) : IStartInfo
    {
        public string ShortName => baseStartIfo.ShortName;

        public string ExecutablePath => baseStartIfo.ExecutablePath;

        public string WorkingDirectory => baseStartIfo.WorkingDirectory;

        public IEnumerable<string> Args => baseStartIfo.Args;

        public IEnumerable<(string name, string value)> Vars =>
            new[] {(FlowIdEnvironmentVariableName: CISettings.TeamCityFlowIdEnvironmentVariableName, _flowId: flowId)}
                .Concat(baseStartIfo.Vars);

        public override string? ToString() => baseStartIfo.ToString();
    }
}