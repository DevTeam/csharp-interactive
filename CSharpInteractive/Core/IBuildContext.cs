// ReSharper disable InconsistentNaming
namespace CSharpInteractive.Core;

using HostApi;
using JetBrains.TeamCity.ServiceMessages;

internal interface IBuildContext
{
    IReadOnlyList<BuildMessage> ProcessMessage(Output output, IServiceMessage message);

    IReadOnlyList<BuildMessage> ProcessOutput(Output output);

    IBuildResult Create(ICommandLineResult commandLineResult);
}