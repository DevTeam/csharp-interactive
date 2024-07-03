namespace CSharpInteractive.Core;

internal interface ITargetFrameworkMonikerParser
{
    string Parse(string tfm);
}