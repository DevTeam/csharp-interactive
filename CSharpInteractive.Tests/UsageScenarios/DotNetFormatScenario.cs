// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable CommentTypo

namespace CSharpInteractive.Tests.UsageScenarios;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class DotNetFormatScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact]
    public void Run()
    {
        new DotNetNew()
            .WithTemplateName("classlib")
            .WithName("MyLib")
            .WithForce(true)
            .Run().EnsureSuccess();

        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Formatting a code
        // {
        // ## using HostApi;

        new DotNetFormat()
            .WithWorkingDirectory("MyLib")
            .WithProject("MyLib.csproj")
            .AddDiagnostics("IDE0005", "IDE0006")
            .AddIncludes(".", "./tests")
            .AddExcludes("./obj")
            .WithSeverity(DotNetFormatSeverity.Information)
            .Run().EnsureSuccess();
        // }
    }
}