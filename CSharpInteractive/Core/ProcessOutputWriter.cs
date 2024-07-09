// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive.Core;

using HostApi;

internal class ProcessOutputWriter(IConsole console) : IProcessOutputWriter
{
    public void Write(Output output)
    {
        if (output.IsError)
        {
            console.WriteToErr(output.Line, System.Environment.NewLine);
        }
        else
        {
            console.WriteToOut((default, output.Line), (default, System.Environment.NewLine));
        }
    }
}