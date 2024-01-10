// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive;

[ExcludeFromCodeCoverage]
internal class HostEnvironment : IHostEnvironment
{
    public string? GetEnvironmentVariable(string name) => System.Environment.GetEnvironmentVariable(name);
}