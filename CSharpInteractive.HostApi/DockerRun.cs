// ReSharper disable InconsistentNaming
// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable InvertIf
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedType.Global
namespace HostApi;

using Internal;
using Internal.Cmd;
using Internal.Docker;

/// <summary>
/// Docker runs a command in isolated containers. A container is a process which runs on a host. The host may be local or remote. When an operator executes docker run, the container process that runs is isolated in that it has its own file system, its own networking, and its own isolated process tree separate from the host.
/// <example>
/// <code>
/// var cmd = new CommandLine("whoami");
///
/// await new DockerRun("ubuntu")
///     .WithCommandLine(cmd)
///     .WithPull(DockerPullType.Always)
///     .WithAutoRemove(true)
///     .RunAsync().EnsureSuccess();
/// </code>
/// </example>
/// </summary>
[Target]
public partial record DockerRun(
    // Command to run in container.
    ICommandLine CommandLine,
    // Docker image.
    string Image,
    // Specifies the set of command line arguments to use when starting the tool.
    IEnumerable<string> Args,
    // Specifies the set of environment variables that apply to this process and its child processes.
    IEnumerable<(string name, string value)> Vars,
    // Additional docker options
    IEnumerable<string> Options,
    // Expose a port or a range of ports
    IEnumerable<string> ExposedPorts,
    // Publish a container's port(s) to the host
    IEnumerable<string> PublishedPorts,
    // Adds bind mounts or volumes using the --mount flag
    IEnumerable<string> Mounts,
    // Bind mount a volume
    IEnumerable<(string from, string to)> Volumes,
    // Overrides the tool executable path.
    string ExecutablePath = "",
    // Specifies the working directory for the tool to be started.
    string WorkingDirectory = "",
    // Number of CPUs
    int? CPUs = default,
    // Overwrite the default ENTRYPOINT of the image
    string EntryPoint = "",
    // Container host name
    string HostName = "",
    // Kernel memory limit
    int? KernelMemory = default,
    // Memory limit
    int? Memory = default,
    // Assign a name to the container
    string? Name = default,
    // Connect a container to a network
    string Network = "",
    // Set platform if server is multi-platform capable
    string Platform = "",
    // Give extended privileges to this container
    bool? Privileged = default,
    // Pull image before running (&quot;always&quot;|&quot;missing&quot;|&quot;never&quot;)
    DockerPullType? Pull = default,
    // Mount the container's root filesystem as read only
    bool? ReadOnly = default,
    // Automatically remove the container when it exits
    bool? AutoRemove = default,
    // Username or UID (format: &lt;name|uid&gt;[:&lt;group|gid&gt;])
    string User = "",
    // Working directory inside the container
    string ContainerWorkingDirectory = "",
    // A file with environment variables inside the container
    string EnvFile = "",
    // Specifies a short name for this operation.
    string ShortName = "",
    // Keep STDIN open even if not attached
    bool Interactive = false,
    // Allocate a pseudo-TTY
    bool Tty = false
    )
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="image">Docker image.</param>
    public DockerRun(string image = "") : this(new CommandLine(string.Empty), image)
    { }

    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="commandLine">Command to run in container.</param>
    /// <param name="image">Docker image.</param>
    public DockerRun(ICommandLine commandLine, string image)
        : this(
            commandLine,
            image,
            [],
            [],
            [],
            [],
            [],
            [],
            [])
    { }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        var directoryMap = new Dictionary<string, string>();
        var pathResolver = new PathResolver(Platform, directoryMap);
        IStartInfo startInfo;
        using (host.GetService<HostComponents>().PathResolverContext.Register(pathResolver))
        {
            startInfo = CommandLine.GetStartInfo(host);
        }
        
        var settings = host.GetService<HostComponents>().DockerSettings;
        var cmd = new CommandLine(string.IsNullOrWhiteSpace(ExecutablePath) ? settings.DockerExecutablePath : ExecutablePath)
            .WithShortName(!string.IsNullOrWhiteSpace(ShortName) ? ShortName : $"{startInfo.ShortName} in the docker container {Image}")
            .WithWorkingDirectory(WorkingDirectory)
            .WithArgs("run")
            .AddBooleanArgs(
                ("--interactive", Interactive),
                ("--tty", Tty),
                ("--privileged", Privileged),
                ("--read-only", ReadOnly),
                ("--rm", AutoRemove))
            .AddArgs("--expose", ExposedPorts)
            .AddArgs("--publish", PublishedPorts)
            .AddArgs("--mount", Mounts)
            .AddArgs(
                ("--cpus", CPUs?.ToString() ?? ""),
                ("--entrypoint", EntryPoint),
                ("--hostname", HostName),
                ("--kernel-memory", KernelMemory?.ToString() ?? ""),
                ("--memory", Memory?.ToString() ?? ""),
                ("--name", Name ?? string.Empty),
                ("--network", Network),
                ("--platform", Platform),
                ("--pull", Pull?.ToString()?.ToLowerInvariant() ?? string.Empty),
                ("--user", User),
                ("--workdir", ContainerWorkingDirectory),
                ("--env-file", EnvFile))
            .AddArgs(Args.ToArray())
            .AddValues("-e", "=", startInfo.Vars.ToArray());

        var additionalVolums = directoryMap.Select(i => (i.Key, i.Value));
        return cmd
            .AddValues("-v", ":", additionalVolums.Select(i => (pathResolver.ToAbsolutPath(i.Key), i.Value)).ToArray())
            .AddValues("-v", ":", Volumes.ToArray())
            .AddArgs(Options.ToArray())
            .AddArgs(Image)
            .AddArgs(startInfo.ExecutablePath)
            .AddArgs(startInfo.Args.ToArray())
            .WithVars(Vars.ToArray());
    }
    
    /// <inheritdoc/>
    public override string ToString() => 
        string.IsNullOrWhiteSpace(ShortName)
            ? $"{CommandLine} in the docker container {Image}"
            : ShortName;
    
    private class PathResolver(string platform, IDictionary<string, string> directoryMap) : IPathResolver
    {
        public string Resolve(IHost host, string path, IPathResolver nextResolver)
        {
            path = Path.GetFullPath(path);
            if (!directoryMap.TryGetValue(path, out var toPath))
            {
                var isWindows = IsWindows();
                var rootDirectory = isWindows ? "c:" : string.Empty;
                toPath = $"{rootDirectory}/.{Guid.NewGuid().ToString().Substring(0, 8)}";
                directoryMap.Add(path, toPath);
            }

            return toPath;
        }
        
        public string ToAbsolutPath(string path)
        {
            if (IsWindows())
            {
                return path;
            }
            
            path = path.Replace(":", string.Empty).Replace('\\', '/');
            if (path.Length > 0 && path[0] != '/')
            {
                path = "/" + path;
            }

            return path;
        }
        
        private bool IsWindows() => platform.ToLower().Contains("windows");
    }
}