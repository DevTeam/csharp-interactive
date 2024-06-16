namespace HostApi;

[ExcludeFromCodeCoverage]
public static class CommandLineTools
{
    public static CommandLine AsCommandLine(this string executable, params string[] args) => new(executable, args);
    
    public static ICommandLine Customize(this ICommandLine baseCommandLine, Func<CommandLine, ICommandLine> customizer) => 
        new CustomCommandLine(baseCommandLine, customizer);

    private class CustomCommandLine(ICommandLine baseCommandLine, Func<CommandLine, ICommandLine> customizer) : ICommandLine
    {
        public IStartInfo GetStartInfo(IHost host) =>
            customizer(new CommandLine(baseCommandLine.GetStartInfo(host))).GetStartInfo(host);
    }
}