// ReSharper disable UnusedMemberInSuper.Global
namespace CSharpInteractive;

using System.Runtime.InteropServices;

internal interface IEnvironment
{
    OSPlatform OperatingSystemPlatform { get; }

    Architecture ProcessArchitecture { get; }

    IEnumerable<string> GetCommandLineArgs();

    string GetPath(SpecialFolder specialFolder);

    void Exit(int exitCode);
}