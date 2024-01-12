// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive;

internal class TracePresenter(ILog<TracePresenter> log) : IPresenter<IEnumerable<ITraceSource>>
{
    public void Show(IEnumerable<ITraceSource> data) =>
        log.Trace(() => new[] {Text.NewLine}.Concat(
            from source in data
            from text in source.Trace
            select text).ToArray(), "Trace:");
}