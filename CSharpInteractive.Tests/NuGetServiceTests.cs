namespace CSharpInteractive.Tests;

using System.Diagnostics.CodeAnalysis;
using NuGet.Versioning;

[SuppressMessage("Usage", "xUnit1042:The member referenced by the MemberData attribute returns untyped data rows")]
public class NuGetServiceTests
{
    private static readonly NuGetPackage NuGetPackage1 = new("Abc", new Version(1, 2, 3), new NuGetVersion(1, 2, 3), "package", "AbcPath", "111222", Array.Empty<string>(), false, false);
    private static readonly NuGetPackage NuGetPackage2 = new("Abc.Contracts", new Version(1, 2, 3), new NuGetVersion(3, 2, 1), "package", "AbcContractsPath", "111233", Array.Empty<string>(), false, false);
    private static readonly IEnumerable<string> Sources = ["src1", "src2"];
    private static readonly IEnumerable<string> FallBacks = ["fb1", "fb2"];

    private readonly Mock<ILog<NuGetService>> _log;
    private readonly Mock<IFileSystem> _fileSystem;
    private readonly Mock<IEnvironment> _environment;
    private readonly Mock<INuGetEnvironment> _nugetEnvironment;
    private readonly Mock<INuGetRestoreService> _nugetRestoreService;
    private readonly Mock<INuGetAssetsReader> _nugetAssetsReader;
    private readonly Mock<ICleaner> _cleaner;
    private readonly Mock<IDisposable> _trackToken;

    public NuGetServiceTests()
    {
        _log = new Mock<ILog<NuGetService>>();
        _fileSystem = new Mock<IFileSystem>();
        _environment = new Mock<IEnvironment>();
        _nugetEnvironment = new Mock<INuGetEnvironment>();
        _nugetRestoreService = new Mock<INuGetRestoreService>();
        _nugetAssetsReader = new Mock<INuGetAssetsReader>();
        _cleaner = new Mock<ICleaner>();

        _environment.Setup(i => i.GetPath(SpecialFolder.Temp)).Returns("TMP");
        _environment.Setup(i => i.GetPath(SpecialFolder.Working)).Returns("WD");
        _nugetEnvironment.SetupGet(i => i.Sources).Returns(Sources);
        _nugetEnvironment.SetupGet(i => i.FallbackFolders).Returns(FallBacks);
        _trackToken = new Mock<IDisposable>();
        _cleaner = new Mock<ICleaner>();
        _cleaner.Setup(i => i.Track("AssetsTmp")).Returns(_trackToken.Object);
    }

    public static IEnumerable<object?[]> Data => new List<object?[]>
    {
        new object?[] {"myPackages", false, Path.Combine("WD", "myPackages")},
        new object?[] {"myPackages", true, "myPackages"},
        new object?[] {null, true, "defaultPackagesPath"}
    };

    [Theory]
    [MemberData(nameof(Data))]
    public void ShouldRestore(string? packagesPath, bool isPathRooted, string expectedNuGtePackagesDir)
    {
        // Given
        var projectAssetsJson = Path.Combine("AssetsTmp", "assets.json");
        var nuGet = CreateInstance();
        _nugetEnvironment.SetupGet(i => i.PackagesPath).Returns("defaultPackagesPath");
        _fileSystem.Setup(i => i.IsPathRooted(It.IsAny<string>())).Returns(isPathRooted);
        _nugetAssetsReader.Setup(i => i.ReadPackages(expectedNuGtePackagesDir, projectAssetsJson)).Returns([NuGetPackage1, NuGetPackage2]);
        _nugetRestoreService.Setup(i => i.TryRestore(It.IsAny<NuGetRestoreSettings>(), out projectAssetsJson)).Returns(true);

        // When
        var packages = nuGet.Restore(
            new NuGetRestoreSettings("Abc")
                .WithVersionRange(VersionRange.Parse("1.2.3"))
                .WithTargetFrameworkMoniker(".NETCoreApp,Version=v3.1")
                .WithPackagesPath(packagesPath)).ToArray();

        // Then
        _nugetRestoreService.Verify(i => i.TryRestore(
            new NuGetRestoreSettings("Abc")
                .WithSources(Sources)
                .WithFallbackFolders(FallBacks)
                .WithVersionRange(VersionRange.Parse("1.2.3"))
                .WithTargetFrameworkMoniker(".NETCoreApp,Version=v3.1")
                .WithPackagesPath(expectedNuGtePackagesDir),
            out projectAssetsJson));

        packages.ShouldBe([NuGetPackage1, NuGetPackage2]);
        _trackToken.Verify(i => i.Dispose());
    }

    private NuGetService CreateInstance() =>
        new(
            _log.Object,
            _fileSystem.Object,
            _environment.Object,
            _nugetEnvironment.Object,
            _nugetRestoreService.Object,
            _nugetAssetsReader.Object,
            _cleaner.Object);
}