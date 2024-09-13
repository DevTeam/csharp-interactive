// ReSharper disable UnusedMember.Global

namespace HostApi;

/// <summary>
/// Docker pull type.
/// </summary>
/// <seealso cref="DockerRun"/>
public enum DockerPullType
{
    /// <summary>
    /// Docker always pulls the image from the registry.
    /// </summary>
    Always,

    /// <summary>
    /// Docker pulls the image only if it's not available in the platform cache.
    /// </summary>
    Missing,

    /// <summary>
    /// Docker doesn't pull the image from a registry and relies on the platform cached image. If there is no cached image, a failure is reported.
    /// </summary>
    Never
}