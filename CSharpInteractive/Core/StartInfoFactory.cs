// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive.Core;

using System.Diagnostics;
using System.Text;
using HostApi;

internal class StartInfoFactory(
    ILog<StartInfoFactory> log,
    IEnvironment environment,
    IStartInfoDescription startInfoDescription) : IStartInfoFactory
{
    public ProcessStartInfo Create(IStartInfo info)
    {
        var workingDirectory = info.WorkingDirectory;
        var directory = workingDirectory;
        var description = startInfoDescription.GetDescription(info);
        log.Trace(() => [new Text($"Working directory: \"{directory}\".")], description);
        if (string.IsNullOrWhiteSpace(workingDirectory))
        {
            workingDirectory = environment.GetPath(SpecialFolder.Working);
            log.Trace(() => [new Text($"The working directory has been replaced with the directory \"{workingDirectory}\".")], description);
        }

        var startInfo = new ProcessStartInfo
        {
            FileName = info.ExecutablePath,
            WorkingDirectory = workingDirectory,
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            StandardErrorEncoding = Encoding.UTF8,
            StandardOutputEncoding = Encoding.UTF8
        };

        log.Trace(() => [new Text($"File name: \"{startInfo.FileName}\".")], description);

        foreach (var arg in info.Args)
        {
            startInfo.ArgumentList.Add(arg);
            log.Trace(() => [new Text($"Add the argument \"{arg}\".")], description);
        }

        foreach (var (name, value) in info.Vars)
        {
            startInfo.Environment[name] = value;
            log.Trace(() => [new Text($"Add the environment variable {name}={value}.")], description);
        }

        return startInfo;
    }
}