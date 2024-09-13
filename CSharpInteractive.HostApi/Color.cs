namespace HostApi;

/// <summary>
/// Logical colors.
/// <example>
/// <code>
/// WriteLine("Hello !!!", Color.Highlighted);
/// </code>
/// </example>
/// </summary>
/// <seealso cref="IHost.WriteLine"/>
public enum Color
{
    /// <summary>
    /// Standard text output color.
    /// </summary>
    Default,

    /// <summary>
    /// The color of the header output. 
    /// </summary>
    Header,

    /// <summary>
    /// Color of trace messages. 
    /// </summary>
    Trace,

    /// <summary>
    /// Color of the success messages. 
    /// </summary>
    Success,

    /// <summary>
    /// Color of messages containing warnings.
    /// </summary>
    Warning,

    /// <summary>
    /// Color of messages containing errors.
    /// </summary>
    Error,

    /// <summary>
    /// Colour for details.
    /// </summary>
    Details,

    /// <summary>
    /// Colour to display important information.
    /// </summary>
    Highlighted
}