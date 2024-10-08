// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed

namespace CSharpInteractive.Tests.UsageScenarios;

using System.Diagnostics.CodeAnalysis;
using HostApi;
using NuGet.Versioning;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "true")]
[SuppressMessage("Performance", "CA1806:Do not ignore method results")]
public class DotNetCustomScenario : BaseScenario
{
    [Fact]
    public void Run()
    {
        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Run a custom .NET command
        // {
        // Adds the namespace "HostApi" to use .NET build API
        // ## using HostApi;

        // Gets the dotnet version, running a command like: "dotnet --version"
        NuGetVersion? version = default;
        new DotNetCustom("--version")
            .Run(message => NuGetVersion.TryParse(message.Line, out version))
            .EnsureSuccess();

        version.ShouldNotBeNull();
        // }
    }
}