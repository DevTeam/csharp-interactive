namespace CSharpInteractive.Core;

internal interface ITeamCityLineFormatter
{
    string Format(params Text[] line);
}