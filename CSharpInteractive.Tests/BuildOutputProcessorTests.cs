namespace CSharpInteractive.Tests;

using Core;
using HostApi;
using JetBrains.TeamCity.ServiceMessages;
using JetBrains.TeamCity.ServiceMessages.Read;

public class BuildOutputProcessorTests
{
    private readonly Mock<IServiceMessageParser> _serviceMessageParser = new();
    private readonly Mock<IStartInfo> _startInfo = new();
    private readonly Mock<IBuildContext> _buildResult = new();

    [Fact]
    public void ShouldConvertMessages()
    {
        // Given
        Mock<IServiceMessage> msg1 = new();
        Mock<IServiceMessage> msg2 = new();
        var output = new Output(_startInfo.Object, false, "Messages", 33);
        BuildMessage buildMsg1 = new(output, BuildMessageState.StdOut);
        BuildMessage buildMsg2 = new(output, BuildMessageState.StdOut);
        BuildMessage buildMsg3 = new(output, BuildMessageState.StdOut);
        _serviceMessageParser.Setup(i => i.ParseServiceMessages("Messages")).Returns([msg1.Object, msg2.Object]);
        _buildResult.Setup(i => i.ProcessMessage(output, msg1.Object)).Returns([buildMsg1, buildMsg2]);
        _buildResult.Setup(i => i.ProcessMessage(output, msg2.Object)).Returns([buildMsg3]);
        var processor = CreateInstance();

        // When
        var messages = processor.Convert(output, _buildResult.Object).ToArray();

        // Then
        messages.ShouldBe(
        [
            new BuildMessage(output, BuildMessageState.ServiceMessage, msg1.Object),
                buildMsg1,
                buildMsg2,
                new BuildMessage(output, BuildMessageState.ServiceMessage, msg2.Object),
                buildMsg3
        ]);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ShouldConvertWhenHasNoServiceMessages(bool isError)
    {
        // Given
        var output = new Output(_startInfo.Object, isError, "some output", 33);
        _serviceMessageParser.Setup(i => i.ParseServiceMessages("some output")).Returns([]);
        _buildResult.Setup(i => i.ProcessMessage(output, It.IsAny<IServiceMessage>())).Returns(Array.Empty<BuildMessage>());
        BuildMessage buildMsg = new(output, BuildMessageState.StdOut);
        _buildResult.Setup(i => i.ProcessOutput(output)).Returns([buildMsg]);
        var processor = CreateInstance();

        // When
        var messages = processor.Convert(output, _buildResult.Object).ToArray();

        // Then
        messages.ShouldBe([buildMsg]);
    }

    private BuildOutputProcessor CreateInstance() =>
        new(_serviceMessageParser.Object);
}