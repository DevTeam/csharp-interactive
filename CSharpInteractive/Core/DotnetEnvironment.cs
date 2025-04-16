// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global

namespace CSharpInteractive.Core;

using System.Runtime.InteropServices;
using HostApi;
using Pure.DI;
using static Pure.DI.Tag;

internal class DotNetEnvironment(
    [Tag(TargetFrameworkMonikerTag)] string targetFrameworkMoniker,
    [Tag(ModuleFileTag)] string moduleFile,
    IEnvironment environment,
    IFileExplorer fileExplorer)
    : IDotNetEnvironment, ITraceSource
{
    public string Path
    {
        get
        {
            var executable = environment.OperatingSystemPlatform == OSPlatform.Windows ? "dotnet.exe" : "dotnet";
            try
            {
                // ReSharper disable once InvertIf
                if (System.IO.Path.GetFileName(moduleFile).Equals(executable, StringComparison.InvariantCultureIgnoreCase))
                {
                    System.Console.WriteLine($"From module {moduleFile}");
                    return moduleFile;
                }

                return fileExplorer.FindFiles(executable, "DOTNET_ROOT", "DOTNET_HOME").FirstOrDefault() ?? executable;
            }
            catch
            {
                // ignored
            }

            return executable;
        }
    }

    public string TargetFrameworkMoniker { get; } = targetFrameworkMoniker;

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