namespace CSharpInteractive.Tests;

using System.Diagnostics.CodeAnalysis;
using CSharpInteractive;
using Environment = System.Environment;

[SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments")]
public class CodeSourceCommandFactoryTests
{
    private const string SourceName = "Abc";
    private static readonly ScriptCommand ScriptCommand11 = new(SourceName, "code11");
    private static readonly ScriptCommand ScriptCommand12 = new(SourceName, "code12");
    private static readonly ScriptCommand ScriptCommand2 = new(SourceName, "code2");
    private static readonly ScriptCommand ScriptCommand3 = new(SourceName, "#code3");
    private static readonly ScriptCommand ScriptCommand4 = new(SourceName, "code1 + code2");
    private readonly Mock<ILog<CodeSourceCommandFactory>> _log;
    private readonly Mock<ICommandFactory<string>> _replCommandFactory1;
    private readonly Mock<ICommandFactory<string>> _replCommandFactory2;
    private readonly Mock<ICommandFactory<ScriptCommand>> _scriptCommandFactory;
    private readonly Mock<ICodeSource> _codeSource;

    public CodeSourceCommandFactoryTests()
    {
        _log = new Mock<ILog<CodeSourceCommandFactory>>();
        _replCommandFactory1 = new Mock<ICommandFactory<string>>();
        _replCommandFactory1.Setup(i => i.Create(It.IsAny<string>())).Returns(Enumerable.Empty<ICommand>());
        _replCommandFactory1.SetupGet(i => i.Order).Returns(1);
        _replCommandFactory2 = new Mock<ICommandFactory<string>>();
        _replCommandFactory2.Setup(i => i.Create("#help")).Returns(new[] {HelpCommand.Shared});
        _replCommandFactory2.SetupGet(i => i.Order).Returns(2);
        _scriptCommandFactory = new Mock<ICommandFactory<ScriptCommand>>();
        _scriptCommandFactory.Setup(i => i.Create(new ScriptCommand(SourceName, "code1" + Environment.NewLine, false))).Returns(new[] {ScriptCommand11, ScriptCommand12});
        _scriptCommandFactory.Setup(i => i.Create(new ScriptCommand(SourceName, "code2" + Environment.NewLine, false))).Returns(new[] {ScriptCommand2});
        _scriptCommandFactory.Setup(i => i.Create(new ScriptCommand(SourceName, "#code3" + Environment.NewLine, false))).Returns(new[] {ScriptCommand3});
        _scriptCommandFactory.Setup(i => i.Create(new ScriptCommand(SourceName, "code1" + Environment.NewLine + "code2" + Environment.NewLine, false))).Returns(new[] {ScriptCommand4});
        _codeSource = new Mock<ICodeSource>();
    }

    [Theory]
    [MemberData(nameof(Data))]
    internal void ShouldCreateCodeSource(IEnumerable<string> lines, ICommand[] expectedCommands)
    {
        // Given
        _codeSource.SetupGet(i => i.Name).Returns(SourceName);
        // ReSharper disable once NotDisposedResourceIsReturned
        _codeSource.Setup(i => i.GetEnumerator()).Returns(lines.GetEnumerator());
        var factory = CreateInstance();

        // When
        var actualCommands = factory.Create(_codeSource.Object).ToArray();

        // Then
        actualCommands.ShouldBe(expectedCommands);
    }

    public static IEnumerable<object?[]> Data => new List<object?[]>
    {
        new object[]
        {
            new[] {"code1", "#help", "code2"},
            new[] {ScriptCommand11, ScriptCommand12, HelpCommand.Shared, ScriptCommand2}
        },
        new object[]
        {
            new[] {"#code3", "#help", "code2"},
            new[] {ScriptCommand3, HelpCommand.Shared, ScriptCommand2}
        },
        new object[]
        {
            new[] {"code1", "#help", "code2"},
            new[] {ScriptCommand11, ScriptCommand12, HelpCommand.Shared, ScriptCommand2}
        },
        new object[]
        {
            new[] {"code1", "code2"},
            new[] {ScriptCommand4}
        },
        new object[]
        {
            new[] {"code1", null, "code2"},
            new[] {ScriptCommand11, ScriptCommand12, ScriptCommand2}
        }
    };

    private CodeSourceCommandFactory CreateInstance() =>
        new(
            _log.Object,
            new[] {_replCommandFactory2.Object, _replCommandFactory1.Object},
            _scriptCommandFactory.Object);
}