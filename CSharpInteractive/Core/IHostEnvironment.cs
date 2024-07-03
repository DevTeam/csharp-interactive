namespace CSharpInteractive.Core;

internal interface IHostEnvironment
{
    string? GetEnvironmentVariable(string name);
}