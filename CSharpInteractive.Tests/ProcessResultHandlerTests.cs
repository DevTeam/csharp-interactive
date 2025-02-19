namespace CSharpInteractive.Tests;

public class ProcessResultHandlerTests
{
    private readonly Mock<ILog<ProcessResultHandler>> _log = new();
    private readonly Mock<IStartInfo> _startInfo = new();
    private readonly Mock<IExitTracker> _exitTracker = new();
    private readonly Text[] _description = [new("Abc")];
    private readonly Action<object> _handler = Mock.Of<Action<object>>();
    private readonly ProcessInfo _processInfo;

    public ProcessResultHandlerTests()
    {
        _processInfo = new ProcessInfo(_startInfo.Object, Mock.Of<IProcessMonitor>(), 1);
    }

    [Theory]
    [InlineData(ProcessState.Finished)]
    internal void ShouldLogInfoWhenFinishedAndHasHandler(ProcessState state)
    {
        // Given
        var handler = CreateInstance();

        // When
        handler.Handle(new ProcessResult(_processInfo, state, 12, _description), _handler);

        // Then
        _log.Verify(i => i.Info(_description));
    }

    [Fact]
    public void ShouldLogSummaryWhenFinishedAndHasNoHandler()
    {
        // Given
        var handler = CreateInstance();

        // When
        handler.Handle(new ProcessResult(_processInfo, ProcessState.Finished, 12, _description), default(Action<object>));

        // Then
        _log.Verify(i => i.Summary(_description));
    }

    [Fact]
    public void ShouldLogWarningWhenCanceledAndHasNoHandler()
    {
        // Given
        var handler = CreateInstance();

        // When
        handler.Handle(new ProcessResult(_processInfo, ProcessState.Canceled, 12, _description), default(Action<object>));

        // Then
        _log.Verify(i => i.Warning(_description));
    }

    [Fact]
    public void ShouldNotLogWarningWhenCanceledAndTerminating()
    {
        // Given
        var handler = CreateInstance();

        // When
        _exitTracker.SetupGet(i => i.IsTerminating).Returns(true);
        handler.Handle(new ProcessResult(_processInfo, ProcessState.Canceled, 12, _description), default(Action<object>));

        // Then
        _log.Verify(i => i.Warning(_description), Times.Never);
    }

    [Fact]
    public void ShouldLogErrorWhenFailedAndHasNoHandler()
    {
        // Given
        var handler = CreateInstance();

        // When
        handler.Handle(new ProcessResult(_processInfo, ProcessState.FailedToStart, 12, _description), default(Action<object>));

        // Then
        _log.Verify(i => i.Error(ErrorId.Process, _description));
    }

    [Fact]
    public void ShouldLogErrorWhenFailedAndHasNoHandlerAndHasError()
    {
        // Given
        var handler = CreateInstance();
        var error = new Exception("Some error.");

        // When
        handler.Handle(new ProcessResult(_processInfo, ProcessState.FailedToStart, 12, _description, null, error), default(Action<object>));

        // Then
        _log.Verify(i => i.Error(ErrorId.Process, It.Is<Text[]>(text => text.Length == 3)));
    }

    private ProcessResultHandler CreateInstance() =>
        new(_log.Object, _exitTracker.Object);
}