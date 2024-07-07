// ReSharper disable InconsistentNaming
namespace CSharpInteractive.Core;

using HostApi;
using JetBrains.TeamCity.ServiceMessages;

internal interface IBuildContext
{
    IReadOnlyList<BuildMessage> ProcessMessage(in Output output, IServiceMessage message);

    IReadOnlyList<BuildMessage> ProcessOutput(in Output output);

    IBuildResult Create(ICommandLineResult commandLineResult);
}