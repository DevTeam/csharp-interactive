namespace HostApi;

/// <summary>
/// Workload-set update mode.
/// </summary>
public enum DotNetWorkloadUpdateMode
{
    /// <summary>
    /// Workload-set update mode.
    /// </summary>
    WorkloadSet,
    
    /// <summary>
    /// Manifests update mode.
    /// </summary>
    Manifests
}

// ReSharper disable once UnusedType.Global
internal static class DotNetWorkloadUpdateModeExtensions
{
    // ReSharper disable once UnusedParameter.Global
    // ReSharper disable once UnusedMember.Global
    public static string[] ToArgs(this DotNetWorkloadUpdateMode? updateMode, string name, string collectionSeparator) =>
        updateMode switch
        {
            DotNetWorkloadUpdateMode.WorkloadSet => [name, "workload-set"],
            DotNetWorkloadUpdateMode.Manifests => [name, "manifests"],
            _ => []
        };
}