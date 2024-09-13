// ReSharper disable InconsistentNaming

namespace CSharpInteractive.Tests;

using Core;

public class TeamCityContextTests
{
    private readonly Mock<IEnvironment> _environment = new();
    private readonly Mock<IDotNetEnvironment> _dotnetEnvironment = new();
    private readonly Mock<ICISettings> _teamCitySettings = new();

    [Fact]
    public void ShouldGetDotNetExecutablePath()
    {
        // Given
        var settings = CreateInstance();

        // When
        _dotnetEnvironment.SetupGet(i => i.Path).Returns("Bin");

        // Then
        settings.DotNetExecutablePath.ShouldBe("Bin");
    }

    [Fact]
    public void ShouldGetDotNetMSBuildLoggerDirectory()
    {
        // Given
        var settings = CreateInstance();

        // When
        _environment.Setup(i => i.GetPath(SpecialFolder.Bin)).Returns("Bin");

        // Then
        settings.DotNetMSBuildLoggerDirectory.ShouldBe(Path.Combine("Bin", "msbuild"));
    }

    [Fact]
    public void ShouldGetDotNetVSTestLoggerDirectory()
    {
        // Given
        var settings = CreateInstance();

        // When
        _environment.Setup(i => i.GetPath(SpecialFolder.Bin)).Returns("Bin");

        // Then
        settings.DotNetVSTestLoggerDirectory.ShouldBe(Path.Combine("Bin", "vstest"));
    }

    [Fact]
    public void ShouldGetTeamCityMessagesPath()
    {
        // Given
        var resolver = CreateInstance();

        // When
        _teamCitySettings.SetupGet(i => i.ServiceMessagesPath).Returns("Tmp");

        // Then
        resolver.TeamCityMessagesPath.ShouldBe("Tmp");
    }

    private TeamCityContext CreateInstance() =>
        new(_environment.Object, _dotnetEnvironment.Object, _teamCitySettings.Object);
}