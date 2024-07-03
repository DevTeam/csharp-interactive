// ReSharper disable UnusedMember.Global
namespace CSharpInteractive.Core;

internal interface IStdErr
{
    void WriteLine(params Text[] errorLine);
}