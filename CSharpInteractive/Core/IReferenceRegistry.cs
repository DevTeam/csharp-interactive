namespace CSharpInteractive.Core;

internal interface IReferenceRegistry
{
    bool TryRegisterAssembly(string assemblyPath, out string description);
}