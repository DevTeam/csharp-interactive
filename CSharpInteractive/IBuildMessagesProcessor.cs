namespace CSharpInteractive;

using HostApi;

internal interface IBuildMessagesProcessor
{
    void ProcessMessages(in Output output, IEnumerable<BuildMessage> messages, Action<BuildMessage> nextHandler);
}