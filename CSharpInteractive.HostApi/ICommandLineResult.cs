namespace HostApi;

public interface ICommandLineResult
{
    IStartInfo StartInfo { get; }
    
    ProcessState State { get; }
    
    long ElapsedMilliseconds { get; }
    
    int? ExitCode { get; }
    
    Exception? Error { get; }
}