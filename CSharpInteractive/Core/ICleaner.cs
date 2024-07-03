namespace CSharpInteractive.Core;

internal interface ICleaner
{
    IDisposable Track(string path);
}