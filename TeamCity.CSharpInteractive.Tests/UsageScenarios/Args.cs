// ReSharper disable StringLiteralTypo
// ReSharper disable SuggestVarOrType_BuiltInTypes
namespace TeamCity.CSharpInteractive.Tests.UsageScenarios
{
    using Xunit;

    public class Args: Scenario
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=08 Global state
            // $priority=00
            // $description=Using _Args_
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
}