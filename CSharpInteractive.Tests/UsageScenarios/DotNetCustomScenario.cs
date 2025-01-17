// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed

namespace CSharpInteractive.Tests.UsageScenarios;

using System.Diagnostics.CodeAnalysis;
using NuGet.Versioning;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
[SuppressMessage("Performance", "CA1806:Do not ignore method results")]
public class DotNetCustomScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact]
    public void Run()
    {
        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Running a custom .NET command
        // {
        // ## using HostApi;

        // Gets the dotnet version, running a command like: "dotnet --version"
        NuGetVersion? version = null;
        new DotNetCustom("--version")
            .Run(message => NuGetVersion.TryParse(message.Line, out version))
            .EnsureSuccess();

        version.ShouldNotBeNull();
        // }
    }
}