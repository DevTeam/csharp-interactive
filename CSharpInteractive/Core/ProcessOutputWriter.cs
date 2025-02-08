// ReSharper disable ClassNeverInstantiated.Global

namespace CSharpInteractive.Core;

using HostApi;

internal class ProcessOutputWriter(IConsole console) : IProcessOutputWriter
{
    public void Write(Output output)
    {
        if (output.IsError)
        {
            console.WriteToErr((null, output.Line), (null, System.Environment.NewLine));
        }
        else
        {
            console.WriteToOut((null, output.Line), (null, System.Environment.NewLine));
        }
    }
}