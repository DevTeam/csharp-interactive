// ReSharper disable StringLiteralTypo
// ReSharper disable SuggestVarOrType_Elsewhere

namespace CSharpInteractive.Tests.UsageScenarios;

using NuGet.Versioning;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class NuGetRestoreAdvanced(ITestOutputHelper output) : BaseScenario(output)
{
    [SkippableFact]
    public void Run()
    {
        // $visible=true
        // $tag=04 NuGet
        // $priority=01
        // $description=Restoring a NuGet package by a version range for the specified .NET and path
        // {
        // ## using HostApi;

        var packagesPath = Path.Combine(
            Path.GetTempPath(),
            Guid.NewGuid().ToString()[..4]);

        var settings = new NuGetRestoreSettings("IoC.Container")
            .WithVersionRange(VersionRange.Parse("[1.3, 1.3.8)"))
            .WithTargetFrameworkMoniker("net5.0")
            .WithPackagesPath(packagesPath);

        IEnumerable<NuGetPackage> packages = GetService<INuGet>().Restore(settings);
        // }

        packages.ShouldNotBeEmpty();
    }
}