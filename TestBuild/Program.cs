using System.CommandLine;
using HostApi;
using NuGet.Configuration;
using Pure.DI;
using static Pure.DI.Lifetime;
// ReSharper disable HeuristicUnreachableCode
#pragma warning disable CS0162 // Unreachable code detected

WriteLine("Hello");
return await new Composition().Root.RunAsync(CancellationToken.None);

DI.Setup(nameof(Composition))
    .Hint(Hint.ThreadSafe, "Off")
    .Hint(Hint.Resolve, "Off")
    .Root<RootTarget>(nameof(Composition.Root))
    
    .DefaultLifetime(PerResolve)
    .Bind().To<RootCommand>()
    .Bind().To<Settings>()
    
    .DefaultLifetime(PerBlock)
    
    // Targets
    .Bind(Tag.Type).To<MyTarget>();

internal interface ITarget<T>
{
    Task<T> RunAsync(CancellationToken cancellationToken);
}

internal interface IInitializable
{
    Task InitializeAsync(CancellationToken cancellationToken);
}

internal class RootTarget(
    RootCommand rootCommand,
    IEnumerable<IInitializable> initializables)
    : ITarget<int>
{
    public async Task<int> RunAsync(CancellationToken cancellationToken)
    {
        foreach (var initializable in initializables)
        {
            await initializable.InitializeAsync(cancellationToken);
        }

        return await rootCommand.InvokeAsync(Args.ToArray());
    }

}

internal class MyTarget(Commands commands)
    : IInitializable, ITarget<int>
{
    public Task InitializeAsync(CancellationToken cancellationToken) =>
        commands.RegisterAsync(this, "Run", "run", "r");

    public async Task<int> RunAsync(CancellationToken cancellationToken)
    {
        await new DotNetBuild()
            .WithProject("unknown project")
            .BuildAsync(cancellationToken: cancellationToken).EnsureSuccess(failureExitCode: 33);

        return 0;
    }

}

internal class Commands(RootCommand rootCommand)
{
    public Task RegisterAsync<T>(
        ITarget<T> target,
        string description,
        string name,
        params string[] aliases)
    {
        var command = new Command(name, description);
        command.SetHandler(ctx => target.RunAsync(ctx.GetCancellationToken()));
        foreach (var alias in aliases)
        {
            command.AddAlias(alias);
        }
        
        rootCommand.AddCommand(command);
        return Task.CompletedTask;
    }
}