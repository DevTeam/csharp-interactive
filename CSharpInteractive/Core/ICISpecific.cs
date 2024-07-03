// ReSharper disable InconsistentNaming
namespace CSharpInteractive.Core;

internal interface ICISpecific<out T>
{
    T Instance { get; }
}