namespace CSharpInteractive.Tests;

using Core;
using HostApi;

public class CommandLineRunnerTests
{
    private readonly Mock<IHost> _host = new();
    private readonly Mock<IProcessRunner> _processRunner = new();
    private readonly Mock<IProcessResultHandler> _processResultHandler = new();
    private readonly Mock<IStartInfo> _startInfo = new();
    private readonly Mock<IStartInfoDescription> _startInfoDescription = new();
    private readonly Mock<ICommandLineStatistics> _statistics = new();
    private readonly ProcessResult _processResult;

    public CommandLineRunnerTests() => 
        _processResult = new ProcessResult(_startInfo.Object, ProcessState.Finished, 12, [], 33);

    [Fact]
    public void ShouldRunProcess()
    {
        // Given
        var process = new Mock<ICommandLine>();
        process.Setup(i => i.GetStartInfo(_host.Object)).Returns(_startInfo.Object);
        var cmdService = CreateInstance();
        _processRunner.Setup(i => i.Run(It.IsAny<ProcessInfo>(), TimeSpan.FromSeconds(1))).Returns(_processResult);

        // When
        var result = cmdService.Run(process.Object, Handler, TimeSpan.FromSeconds(1));

        // Then
        result.ExitCode.ShouldBe(33);
        _processResultHandler.Verify(i => i.Handle<Output>(_processResult, Handler));
        _statistics.Verify(i => i.Register(new CommandLineInfo(result, _processResult)));
    }

    [Fact]
    public async Task ShouldRunProcessAsync()
    {
        // Given
        using var cancellationTokenSource = new CancellationTokenSource();
        var token = cancellationTokenSource.Token;
        var process = new Mock<ICommandLine>();
        process.Setup(i => i.GetStartInfo(_host.Object)).Returns(_startInfo.Object);
        var cmdService = CreateInstance();
        _processRunner.Setup(i => i.RunAsync(It.IsAny<ProcessInfo>(), token)).Returns(Task.FromResult(_processResult));

        // When
        var result = await cmdService.RunAsync(process.Object, Handler, token);

        // Then
        result.ExitCode.ShouldBe(33);
        _processResultHandler.Verify(i => i.Handle<Output>(_processResult, Handler));
        _statistics.Verify(i => i.Register(new CommandLineInfo(result, _processResult)));
    }

    private static void Handler(Output obj) { }

    private CommandLineRunner CreateInstance() =>
        new(_host.Object,
            _processRunner.Object,
            Mock.Of<IProcessMonitor>,
            _processResultHandler.Object,
            _startInfoDescription.Object,
            _statistics.Object);
}