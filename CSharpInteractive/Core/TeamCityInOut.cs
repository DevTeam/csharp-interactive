// ReSharper disable ClassNeverInstantiated.Global

namespace CSharpInteractive.Core;

using HostApi;
using JetBrains.TeamCity.ServiceMessages.Write.Special;

internal class TeamCityInOut(
    ITeamCityLineFormatter lineFormatter,
    // ReSharper disable once SuggestBaseTypeForParameterInConstructor
    ITeamCityWriter teamCityWriter) : IStdOut, IStdErr
{
    public void Write(params Text[] text) => teamCityWriter.WriteMessage(lineFormatter.Format(text));

    void IStdOut.WriteLine(params Text[] line) => teamCityWriter.WriteMessage(lineFormatter.Format(line));

    void IStdErr.WriteLine(params Text[] errorLine) => teamCityWriter.WriteError(lineFormatter.Format(errorLine));
}