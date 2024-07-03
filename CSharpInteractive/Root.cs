// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive;

using Core;
using HostApi;
using JetBrains.TeamCity.ServiceMessages.Read;
using JetBrains.TeamCity.ServiceMessages.Write.Special;
using Microsoft.Extensions.DependencyInjection;

[ExcludeFromCodeCoverage]
internal record Root(
#if TOOL
    Program Program,
#endif
    IHost Host,
    IStatistics Statistics,
    ILog<Root> Log,
    IInfo Info,
    IExitTracker ExitTracker,
    HostComponents HostComponents,
    ICommandLineRunner CommandLineRunner,
    IBuildRunner BuildRunner,
    IEnvironment Environment,
    IDotNetEnvironment DotNetEnvironment,
    INuGet NuGet,
    IServiceMessageParser ServiceMessageParser,
    ITeamCityWriter TeamCityWriter): IServiceProvider
{
    private readonly Lazy<IServiceProvider> _serviceProvider = new(() =>
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<IServiceCollection>(_ => serviceCollection);
        serviceCollection.AddTransient(_ => Host);
        serviceCollection.AddTransient(_ => Host);
        serviceCollection.AddTransient(_ => HostComponents);
        serviceCollection.AddTransient(_ => NuGet);
        serviceCollection.AddTransient(_ => CommandLineRunner);
        serviceCollection.AddTransient(_ => BuildRunner);
        serviceCollection.AddTransient(_ => ServiceMessageParser);
        serviceCollection.AddTransient(_ => TeamCityWriter);
        return serviceCollection.BuildServiceProvider();
    });
    
    public object? GetService(Type serviceType) => 
        _serviceProvider.Value.GetService(serviceType);
}