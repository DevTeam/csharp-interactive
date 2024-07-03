namespace CSharpInteractive.Core;

internal interface ITraceSource
{
    IEnumerable<Text> Trace { get; }
}