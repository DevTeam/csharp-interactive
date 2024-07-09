namespace CSharpInteractive.Core;

using HostApi;

internal interface IBuildOutputProcessor
{
    IEnumerable<BuildMessage> Convert(Output output, IBuildContext context);
}