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
/// <param name="CommandLine">Command to run in container.</param>
/// <param name="Image">Docker image.</param>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Options">Additional docker options.</param>
/// <param name="ExposedPorts">Expose a port or a range of ports.</param>
/// <param name="PublishedPorts">Publish a container's port(s) to the host.</param>
/// <param name="Mounts">Adds bind mounts or volumes using the --mount flag.</param>
/// <param name="Volumes">Bind mount a volume.</param>
/// <param name="Hosts">Adds entries to container hosts file.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="CPUs">Number of CPUs.</param>
/// <param name="EntryPoint">Overwrite the default ENTRYPOINT of the image.</param>
/// <param name="HostName">Container host name.</param>
/// <param name="KernelMemory">Kernel memory limit.</param>
/// <param name="Memory">Memory limit.</param>
/// <param name="Name">Assign a name to the container.</param>
/// <param name="Network">Connect a container to a network.</param>
/// <param name="Platform">Set platform if server is multi-platform capable.</param>
/// <param name="Privileged">Give extended privileges to this container.</param>
/// <param name="Pull">Pull image before running (&quot;always&quot;|&quot;missing&quot;|&quot;never&quot;).</param>
/// <param name="Quiet">Suppress the pull output.</param>
/// <param name="ReadOnly">Mount the container's root filesystem as read only.</param>
/// <param name="AutoRemove">Automatically remove the container when it exits.</param>
/// <param name="User">Username or UID (format: &lt;name|uid&gt;[:&lt;group|gid&gt;]).</param>
/// <param name="ContainerWorkingDirectory">Working directory inside the container.</param>
/// <param name="EnvFile">A file with environment variables inside the container.</param>
/// <param name="Interactive">Keep STDIN open even if not attached.</param>
/// <param name="Tty">Allocate a pseudo-TTY.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DockerRun(
    ICommandLine CommandLine,
    string Image,
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<string> Options,
    IEnumerable<string> ExposedPorts,
    IEnumerable<string> PublishedPorts,
    IEnumerable<string> Mounts,
    IEnumerable<(string from, string to)> Volumes,
    IEnumerable<(string host, string ip)> Hosts,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    int? CPUs = null,
    string EntryPoint = "",
    string HostName = "",
    int? KernelMemory = null,
    int? Memory = null,
    string? Name = null,
    string Network = "",
    string Platform = "",
    bool? Privileged = null,
    DockerPullType? Pull = null,
    bool Quiet = false,
    bool? ReadOnly = null,
    bool? AutoRemove = null,
    string User = "",
    string ContainerWorkingDirectory = "",
    string EnvFile = "",
    bool Interactive = false,
    bool Tty = false,
    string ShortName = "")
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
                ("--rm", AutoRemove),
                ("--quiet", Quiet))
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
            .AddArgs(Hosts.Select(i => $"--add-host={i.host}={i.ip}"))
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