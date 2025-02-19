namespace CSharpInteractive.Tests;

using CSharpInteractive;

public class HostServiceTests
{
    private readonly Mock<ILog<HostService>> _log = new();
    private readonly Mock<ISettings> _settings = new();
    private readonly Mock<IStdOut> _stdOut = new();
    private readonly Mock<IProperties> _properties = new();

    [Fact]
    public void ShouldProvideArgs()
    {
        // Given
        var host = CreateInstance();
        var args = Mock.Of<IReadOnlyList<string>>();
        _settings.SetupGet(i => i.ScriptArguments).Returns(args);

        // When

        // Then
        host.Args.ShouldBe(args);
    }

    [Fact]
    public void ShouldProvideProps()
    {
        // Given
        var host = CreateInstance();

        // When

        // Then
        host.Props.ShouldBe(_properties.Object);
    }

    [Fact]
    public void ShouldWritEmptyLine()
    {
        // Given
        var host = CreateInstance();

        // When
        host.WriteLine();

        // Then
        _stdOut.Verify(i => i.WriteLine());
    }

    [Fact]
    public void ShouldWritLine()
    {
        // Given
        var host = CreateInstance();

        // When
        host.WriteLine("Out");

        // Then
        _stdOut.Verify(i => i.WriteLine(It.Is<Text[]>(text => text.Length == 1 && text[0].Value == "Out" && text[0].Color == Color.Default)));
    }

    [Fact]
    public void ShouldSendError()
    {
        // Given
        var host = CreateInstance();

        // When
        host.Error("Err", "Id");

        // Then
        _log.Verify(i => i.Error(new ErrorId("Id"), "Err"));
    }

    [Fact]
    public void ShouldSendWarning()
    {
        // Given
        var host = CreateInstance();

        // When
        host.Warning("Warn");

        // Then
        _log.Verify(i => i.Warning("Warn"));
    }

    [Fact]
    public void ShouldSendSummary()
    {
        // Given
        var host = CreateInstance();

        // When
        host.Summary("Summary message");

        // Then
        _log.Verify(i => i.Summary("Summary message"));
    }

    [Fact]
    public void ShouldSendInfo()
    {
        // Given
        var host = CreateInstance();

        // When
        host.Info("Info");

        // Then
        _log.Verify(i => i.Info("Info"));
    }

    [Fact]
    public void ShouldSendTrace()
    {
        // Given
        var host = CreateInstance();

        // When
        host.Trace("Trace");

        // Then
        _log.Verify(i => i.Trace(It.Is<Func<Text[]>>(textProvider => CheckTrace(textProvider())), string.Empty));
    }

    [Fact]
    public void ShouldGetServiceProvider()
    {
        // Given
        var host = CreateInstance();

        // When
        var serviceProvider = host.GetService<IServiceProvider>();

        // Then
        serviceProvider.ShouldNotBeNull();
    }

    [Fact]
    public void ShouldProvideHostViaServiceProvider()
    {
        // Given
        var host = Composition.Shared.Root.Host;

        // When
        var actualHost = host.GetService<IServiceProvider>().GetService(typeof(IHost));

        // Then
        actualHost.ShouldBe(host);
    }

    private static bool CheckTrace(IReadOnlyList<Text> text) =>
        text is [{Value: "Trace", Color: Color.Default}];

    private HostService CreateInstance() =>
        new(_log.Object, _settings.Object, _stdOut.Object, _properties.Object);
}