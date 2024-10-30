// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable CommentTypo

namespace CSharpInteractive.Tests.UsageScenarios;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class DotNetFormatAnalyzersScenario(ITestOutputHelper output) : BaseScenario(output)
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
        // $description=Fixing (non code style) analyzer issues
        // {
        // ## using HostApi;

        new DotNetFormatAnalyzers()
            .WithWorkingDirectory("MyLib")
            .WithProject("MyLib.csproj")
            .AddDiagnostics("CA1831", "CA1832")
            .WithSeverity(DotNetFormatSeverity.Warning)
            .Run().EnsureSuccess();
        // }
    }
}