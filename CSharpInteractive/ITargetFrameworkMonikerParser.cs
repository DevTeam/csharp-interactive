namespace CSharpInteractive;

internal interface ITargetFrameworkMonikerParser
{
    string Parse(string tfm);
}