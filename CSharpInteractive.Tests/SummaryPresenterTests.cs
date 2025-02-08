namespace CSharpInteractive.Tests;

using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments")]
public class SummaryPresenterTests
{
    private readonly Mock<ILog<SummaryPresenter>> _log = new();
    private readonly Mock<IStatistics> _statistics = new();
    private readonly Mock<IPresenter<IStatistics>> _statisticsPresenter = new();

    [Theory]
    [InlineData(true, false, false, SummaryPresenter.RunningSucceeded, Color.Success)]
    [InlineData(true, false, true, SummaryPresenter.RunningSucceededWithWarnings, Color.Warning)]
    [InlineData(false, false, true, SummaryPresenter.RunningFailed, Color.Error)]
    [InlineData(true, true, true, SummaryPresenter.RunningFailed, Color.Error)]
    [InlineData(false, true, true, SummaryPresenter.RunningFailed, Color.Error)]
    [InlineData(false, false, false, SummaryPresenter.RunningFailed, Color.Error)]
    [InlineData(true, true, false, SummaryPresenter.RunningFailed, Color.Error)]
    [InlineData(false, true, false, SummaryPresenter.RunningFailed, Color.Error)]
    public void ShouldSummary(bool? success, bool hasError, bool hasWarning, string message, Color color)
    {
        // Given
        var presenter = CreateInstance();
        _statistics.SetupGet(i => i.CommandLines).Returns(ArraySegment<CommandLineInfo>.Empty);
        var statisticsItems = new List<StatisticsItem>();
        if (hasError)
        {
            statisticsItems.Add(new StatisticsItem(StatisticsType.Error, new Text("Err")));
        }

        if (hasWarning)
        {
            statisticsItems.Add(new StatisticsItem(StatisticsType.Warning, new Text("Warn")));
        }

        _statistics.SetupGet(i => i.Items).Returns(statisticsItems);

        // When
        presenter.Show(new Summary(success));

        // Then
        _statisticsPresenter.Verify(i => i.Show(_statistics.Object));
        _log.Verify(i => i.Info(new Text(message, color)));
    }

    private SummaryPresenter CreateInstance() =>
        new(_log.Object,
            _statistics.Object,
            _statisticsPresenter.Object);
}