namespace CSharpInteractive.Tests;

public class StatisticsTests
{
    [Fact]
    public void ShouldTrackTimeElapsed()
    {
        // Given
        var statistics = new Statistics();

        // When
        using (statistics.Start())
        {
            Thread.Sleep(2);
        }

        // Then
        statistics.TimeElapsed.ShouldNotBe(TimeSpan.Zero);
    }

    [Theory]
    [InlineData(StatisticsType.Error)]
    [InlineData(StatisticsType.Warning)]
    [InlineData(StatisticsType.Summary)]
    internal void ShouldRegisterError(StatisticsType statisticsType)
    {
        // Given
        var statistics = new Statistics();

        // When
        statistics.Register(statisticsType, new Text("Abc"));
        statistics.Register(statisticsType, Text.Empty);
        statistics.Register(statisticsType, new Text("   "));
        statistics.Register(statisticsType, new Text("Xyz"));

        // Then
        statistics.Items.ShouldBe([new StatisticsItem(statisticsType, new Text("Abc")), new StatisticsItem(statisticsType, new Text("Xyz"))]);
    }
}