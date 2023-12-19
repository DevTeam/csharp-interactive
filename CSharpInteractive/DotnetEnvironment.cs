// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global
namespace CSharpInteractive;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Pure.DI;

internal class DotNetEnvironment : IDotNetEnvironment, ITraceSource
{
    private readonly string _moduleFile;
    private readonly IEnvironment _environment;
    private readonly IFileExplorer _fileExplorer;
    public DotNetEnvironment(
        [Tag("TargetFrameworkMoniker")] string targetFrameworkMoniker,
        [Tag("ModuleFile")] string moduleFile,
        IEnvironment environment,
        IFileExplorer fileExplorer)
    {
        _moduleFile = moduleFile;
        _environment = environment;
        _fileExplorer = fileExplorer;
        TargetFrameworkMoniker = targetFrameworkMoniker;
    }

    public string Path
    {
        get
        {
            var executable = _environment.OperatingSystemPlatform == OSPlatform.Windows ? "dotnet.exe" : "dotnet";
            try
            {
                // ReSharper disable once InvertIf
                if (System.IO.Path.GetFileName(_moduleFile).Equals(executable, StringComparison.InvariantCultureIgnoreCase))
                {
                    System.Console.WriteLine($"From module {_moduleFile}");
                    return _moduleFile;
                }

                return _fileExplorer.FindFiles(executable, "DOTNET_ROOT", "DOTNET_HOME").FirstOrDefault() ?? executable;
            }
            catch
            {
                // ignored
            }

            return executable;
        }
    }

    public string TargetFrameworkMoniker { get; }

    [ExcludeFromCodeCoverage]
    public IEnumerable<Text> Trace
    {
        get
        {
            yield return new Text($"FrameworkDescription: {RuntimeInformation.FrameworkDescription}");
            yield return Text.NewLine;
            yield return new Text($"Default C# version: {ScriptCommandFactory.ParseOptions.LanguageVersion}");
            yield return Text.NewLine;
            yield return new Text($"DotNetPath: {Path}");
            yield return Text.NewLine;
            yield return new Text($"TargetFrameworkMoniker: {TargetFrameworkMoniker}");
            yield return Text.NewLine;
        }
    }
}