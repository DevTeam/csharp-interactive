// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive.Core;

using HostApi;
using JetBrains.TeamCity.ServiceMessages.Read;

internal class BuildOutputProcessor(IServiceMessageParser serviceMessageParser) : IBuildOutputProcessor
{
    public IEnumerable<BuildMessage> Convert(in Output output, IBuildContext context)
    {
        var messages = new List<BuildMessage>();
        foreach (var message in serviceMessageParser.ParseServiceMessages(output.Line).Where(message => message != default))
        {
            var buildMessage = new BuildMessage(BuildMessageState.ServiceMessage, message);
            if (buildMessage.TestResult.HasValue)
            {
                buildMessage = buildMessage.WithState(BuildMessageState.TestResult);
            }

            messages.Add(buildMessage);
            messages.AddRange(context.ProcessMessage(output, message));
        }

        if (messages.Count == 0) messages.AddRange(context.ProcessOutput(output));

        return messages;
    }
}