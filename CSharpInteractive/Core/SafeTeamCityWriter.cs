namespace CSharpInteractive.Core;

using JetBrains.TeamCity.ServiceMessages;
using JetBrains.TeamCity.ServiceMessages.Write.Special;
using Pure.DI;
using static Pure.DI.Tag;

internal class SafeTeamCityWriter([Tag(BaseTag)] ITeamCityWriter writer): ITeamCityWriter
{
    private readonly object _lockObject = new();

    public ITeamCityWriter OpenBlock(string blockName)
    {
        lock (_lockObject)
        {
            return writer.OpenBlock(blockName);
        }
    }

    public ITeamCityWriter OpenFlow()
    {
        lock (_lockObject)
        {
            return writer.OpenFlow();
        }
    }

    public void WriteMessage(string text)
    {
        lock (_lockObject)
        {
            writer.WriteMessage(text);
        }
    }

    public void WriteWarning(string text)
    {
        writer.WriteWarning(text);
    }

    public void WriteError(string text, string? errorDetails = null)
    {
        lock (_lockObject)
        {
            writer.WriteError(text, errorDetails);
        }
    }

    public ITeamCityTestsSubWriter OpenTestSuite(string suiteName)
    {
        lock (_lockObject)
        {
            return writer.OpenTestSuite(suiteName);
        }
    }

    public ITeamCityTestWriter OpenTest(string testName)
    {
        lock (_lockObject)
        {
            return writer.OpenTest(testName);
        }
    }

    public ITeamCityWriter OpenCompilationBlock(string compilerName)
    {
        lock (_lockObject)
        {
            return writer.OpenCompilationBlock(compilerName);
        }
    }

    public void PublishArtifact(string rules)
    {
        lock (_lockObject)
        {
            writer.PublishArtifact(rules);
        }
    }

    public void WriteBuildNumber(string buildNumber)
    {
        lock (_lockObject)
        {
            writer.WriteBuildNumber(buildNumber);
        }
    }

    public void WriteBuildProblem(string identity, string description)
    {
        lock (_lockObject)
        {
            writer.WriteBuildProblem(identity, description);
        }
    }

    public void WriteBuildParameter(string parameterName, string parameterValue)
    {
        lock (_lockObject)
        {
            writer.WriteBuildParameter(parameterName, parameterValue);
        }
    }

    public void WriteBuildStatistics(string statisticsKey, string statisticsValue)
    {
        lock (_lockObject)
        {
            writer.WriteBuildStatistics(statisticsKey, statisticsValue);
        }
    }

    public void Dispose()
    {
        lock (_lockObject)
        {
            writer.Dispose();
        }
    }

    public void WriteRawMessage(IServiceMessage message)
    {
        lock (_lockObject)
        {
            writer.WriteRawMessage(message);
        }
    }
}