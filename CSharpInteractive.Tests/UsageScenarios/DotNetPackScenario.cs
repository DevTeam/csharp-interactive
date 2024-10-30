// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable CommentTypo

namespace CSharpInteractive.Tests.UsageScenarios;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class DotNetPackScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact]
    public void Run()
    {
        new DotNetNew()
            .WithTemplateName("classlib")
            .WithName("MyLib")
            .WithForce(true)
            .Run().EnsureSuccess();

        var path = Path.GetFullPath(".");

        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Packing a code into a NuGet package
        // {
        // ## using HostApi;

        // Creates a NuGet package of version 1.2.3 for the project
        new DotNetPack()
            .WithWorkingDirectory("MyLib")
            .WithOutput(path)
            .AddProps(("version", "1.2.3"))
            .Build().EnsureSuccess();
        // }

        File.Exists(Path.Combine(path, "MyLib.1.2.3.nupkg")).ShouldBeTrue();
    }
}