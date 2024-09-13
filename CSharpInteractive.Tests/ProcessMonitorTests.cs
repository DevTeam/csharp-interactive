namespace CSharpInteractive.Tests;

using System.Diagnostics.CodeAnalysis;
using Core;
using HostApi;

[SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments")]
public class ProcessMonitorTests
{
    private static readonly Text Description = new("My process description", Color.Details);
    private readonly Mock<ILog<ProcessMonitor>> _log = new();
    private readonly Mock<IEnvironment> _environment = new();
    private readonly Mock<IStartInfo> _startInfo = new();
    private readonly Mock<IStartInfoDescription> _startInfoDescription = new();
    private readonly ProcessInfo _processInfo;

    public ProcessMonitorTests()
    {
        _processInfo = new ProcessInfo(_startInfo.Object, Mock.Of<IProcessMonitor>(), 1);
        _startInfoDescription.Setup(i => i.GetDescriptionText(_startInfo.Object, It.IsAny<int?>())).Returns([Description]);
        _startInfo.SetupGet(i => i.ExecutablePath).Returns("Cm d");
        _startInfo.SetupGet(i => i.WorkingDirectory).Returns("W d");
        _startInfo.SetupGet(i => i.Args).Returns(["Arg1", "Arg 2"]);
        _startInfo.SetupGet(i => i.Vars).Returns([("Var1", "Val 1"), ("Var2", "Val 2")]);
    }

    [Fact]
    public void ShouldLogHeaderOnStart()
    {
        // Given
        _startInfo.SetupGet(i => i.ShortName).Returns("Abc xyz");
        var monitor = CreateInstance();

        // When
        monitor.Started(_startInfo.Object, 99);

        // Then
        _log.Verify(i => i.Info(It.Is<Text[]>(text => text.SequenceEqual(new[] {Description, new Text(" started "), new("\"Cm d\""), Text.Space, new("Arg1"), Text.Space, new("\"Arg 2\"")}))));
        _log.Verify(i => i.Info(It.Is<Text[]>(text => text.SequenceEqual(new Text[] {new("in directory: "), new("\"W d\"")}))));
        _log.Verify(i => i.Trace(It.IsAny<Func<Text[]>>(), It.IsAny<string>()), Times.Never);
        _log.Verify(i => i.Warning(It.IsAny<Text[]>()), Times.Never);
        _log.Verify(i => i.Error(It.IsAny<ErrorId>(), It.IsAny<Text[]>()), Times.Never);
    }

    [Fact]
    public void ShouldLogCurrentWorkingDirectoryWhenWasNotSpecified()
    {
        // Given
        _startInfo.SetupGet(i => i.WorkingDirectory).Returns(string.Empty);
        _startInfo.SetupGet(i => i.ShortName).Returns("Abc xyz");
        _environment.Setup(i => i.GetPath(SpecialFolder.Working)).Returns("Cur Wd");
        var monitor = CreateInstance();

        // When
        monitor.Started(_startInfo.Object, 99);

        // Then
        _log.Verify(i => i.Info(It.Is<Text[]>(text => text.SequenceEqual(new[] {Description, new Text(" started "), new("\"Cm d\""), Text.Space, new("Arg1"), Text.Space, new("\"Arg 2\"")}))));
        _log.Verify(i => i.Info(It.Is<Text[]>(text => text.SequenceEqual(new Text[] {new("in directory: "), new("\"Cur Wd\"")}))));
        _log.Verify(i => i.Trace(It.IsAny<Func<Text[]>>(), It.IsAny<string>()), Times.Never);
        _log.Verify(i => i.Warning(It.IsAny<Text[]>()), Times.Never);
        _log.Verify(i => i.Error(It.IsAny<ErrorId>(), It.IsAny<Text[]>()), Times.Never);
    }

    [Theory]
    [InlineData(ProcessState.Finished, "finished")]
    internal void ShouldCreateResultWhenFinishedWithSuccess(ProcessState state, string stateDescription)
    {
        // Given
        _startInfo.SetupGet(i => i.ShortName).Returns("Abc xyz");
        var monitor = CreateInstance();
        monitor.Started(_startInfo.Object, 99);

        // When
        var result = monitor.Finished(_processInfo, 22, state, 33);

        // Then
        result.Description.ShouldBe([Description, Text.Space, new Text(stateDescription, Color.Success), new Text(" (in 22 ms)"), new Text(" with exit code 33"), new Text(".")]);
    }

    [Fact]
    public void ShouldCreateResultWhenFailed()
    {
        // Given
        var monitor = CreateInstance();
        _startInfo.SetupGet(i => i.ShortName).Returns("Abc xyz");
        monitor.Started(_startInfo.Object, 99);

        // When
        var result = monitor.Finished(_processInfo, 22, ProcessState.FailedToStart, 33);

        // Then
        result.Description.ShouldBe([Description, Text.Space, new Text("failed to start", Color.Error), new Text(" (in 22 ms)"), new Text(" with exit code 33"), new Text(".")]);
    }

    [Fact]
    public void ShouldCreateResultWhenFailedToStart()
    {
        // Given
        _startInfo.SetupGet(i => i.ShortName).Returns("Abc xyz");
        var monitor = CreateInstance();

        // When
        var result = monitor.Finished(_processInfo, 22, ProcessState.FailedToStart);

        // Then
        result.Description.ShouldBe([Description, Text.Space, new Text("failed to start", Color.Error), new Text(" (in 22 ms)"), new Text(".")]);
    }

    [Fact]
    public void ShouldCreateResultWhenCanceled()
    {
        // Given
        _startInfo.SetupGet(i => i.ShortName).Returns("Abc xyz");
        var monitor = CreateInstance();
        monitor.Started(_startInfo.Object, 99);

        // When
        var result = monitor.Finished(_processInfo, 22, ProcessState.Canceled);

        // Then
        result.Description.ShouldBe([Description, Text.Space, new Text("canceled", Color.Warning), new Text(" (in 22 ms)"), new Text(".")]);
    }

    private ProcessMonitor CreateInstance() =>
        new(_log.Object,
            _environment.Object,
            _startInfoDescription.Object);
}