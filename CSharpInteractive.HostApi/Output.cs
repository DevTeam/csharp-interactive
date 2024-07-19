namespace HostApi;

/// <summary>
/// Represents a command line event.
/// </summary>
/// <param name="StartInfo">Process startup information.</param>
/// <param name="IsError"><c>False</c> if a line from stdOut and <c>True</c> when from stdErr.</param>
/// <param name="Line">Output line from a running command line.</param>
/// <param name="ProcessId">Unique identifier of the managed .NET process.</param>
public record Output(
    // Process startup information.
    IStartInfo StartInfo,
    // <c>False</c> if a line from stdOut and <c>True</c> when from stdErr.
    bool IsError,
    // Output line from a running command line.
    string Line,
    // Unique identifier of the managed .NET process.
    int ProcessId)
{
    /// <summary>
    /// Determines whether the event was handled. If you set this property to <c>True</c>, the event will not be included in the summary statistics and will not be output to the output stream, so it is assumed that in this case the processing of this event is completely manual.
    /// </summary>
    public bool Handled { get; set; }

    /// <inheritdoc />
    public override string ToString() => IsError ? $"ERR {Line}" : Line;
}