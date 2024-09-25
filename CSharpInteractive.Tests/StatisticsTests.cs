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

    [Fact]
    public void ShouldRegisterError()
    {
        // Given
        var statistics = new Statistics();

        // When
        statistics.RegisterError(new Text("error1"));
        statistics.RegisterError(Text.Empty);
        statistics.RegisterError(new Text("   "));
        statistics.RegisterError(new Text("error2"));

        // Then
        statistics.Errors.ToArray().ShouldBe([new Text("error1"), new Text("error2")]);
    }

    [Fact]
    public void ShouldRegisterWarning()
    {
        // Given
        var statistics = new Statistics();

        // When
        statistics.RegisterWarning(new Text("warning1"));
        statistics.RegisterWarning(Text.Empty);
        statistics.RegisterWarning(new Text("   "));
        statistics.RegisterWarning(new Text("warning2"));

        // Then
        statistics.Warnings.ToArray().ShouldBe([new Text("warning1"), new Text("warning2")]);
    }
}