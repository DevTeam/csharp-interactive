namespace CSharpInteractive.Tests;

using Core;

public class TracePresenterTests
{
    private readonly Mock<ILog<TracePresenter>> _log = new();

    [Fact]
    public void ShouldShowTraceFromSources()
    {
        // Given
        var presenter = CreateInstance();

        var source1 = new Mock<ITraceSource>();
        var text11 = new Text("a");
        var text12 = new Text("b");
        source1.SetupGet(i => i.Trace).Returns([text11, text12]);

        var source2 = new Mock<ITraceSource>();
        var text21 = new Text("c");
        source2.SetupGet(i => i.Trace).Returns([text21]);

        // When
        presenter.Show([source1.Object, source2.Object]);

        // Then
        _log.Verify(i => i.Trace(It.Is<Func<Text[]>>(func => func().SequenceEqual(new[] {Text.NewLine, text11, text12, text21})), "Trace:"));
    }

    private TracePresenter CreateInstance() =>
        new(_log.Object);
}