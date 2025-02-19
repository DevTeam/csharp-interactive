namespace CSharpInteractive.Tests;

using System.Diagnostics.CodeAnalysis;
using NuGet.Versioning;

[SuppressMessage("Usage", "xUnit1042:The member referenced by the MemberData attribute returns untyped data rows")]
public class AddNuGetReferenceCommandFactoryTests
{
    private readonly Mock<ILog<AddNuGetReferenceCommandFactory>> _log;
    private readonly List<Text> _errors = [];

    public AddNuGetReferenceCommandFactoryTests()
    {
        _log = new Mock<ILog<AddNuGetReferenceCommandFactory>>();
        _log.Setup(i => i.Error(It.IsAny<ErrorId>(), It.IsAny<Text[]>())).Callback<ErrorId, Text[]>((_, text) => _errors.AddRange(text));
    }

    [Fact]
    public void ShouldProvideOrder()
    {
        // Given
        var factory = CreateInstance();

        // When

        // Then
        factory.Order.ShouldBe(0);
    }

    [Theory]
    [MemberData(nameof(Data))]
    internal void ShouldCreateCommands(string replCommand, ICommand[] expectedCommands, bool hasErrors)
    {
        // Given
        var factory = CreateInstance();

        // When
        var actualCommands = factory.Create(replCommand).ToArray();

        // Then
        actualCommands.ShouldBe(expectedCommands);
        (_errors.Count != 0).ShouldBe(hasErrors);
    }

    public static IEnumerable<object?[]> Data => new List<object?[]>
    {
        new object[]
        {
            "#r \"Abc.dll\"",
            Array.Empty<ICommand>(),
            false
        },
        new object[]
        {
            "#",
            Array.Empty<ICommand>(),
            false
        },
        new object[]
        {
            "#   ",
            Array.Empty<ICommand>(),
            false
        },
        new object[]
        {
            "",
            Array.Empty<ICommand>(),
            false
        },
        new object[]
        {
            "   ",
            Array.Empty<ICommand>(),
            false
        },
        new object[]
        {
            "#r \"nuget:Abc, 1.2.3\"",
            new[] {new AddNuGetReferenceCommand("Abc", new VersionRange(new NuGetVersion(1, 2, 3)))},
            false
        },
        new object[]
        {
            "#r \"nuget:Abc, [1.2.3, 1.4)\"",
            new[] {new AddNuGetReferenceCommand("Abc", new VersionRange(new NuGetVersion(1, 2, 3), true, new NuGetVersion(1, 4, 0)))},
            false
        },
        new object[]
        {
            "  #r  \"NuGet:  Abc,    1.2.3 \"",
            new[] {new AddNuGetReferenceCommand("Abc", new VersionRange(new NuGetVersion(1, 2, 3)))},
            false
        },
        new object[]
        {
            "#r \"nuget:Abc, 1.2.3-beta1\"",
            new[] {new AddNuGetReferenceCommand("Abc", new VersionRange(new NuGetVersion(new Version(1, 2, 3), "beta1")))},
            false
        },
        new object[]
        {
            "#r \"nuget:Abc\"",
            new[] {new AddNuGetReferenceCommand("Abc", null)},
            false
        },
        // Errors
        new object[]
        {
            "#r \"nuget:Abc, 1.2.3 xyz\"",
            Array.Empty<ICommand>(),
            true
        },
        new object[]
        {
            "#r \":nuget:Abc, xyz\"",
            Array.Empty<ICommand>(),
            false
        },
        new object[]
        {
            "#r   ",
            Array.Empty<ICommand>(),
            false
        },
        new object[]
        {
            "#r",
            Array.Empty<ICommand>(),
            false
        }
    };

    private AddNuGetReferenceCommandFactory CreateInstance() =>
        new(_log.Object);
}