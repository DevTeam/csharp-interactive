namespace CSharpInteractive.Tests;

using CSharpInteractive;

public class LoadFileCodeSourceTests
{
    private readonly Mock<IFilePathResolver> _filePathResolver = new();
    private readonly Mock<IScriptContext> _workingDirectoryContext = new();
    private readonly Mock<IDisposable> _workingDirectoryToken = new();

    [Fact]
    public void ShouldProvideLoadCommand()
    {
        // Given
        var fullPath = Path.Combine("wd", "zx", "Abc");
        _filePathResolver.Setup(i => i.TryResolve(Path.Combine("zx", "Abc"), out fullPath)).Returns(true);
        var source = CreateInstance(Path.Combine("zx", "Abc"));
        _workingDirectoryContext.Setup(i => i.CreateScope(source)).Returns(_workingDirectoryToken.Object);

        // When
        var expectedResult = source.ToArray();

        // Then
        expectedResult.ShouldBe([$"#load \"{Path.Combine("wd", "zx", "Abc")}\""]);
        _workingDirectoryContext.Verify(i => i.CreateScope(source));
        _workingDirectoryToken.Verify(i => i.Dispose());
    }

    private LoadFileCodeSource CreateInstance(string fileName) =>
        new(_filePathResolver.Object, _workingDirectoryContext.Object) {Name = fileName};
}