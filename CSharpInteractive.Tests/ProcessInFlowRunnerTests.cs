namespace CSharpInteractive.Tests;

using Core;
using HostApi;
using JetBrains.TeamCity.ServiceMessages.Write.Special;

public class ProcessInFlowRunnerTests
{
    private readonly Mock<IProcessRunner> _baseProcessRunner = new();
    private readonly Mock<IStartInfo> _startInfo = new();
    private readonly Mock<ICISettings> _teamCitySettings = new();
    private readonly Mock<ITeamCityWriter> _teamCityWriter = new();
    private readonly Mock<ITeamCityWriter> _blockWriter = new();
    private readonly Mock<IFlowContext> _flowContext = new();
    private readonly Mock<IProcessMonitor> _monitor = new();
    private static readonly (string name, string value)[] InitialVars = [("Var1", "Val 1")];
    private static readonly (string name, string value)[] ModifiedVars = new (string name, string value)[] {(CISettings.TeamCityFlowIdEnvironmentVariableName, "FlowId123")}.Concat(InitialVars).ToArray();
    private readonly ProcessInfo _processInfo;
    private readonly ProcessResult _processResult;


    public ProcessInFlowRunnerTests()
    {
        _processInfo = new ProcessInfo(_startInfo.Object, _monitor.Object, 1, Handler);
        _processResult = new ProcessResult(_processInfo, ProcessState.Finished, 33, [], 12);
        _flowContext.SetupGet(i => i.CurrentFlowId).Returns("FlowId123");
        _startInfo.SetupGet(i => i.Vars).Returns(InitialVars);
        _teamCityWriter.Setup(i => i.OpenFlow()).Returns(_blockWriter.Object);
    }

    [Fact]
    public void ShouldRunInFlow()
    {
        // Given
        _teamCitySettings.SetupGet(i => i.CIType).Returns(CIType.TeamCity);
        _baseProcessRunner.Setup(i => i.Run(It.Is<ProcessInfo>(processRun => processRun.StartInfo.Vars.SequenceEqual(ModifiedVars)), TimeSpan.FromDays(1))).Returns(_processResult);
        var runner = CreateInstance();

        // When
        var result = runner.Run(_processInfo, TimeSpan.FromDays(1));

        // Then
        _teamCityWriter.Verify(i => i.OpenFlow());
        _blockWriter.Verify(i => i.Dispose());
        result.ShouldBe(_processResult);
    }

    [Fact]
    public void ShouldRunWithoutFlowWhenNotUnderTeamCity()
    {
        // Given
        _teamCitySettings.SetupGet(i => i.CIType).Returns(CIType.Unknown);
        _baseProcessRunner.Setup(i => i.Run(It.IsAny<ProcessInfo>(), TimeSpan.FromDays(1))).Returns(_processResult);
        var runner = CreateInstance();

        // When
        var result = runner.Run(_processInfo, TimeSpan.FromDays(1));

        // Then
        _teamCityWriter.Verify(i => i.OpenFlow(), Times.Never);
        _blockWriter.Verify(i => i.Dispose(), Times.Never);
        result.ShouldBe(_processResult);
    }

    [Fact]
    public async Task ShouldRunAsyncInBlock()
    {
        // Given
        using var tokenSource = new CancellationTokenSource();
        var token = tokenSource.Token;
        _teamCitySettings.SetupGet(i => i.CIType).Returns(CIType.TeamCity);
        _baseProcessRunner.Setup(i => i.RunAsync(It.Is<ProcessInfo>(processRun => processRun.StartInfo.Vars.SequenceEqual(ModifiedVars)), token)).Returns(Task.FromResult(_processResult));
        var runner = CreateInstance();

        // When
        var result = await runner.RunAsync(_processInfo, token);

        // Then
        _teamCityWriter.Verify(i => i.OpenFlow());
        _blockWriter.Verify(i => i.Dispose());
        result.ShouldBe(_processResult);
    }

    [Fact]
    public async Task ShouldRunAsyncWithoutFlowWhenNotUnderTeamCity()
    {
        // Given
        using var tokenSource = new CancellationTokenSource();
        var token = tokenSource.Token;
        _teamCitySettings.SetupGet(i => i.CIType).Returns(CIType.Unknown);
        _baseProcessRunner.Setup(i => i.RunAsync(It.IsAny<ProcessInfo>(), token)).Returns(Task.FromResult(_processResult));
        var runner = CreateInstance();

        // When
        var result = await runner.RunAsync(_processInfo, token);

        // Then
        _teamCityWriter.Verify(i => i.OpenFlow(), Times.Never);
        _blockWriter.Verify(i => i.Dispose(), Times.Never);
        result.ShouldBe(_processResult);
    }

    private static void Handler(Output obj) { }

    private ProcessInFlowRunner CreateInstance() =>
        new(_baseProcessRunner.Object, _teamCitySettings.Object, _teamCityWriter.Object, _flowContext.Object);
}