namespace CSharpInteractive.Core;

using HostApi;

internal interface IBuildMessagesProcessor
{
    void ProcessMessages(
        ProcessInfo processInfo,
        Output output,
        IReadOnlyCollection<BuildMessage> messages,
        Action<BuildMessage> nextHandler);
}