// ReSharper disable InconsistentNaming
namespace CSharpInteractive;

internal interface ICISpecific<out T>
{
    T Instance { get; }
}