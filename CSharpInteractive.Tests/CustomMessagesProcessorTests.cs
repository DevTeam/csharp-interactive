namespace CSharpInteractive.Tests;

using Core;
using HostApi;

public class CustomMessagesProcessorTests
{
    private static readonly ProcessInfo ProcessInfo = new(Mock.Of<IStartInfo>(), Mock.Of<IProcessMonitor>(), 1);
    private static readonly Output Output = new(Mock.Of<IStartInfo>(), false, "", 99);
    private readonly Mock<IStartInfo> _startInfo = new();

    [Fact]
    public void ShouldProcessMessages()
    {
        // Given
        var output = new Output(_startInfo.Object, false, "Output", 11);
        var msg1 = new BuildMessage(Output, BuildMessageState.StdOut, default, "Msg1");
        var msg2 = new BuildMessage(Output, BuildMessageState.StdError, default, "Msg2");
        var messages = new[] {msg1, msg2};
        var nextHandler = new Mock<Action<BuildMessage>>();
        var processor = CreateInstance();

        // When
        processor.ProcessMessages(ProcessInfo, output, messages, nextHandler.Object);

        // Then
        nextHandler.Verify(i => i(msg1));
        nextHandler.Verify(i => i(msg2));
    }

    private static CustomMessagesProcessor CreateInstance() =>
        new();
}