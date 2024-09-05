namespace CSharpInteractive.Tests;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Core;

[SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments")]
public class DotNetEnvironmentTests
{
    private readonly Mock<IEnvironment> _environment = new();
    private readonly Mock<IFileExplorer> _fileExplorer = new();

    [Fact]
    public void ShouldProvideTargetFrameworkMoniker()
    {
        // Given

        // When
        var instance = CreateInstance(".NETCoreApp,Version=v3.1", "dotnet.exe");

        // Then
        instance.TargetFrameworkMoniker.ShouldBe(".NETCoreApp,Version=v3.1");
    }

    [Theory]
    [InlineData("Windows", "dotnet.exe")]
    [InlineData("Linux", "dotnet")]
    [InlineData("FreeBSD", "dotnet")]
    [InlineData("Unknown", "dotnet")]
    public void ShouldProvidePathFromModule(string platform, string dotnetExecutable)
    {
        // Given
        _environment.SetupGet(i => i.OperatingSystemPlatform).Returns(OSPlatform.Create(platform));
        var modulePath = Path.Combine("Bin", dotnetExecutable);

        // When
        var instance = CreateInstance(".NETCoreApp,Version=v3.1", modulePath);

        // Then
        instance.Path.ShouldBe(modulePath);
    }

    [Theory]
    [InlineData("Windows", "dotnet.exe")]
    [InlineData("Linux", "dotnet")]
    [InlineData("FreeBSD", "dotnet")]
    [InlineData("Unknown", "dotnet")]
    public void ShouldFindPath(string platform, string defaultPath)
    {
        // Given
        _environment.SetupGet(i => i.OperatingSystemPlatform).Returns(OSPlatform.Create(platform));
        _fileExplorer.Setup(i => i.FindFiles(defaultPath, "DOTNET_ROOT", "DOTNET_HOME")).Returns(["Abc", "Xyz"]);

        // When
        var instance = CreateInstance(".NETCoreApp,Version=v3.1", "Abc");

        // Then
        instance.Path.ShouldBe("Abc");
    }

    [Theory]
    [InlineData("Windows", "dotnet.exe")]
    [InlineData("Linux", "dotnet")]
    [InlineData("FreeBSD", "dotnet")]
    [InlineData("Unknown", "dotnet")]
    public void ShouldProvideDefaultPathWhenException(string platform, string defaultPath)
    {
        // Given
        _environment.SetupGet(i => i.OperatingSystemPlatform).Returns(OSPlatform.Create(platform));
        _fileExplorer.Setup(i => i.FindFiles(defaultPath, "DOTNET_ROOT", "DOTNET_HOME")).Throws<Exception>();

        // When
        var instance = CreateInstance(".NETCoreApp,Version=v3.1", "Abc");

        // Then
        instance.Path.ShouldBe(defaultPath);
    }

    private DotNetEnvironment CreateInstance(string frameworkName, string moduleFile) =>
        new(frameworkName, moduleFile, _environment.Object, _fileExplorer.Object);
}