namespace CSharpInteractive;

internal interface IHostEnvironment
{
    string? GetEnvironmentVariable(string name);
}