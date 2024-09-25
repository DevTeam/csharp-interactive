// ReSharper disable StringLiteralTypo
// ReSharper disable SuggestVarOrType_Elsewhere

namespace CSharpInteractive.Tests.UsageScenarios;

using NuGet.Versioning;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class NuGetRestoreScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [SkippableFact]
    public void Run()
    {
        // $visible=true
        // $tag=04 NuGet
        // $priority=00
        // $description=Restore NuGet a package of newest version
        // {
        // ## using HostApi;

        IEnumerable<NuGetPackage> packages = GetService<INuGet>()
            .Restore(new NuGetRestoreSettings("IoC.Container").WithVersionRange(VersionRange.All));
        // }

        packages.ShouldNotBeEmpty();
    }
}