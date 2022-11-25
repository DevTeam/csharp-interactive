// ReSharper disable UnusedMemberInSuper.Global
namespace CSharpInteractive;

internal interface IDotNetEnvironment
{
    string Path { get; }

    string TargetFrameworkMoniker { get; }
}