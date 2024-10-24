// Adds the namespace "HostApi" to use .NET build API
using HostApi;

var projectDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()[..4]);
Directory.CreateDirectory(projectDir);

// Creates a local tool manifest 
new DotNetNew()
    .WithTemplateName("tool-manifest")
    .WithWorkingDirectory(projectDir)
    .Run()
    .EnsureSuccess();

// Restore local tools
new DotNetToolRestore()
    .WithWorkingDirectory(projectDir)
    .Run()
    .EnsureSuccess();
