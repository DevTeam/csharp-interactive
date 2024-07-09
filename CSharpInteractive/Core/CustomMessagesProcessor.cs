// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive.Core;

using HostApi;

internal class CustomMessagesProcessor : IBuildMessagesProcessor
{
    public void ProcessMessages(Output output, IReadOnlyCollection<BuildMessage> messages, Action<BuildMessage> nextHandler)
    {
        foreach (var buildMessage in messages)
        {
            nextHandler(buildMessage);
        }
    }
}