// ReSharper disable ClassNeverInstantiated.Global

namespace CSharpInteractive.Core;

using Immutype;

[Target]
internal record Summary(bool? Success = default)
{
    public static readonly Summary Empty = new();
}