// ReSharper disable UnusedTypeParameter

namespace CSharpInteractive.Core;

internal interface ILog<T>
{
    void Error(ErrorId id, params Text[] error);

    void Warning(params Text[] warning);

    void Summary(params Text[] summary);

    void Info(params Text[] message);

    void Trace(Func<Text[]> traceMessagesFactory, string origin = "");
}