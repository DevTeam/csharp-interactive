// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InconsistentNaming
namespace CSharpInteractive;

internal class CISettings : ICISettings
{
    private const string TeamCityVersionVariableName = "TEAMCITY_VERSION";
    private const string TeamCityProjectNameVariableName = "TEAMCITY_PROJECT_NAME";
    internal const string TeamCityFlowIdEnvironmentVariableName = "TEAMCITY_PROCESS_FLOW_ID";
    private const string TeamCityServiceMessagesPathEnvironmentVariableName = "TEAMCITY_SERVICE_MESSAGES_PATH";
    private const string GitLabPipelineIdVariableName = "CI_PIPELINE_ID";
    private const string AzureDevOpsBuildVariableName = "TF_BUILD";
    private const string DefaultFlowId = "ROOT";
    private readonly IHostEnvironment _hostEnvironment;
    private readonly Lazy<bool> _isUnderTeamCity;
    private readonly Lazy<string> _flowId;
    private readonly Lazy<string> _serviceMessagesPath;
    private readonly Lazy<bool> _isUnderGitLab;
    private readonly Lazy<bool> _isUnderAzureDevOps;

    public CISettings(
        IHostEnvironment hostEnvironment,
        IEnvironment environment)
    {
        _hostEnvironment = hostEnvironment;
        _isUnderTeamCity = new Lazy<bool>(() =>
            !string.IsNullOrWhiteSpace(_hostEnvironment.GetEnvironmentVariable(TeamCityProjectNameVariableName))
            || !string.IsNullOrWhiteSpace(_hostEnvironment.GetEnvironmentVariable(TeamCityVersionVariableName)));
        
        _isUnderGitLab = new Lazy<bool>(() => !string.IsNullOrWhiteSpace(_hostEnvironment.GetEnvironmentVariable(GitLabPipelineIdVariableName)));
        _isUnderAzureDevOps = new Lazy<bool>(() => !string.IsNullOrWhiteSpace(_hostEnvironment.GetEnvironmentVariable(AzureDevOpsBuildVariableName)));

        _flowId = new Lazy<string>(() =>
        {
            var flowId = _hostEnvironment.GetEnvironmentVariable(TeamCityFlowIdEnvironmentVariableName);
            return string.IsNullOrWhiteSpace(flowId) ? DefaultFlowId : flowId;
        });

        _serviceMessagesPath = new Lazy<string>(() =>
        {
            var serviceMessagesPath = _hostEnvironment.GetEnvironmentVariable(TeamCityServiceMessagesPathEnvironmentVariableName);
            return string.IsNullOrWhiteSpace(serviceMessagesPath) ? environment.GetPath(SpecialFolder.Temp) : serviceMessagesPath;
        });
    }

    [SuppressMessage("ReSharper", "ConvertIfStatementToReturnStatement")]
    public CIType CIType
    {
        get
        {
            if (_isUnderTeamCity.Value)
            {
                return CIType.TeamCity;
            }

            if (_isUnderGitLab.Value)
            {
                return CIType.GitLab;
            }
            
            if (_isUnderAzureDevOps.Value)
            {
                return CIType.AzureDevOps;
            }

            return CIType.Unknown;
        }
    }

    public string Version => (_hostEnvironment.GetEnvironmentVariable(TeamCityVersionVariableName) ?? string.Empty).Trim();

    public string FlowId => _flowId.Value;

    public string ServiceMessagesPath => _serviceMessagesPath.Value;
}