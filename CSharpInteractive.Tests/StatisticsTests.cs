namespace CSharpInteractive.Tests;

using CSharpInteractive;

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
        statistics.RegisterError("error1");
        statistics.RegisterError("");
        statistics.RegisterError("   ");
        statistics.RegisterError("error2");

        // Then
        statistics.Errors.ToArray().ShouldBe(["error1", "error2"]);
    }

    [Fact]
    public void ShouldRegisterWarning()
    {
        // Given
        var statistics = new Statistics();

        // When
        statistics.RegisterWarning("warning1");
        statistics.RegisterWarning("");
        statistics.RegisterWarning("   ");
        statistics.RegisterWarning("warning2");

        // Then
        statistics.Warnings.ToArray().ShouldBe(["warning1", "warning2"]);
    }
}