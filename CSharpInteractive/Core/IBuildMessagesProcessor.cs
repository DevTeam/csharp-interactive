namespace CSharpInteractive.Core;

using HostApi;

internal interface IBuildMessagesProcessor
{
    void ProcessMessages(Output output, IReadOnlyCollection<BuildMessage> messages, Action<BuildMessage> nextHandler);
}