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
public partial record DotNetNewList: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewList operator +(DotNetNewList command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewList operator -(DotNetNewList command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetNewList operator +(DotNetNewList command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewList operator -(DotNetNewList command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewList operator +(DotNetNewList command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewList operator -(DotNetNewList command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewList operator +(DotNetNewList command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewList operator -(DotNetNewList command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetNewSearch: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewSearch operator +(DotNetNewSearch command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewSearch operator -(DotNetNewSearch command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetNewSearch operator +(DotNetNewSearch command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewSearch operator -(DotNetNewSearch command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewSearch operator +(DotNetNewSearch command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewSearch operator -(DotNetNewSearch command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewSearch operator +(DotNetNewSearch command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewSearch operator -(DotNetNewSearch command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetNewDetails: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewDetails operator +(DotNetNewDetails command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewDetails operator -(DotNetNewDetails command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetNewDetails operator +(DotNetNewDetails command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewDetails operator -(DotNetNewDetails command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewDetails operator +(DotNetNewDetails command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewDetails operator -(DotNetNewDetails command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewDetails operator +(DotNetNewDetails command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewDetails operator -(DotNetNewDetails command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetNewInstall: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewInstall operator +(DotNetNewInstall command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewInstall operator -(DotNetNewInstall command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetNewInstall operator +(DotNetNewInstall command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewInstall operator -(DotNetNewInstall command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewInstall operator +(DotNetNewInstall command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewInstall operator -(DotNetNewInstall command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewInstall operator +(DotNetNewInstall command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewInstall operator -(DotNetNewInstall command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetNewUninstall: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewUninstall operator +(DotNetNewUninstall command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewUninstall operator -(DotNetNewUninstall command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetNewUninstall operator +(DotNetNewUninstall command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewUninstall operator -(DotNetNewUninstall command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewUninstall operator +(DotNetNewUninstall command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewUninstall operator -(DotNetNewUninstall command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewUninstall operator +(DotNetNewUninstall command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewUninstall operator -(DotNetNewUninstall command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetNewUpdate: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewUpdate operator +(DotNetNewUpdate command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewUpdate operator -(DotNetNewUpdate command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetNewUpdate operator +(DotNetNewUpdate command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewUpdate operator -(DotNetNewUpdate command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewUpdate operator +(DotNetNewUpdate command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewUpdate operator -(DotNetNewUpdate command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewUpdate operator +(DotNetNewUpdate command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNewUpdate operator -(DotNetNewUpdate command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetNuGetDelete: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetDelete operator +(DotNetNuGetDelete command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetDelete operator -(DotNetNuGetDelete command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetNuGetDelete operator +(DotNetNuGetDelete command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetDelete operator -(DotNetNuGetDelete command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetDelete operator +(DotNetNuGetDelete command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetDelete operator -(DotNetNuGetDelete command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetDelete operator +(DotNetNuGetDelete command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetDelete operator -(DotNetNuGetDelete command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetNuGetLocalsClear: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetLocalsClear operator +(DotNetNuGetLocalsClear command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetLocalsClear operator -(DotNetNuGetLocalsClear command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetNuGetLocalsClear operator +(DotNetNuGetLocalsClear command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetLocalsClear operator -(DotNetNuGetLocalsClear command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetLocalsClear operator +(DotNetNuGetLocalsClear command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetLocalsClear operator -(DotNetNuGetLocalsClear command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetLocalsClear operator +(DotNetNuGetLocalsClear command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetLocalsClear operator -(DotNetNuGetLocalsClear command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetNuGetLocalsList: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetLocalsList operator +(DotNetNuGetLocalsList command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetLocalsList operator -(DotNetNuGetLocalsList command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetNuGetLocalsList operator +(DotNetNuGetLocalsList command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetLocalsList operator -(DotNetNuGetLocalsList command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetLocalsList operator +(DotNetNuGetLocalsList command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetLocalsList operator -(DotNetNuGetLocalsList command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetLocalsList operator +(DotNetNuGetLocalsList command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetLocalsList operator -(DotNetNuGetLocalsList command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
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
public partial record DotNetNuGetAddSource: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetAddSource operator +(DotNetNuGetAddSource command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetAddSource operator -(DotNetNuGetAddSource command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetNuGetAddSource operator +(DotNetNuGetAddSource command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetAddSource operator -(DotNetNuGetAddSource command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetAddSource operator +(DotNetNuGetAddSource command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetAddSource operator -(DotNetNuGetAddSource command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetAddSource operator +(DotNetNuGetAddSource command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetAddSource operator -(DotNetNuGetAddSource command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetNuGetDisableSource: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetDisableSource operator +(DotNetNuGetDisableSource command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetDisableSource operator -(DotNetNuGetDisableSource command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetNuGetDisableSource operator +(DotNetNuGetDisableSource command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetDisableSource operator -(DotNetNuGetDisableSource command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetDisableSource operator +(DotNetNuGetDisableSource command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetDisableSource operator -(DotNetNuGetDisableSource command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetDisableSource operator +(DotNetNuGetDisableSource command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetDisableSource operator -(DotNetNuGetDisableSource command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetNuGetEnableSource: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetEnableSource operator +(DotNetNuGetEnableSource command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetEnableSource operator -(DotNetNuGetEnableSource command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetNuGetEnableSource operator +(DotNetNuGetEnableSource command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetEnableSource operator -(DotNetNuGetEnableSource command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetEnableSource operator +(DotNetNuGetEnableSource command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetEnableSource operator -(DotNetNuGetEnableSource command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetEnableSource operator +(DotNetNuGetEnableSource command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetEnableSource operator -(DotNetNuGetEnableSource command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetNuGetListSource: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetListSource operator +(DotNetNuGetListSource command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetListSource operator -(DotNetNuGetListSource command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetNuGetListSource operator +(DotNetNuGetListSource command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetListSource operator -(DotNetNuGetListSource command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetListSource operator +(DotNetNuGetListSource command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetListSource operator -(DotNetNuGetListSource command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetListSource operator +(DotNetNuGetListSource command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetListSource operator -(DotNetNuGetListSource command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetNuGetRemoveSource: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetRemoveSource operator +(DotNetNuGetRemoveSource command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetRemoveSource operator -(DotNetNuGetRemoveSource command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetNuGetRemoveSource operator +(DotNetNuGetRemoveSource command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetRemoveSource operator -(DotNetNuGetRemoveSource command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetRemoveSource operator +(DotNetNuGetRemoveSource command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetRemoveSource operator -(DotNetNuGetRemoveSource command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetRemoveSource operator +(DotNetNuGetRemoveSource command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetRemoveSource operator -(DotNetNuGetRemoveSource command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetNuGetUpdateSource: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetUpdateSource operator +(DotNetNuGetUpdateSource command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetUpdateSource operator -(DotNetNuGetUpdateSource command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetNuGetUpdateSource operator +(DotNetNuGetUpdateSource command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetUpdateSource operator -(DotNetNuGetUpdateSource command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetUpdateSource operator +(DotNetNuGetUpdateSource command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetUpdateSource operator -(DotNetNuGetUpdateSource command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetUpdateSource operator +(DotNetNuGetUpdateSource command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetUpdateSource operator -(DotNetNuGetUpdateSource command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetNuGetVerify: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetVerify operator +(DotNetNuGetVerify command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetVerify operator -(DotNetNuGetVerify command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetNuGetVerify operator +(DotNetNuGetVerify command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetVerify operator -(DotNetNuGetVerify command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetVerify operator +(DotNetNuGetVerify command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetVerify operator -(DotNetNuGetVerify command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetVerify operator +(DotNetNuGetVerify command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetVerify operator -(DotNetNuGetVerify command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetNuGetTrustList: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustList operator +(DotNetNuGetTrustList command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustList operator -(DotNetNuGetTrustList command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetNuGetTrustList operator +(DotNetNuGetTrustList command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustList operator -(DotNetNuGetTrustList command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustList operator +(DotNetNuGetTrustList command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustList operator -(DotNetNuGetTrustList command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustList operator +(DotNetNuGetTrustList command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustList operator -(DotNetNuGetTrustList command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetNuGetTrustSync: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustSync operator +(DotNetNuGetTrustSync command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustSync operator -(DotNetNuGetTrustSync command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetNuGetTrustSync operator +(DotNetNuGetTrustSync command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustSync operator -(DotNetNuGetTrustSync command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustSync operator +(DotNetNuGetTrustSync command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustSync operator -(DotNetNuGetTrustSync command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustSync operator +(DotNetNuGetTrustSync command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustSync operator -(DotNetNuGetTrustSync command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetNuGetTrustRemove: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustRemove operator +(DotNetNuGetTrustRemove command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustRemove operator -(DotNetNuGetTrustRemove command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetNuGetTrustRemove operator +(DotNetNuGetTrustRemove command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustRemove operator -(DotNetNuGetTrustRemove command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustRemove operator +(DotNetNuGetTrustRemove command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustRemove operator -(DotNetNuGetTrustRemove command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustRemove operator +(DotNetNuGetTrustRemove command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustRemove operator -(DotNetNuGetTrustRemove command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetNuGetTrustAuthor: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustAuthor operator +(DotNetNuGetTrustAuthor command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustAuthor operator -(DotNetNuGetTrustAuthor command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetNuGetTrustAuthor operator +(DotNetNuGetTrustAuthor command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustAuthor operator -(DotNetNuGetTrustAuthor command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustAuthor operator +(DotNetNuGetTrustAuthor command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustAuthor operator -(DotNetNuGetTrustAuthor command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustAuthor operator +(DotNetNuGetTrustAuthor command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustAuthor operator -(DotNetNuGetTrustAuthor command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetNuGetTrustRepository: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustRepository operator +(DotNetNuGetTrustRepository command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustRepository operator -(DotNetNuGetTrustRepository command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetNuGetTrustRepository operator +(DotNetNuGetTrustRepository command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustRepository operator -(DotNetNuGetTrustRepository command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustRepository operator +(DotNetNuGetTrustRepository command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustRepository operator -(DotNetNuGetTrustRepository command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustRepository operator +(DotNetNuGetTrustRepository command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustRepository operator -(DotNetNuGetTrustRepository command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetNuGetTrustCertificate: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustCertificate operator +(DotNetNuGetTrustCertificate command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustCertificate operator -(DotNetNuGetTrustCertificate command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetNuGetTrustCertificate operator +(DotNetNuGetTrustCertificate command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustCertificate operator -(DotNetNuGetTrustCertificate command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustCertificate operator +(DotNetNuGetTrustCertificate command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustCertificate operator -(DotNetNuGetTrustCertificate command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustCertificate operator +(DotNetNuGetTrustCertificate command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustCertificate operator -(DotNetNuGetTrustCertificate command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetNuGetTrustSource: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustSource operator +(DotNetNuGetTrustSource command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustSource operator -(DotNetNuGetTrustSource command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetNuGetTrustSource operator +(DotNetNuGetTrustSource command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustSource operator -(DotNetNuGetTrustSource command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustSource operator +(DotNetNuGetTrustSource command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustSource operator -(DotNetNuGetTrustSource command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustSource operator +(DotNetNuGetTrustSource command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetTrustSource operator -(DotNetNuGetTrustSource command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetNuGetSign: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetSign operator +(DotNetNuGetSign command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetSign operator -(DotNetNuGetSign command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetNuGetSign operator +(DotNetNuGetSign command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetSign operator -(DotNetNuGetSign command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetSign operator +(DotNetNuGetSign command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetSign operator -(DotNetNuGetSign command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetSign operator +(DotNetNuGetSign command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetSign operator -(DotNetNuGetSign command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetNuGetWhy: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetWhy operator +(DotNetNuGetWhy command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetWhy operator -(DotNetNuGetWhy command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetNuGetWhy operator +(DotNetNuGetWhy command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetWhy operator -(DotNetNuGetWhy command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetWhy operator +(DotNetNuGetWhy command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetWhy operator -(DotNetNuGetWhy command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetWhy operator +(DotNetNuGetWhy command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetWhy operator -(DotNetNuGetWhy command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetNuGetConfigGet: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetConfigGet operator +(DotNetNuGetConfigGet command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetConfigGet operator -(DotNetNuGetConfigGet command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetNuGetConfigGet operator +(DotNetNuGetConfigGet command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetConfigGet operator -(DotNetNuGetConfigGet command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetConfigGet operator +(DotNetNuGetConfigGet command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetConfigGet operator -(DotNetNuGetConfigGet command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetConfigGet operator +(DotNetNuGetConfigGet command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetConfigGet operator -(DotNetNuGetConfigGet command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetNuGetConfigSet: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetConfigSet operator +(DotNetNuGetConfigSet command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetConfigSet operator -(DotNetNuGetConfigSet command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetNuGetConfigSet operator +(DotNetNuGetConfigSet command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetConfigSet operator -(DotNetNuGetConfigSet command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetConfigSet operator +(DotNetNuGetConfigSet command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetConfigSet operator -(DotNetNuGetConfigSet command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetConfigSet operator +(DotNetNuGetConfigSet command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetConfigSet operator -(DotNetNuGetConfigSet command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetNuGetConfigUnset: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetConfigUnset operator +(DotNetNuGetConfigUnset command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetConfigUnset operator -(DotNetNuGetConfigUnset command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetNuGetConfigUnset operator +(DotNetNuGetConfigUnset command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetConfigUnset operator -(DotNetNuGetConfigUnset command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetConfigUnset operator +(DotNetNuGetConfigUnset command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetConfigUnset operator -(DotNetNuGetConfigUnset command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetConfigUnset operator +(DotNetNuGetConfigUnset command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetConfigUnset operator -(DotNetNuGetConfigUnset command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetNuGetConfigPaths: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetConfigPaths operator +(DotNetNuGetConfigPaths command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetConfigPaths operator -(DotNetNuGetConfigPaths command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetNuGetConfigPaths operator +(DotNetNuGetConfigPaths command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetConfigPaths operator -(DotNetNuGetConfigPaths command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetConfigPaths operator +(DotNetNuGetConfigPaths command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetConfigPaths operator -(DotNetNuGetConfigPaths command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetConfigPaths operator +(DotNetNuGetConfigPaths command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetNuGetConfigPaths operator -(DotNetNuGetConfigPaths command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetPackageSearch: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetPackageSearch operator +(DotNetPackageSearch command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetPackageSearch operator -(DotNetPackageSearch command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetPackageSearch operator +(DotNetPackageSearch command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetPackageSearch operator -(DotNetPackageSearch command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetPackageSearch operator +(DotNetPackageSearch command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetPackageSearch operator -(DotNetPackageSearch command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetPackageSearch operator +(DotNetPackageSearch command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetPackageSearch operator -(DotNetPackageSearch command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
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
public partial record DotNetSdkCheck: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetSdkCheck operator +(DotNetSdkCheck command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetSdkCheck operator -(DotNetSdkCheck command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetSdkCheck operator +(DotNetSdkCheck command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetSdkCheck operator -(DotNetSdkCheck command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetSdkCheck operator +(DotNetSdkCheck command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetSdkCheck operator -(DotNetSdkCheck command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetSdkCheck operator +(DotNetSdkCheck command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetSdkCheck operator -(DotNetSdkCheck command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetSlnList: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetSlnList operator +(DotNetSlnList command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetSlnList operator -(DotNetSlnList command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetSlnList operator +(DotNetSlnList command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetSlnList operator -(DotNetSlnList command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetSlnList operator +(DotNetSlnList command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetSlnList operator -(DotNetSlnList command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetSlnList operator +(DotNetSlnList command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetSlnList operator -(DotNetSlnList command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetSlnAdd: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetSlnAdd operator +(DotNetSlnAdd command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetSlnAdd operator -(DotNetSlnAdd command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetSlnAdd operator +(DotNetSlnAdd command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetSlnAdd operator -(DotNetSlnAdd command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetSlnAdd operator +(DotNetSlnAdd command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetSlnAdd operator -(DotNetSlnAdd command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetSlnAdd operator +(DotNetSlnAdd command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetSlnAdd operator -(DotNetSlnAdd command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetSlnRemove: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetSlnRemove operator +(DotNetSlnRemove command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetSlnRemove operator -(DotNetSlnRemove command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetSlnRemove operator +(DotNetSlnRemove command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetSlnRemove operator -(DotNetSlnRemove command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetSlnRemove operator +(DotNetSlnRemove command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetSlnRemove operator -(DotNetSlnRemove command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetSlnRemove operator +(DotNetSlnRemove command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetSlnRemove operator -(DotNetSlnRemove command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetStore: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetStore operator +(DotNetStore command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetStore operator -(DotNetStore command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetStore operator +(DotNetStore command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetStore operator -(DotNetStore command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetStore operator +(DotNetStore command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetStore operator -(DotNetStore command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetStore operator +(DotNetStore command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetStore operator -(DotNetStore command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
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
public partial record DotNetToolInstall: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolInstall operator +(DotNetToolInstall command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolInstall operator -(DotNetToolInstall command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetToolInstall operator +(DotNetToolInstall command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolInstall operator -(DotNetToolInstall command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolInstall operator +(DotNetToolInstall command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolInstall operator -(DotNetToolInstall command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolInstall operator +(DotNetToolInstall command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolInstall operator -(DotNetToolInstall command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetToolList: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolList operator +(DotNetToolList command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolList operator -(DotNetToolList command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetToolList operator +(DotNetToolList command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolList operator -(DotNetToolList command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolList operator +(DotNetToolList command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolList operator -(DotNetToolList command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolList operator +(DotNetToolList command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolList operator -(DotNetToolList command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
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
public partial record DotNetToolRun: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolRun operator +(DotNetToolRun command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolRun operator -(DotNetToolRun command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetToolRun operator +(DotNetToolRun command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolRun operator -(DotNetToolRun command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolRun operator +(DotNetToolRun command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolRun operator -(DotNetToolRun command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolRun operator +(DotNetToolRun command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolRun operator -(DotNetToolRun command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetToolSearch: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolSearch operator +(DotNetToolSearch command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolSearch operator -(DotNetToolSearch command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetToolSearch operator +(DotNetToolSearch command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolSearch operator -(DotNetToolSearch command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolSearch operator +(DotNetToolSearch command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolSearch operator -(DotNetToolSearch command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolSearch operator +(DotNetToolSearch command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolSearch operator -(DotNetToolSearch command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetToolUninstall: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolUninstall operator +(DotNetToolUninstall command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolUninstall operator -(DotNetToolUninstall command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetToolUninstall operator +(DotNetToolUninstall command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolUninstall operator -(DotNetToolUninstall command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolUninstall operator +(DotNetToolUninstall command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolUninstall operator -(DotNetToolUninstall command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolUninstall operator +(DotNetToolUninstall command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolUninstall operator -(DotNetToolUninstall command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetToolUpdate: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolUpdate operator +(DotNetToolUpdate command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolUpdate operator -(DotNetToolUpdate command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetToolUpdate operator +(DotNetToolUpdate command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolUpdate operator -(DotNetToolUpdate command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolUpdate operator +(DotNetToolUpdate command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolUpdate operator -(DotNetToolUpdate command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolUpdate operator +(DotNetToolUpdate command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetToolUpdate operator -(DotNetToolUpdate command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
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
public partial record DotNetWorkload: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkload operator +(DotNetWorkload command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkload operator -(DotNetWorkload command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetWorkload operator +(DotNetWorkload command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkload operator -(DotNetWorkload command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkload operator +(DotNetWorkload command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkload operator -(DotNetWorkload command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkload operator +(DotNetWorkload command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkload operator -(DotNetWorkload command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetWorkloadConfig: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadConfig operator +(DotNetWorkloadConfig command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadConfig operator -(DotNetWorkloadConfig command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetWorkloadConfig operator +(DotNetWorkloadConfig command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadConfig operator -(DotNetWorkloadConfig command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadConfig operator +(DotNetWorkloadConfig command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadConfig operator -(DotNetWorkloadConfig command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadConfig operator +(DotNetWorkloadConfig command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadConfig operator -(DotNetWorkloadConfig command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetWorkloadInstall: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadInstall operator +(DotNetWorkloadInstall command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadInstall operator -(DotNetWorkloadInstall command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetWorkloadInstall operator +(DotNetWorkloadInstall command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadInstall operator -(DotNetWorkloadInstall command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadInstall operator +(DotNetWorkloadInstall command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadInstall operator -(DotNetWorkloadInstall command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadInstall operator +(DotNetWorkloadInstall command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadInstall operator -(DotNetWorkloadInstall command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetWorkloadList: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadList operator +(DotNetWorkloadList command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadList operator -(DotNetWorkloadList command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetWorkloadList operator +(DotNetWorkloadList command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadList operator -(DotNetWorkloadList command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadList operator +(DotNetWorkloadList command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadList operator -(DotNetWorkloadList command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadList operator +(DotNetWorkloadList command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadList operator -(DotNetWorkloadList command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetWorkloadRepair: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadRepair operator +(DotNetWorkloadRepair command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadRepair operator -(DotNetWorkloadRepair command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetWorkloadRepair operator +(DotNetWorkloadRepair command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadRepair operator -(DotNetWorkloadRepair command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadRepair operator +(DotNetWorkloadRepair command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadRepair operator -(DotNetWorkloadRepair command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadRepair operator +(DotNetWorkloadRepair command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadRepair operator -(DotNetWorkloadRepair command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetWorkloadRestore: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadRestore operator +(DotNetWorkloadRestore command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadRestore operator -(DotNetWorkloadRestore command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetWorkloadRestore operator +(DotNetWorkloadRestore command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadRestore operator -(DotNetWorkloadRestore command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadRestore operator +(DotNetWorkloadRestore command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadRestore operator -(DotNetWorkloadRestore command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadRestore operator +(DotNetWorkloadRestore command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadRestore operator -(DotNetWorkloadRestore command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetWorkloadSearch: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadSearch operator +(DotNetWorkloadSearch command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadSearch operator -(DotNetWorkloadSearch command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetWorkloadSearch operator +(DotNetWorkloadSearch command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadSearch operator -(DotNetWorkloadSearch command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadSearch operator +(DotNetWorkloadSearch command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadSearch operator -(DotNetWorkloadSearch command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadSearch operator +(DotNetWorkloadSearch command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadSearch operator -(DotNetWorkloadSearch command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetWorkloadUninstall: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadUninstall operator +(DotNetWorkloadUninstall command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadUninstall operator -(DotNetWorkloadUninstall command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetWorkloadUninstall operator +(DotNetWorkloadUninstall command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadUninstall operator -(DotNetWorkloadUninstall command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadUninstall operator +(DotNetWorkloadUninstall command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadUninstall operator -(DotNetWorkloadUninstall command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadUninstall operator +(DotNetWorkloadUninstall command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadUninstall operator -(DotNetWorkloadUninstall command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetWorkloadUpdate: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadUpdate operator +(DotNetWorkloadUpdate command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadUpdate operator -(DotNetWorkloadUpdate command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetWorkloadUpdate operator +(DotNetWorkloadUpdate command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadUpdate operator -(DotNetWorkloadUpdate command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadUpdate operator +(DotNetWorkloadUpdate command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadUpdate operator -(DotNetWorkloadUpdate command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadUpdate operator +(DotNetWorkloadUpdate command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetWorkloadUpdate operator -(DotNetWorkloadUpdate command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
}

[ExcludeFromCodeCoverage]
public partial record DotNetCsi: ICommandLine
{
    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="command">The command to which an argument will be added.</param>
    /// <param name="arg">Argument to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetCsi operator +(DotNetCsi command, string arg) => command.AddArgs(arg);
    
    /// <summary>
    /// Removes an argument by its name.
    /// </summary>
    /// <param name="command">The command to which an argument will be removed.</param>
    /// <param name="arg">Argument to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetCsi operator -(DotNetCsi command, string arg) => command.RemoveArgs(arg);

    /// <summary>
    /// Appends arguments.
    /// </summary>
    /// <param name="command">The command to which arguments will be added.</param>
    /// <param name="args">Arguments to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>    
    public static DotNetCsi operator +(DotNetCsi command, IEnumerable<string> args) => command.AddArgs(args);

    /// <summary>
    /// Removes arguments by their name.
    /// </summary>
    /// <param name="command">The command to which arguments will be removed.</param>
    /// <param name="args">Arguments to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetCsi operator -(DotNetCsi command, IEnumerable<string> args) => command.RemoveArgs(args);
    
    /// <summary>
    /// Appends an environment variable.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be added.</param>
    /// <param name="var">Environment variable to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetCsi operator +(DotNetCsi command, (string name, string value) var) => command.AddVars(var);
    
    /// <summary>
    /// Removes environment variable by its name and value.
    /// </summary>
    /// <param name="command">The command to which an environment variable will be removed.</param>
    /// <param name="var">Environment variable to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetCsi operator -(DotNetCsi command, (string name, string value) var) => command.RemoveVars(var);
    
    /// <summary>
    /// Appends environment variables.
    /// </summary>
    /// <param name="command">The command to which environment variables will be added.</param>
    /// <param name="vars">Environment variables to add.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetCsi operator +(DotNetCsi command, IEnumerable<(string name, string value)> vars) => command.AddVars(vars);
    
    /// <summary>
    /// Removes environment variables by their name and value.
    /// </summary>
    /// <param name="command">The command to which environment variables will be removed.</param>
    /// <param name="vars">environment variables to remove.</param>
    /// <returns>Returns a new command with the corresponding changes.</returns>
    public static DotNetCsi operator -(DotNetCsi command, IEnumerable<(string name, string value)> vars) => command.RemoveVars(vars);
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

