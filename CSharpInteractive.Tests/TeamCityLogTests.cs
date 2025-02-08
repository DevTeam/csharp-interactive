namespace CSharpInteractive.Tests;

using JetBrains.TeamCity.ServiceMessages.Write.Special;

public class TeamCityLogTests
{
    private readonly Mock<ISettings> _settings;
    private readonly Mock<ITeamCityLineFormatter> _lineFormatter;
    private readonly Mock<ITeamCityWriter> _teamCityWriter;
    private readonly Text[] _text = [new("line1"), new("line2", Color.Details)];
    private readonly Mock<IStatisticsRegistry> _statisticsRegistry;

    public TeamCityLogTests()
    {
        _settings = new Mock<ISettings>();
        _lineFormatter = new Mock<ITeamCityLineFormatter>();
        _lineFormatter.Setup(i => i.Format(It.IsAny<Text[]>())).Returns<Text[]>(i => "F_" + i.ToSimpleString());
        _teamCityWriter = new Mock<ITeamCityWriter>();
        _statisticsRegistry = new Mock<IStatisticsRegistry>();
    }

    [Theory]
    [InlineData(VerbosityLevel.Normal)]
    [InlineData(VerbosityLevel.Quiet)]
    [InlineData(VerbosityLevel.Diagnostic)]
    internal void ShouldSupportError(VerbosityLevel verbosityLevel)
    {
        // Given
        var log = CreateInstance();
        _settings.SetupGet(i => i.VerbosityLevel).Returns(verbosityLevel);

        // When
        log.Error(new ErrorId("id"), _text);

        // Then
        _teamCityWriter.Verify(i => i.WriteBuildProblem("id", "line1line2"));
        _statisticsRegistry.Verify(i => i.Register(StatisticsType.Error, _text.WithDefaultColor(Color.Error)));
    }

    [Theory]
    [InlineData(VerbosityLevel.Normal)]
    [InlineData(VerbosityLevel.Quiet)]
    [InlineData(VerbosityLevel.Diagnostic)]
    internal void ShouldSupportWarning(VerbosityLevel verbosityLevel)
    {
        // Given
        var log = CreateInstance();
        _settings.SetupGet(i => i.VerbosityLevel).Returns(verbosityLevel);

        // When
        log.Warning(_text);

        // Then
        _teamCityWriter.Verify(i => i.WriteWarning("line1line2"));
        _statisticsRegistry.Verify(i => i.Register(StatisticsType.Warning, _text.WithDefaultColor(Color.Warning)));
    }

    [Theory]
    [InlineData(VerbosityLevel.Normal)]
    [InlineData(VerbosityLevel.Quiet)]
    [InlineData(VerbosityLevel.Diagnostic)]
    internal void ShouldSupportSummary(VerbosityLevel verbosityLevel)
    {
        // Given
        var log = CreateInstance();
        _settings.SetupGet(i => i.VerbosityLevel).Returns(verbosityLevel);

        // When
        log.Summary(_text);

        // Then
        _teamCityWriter.Verify(i => i.WriteMessage("F_line1line2"));
        _statisticsRegistry.Verify(i => i.Register(StatisticsType.Summary, _text.WithDefaultColor(Color.Highlighted)));
    }

    [Theory]
    [InlineData(VerbosityLevel.Normal, true)]
    [InlineData(VerbosityLevel.Quiet, false)]
    [InlineData(VerbosityLevel.Diagnostic, true)]
    internal void ShouldSupportInfo(VerbosityLevel verbosityLevel, bool enabled)
    {
        // Given
        var times = Times.Exactly(enabled ? 1 : 0);
        var log = CreateInstance();
        _settings.SetupGet(i => i.VerbosityLevel).Returns(verbosityLevel);

        // When
        log.Info(_text);

        // Then
        _teamCityWriter.Verify(i => i.WriteMessage("F_line1line2"), times);
    }

    [Theory]
    [InlineData(VerbosityLevel.Normal, false)]
    [InlineData(VerbosityLevel.Quiet, false)]
    [InlineData(VerbosityLevel.Diagnostic, true)]
    internal void ShouldSupportTrace(VerbosityLevel verbosityLevel, bool enabled)
    {
        // Given
        var times = Times.Exactly(enabled ? 1 : 0);
        var log = CreateInstance();
        _settings.SetupGet(i => i.VerbosityLevel).Returns(verbosityLevel);

        // When
        log.Trace(() => _text, "Orig");

        // Then
        _teamCityWriter.Verify(i => i.WriteMessage($"F_{"Orig",-40}line1line2"), times);
    }

    private TeamCityLog<string> CreateInstance() =>
        new(
            _settings.Object,
            _teamCityWriter.Object,
            _lineFormatter.Object,
            _statisticsRegistry.Object);
}