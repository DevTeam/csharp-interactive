// ReSharper disable UnusedMember.Global
namespace CSharpInteractive;

internal interface IStdErr
{
    void WriteLine(params Text[] errorLine);
}