Install the C# script template [CSharpInteractive.Templates](https://www.nuget.org/packages/CSharpInteractive.Templates)

```shell
dotnet new -i CSharpInteractive.Templates
```

Create a console project "Build" containing a script from the template *__build__*

```shell
dotnet new build -o ./Build
```

This projects contains the script *__./Build/Program.csx__*. To run this script from the command line from the directory *__Build__*:

```shell
dotnet csi Build
```

To run as a .NET console application:

```shell
dotnet run --project Build
```
