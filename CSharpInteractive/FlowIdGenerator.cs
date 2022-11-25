﻿// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive;

using JetBrains.TeamCity.ServiceMessages.Write.Special;

internal class FlowIdGenerator : IFlowIdGenerator, IFlowContext
{
    private readonly object _lockObject = new();
    private string _nextFlowId;
    [ThreadStatic] private static string? _currentFlowId;

    public FlowIdGenerator(ICISettings ciSettings) =>
        _nextFlowId = ciSettings.FlowId;

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