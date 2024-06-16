// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive;

using HostApi;

internal class AddNuGetReferenceCommandRunner(
    ILog<AddNuGetReferenceCommandRunner> log,
    INuGetReferenceResolver nuGetReferenceResolver,
    IReferenceRegistry referenceRegistry) : ICommandRunner
{
    public CommandResult TryRun(ICommand command)
    {
        if (command is not AddNuGetReferenceCommand addPackageReferenceCommand)
        {
            return new CommandResult(command, default);
        }

        if (!nuGetReferenceResolver.TryResolveAssemblies(addPackageReferenceCommand.PackageId, addPackageReferenceCommand.VersionRange, out var assemblies))
        {
            return new CommandResult(command, false);
        }

        var success = true;
        foreach (var assembly in assemblies)
        {
            if (referenceRegistry.TryRegisterAssembly(assembly.FilePath, out var description))
            {
                log.Info(Text.Tab, new Text(assembly.Name, Color.Highlighted));
            }
            else
            {
                log.Error(ErrorId.NuGet, $"Cannot add the reference \"{assembly.Name}\": {description}");
                success = false;
            }
        }

        return new CommandResult(command, success);
    }
}