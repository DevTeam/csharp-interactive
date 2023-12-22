// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive;

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Build.Framework;

[ExcludeFromCodeCoverage]
internal class BuildEngine : IBuildEngine
{
    private readonly ILog<BuildEngine> _log;

    public BuildEngine(ILog<BuildEngine> log) => _log = log;

    public void LogErrorEvent(BuildErrorEventArgs e)
    {
        if (e.Message is { } message)
        {
            _log.Error(new ErrorId(e.Code), [new Text(message)]);
        }
    }

    public void LogWarningEvent(BuildWarningEventArgs e)
    {
        if (e.Message is { } message)
        {
            _log.Warning([new Text(message)]);
        }
    }

    public void LogMessageEvent(BuildMessageEventArgs e)
    {
        if (e.Message is { } message)
        {
            // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
            switch (e.Importance)
            {
                case MessageImportance.High:
                    _log.Info([new Text(message)]);
                    break;

                case MessageImportance.Normal:
                    _log.Info([new Text(message)]);
                    break;

                default:
                    _log.Trace(() => [new Text(message)], "MSBuild");
                    break;
            }
        }
    }

    public void LogCustomEvent(CustomBuildEventArgs e)
    {
        if (e.Message is { } message)
        {
            _log.Trace(() => [new Text(message)], "MSBuild");
        }
    }

    public bool BuildProjectFile(string projectFileName, string[] targetNames, IDictionary globalProperties, IDictionary targetOutputs) =>
        true;

    public bool ContinueOnError => true;

    public int LineNumberOfTaskNode => 0;

    public int ColumnNumberOfTaskNode => 0;

    public string ProjectFileOfTaskNode => "Restore";
}