namespace HostApi;

public record Output(
    IStartInfo StartInfo,
    bool IsError,
    string Line,
    int ProcessId)
{
    public bool Handled { get; set; }

    public override string ToString() => IsError ? $"ERR {Line}" : Line;
}