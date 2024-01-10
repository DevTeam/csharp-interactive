namespace CSharpInteractive;

using System.Collections.ObjectModel;
using Microsoft.CodeAnalysis;

[ExcludeFromCodeCoverage]
internal readonly record struct CompilationDiagnostics(ICommand SourceCommand, ReadOnlyCollection<Diagnostic> Diagnostics);