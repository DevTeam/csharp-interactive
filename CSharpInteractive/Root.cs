// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace CSharpInteractive;

using Core;
using HostApi;
using HostApi.Internal;
using JetBrains.TeamCity.ServiceMessages.Read;
using JetBrains.TeamCity.ServiceMessages.Write.Special;
using Microsoft.Extensions.DependencyInjection;

[ExcludeFromCodeCoverage]
internal record Root(
#if TOOL
    Program Program,
#endif
    IHost Host,
    ITestEnvironment TestEnvironment,
    IStatisticsRegistry StatisticsRegistry,
    ILog<Root> Log,
    IInfo Info,
    IExitTracker ExitTracker,
    Lazy<HostComponents> HostComponents,
    Lazy<ICommandLineRunner> CommandLineRunner,
    Lazy<IBuildRunner> BuildRunner,
    Lazy<IEnvironment> Environment,
    Lazy<IDotNetEnvironment> DotNetEnvironment,
    Lazy<INuGet> NuGet,
    Lazy<IServiceMessageParser> ServiceMessageParser,
    Lazy<ITeamCityWriter> TeamCityWriter,
    IConsoleHandler ConsoleHandler) : IServiceProvider
{
    private readonly Lazy<IServiceProvider> _serviceProvider = new(() =>
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<IServiceCollection>(_ => serviceCollection);
        serviceCollection.AddTransient(_ => Host);
        serviceCollection.AddTransient(_ => HostComponents.Value);
        serviceCollection.AddTransient(_ => NuGet.Value);
        serviceCollection.AddTransient(_ => CommandLineRunner.Value);
        serviceCollection.AddTransient(_ => BuildRunner.Value);
        serviceCollection.AddTransient(_ => ServiceMessageParser.Value);
        serviceCollection.AddTransient(_ => TeamCityWriter.Value);
        return serviceCollection.BuildServiceProvider();
    });

    public object? GetService(Type serviceType) =>
        _serviceProvider.Value.GetService(serviceType);
}