namespace CSharpInteractive.Core;

using HostApi;

internal interface ITeamCityLineFormatter
{
    string Format(params Text[] line);
}