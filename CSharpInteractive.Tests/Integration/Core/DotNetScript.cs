namespace CSharpInteractive.Tests.Integration.Core;

using CSharpInteractive;

internal static class DotNetScript
{
    public static CommandLine Create(IEnumerable<string> args, params string[] lines) =>
        CreateForScript("script.csx", null, args, lines);

    public static CommandLine Create(params string[] lines) =>
        Create([], lines);

    public static CommandLine Create() =>
        new(
            Composition.Shared.Root.DotNetEnvironment.Value.Path,
            Path.Combine(Path.GetDirectoryName(typeof(DotNetScript).Assembly.Location)!, "dotnet-csi.dll"));

    public static CommandLine CreateForScript(string scriptName, string? workingDirectory, IEnumerable<string> args, params string[] lines)
    {
        workingDirectory ??= GetWorkingDirectory();
        var scriptFile = Path.Combine(workingDirectory, scriptName);
        TestComposition.FileSystem.AppendAllLines(scriptFile, lines);
        return Create().AddArgs(args.ToArray()).AddArgs(scriptFile).WithWorkingDirectory(workingDirectory);
    }

    public static string GetWorkingDirectory()
    {
        var uniqueNameGenerator = new UniqueNameGenerator();
        var environment = Composition.Shared.Root.Environment;
        var tempDirectory = environment.Value.GetPath(SpecialFolder.Temp);
        return Path.Combine(tempDirectory, uniqueNameGenerator.Generate());
    }
}