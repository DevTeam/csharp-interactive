// ReSharper disable InconsistentNaming
namespace HostApi;

[ExcludeFromCodeCoverage]
public partial record CommandLine: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static CommandLine operator +(CommandLine command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static CommandLine operator -(CommandLine command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static CommandLine operator +(CommandLine command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static CommandLine operator -(CommandLine command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static CommandLine operator +(CommandLine command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static CommandLine operator -(CommandLine command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static CommandLine operator +(CommandLine command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static CommandLine operator -(CommandLine command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetCustom: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetCustom operator +(DotNetCustom command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetCustom operator -(DotNetCustom command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetCustom operator +(DotNetCustom command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetCustom operator -(DotNetCustom command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetCustom operator +(DotNetCustom command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetCustom operator -(DotNetCustom command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetCustom operator +(DotNetCustom command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetCustom operator -(DotNetCustom command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNet: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNet operator +(DotNet command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNet operator -(DotNet command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNet operator +(DotNet command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNet operator -(DotNet command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNet operator +(DotNet command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNet operator -(DotNet command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNet operator +(DotNet command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNet operator -(DotNet command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetExec: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetExec operator +(DotNetExec command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetExec operator -(DotNetExec command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetExec operator +(DotNetExec command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetExec operator -(DotNetExec command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetExec operator +(DotNetExec command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetExec operator -(DotNetExec command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetExec operator +(DotNetExec command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetExec operator -(DotNetExec command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetAddPackage: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetAddPackage operator +(DotNetAddPackage command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetAddPackage operator -(DotNetAddPackage command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetAddPackage operator +(DotNetAddPackage command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetAddPackage operator -(DotNetAddPackage command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetAddPackage operator +(DotNetAddPackage command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetAddPackage operator -(DotNetAddPackage command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetAddPackage operator +(DotNetAddPackage command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetAddPackage operator -(DotNetAddPackage command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetListPackage: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetListPackage operator +(DotNetListPackage command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetListPackage operator -(DotNetListPackage command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetListPackage operator +(DotNetListPackage command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetListPackage operator -(DotNetListPackage command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetListPackage operator +(DotNetListPackage command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetListPackage operator -(DotNetListPackage command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetListPackage operator +(DotNetListPackage command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetListPackage operator -(DotNetListPackage command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetRemovePackage: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetRemovePackage operator +(DotNetRemovePackage command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetRemovePackage operator -(DotNetRemovePackage command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetRemovePackage operator +(DotNetRemovePackage command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetRemovePackage operator -(DotNetRemovePackage command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetRemovePackage operator +(DotNetRemovePackage command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetRemovePackage operator -(DotNetRemovePackage command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetRemovePackage operator +(DotNetRemovePackage command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetRemovePackage operator -(DotNetRemovePackage command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetAddReference: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetAddReference operator +(DotNetAddReference command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetAddReference operator -(DotNetAddReference command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetAddReference operator +(DotNetAddReference command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetAddReference operator -(DotNetAddReference command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetAddReference operator +(DotNetAddReference command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetAddReference operator -(DotNetAddReference command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetAddReference operator +(DotNetAddReference command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetAddReference operator -(DotNetAddReference command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetListReference: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetListReference operator +(DotNetListReference command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetListReference operator -(DotNetListReference command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetListReference operator +(DotNetListReference command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetListReference operator -(DotNetListReference command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetListReference operator +(DotNetListReference command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetListReference operator -(DotNetListReference command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetListReference operator +(DotNetListReference command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetListReference operator -(DotNetListReference command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetRemoveReference: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetRemoveReference operator +(DotNetRemoveReference command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetRemoveReference operator -(DotNetRemoveReference command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetRemoveReference operator +(DotNetRemoveReference command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetRemoveReference operator -(DotNetRemoveReference command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetRemoveReference operator +(DotNetRemoveReference command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetRemoveReference operator -(DotNetRemoveReference command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetRemoveReference operator +(DotNetRemoveReference command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetRemoveReference operator -(DotNetRemoveReference command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetBuild: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetBuild operator +(DotNetBuild command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetBuild operator -(DotNetBuild command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetBuild operator +(DotNetBuild command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetBuild operator -(DotNetBuild command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetBuild operator +(DotNetBuild command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetBuild operator -(DotNetBuild command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetBuild operator +(DotNetBuild command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetBuild operator -(DotNetBuild command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetBuildServerShutdown: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetBuildServerShutdown operator +(DotNetBuildServerShutdown command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetBuildServerShutdown operator -(DotNetBuildServerShutdown command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetBuildServerShutdown operator +(DotNetBuildServerShutdown command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetBuildServerShutdown operator -(DotNetBuildServerShutdown command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetBuildServerShutdown operator +(DotNetBuildServerShutdown command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetBuildServerShutdown operator -(DotNetBuildServerShutdown command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetBuildServerShutdown operator +(DotNetBuildServerShutdown command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetBuildServerShutdown operator -(DotNetBuildServerShutdown command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetClean: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetClean operator +(DotNetClean command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetClean operator -(DotNetClean command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetClean operator +(DotNetClean command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetClean operator -(DotNetClean command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetClean operator +(DotNetClean command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetClean operator -(DotNetClean command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetClean operator +(DotNetClean command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetClean operator -(DotNetClean command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetDevCertsHttps: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetDevCertsHttps operator +(DotNetDevCertsHttps command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetDevCertsHttps operator -(DotNetDevCertsHttps command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetDevCertsHttps operator +(DotNetDevCertsHttps command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetDevCertsHttps operator -(DotNetDevCertsHttps command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetDevCertsHttps operator +(DotNetDevCertsHttps command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetDevCertsHttps operator -(DotNetDevCertsHttps command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetDevCertsHttps operator +(DotNetDevCertsHttps command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetDevCertsHttps operator -(DotNetDevCertsHttps command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetFormat: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetFormat operator +(DotNetFormat command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetFormat operator -(DotNetFormat command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetFormat operator +(DotNetFormat command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetFormat operator -(DotNetFormat command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetFormat operator +(DotNetFormat command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetFormat operator -(DotNetFormat command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetFormat operator +(DotNetFormat command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetFormat operator -(DotNetFormat command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetFormatWhitespace: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetFormatWhitespace operator +(DotNetFormatWhitespace command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetFormatWhitespace operator -(DotNetFormatWhitespace command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetFormatWhitespace operator +(DotNetFormatWhitespace command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetFormatWhitespace operator -(DotNetFormatWhitespace command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetFormatWhitespace operator +(DotNetFormatWhitespace command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetFormatWhitespace operator -(DotNetFormatWhitespace command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetFormatWhitespace operator +(DotNetFormatWhitespace command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetFormatWhitespace operator -(DotNetFormatWhitespace command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetFormatStyle: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetFormatStyle operator +(DotNetFormatStyle command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetFormatStyle operator -(DotNetFormatStyle command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetFormatStyle operator +(DotNetFormatStyle command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetFormatStyle operator -(DotNetFormatStyle command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetFormatStyle operator +(DotNetFormatStyle command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetFormatStyle operator -(DotNetFormatStyle command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetFormatStyle operator +(DotNetFormatStyle command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetFormatStyle operator -(DotNetFormatStyle command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetFormatAnalyzers: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetFormatAnalyzers operator +(DotNetFormatAnalyzers command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetFormatAnalyzers operator -(DotNetFormatAnalyzers command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetFormatAnalyzers operator +(DotNetFormatAnalyzers command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetFormatAnalyzers operator -(DotNetFormatAnalyzers command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetFormatAnalyzers operator +(DotNetFormatAnalyzers command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetFormatAnalyzers operator -(DotNetFormatAnalyzers command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetFormatAnalyzers operator +(DotNetFormatAnalyzers command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetFormatAnalyzers operator -(DotNetFormatAnalyzers command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record MSBuild: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static MSBuild operator +(MSBuild command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static MSBuild operator -(MSBuild command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static MSBuild operator +(MSBuild command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static MSBuild operator -(MSBuild command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static MSBuild operator +(MSBuild command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static MSBuild operator -(MSBuild command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static MSBuild operator +(MSBuild command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static MSBuild operator -(MSBuild command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetNew: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNew operator +(DotNetNew command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNew operator -(DotNetNew command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetNew operator +(DotNetNew command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNew operator -(DotNetNew command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNew operator +(DotNetNew command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNew operator -(DotNetNew command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNew operator +(DotNetNew command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNew operator -(DotNetNew command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetNuGetPush: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetPush operator +(DotNetNuGetPush command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetPush operator -(DotNetNuGetPush command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetNuGetPush operator +(DotNetNuGetPush command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetPush operator -(DotNetNuGetPush command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetPush operator +(DotNetNuGetPush command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetPush operator -(DotNetNuGetPush command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetPush operator +(DotNetNuGetPush command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetPush operator -(DotNetNuGetPush command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetPack: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetPack operator +(DotNetPack command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetPack operator -(DotNetPack command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetPack operator +(DotNetPack command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetPack operator -(DotNetPack command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetPack operator +(DotNetPack command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetPack operator -(DotNetPack command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetPack operator +(DotNetPack command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetPack operator -(DotNetPack command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetPublish: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetPublish operator +(DotNetPublish command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetPublish operator -(DotNetPublish command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetPublish operator +(DotNetPublish command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetPublish operator -(DotNetPublish command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetPublish operator +(DotNetPublish command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetPublish operator -(DotNetPublish command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetPublish operator +(DotNetPublish command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetPublish operator -(DotNetPublish command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetRestore: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetRestore operator +(DotNetRestore command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetRestore operator -(DotNetRestore command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetRestore operator +(DotNetRestore command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetRestore operator -(DotNetRestore command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetRestore operator +(DotNetRestore command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetRestore operator -(DotNetRestore command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetRestore operator +(DotNetRestore command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetRestore operator -(DotNetRestore command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetRun: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetRun operator +(DotNetRun command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetRun operator -(DotNetRun command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetRun operator +(DotNetRun command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetRun operator -(DotNetRun command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetRun operator +(DotNetRun command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetRun operator -(DotNetRun command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetRun operator +(DotNetRun command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetRun operator -(DotNetRun command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetTest: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetTest operator +(DotNetTest command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetTest operator -(DotNetTest command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetTest operator +(DotNetTest command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetTest operator -(DotNetTest command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetTest operator +(DotNetTest command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetTest operator -(DotNetTest command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetTest operator +(DotNetTest command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetTest operator -(DotNetTest command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetToolRestore: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolRestore operator +(DotNetToolRestore command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolRestore operator -(DotNetToolRestore command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetToolRestore operator +(DotNetToolRestore command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolRestore operator -(DotNetToolRestore command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolRestore operator +(DotNetToolRestore command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolRestore operator -(DotNetToolRestore command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolRestore operator +(DotNetToolRestore command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolRestore operator -(DotNetToolRestore command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record VSTest: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static VSTest operator +(VSTest command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static VSTest operator -(VSTest command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static VSTest operator +(VSTest command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static VSTest operator -(VSTest command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static VSTest operator +(VSTest command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static VSTest operator -(VSTest command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static VSTest operator +(VSTest command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static VSTest operator -(VSTest command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DockerCustom: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DockerCustom operator +(DockerCustom command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DockerCustom operator -(DockerCustom command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DockerCustom operator +(DockerCustom command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DockerCustom operator -(DockerCustom command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DockerCustom operator +(DockerCustom command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DockerCustom operator -(DockerCustom command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DockerCustom operator +(DockerCustom command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DockerCustom operator -(DockerCustom command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DockerRun: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DockerRun operator +(DockerRun command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DockerRun operator -(DockerRun command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DockerRun operator +(DockerRun command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DockerRun operator -(DockerRun command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DockerRun operator +(DockerRun command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DockerRun operator -(DockerRun command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DockerRun operator +(DockerRun command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DockerRun operator -(DockerRun command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

