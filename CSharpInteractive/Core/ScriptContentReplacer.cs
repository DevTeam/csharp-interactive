// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive.Core;

using NuGet.Packaging;
using Pure.DI;

internal class ScriptContentReplacer(
    INuGetReferenceResolver nuGetReferenceResolver,
    ICommandFactory<ICodeSource> commandFactory,
    IRuntimeExplorer runtimeExplorer,
    ICommandsRunner commandsRunner,
    IFileSystem fileSystem,
    IUniqueNameGenerator uniqueNameGenerator,
    IEnvironment environment,
    [Tag(typeof(LineCodeSource))] Func<string, ICodeSource> codeSourceFactory)
    : IScriptContentReplacer
{
    [SuppressMessage("Performance", "CA1806:Do not ignore method results")]
    public IEnumerable<string> Replace(IEnumerable<string> lines)
    {
        var allAssemblies = new HashSet<string>();
        var commandsToRun = new List<ICommand>();
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var assemblies = new HashSet<string>();
            var commands = commandFactory.Create(codeSourceFactory(line)).ToArray();
            var repl = false;
            foreach (var command in commands)
            {
                switch (command)
                {
                    case AddNuGetReferenceCommand referenceCommand:
                        repl = true;
                        if (nuGetReferenceResolver.TryResolveAssemblies(referenceCommand.PackageId, referenceCommand.VersionRange, out var resolvedAssemblies))
                        {
                            foreach (var assembly in resolvedAssemblies.Where(i => !allAssemblies.Contains(i.FilePath)))
                            {
                                // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                                if (runtimeExplorer.TryFindRuntimeAssembly(assembly.FilePath, out var runtimeAssemblyPath))
                                {
                                    assemblies.Add(runtimeAssemblyPath);
                                }
                                else
                                {
                                    assemblies.Add(assembly.FilePath);
                                }
                            }
                        }
                        break;

                    case ScriptCommand:
                    case CodeCommand:
                        break;

                    default:
                        repl = true;
                        commandsToRun.Add(command);
                        break;
                }
            }

            var newAssemblies = assemblies.Except(allAssemblies).ToArray();
            if (newAssemblies.Length == 0)
            {
                if (!repl)
                {
                    yield return line;
                }
            }
            else
            {
                allAssemblies.AddRange(newAssemblies);
                var scriptFile = Path.Combine(environment.GetPath(SpecialFolder.Temp), uniqueNameGenerator.Generate());
                fileSystem.WriteAllLines(scriptFile, newAssemblies.Select(i => $"#r \"{i}\""));
                yield return $"#load \"{scriptFile}\"";
            }
        }

        // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
        commandsRunner.Run(commandsToRun).ToArray();
    }

}