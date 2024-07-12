namespace CSharpInteractive.Tests;

using Core;
using HostApi;
using JetBrains.TeamCity.ServiceMessages;

public class DefaultBuildMessagesProcessorTests
{
    private static readonly ProcessInfo ProcessInfo = new(Mock.Of<IStartInfo>(), Mock.Of<IProcessMonitor>(), 1);
    private static readonly Output Output = new(Mock.Of<IStartInfo>(), false, "", 99);
    private readonly Mock<ICISettings> _teamCitySettings = new();
    private readonly Mock<IProcessOutputWriter> _processOutputWriter = new();
    private readonly Mock<IBuildMessageLogWriter> _buildMessageLogWriter = new();
    private readonly Mock<IStartInfo> _startInfo = new();

    [Theory]
    [InlineData(BuildMessageState.ServiceMessage, true)]
    [InlineData(BuildMessageState.TestResult, true)]
    [InlineData(BuildMessageState.StdOut, false)]
    [InlineData(BuildMessageState.StdError, false)]
    [InlineData(BuildMessageState.BuildProblem, false)]
    [InlineData(BuildMessageState.Failure, false)]
    [InlineData(BuildMessageState.Warning, false)]
    public void ShouldSendServiceMessagesToTeamCityViaProcessOutputWhenIsUnderTeamCityAndHasNayServiceMessage(BuildMessageState state, bool write)
    {
        // Given
        var output = new Output(_startInfo.Object, false, "Output", 11);
        var messages = new BuildMessage[]
        {
            new(Output, BuildMessageState.StdOut, default, "Msg1"),
            new(Output, state, Mock.Of<IServiceMessage>())
        };

        _teamCitySettings.SetupGet(i => i.CIType).Returns(CIType.TeamCity);
        var nextHandler = new Mock<Action<BuildMessage>>();
        var processor = CreateInstance();

        // When
        processor.ProcessMessages(ProcessInfo, output, messages, nextHandler.Object);

        // Then
        _processOutputWriter.Verify(i => i.Write(output), Times.Exactly(write ? 1 : 0));
        nextHandler.Verify(i => i(It.IsAny<BuildMessage>()), Times.Never);
    }

    [Fact]
    public void ShouldProcessBuildMessageWhenIsNotUnderTeamCity()
    {
        // Given
        var output = new Output(_startInfo.Object, false, "Output", 11);
        var msg1 = new BuildMessage(Output, BuildMessageState.StdOut, default, "Msg1");
        var msg2 = new BuildMessage(Output, BuildMessageState.ServiceMessage, Mock.Of<IServiceMessage>());

        _teamCitySettings.SetupGet(i => i.CIType).Returns(CIType.Unknown);
        var nextHandler = new Mock<Action<BuildMessage>>();
        var processor = CreateInstance();

        // When
        processor.ProcessMessages(ProcessInfo, output, new[] {msg1, msg2}, nextHandler.Object);

        // Then
        _buildMessageLogWriter.Verify(i => i.Write(ProcessInfo, msg1));
        _buildMessageLogWriter.Verify(i => i.Write(ProcessInfo, msg2));
        _processOutputWriter.Verify(i => i.Write(It.IsAny<Output>()), Times.Never);
        nextHandler.Verify(i => i(It.IsAny<BuildMessage>()), Times.Never);
    }

    [Fact]
    public void ShouldProcessBuildMessageWhenHasNotTeamCityServiceMessages()
    {
        // Given
        var output = new Output(_startInfo.Object, false, "Output", 11);
        var msg1 = new BuildMessage(Output, BuildMessageState.StdOut, default, "Msg1");
        var msg2 = new BuildMessage(Output, BuildMessageState.StdError, default, "Error");

        _teamCitySettings.SetupGet(i => i.CIType).Returns(CIType.TeamCity);
        var nextHandler = new Mock<Action<BuildMessage>>();
        var processor = CreateInstance();

        // When
        processor.ProcessMessages(ProcessInfo, output, new[] {msg1, msg2}, nextHandler.Object);

        // Then
        _buildMessageLogWriter.Verify(i => i.Write(ProcessInfo, msg1));
        _buildMessageLogWriter.Verify(i => i.Write(ProcessInfo, msg2));
        _processOutputWriter.Verify(i => i.Write(It.IsAny<Output>()), Times.Never);
        nextHandler.Verify(i => i(It.IsAny<BuildMessage>()), Times.Never);
    }

    private DefaultBuildMessagesProcessor CreateInstance() =>
        new(_teamCitySettings.Object, _processOutputWriter.Object, _buildMessageLogWriter.Object);
}