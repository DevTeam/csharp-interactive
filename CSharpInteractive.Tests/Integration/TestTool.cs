namespace CSharpInteractive.Tests.Integration;

using System.Text;
using Core;
using JetBrains.TeamCity.ServiceMessages;

internal static class TestTool
{
    public static readonly (string, string)[] DefaultVars =
    [
        ("TEAMCITY_VERSION", string.Empty),
        ("TEAMCITY_PROJECT_NAME", string.Empty)
    ];

    private static readonly (string, string)[] TeamCityVars =
    [
        ("TEAMCITY_VERSION", "2021.2"),
        ("TEAMCITY_PROJECT_NAME", "Test")
    ];

    public static IProcessResult Run(in CommandLine commandLine)
    {
        var events = new List<Output>();
        var result = Composition.Shared.Root.CommandLineRunner.Value.Run(commandLine, output =>
        {
            output.Handled = true;
            events.Add(output);
        });
        return new ProcessResult(result.ExitCode!.Value, events);
    }

    private static CommandLine CreateScriptCommandLine(IEnumerable<string> args, IEnumerable<string> scriptArgs, IEnumerable<(string, string)> vars, params string[] lines) =>
        DotNetScript.Create(args, lines).AddArgs(scriptArgs.ToArray()).AddVars(vars.ToArray());

    public static IProcessResult Run(IEnumerable<string> args, IEnumerable<string> scriptArgs, IEnumerable<(string, string)> vars, params string[] lines) =>
        Run(CreateScriptCommandLine(args, scriptArgs, vars, lines));

    public static IProcessResult Run(params string[] lines) =>
        Run(DotNetScript.Create(lines).WithVars(DefaultVars));

    public static IProcessResult RunUnderTeamCity(params string[] lines) =>
        Run(CreateScriptCommandLine(Array.Empty<string>(), Array.Empty<string>(), TeamCityVars, lines));

    public static void ShouldContainNormalTextMessage(this IEnumerable<IServiceMessage> messages, Predicate<string> textMatcher) =>
        messages.Count(i =>
                i.Name == "message"
                && i.GetValue("status") == "NORMAL"
                && textMatcher(i.GetValue("text")))
            .ShouldBe(1);

    public static void ShouldContainWarningTextMessage(this IEnumerable<IServiceMessage> messages, Predicate<string> textMatcher) =>
        messages.Count(i =>
                i.Name == "message"
                && i.GetValue("status") == "WARNING"
                && textMatcher(i.GetValue("text")))
            .ShouldBe(1);

    public static void ShouldContainBuildProblem(this IEnumerable<IServiceMessage> messages, Predicate<string> errorMatcher, Predicate<string> errorIdMatcher) =>
        messages.Count(i =>
                i.Name == "buildProblem"
                && errorIdMatcher(i.GetValue("identity"))
                && errorMatcher(i.GetValue("description")))
            .ShouldBe(1);

    public static IReadOnlyCollection<IServiceMessage> ParseMessages(this IEnumerable<string> lines) =>
        lines.ParseMessagesInternal().ToList().AsReadOnly();

    private static IEnumerable<IServiceMessage> ParseMessagesInternal(this IEnumerable<string> lines)
    {
        var parser = Composition.Shared.Root.ServiceMessageParser;
        foreach (var line in lines)
        {
            foreach (var message in parser.Value.ParseServiceMessages(line))
            {
                yield return message;
            }
        }
    }

    private class ProcessResult : IProcessResult
    {
        public ProcessResult(int exitCode, IReadOnlyList<Output> events)
        {
            ExitCode = exitCode;
            StdOut = events.Where(i => !i.IsError).Select(i => i.Line).ToList();
            StdErr = events.Where(i => i.IsError).Select(i => i.Line).ToList();
        }

        public int ExitCode { get; }

        public IReadOnlyCollection<string> StdOut { get; }

        public IReadOnlyCollection<string> StdErr { get; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Exit code: {ExitCode}");
            sb.AppendLine();
            sb.AppendLine($"StdOut({StdOut.Count}):");
            foreach (var line in StdOut)
            {
                sb.Append("  ");
                sb.AppendLine(line);
            }

            sb.AppendLine();
            sb.AppendLine($"StdErr({StdErr.Count}):");
            foreach (var line in StdErr)
            {
                sb.Append("  ");
                sb.AppendLine(line);
            }

            return sb.ToString();
        }
    }
}