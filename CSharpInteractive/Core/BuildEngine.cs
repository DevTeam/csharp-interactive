// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive.Core;

using System.Collections;
using Microsoft.Build.Framework;

[ExcludeFromCodeCoverage]
internal class BuildEngine(ILog<BuildEngine> log) : IBuildEngine
{
    public void LogErrorEvent(BuildErrorEventArgs e)
    {
        if (e.Message is { } message)
        {
            log.Error(new ErrorId(e.Code), [new Text(message)]);
        }
    }

    public void LogWarningEvent(BuildWarningEventArgs e)
    {
        if (e.Message is { } message)
        {
            log.Warning([new Text(message)]);
        }
    }

    public void LogMessageEvent(BuildMessageEventArgs e)
    {
        if (e.Message is not { } message)
        {
            return;
        }
        
        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        switch (e.Importance)
        {
            case MessageImportance.High:
                log.Info([new Text(message)]);
                break;

            case MessageImportance.Normal:
                log.Info([new Text(message)]);
                break;

            default:
                log.Trace(() => [new Text(message)], "MSBuild");
                break;
        }
    }

    public void LogCustomEvent(CustomBuildEventArgs e)
    {
        if (e.Message is { } message)
        {
            log.Trace(() => [new Text(message)], "MSBuild");
        }
    }

    public bool BuildProjectFile(string projectFileName, string[] targetNames, IDictionary globalProperties, IDictionary targetOutputs) =>
        true;

    public bool ContinueOnError => true;

    public int LineNumberOfTaskNode => 0;

    public int ColumnNumberOfTaskNode => 0;

    public string ProjectFileOfTaskNode => "Restore";
}