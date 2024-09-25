// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed

// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable NotAccessedPositionalProperty.Local
namespace CSharpInteractive.Tests.UsageScenarios;

using System.Diagnostics.CodeAnalysis;
using NuGet.Versioning;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
[SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments")]
public class DotNetSdkCheckScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact]
    public void Run()
    {
        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Printing the latest available version of the .NET SDK and .NET Runtime, for each feature band
        // {
        // ## using HostApi;

        var sdks = new List<Sdk>();
        new DotNetSdkCheck()
            .Run(output =>
            {
                if (output.Line.StartsWith("Microsoft."))
                {
                    var data = output.Line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length >= 2)
                    {
                        sdks.Add(new Sdk(data[0], NuGetVersion.Parse(data[1])));
                    }
                }
            })
            .EnsureSuccess();

        sdks.Count.ShouldBeGreaterThan(0);
        // }
    }

        // {

        record Sdk(string Name, NuGetVersion Version);
        // }
}