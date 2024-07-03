// ReSharper disable UnusedMemberInSuper.Global
namespace CSharpInteractive.Core;

internal interface IDotNetEnvironment
{
    string Path { get; }

    string TargetFrameworkMoniker { get; }
}