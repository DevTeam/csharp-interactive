// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive;

using HostApi;

internal class DefaultBuildMessagesProcessor(
    ICISettings ciSettings,
    IProcessOutputWriter processOutputWriter,
    IBuildMessageLogWriter buildMessageLogWriter) : IBuildMessagesProcessor
{

    public void ProcessMessages(in Output output, IEnumerable<BuildMessage> messages, Action<BuildMessage> nextHandler)
    {
        var curMessages = messages.ToArray();
        if (ciSettings.CIType == CIType.TeamCity && curMessages.Any(i => i.State == BuildMessageState.ServiceMessage))
        {
            processOutputWriter.Write(output);
        }
        else
        {
            foreach (var buildMessage in curMessages)
            {
                buildMessageLogWriter.Write(buildMessage);
            }
        }
    }
}