namespace CSharpInteractive.Tests.UsageScenarios;

public class ArgsScenario : BaseScenario
{
    [Fact]
    public void Run()
    {
        // $visible=true
        // $tag=02 Arguments and parameters
        // $priority=00
        // $description=Using Args
        // $header=_Args_ have got from the script arguments.
        // {
        if (Args.Count > 0)
        {
            WriteLine(Args[0]);
        }

        if (Args.Count > 1)
        {
            WriteLine(Args[1]);
        }
        // }
    }
}