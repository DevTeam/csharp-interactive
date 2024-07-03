// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive.Core;

[ExcludeFromCodeCoverage]
internal class HostEnvironment : IHostEnvironment
{
    public string? GetEnvironmentVariable(string name) => System.Environment.GetEnvironmentVariable(name);
}