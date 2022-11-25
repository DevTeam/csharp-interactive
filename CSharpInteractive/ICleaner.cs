namespace CSharpInteractive;

internal interface ICleaner
{
    IDisposable Track(string path);
}