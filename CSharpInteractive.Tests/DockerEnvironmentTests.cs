namespace CSharpInteractive.Tests;

using System.Runtime.InteropServices;
using CSharpInteractive;

public class DockerEnvironmentTests
{
    private readonly Mock<IEnvironment> _environment = new();
    private readonly Mock<IFileExplorer> _fileExplorer = new();

    [Theory]
    [InlineData("Windows", "docker.exe")]
    [InlineData("Linux", "docker")]
    [InlineData("FreeBSD", "docker")]
    [InlineData("Unknown", "docker")]
    public void ShouldFindPath(string platform, string defaultPath)
    {
        // Given
        _environment.SetupGet(i => i.OperatingSystemPlatform).Returns(OSPlatform.Create(platform));
        _fileExplorer.Setup(i => i.FindFiles(defaultPath, "DOCKER_HOME")).Returns(new[] {"Abc", "Xyz"});

        // When
        var instance = CreateInstance();

        // Then
        instance.Path.ShouldBe("Abc");
    }

    [Theory]
    [InlineData("Windows", "docker.exe")]
    [InlineData("Linux", "docker")]
    [InlineData("FreeBSD", "docker")]
    [InlineData("Unknown", "docker")]
    public void ShouldProvideDefaultPathWhenException(string platform, string defaultPath)
    {
        // Given
        _environment.SetupGet(i => i.OperatingSystemPlatform).Returns(OSPlatform.Create(platform));
        _fileExplorer.Setup(i => i.FindFiles(defaultPath, "DOCKER_HOME")).Throws<Exception>();

        // When
        var instance = CreateInstance();

        // Then
        instance.Path.ShouldBe(defaultPath);
    }

    private DockerEnvironment CreateInstance() =>
        new(_environment.Object, _fileExplorer.Object);
}