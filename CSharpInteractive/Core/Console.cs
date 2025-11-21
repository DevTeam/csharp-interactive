namespace CSharpInteractive.Core;

using HostApi;

// ReSharper disable once ClassNeverInstantiated.Global
[ExcludeFromCodeCoverage]
internal class Console(IColorTheme colorTheme) : IConsole, IConsoleHandler
{
#if NET9_0_OR_GREATER
    private readonly Lock _lockObject = new();
#else
    private readonly object _lockObject = new();
#endif
    private readonly ConsoleColor _errorColor = colorTheme.GetConsoleColor(Color.Error);
    
    public event EventHandler<string>? OutputHandler;
    public event EventHandler<string>? ErrorHandler;

    public void WriteToOut(params (ConsoleColor? color, string output)[] text)
    {
        lock (_lockObject)
        {
            var foregroundColor = System.Console.ForegroundColor;
            try
            {
                foreach (var (color, output) in text)
                {
                    if (color.HasValue)
                    {
                        System.Console.ForegroundColor = color.Value;
                    }

                    if (OutputHandler is { } outputHandler)
                    {
                        outputHandler(this, output);
                    }

                    System.Console.Out.Write(output);
                }
            }
            finally
            {
                System.Console.ForegroundColor = foregroundColor;
            }
        }
    }

    public void WriteToErr(params (ConsoleColor? color, string output)[] text)
    {
        lock (_lockObject)
        {
            var foregroundColor = System.Console.ForegroundColor;
            try
            {
                foreach (var (color, error) in text)
                {
                    System.Console.ForegroundColor = _errorColor;
                    if (color.HasValue)
                    {
                        System.Console.ForegroundColor = color.Value;
                    }

                    if (ErrorHandler is { } errorHandler)
                    {
                        errorHandler(this, error);
                    }
                    
                    System.Console.Error.Write(error);
                }
            }
            finally
            {
                System.Console.ForegroundColor = foregroundColor;
            }
        }
    }
}