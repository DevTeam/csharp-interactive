// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable CommentTypo

// ReSharper disable ClassNeverInstantiated.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable NotAccessedPositionalProperty.Local
namespace CSharpInteractive.Tests.UsageScenarios;

using System.Text;
using System.Text.Json;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class DotNetPackageSearchScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact]
    public void Run()
    {
        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Searching for a NuGet package
        // {
        // ## using System.Text;
        // ## using System.Text.Json;
        // ## using HostApi;

        var packagesJson = new StringBuilder();
        new DotNetPackageSearch()
            .WithSearchTerm("Pure.DI")
            .WithFormat(DotNetPackageSearchResultFormat.Json)
            .Run(output => packagesJson.AppendLine(output.Line)).EnsureSuccess();
        
        var result = JsonSerializer.Deserialize<Result>(
            packagesJson.ToString(),
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        result.ShouldNotBeNull();
        result.SearchResult.SelectMany(i => i.Packages).Count(i => i.Id == "Pure.DI").ShouldBe(1);
        // }
    }
    
    // {

        record Result(int Version, IReadOnlyCollection<Source> SearchResult);
        
        record Source(string SourceName, IReadOnlyCollection<Package> Packages);

        record Package(
            string Id,
            string LatestVersion,
            int TotalDownloads,
            string Owners);
    // }
}