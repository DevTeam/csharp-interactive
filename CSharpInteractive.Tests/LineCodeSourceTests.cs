namespace CSharpInteractive.Tests;

public class LineCodeSourceTests
{
    [Fact]
    public void Should()
    {
        // Given
        var source = CreateInstance();

        // When
        source.Line = "Abc";

        // Then
        source.Name.ShouldBe("Abc");
        source.ToArray().ShouldBe(["Abc"]);
    }

    private static LineCodeSource CreateInstance() =>
        new();
}