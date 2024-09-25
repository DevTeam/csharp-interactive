namespace CSharpInteractive.Core;

internal interface IConsoleHandler
{
    event EventHandler<string> OutputHandler;
    
    event EventHandler<string> ErrorHandler;
}