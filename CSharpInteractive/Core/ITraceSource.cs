namespace CSharpInteractive.Core;

using HostApi;

internal interface ITraceSource
{
    IEnumerable<Text> Trace { get; }
}