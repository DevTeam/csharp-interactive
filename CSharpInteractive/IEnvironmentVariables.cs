// ReSharper disable UnusedMember.Global
namespace CSharpInteractive;

internal interface IEnvironmentVariables
{
    string? GetEnvironmentVariable(string variable);
}