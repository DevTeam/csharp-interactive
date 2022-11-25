namespace CSharpInteractive;

internal interface IExitCodeParser
{
    bool TryParse(object returnValue, out int exitCode);
}