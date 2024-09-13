// ReSharper disable ClassNeverInstantiated.Global

namespace CSharpInteractive.Core;

internal class Cleaner(ILog<Cleaner> log, IFileSystem fileSystem) : ICleaner
{
    public IDisposable Track(string path)
    {
        log.Trace(() => [new Text($"Start tracking \"{path}\".")]);
        return Disposable.Create(() =>
        {
            log.Trace(() => [new Text($"Delete \"{path}\".")]);
            fileSystem.DeleteDirectory(path, true);
        });
    }
}