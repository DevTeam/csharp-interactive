// ReSharper disable ClassNeverInstantiated.Global

namespace CSharpInteractive.Core;

using JetBrains.TeamCity.ServiceMessages.Write.Special;

internal class FlowIdGenerator(ICISettings ciSettings) : IFlowIdGenerator, IFlowContext
{
#if NET9_0_OR_GREATER
    private readonly Lock _lockObject = new();
#else
    private readonly object _lockObject = new();
#endif
    private string _nextFlowId = ciSettings.FlowId;
    [ThreadStatic] private static string? _currentFlowId;

    public string CurrentFlowId
    {
        get => _currentFlowId ?? _nextFlowId;
        private set => _currentFlowId = value;
    }

    public string NewFlowId() => CurrentFlowId = GenerateFlowId();

    private string GenerateFlowId()
    {
        lock (_lockObject)
        {
            if (string.IsNullOrWhiteSpace(_nextFlowId))
            {
                return Guid.NewGuid().ToString()[..8];
            }

            var currentFlowId = _nextFlowId;
            _nextFlowId = string.Empty;
            return currentFlowId;
        }
    }
}