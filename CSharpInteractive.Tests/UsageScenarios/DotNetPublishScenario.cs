// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable CommentTypo

namespace CSharpInteractive.Tests.UsageScenarios;

using NuGet.Versioning;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class DotNetPublishScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact]
    public void Run()
    {
        var maxSdk = 6;
        new DotNet().WithVersion(true)
            .Run(output => maxSdk = NuGetVersion.Parse(output.Line).Major)
            .EnsureSuccess();
        
        var framework = $"net{maxSdk}.0";
        
        new DotNetNew()
            .WithTemplateName("classlib")
            .WithName("MyLib")
            .WithForce(true)
            .Run().EnsureSuccess();

        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Publishing an application and its dependencies to a folder for deployment to a hosting system
        // {
        // ## using HostApi;

        new DotNetPublish()
            .WithWorkingDirectory("MyLib")
            .WithFramework(framework)
            .WithOutput("bin")
            .Build().EnsureSuccess();
        // }
    }
}