namespace CSharpInteractive.Core;

[ExcludeFromCodeCoverage]
internal class HelpCommand : ICommand
{
    public static readonly ICommand Shared = new HelpCommand();

    private HelpCommand()
    { }

    public string Name => "Help";

    public bool Internal => false;

    public override string ToString() => Name;
}