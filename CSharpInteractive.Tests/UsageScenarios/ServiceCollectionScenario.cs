// ReSharper disable StringLiteralTypo
// ReSharper disable SuggestVarOrType_BuiltInTypes

namespace CSharpInteractive.Tests.UsageScenarios;

using Microsoft.Extensions.DependencyInjection;

public class ServiceCollectionScenario(ITestOutputHelper output) : BaseScenario(output)
{
    [Fact]
    // $visible=true
    // $tag=03 Microsoft DI
    // $priority=03
    // $description=Service collection
    // {
    public void Run()
    {
        var serviceProvider =
            GetService<IServiceCollection>()
                .AddTransient<MyTask>()
                .BuildServiceProvider();

        var myTask = serviceProvider.GetRequiredService<MyTask>();
        var exitCode = myTask.Run();
        exitCode.ShouldBe(0);
    }

    private class MyTask(ICommandLineRunner runner)
    {
        public int? Run() => runner
            .Run(new CommandLine("whoami"))
            .EnsureSuccess()
            .ExitCode;
    }

    // }
}