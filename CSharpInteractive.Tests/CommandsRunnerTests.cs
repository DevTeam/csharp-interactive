namespace CSharpInteractive.Tests;

using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Usage", "xUnit1042:The member referenced by the MemberData attribute returns untyped data rows")]
public class CommandsRunnerTests
{
    private readonly Mock<ICommandRunner> _commandRunner1;
    private readonly Mock<ICommandRunner> _commandRunner2;
    private readonly Mock<IStatisticsRegistry> _statisticsRegistry;
    private readonly Mock<IDisposable> _statisticsToken;

    public CommandsRunnerTests()
    {
        _commandRunner1 = new Mock<ICommandRunner>();
        _commandRunner1.Setup(i => i.TryRun(HelpCommand.Shared)).Returns(new CommandResult(HelpCommand.Shared, true));
        _commandRunner1.Setup(i => i.TryRun(ResetCommand.Shared)).Returns(new CommandResult(HelpCommand.Shared, null));
        _commandRunner1.Setup(i => i.TryRun(new CodeCommand(false))).Returns(new CommandResult(new CodeCommand(), null));
        _commandRunner2 = new Mock<ICommandRunner>();
        _commandRunner2.Setup(i => i.TryRun(ResetCommand.Shared)).Returns(new CommandResult(ResetCommand.Shared, false));
        _commandRunner2.Setup(i => i.TryRun(HelpCommand.Shared)).Returns(new CommandResult(HelpCommand.Shared, null));
        _commandRunner2.Setup(i => i.TryRun(new CodeCommand(false))).Returns(new CommandResult(new CodeCommand(), null));
        _statisticsToken = new Mock<IDisposable>();
        _statisticsRegistry = new Mock<IStatisticsRegistry>();
        _statisticsRegistry.Setup(i => i.Start()).Returns(_statisticsToken.Object);
    }

    [Theory]
    [MemberData(nameof(Data))]
    internal void ShouldRunCommands(ICommand[] commands, CommandResult[] expectedResults)
    {
        // Given
        var runner = CreateInstance();

        // When
        var actualResults = runner.Run(commands).ToArray();

        // Then
        actualResults.ShouldBe(expectedResults);
        _statisticsToken.Verify(i => i.Dispose());
    }

    public static IEnumerable<object?[]> Data => new List<object?[]>
    {
        new object[]
        {
            new[] {HelpCommand.Shared},
            new[] {new CommandResult(HelpCommand.Shared, true)}
        },
        new object[]
        {
            new[] {ResetCommand.Shared},
            new[] {new CommandResult(ResetCommand.Shared, false)}
        },
        new object[]
        {
            new[] {new CodeCommand()},
            new[] {new CommandResult(new CodeCommand(), null)}
        },
        new object[]
        {
            new[] {new CodeCommand(), HelpCommand.Shared, ResetCommand.Shared},
            new[] {new CommandResult(new CodeCommand(), null), new CommandResult(HelpCommand.Shared, true), new CommandResult(ResetCommand.Shared, false)}
        }
    };

    private CommandsRunner CreateInstance() =>
        new([_commandRunner1.Object, _commandRunner2.Object], _statisticsRegistry.Object);
}