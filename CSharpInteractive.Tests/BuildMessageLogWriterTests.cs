namespace CSharpInteractive.Tests;

public class BuildMessageLogWriterTests
{
    private static readonly ProcessInfo ProcessInfo = new(Mock.Of<IStartInfo>(), Mock.Of<IProcessMonitor>(), 1);
    private static readonly Output Output = new(Mock.Of<IStartInfo>(), false, "", 99);
    private readonly Mock<ILog<BuildMessageLogWriter>> _log = new();
    private readonly Mock<IStdOut> _stdOut = new();
    private readonly Mock<IStdErr> _stdErr = new();

    [Fact]
    public void ShouldWriteInfo()
    {
        // Given
        var writer = CreateInstance();

        // When
        writer.Write(ProcessInfo, new BuildMessage(Output, BuildMessageState.StdOut, null, "Abc"));

        // Then
        _stdOut.Verify(i => i.WriteLine(It.Is<Text[]>(text => text.AsEnumerable().SequenceEqual(new[] {new Text("Abc")}))));
    }

    [Fact]
    public void ShouldWriteStdErr()
    {
        // Given
        var writer = CreateInstance();

        // When
        writer.Write(ProcessInfo, new BuildMessage(Output, BuildMessageState.StdError, null, "Abc"));

        // Then
        _stdErr.Verify(i => i.WriteLine(It.Is<Text[]>(text => text.AsEnumerable().SequenceEqual(new[] {new Text("Abc")}))));
    }

    [Fact]
    public void ShouldWriteWarning()
    {
        // Given
        var writer = CreateInstance();

        // When
        writer.Write(ProcessInfo, new BuildMessage(Output, BuildMessageState.Warning, null, "Abc"));

        // Then
        _log.Verify(i => i.Warning(It.IsAny<Text[]>()));
    }

    [Theory]
    [InlineData(BuildMessageState.Failure)]
    [InlineData(BuildMessageState.BuildProblem)]
    public void ShouldWriteError(BuildMessageState state)
    {
        // Given
        var writer = CreateInstance();

        // When
        writer.Write(ProcessInfo, new BuildMessage(Output, state, null, "Abc"));

        // Then
        _log.Verify(i => i.Error(ErrorId.Build, It.IsAny<Text[]>()));
    }

    private BuildMessageLogWriter CreateInstance() =>
        new(_log.Object,
            _stdOut.Object,
            _stdErr.Object);
}