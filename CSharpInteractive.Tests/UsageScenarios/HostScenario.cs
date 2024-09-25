// ReSharper disable StringLiteralTypo
// ReSharper disable SuggestVarOrType_BuiltInTypes
// ReSharper disable UnusedVariable

namespace CSharpInteractive.Tests.UsageScenarios;

public class HostScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact]
    public void Run()
    {
        // $visible=true
        // $tag=03 Microsoft DI
        // $priority=02
        // $description=Using the Host property
        // $header=[_Host_](TeamCity.CSharpInteractive.HostApi/IHost.cs) is actually the provider of all global properties and methods.
        // {
        var packages = Host.GetService<INuGet>();
        Host.WriteLine("Hello");
        // }
    }
}