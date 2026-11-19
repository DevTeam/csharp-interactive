// ReSharper disable StringLiteralTypo
// ReSharper disable RedundantUsingDirective

namespace CSharpInteractive.Tests.Integration;

using CSharpInteractive;
using NuGet.Versioning;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class NuGetTests
{
    [Fact]
    public void ShouldSupportRestore()
    {
        // Given
        var tempPath = CreateTempDirectory();

        // When
        var nuget = Composition.Shared.Root.NuGet;
        var result = nuget.Value.Restore(new NuGetRestoreSettings("IoC.Container").WithVersionRange(VersionRange.Parse("1.3.6")).WithTargetFrameworkMoniker("net5.0").WithPackagesPath(tempPath)).ToList();

        // Then
        result.Count.ShouldBe(1);
        result[0].Name.ShouldBe("IoC.Container");
    }

    [Theory]
    [InlineData("all")]
    [InlineData("(,2.0.0]")]
    [InlineData("[1.3.6,)")]
    [InlineData("*")]
    public void ShouldRestoreForOpenVersionRange(string versionRange)
    {
        // Given
        var nuget = Composition.Shared.Root.NuGet;

        // When
        var result = nuget.Value.Restore(
                new NuGetRestoreSettings("IoC.Container")
                    .WithVersionRange(versionRange == "all" ? VersionRange.All : VersionRange.Parse(versionRange))
                    .WithHideWarningsAndErrors(true))
            .ToList();

        // Then
        result.ShouldContain(i => i.Name == "IoC.Container");
    }

    [Fact]
    public void ShouldSupportRestoreForDefaults()
    {
        // Given

        // When
        var nuget = Composition.Shared.Root.NuGet;
        var result = nuget.Value.Restore(new NuGetRestoreSettings("IoC.Container")).ToList();

        // Then
        result.Count.ShouldBeGreaterThan(0);
    }

    [Fact]
    public void ShouldSupportRestoreWhenScript()
    {
        // Given
        var tempPath = CreateTempDirectory();

        // When
        var result = TestTool.Run(
            "using HostApi;\n"
            + "using NuGet.Versioning;\n"
            + $"GetService<INuGet>().Restore(new NuGetRestoreSettings(\"IoC.Container\").WithVersionRange(VersionRange.Parse(\"1.3.6\")).WithTargetFrameworkMoniker(\"net5.0\").WithPackagesPath(@\"{tempPath}\"));");

        // Then
        result.ExitCode.ShouldBe(0, result.ToString());
    }

    [Fact]
    public void ShouldRestoreLatestVersionWhenNoVersionSpecified()
    {
        // Given
        var nuget = Composition.Shared.Root.NuGet;

        // When
        var version = nuget.Value
            .Restore(new NuGetRestoreSettings("Pure.DI")
                .WithHideWarningsAndErrors(true)
                .WithNoCache(true))
            .Where(i => i.Name == "Pure.DI")
            .Select(i => i.NuGetVersion)
            .Max();

        // Then
        version.ShouldNotBeNull();
        version.ShouldBeGreaterThan(new NuGetVersion(1, 0, 0));
    }

    private static string CreateTempDirectory() => Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()[..4]);
}