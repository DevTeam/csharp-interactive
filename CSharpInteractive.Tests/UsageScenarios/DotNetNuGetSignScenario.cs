// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
// ReSharper disable CommentTypo

namespace CSharpInteractive.Tests.UsageScenarios;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class DotNetNuGetSignScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact(Skip = "Certificate chain validation failed.")]
    public void Run()
    {
        ExpectedExitCode = 1;

        new DotNetNew()
            .WithTemplateName("classlib")
            .WithName("MyLib")
            .WithForce(true)
            .Run().EnsureSuccess();
        
        new DotNetPack()
            .WithWorkingDirectory("MyLib")
            .AddProps(("version", "1.2.3"))
            .WithOutput(Path.GetFullPath("."))
            .Build().EnsureSuccess();
        
        new DotNetDevCertsHttps()
            .WithExportPath("certificate.pfx")
            .WithTrust(true)
            .WithFormat(DotNetCertificateFormat.Pfx)
            .WithPassword("Abc")
            .Run().EnsureSuccess();

        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Signing with certificate
        // {
        // ## using HostApi;

        new DotNetNuGetSign()
            .AddPackages("MyLib.1.2.3.nupkg")
            .WithCertificatePath("certificate.pfx")
            .WithCertificatePassword("Abc")
            .WithTimestampingServer("http://timestamp.digicert.com/")
            .Run().EnsureSuccess();
        // }
    }
}