// ReSharper disable UnusedMember.Global

namespace CSharpInteractive.Core;

internal interface IEnvironmentVariables
{
    string? GetEnvironmentVariable(string variable);
}