namespace CSharpInteractive;

using System.Diagnostics.CodeAnalysis;
using Immutype;

[ExcludeFromCodeCoverage]
[Target]
internal record CommandResult(ICommand Command, bool? Success, int? ExitCode = default);