namespace CSharpInteractive.Tests;

using System.Runtime.InteropServices;
using CSharpInteractive;

public class FileExplorerTests
{
    private readonly Mock<IEnvironment> _environment = new();
    private readonly Mock<IHostEnvironment> _hostEnvironment = new();
    private readonly Mock<IFileSystem> _fileSystem = new();

    [Fact]
    public void ShouldFindFilesFromAdditionalPaths()
    {
        // Given
        var explorer = CreateInstance();
        _hostEnvironment.Setup(i => i.GetEnvironmentVariable("Abc")).Returns(default(string));
        _hostEnvironment.Setup(i => i.GetEnvironmentVariable("DOTNET_HOME")).Returns("DotNet");
        _hostEnvironment.Setup(i => i.GetEnvironmentVariable("PATH")).Returns(default(string));
        _fileSystem.Setup(i => i.IsDirectoryExist("DotNet")).Returns(true);
        _fileSystem.Setup(i => i.EnumerateFileSystemEntries("DotNet", "*", SearchOption.TopDirectoryOnly)).Returns(new[] {"ab", "C", "dd"});
        _fileSystem.Setup(i => i.IsFileExist("ab")).Returns(false);
        _fileSystem.Setup(i => i.IsFileExist("C")).Returns(true);
        _fileSystem.Setup(i => i.IsFileExist("dd")).Returns(true);

        // When
        var actual = explorer.FindFiles("*", "Abc", "DOTNET_HOME").ToArray();

        // Then
        actual.ShouldBe(new[] {"C", "dd"});
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void ShouldFindFiles(OSPlatform platform, char pathSeparator)
    {
        // Given
        _environment.SetupGet(i => i.OperatingSystemPlatform).Returns(platform);
        var explorer = CreateInstance();
        _hostEnvironment.Setup(i => i.GetEnvironmentVariable("Abc")).Returns(default(string));
        _hostEnvironment.Setup(i => i.GetEnvironmentVariable("DOTNET_HOME")).Returns("DotNet");
        _hostEnvironment.Setup(i => i.GetEnvironmentVariable("PATH")).Returns($"Bin1{pathSeparator} bin2");
        _fileSystem.Setup(i => i.IsDirectoryExist("DotNet")).Returns(true);
        _fileSystem.Setup(i => i.IsDirectoryExist("Bin1")).Returns(false);
        _fileSystem.Setup(i => i.IsDirectoryExist("bin2")).Returns(true);
        _fileSystem.Setup(i => i.EnumerateFileSystemEntries("DotNet", "*", SearchOption.TopDirectoryOnly)).Returns(new[] {"ab", "C", "dd"});
        _fileSystem.Setup(i => i.EnumerateFileSystemEntries("bin2", "*", SearchOption.TopDirectoryOnly)).Returns(new[] {"Zz", "zz"});
        _fileSystem.Setup(i => i.IsFileExist("ab")).Returns(false);
        _fileSystem.Setup(i => i.IsFileExist("C")).Returns(true);
        _fileSystem.Setup(i => i.IsFileExist("dd")).Returns(true);
        _fileSystem.Setup(i => i.IsFileExist("Zz")).Returns(false);
        _fileSystem.Setup(i => i.IsFileExist("zz")).Returns(true);

        // When
        var actual = explorer.FindFiles("*", "Abc", "DOTNET_HOME").ToArray();

        // Then
        actual.ShouldBe(new[] {"C", "dd", "zz"});
    }

    [Fact]
    public void ShouldSkipDuplicates()
    {
        // Given
        var explorer = CreateInstance();
        _hostEnvironment.Setup(i => i.GetEnvironmentVariable("Abc")).Returns(default(string));
        _hostEnvironment.Setup(i => i.GetEnvironmentVariable("DOTNET_HOME")).Returns("DotNet");
        _hostEnvironment.Setup(i => i.GetEnvironmentVariable("PATH")).Returns(default(string));
        _fileSystem.Setup(i => i.IsDirectoryExist("DotNet")).Returns(true);
        _fileSystem.Setup(i => i.EnumerateFileSystemEntries("DotNet", "*", SearchOption.TopDirectoryOnly)).Returns(new[] {"ab", "C", "dd", "C"});
        _fileSystem.Setup(i => i.IsFileExist("ab")).Returns(false);
        _fileSystem.Setup(i => i.IsFileExist("C")).Returns(true);
        _fileSystem.Setup(i => i.IsFileExist("dd")).Returns(true);

        // When
        var actual = explorer.FindFiles("*", "Abc", "DOTNET_HOME").ToArray();

        // Then
        actual.ShouldBe(new[] {"C", "dd"});
    }

    private FileExplorer CreateInstance() => new(
        _environment.Object,
        _hostEnvironment.Object,
        _fileSystem.Object);

    public static IEnumerable<object?[]> Data => new List<object?[]>
    {
        new object[] {OSPlatform.Windows, ';'},
        new object[] {OSPlatform.Linux, ':'},
        new object[] {OSPlatform.FreeBSD, ':'},
        new object[] {OSPlatform.OSX, ':'}
    };
}