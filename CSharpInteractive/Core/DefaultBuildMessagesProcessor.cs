// ReSharper disable ClassNeverInstantiated.Global

namespace CSharpInteractive.Core;

using HostApi;

internal class DefaultBuildMessagesProcessor(
    ICISettings ciSettings,
    IProcessOutputWriter processOutputWriter,
    IBuildMessageLogWriter buildMessageLogWriter)
    : IBuildMessagesProcessor
{
    public void ProcessMessages(
        ProcessInfo processInfo,
        Output output,
        IReadOnlyCollection<BuildMessage> messages,
        Action<BuildMessage> nextHandler)
    {
        if (ciSettings.CIType == CIType.TeamCity
            && messages.Any(i => i.State is BuildMessageState.ServiceMessage or BuildMessageState.TestResult))
        {
            processOutputWriter.Write(output);
        }
        else
        {
            foreach (var buildMessage in messages)
            {
                buildMessageLogWriter.Write(processInfo, buildMessage);
            }
        }
    }
}