// ReSharper disable StringLiteralTypo
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed

namespace CSharpInteractive.Tests.UsageScenarios;

using System.Diagnostics.CodeAnalysis;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
[SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments")]
public class DotNetDevCertsHttpsScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact]
    public void Run()
    {
        // $visible=true
        // $tag=07 .NET CLI
        // $priority=01
        // $description=Working with development certificates
        // {
        // ## using HostApi;

        // Create a certificate, trust it, and export it to a PEM file.
        new DotNetDevCertsHttps()
            .WithExportPath("certificate.pem")
            .WithTrust(true)
            .WithFormat(DotNetCertificateFormat.Pem)
            .WithPassword("Abc")
            .Run().EnsureSuccess();
        // }
    }
}