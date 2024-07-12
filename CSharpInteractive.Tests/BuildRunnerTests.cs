namespace CSharpInteractive.Tests;

using Core;
using HostApi;
using BuildResult = Core.BuildResult;

public class BuildRunnerTests
{
    private static readonly Output Output = new(Mock.Of<IStartInfo>(), false, "", 99);
    private readonly Mock<IProcessRunner> _processRunner = new();
    private readonly Mock<IHost> _host = new();
    private readonly Mock<ITeamCityContext> _teamCityContext = new();
    private readonly Mock<IBuildContext> _buildResult = new();
    private readonly Func<IBuildContext> _resultFactory;
    private readonly Mock<IBuildMessagesProcessor> _defaultBuildMessagesProcessor = new();
    private readonly Mock<IBuildMessagesProcessor> _customBuildMessagesProcessor = new();
    private readonly Mock<IBuildOutputProcessor> _buildOutputConverter = new();
    private readonly Mock<IProcessMonitor> _processMonitor = new();
    private readonly Func<IProcessMonitor> _monitorFactory;
    private readonly Mock<ICommandLineResult> _commandLineResult = new();
    private readonly Mock<IStartInfo> _startInfo = new();
    private readonly Mock<IStartInfoDescription> _startInfoDescription = new();
    private readonly Mock<ICommandLineStatisticsRegistry> _statisticsRegistry = new();
    private readonly Mock<ICommandLine> _process = new();
    private readonly Mock<IProcessResultHandler> _processResultHandler = new();
    private readonly ProcessResult _processResult;

    public BuildRunnerTests()
    {
        var processInfo = new ProcessInfo(_startInfo.Object, Mock.Of<IProcessMonitor>());
        _processResult = new ProcessResult(processInfo, ProcessState.Finished, 33, []);
        var buildResult = new BuildResult(_commandLineResult.Object);
        _process.Setup(i => i.GetStartInfo(_host.Object)).Returns(_startInfo.Object);
        _resultFactory = () => _buildResult.Object;
        _monitorFactory = () => _processMonitor.Object;
        _buildResult.Setup(i => i.Create(_commandLineResult.Object)).Returns(buildResult);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ShouldRunBuildWhenHasHandler(bool handled)
    {
        // Given
        var buildMessages = new BuildMessage[]
        {
            new(Output, BuildMessageState.StdOut),
            new(Output, BuildMessageState.StdOut)
        };

        var output = new Output(_startInfo.Object, true, "Msg1", 11);
        _buildOutputConverter.Setup(i => i.Convert(output, _buildResult.Object)).Returns(buildMessages);

        var buildService = CreateInstance();
        _processRunner.Setup(i => i.Run(It.IsAny<ProcessInfo>(), TimeSpan.FromSeconds(1)))
            .Callback<ProcessInfo, TimeSpan>((processRun, _) => processRun.Handler!(output))
            .Returns(_processResult);
        
        var customHandler = Mock.Of<Action<BuildMessage>>();
        _customBuildMessagesProcessor.Setup(i => i.ProcessMessages(output, buildMessages, customHandler))
            .Callback<Output, IReadOnlyCollection<BuildMessage>, Action<BuildMessage>>((o, _, _) => { o.Handled = handled; });

        // When
        var result = buildService.Run(_process.Object, customHandler, TimeSpan.FromSeconds(1));

        // Then
        output.Handled.ShouldBeTrue();
        _customBuildMessagesProcessor.Verify(i => i.ProcessMessages(output, buildMessages, customHandler));
        _defaultBuildMessagesProcessor.Verify(i => i.ProcessMessages(output, buildMessages, It.IsAny<Action<BuildMessage>>()), Times.Exactly(handled ? 0 : 1));
        _teamCityContext.VerifySet(i => i.TeamCityIntegration = true);
        _teamCityContext.VerifySet(i => i.TeamCityIntegration = false);
        _processResultHandler.Verify(i => i.Handle(_processResult, customHandler));
        _statisticsRegistry.Verify(i => i.Register(new CommandLineInfo(result, _processResult)));
    }

    [Fact]
    public void ShouldRunBuildWhenHasNoHandler()
    {
        // Given
        var buildMessages = new BuildMessage[]
        {
            new(Output, BuildMessageState.StdOut),
            new(Output, BuildMessageState.StdOut)
        };

        var output = new Output(_startInfo.Object, true, "Msg1", 11);
        _buildOutputConverter.Setup(i => i.Convert(output, _buildResult.Object)).Returns(buildMessages);

        var buildService = CreateInstance();
        _processRunner.Setup(i => i.Run(It.IsAny<ProcessInfo>(), TimeSpan.FromSeconds(1)))
            .Callback<ProcessInfo, TimeSpan>((processRun, _) => processRun.Handler!(output))
            .Returns(_processResult);

        // When
        var result = buildService.Run(_process.Object, default, TimeSpan.FromSeconds(1));

        // Then
        output.Handled.ShouldBeTrue();
        _defaultBuildMessagesProcessor.Verify(i => i.ProcessMessages(output, buildMessages, It.IsAny<Action<BuildMessage>>()));
        _teamCityContext.VerifySet(i => i.TeamCityIntegration = true);
        _teamCityContext.VerifySet(i => i.TeamCityIntegration = false);
        _processResultHandler.Verify(i => i.Handle(_processResult, default(Action<BuildMessage>)));
        _statisticsRegistry.Verify(i => i.Register(new CommandLineInfo(result, _processResult)));
    }

    [Fact]
    public async Task ShouldRunBuildAsync()
    {
        // Given
        using var cancellationTokenSource = new CancellationTokenSource();
        var token = cancellationTokenSource.Token;
        var buildService = CreateInstance();
        _processRunner.Setup(i => i.RunAsync(It.IsAny<ProcessInfo>(), token)).Returns(Task.FromResult(_processResult));
        var handler = Mock.Of<Action<BuildMessage>>();

        // When
        var result = await buildService.RunAsync(_process.Object, handler, token);

        // Then
        _teamCityContext.VerifySet(i => i.TeamCityIntegration = true);
        _teamCityContext.VerifySet(i => i.TeamCityIntegration = false);
        _processResultHandler.Verify(i => i.Handle(_processResult, handler));
        _statisticsRegistry.Verify(i => i.Register(new CommandLineInfo(result, _processResult)));
    }

    private BuildRunner CreateInstance() =>
        new(_processRunner.Object,
            _host.Object,
            _teamCityContext.Object,
            _resultFactory,
            _buildOutputConverter.Object,
            _monitorFactory,
            _defaultBuildMessagesProcessor.Object,
            _customBuildMessagesProcessor.Object,
            _processResultHandler.Object,
            _startInfoDescription.Object,
            _statisticsRegistry.Object);
}