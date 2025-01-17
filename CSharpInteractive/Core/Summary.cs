// ReSharper disable ClassNeverInstantiated.Global

namespace CSharpInteractive.Core;

using Immutype;

[Target]
internal record Summary(bool? Success = null)
{
    public static readonly Summary Empty = new();
}