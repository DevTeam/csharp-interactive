// ReSharper disable UnusedMember.Global
namespace CSharpInteractive.Core;

internal interface IActive
{
    IDisposable Activate();
}