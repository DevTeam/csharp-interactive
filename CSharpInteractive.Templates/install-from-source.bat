dotnet pack -c Release CSharpInteractive.Templates.csproj -p:version=2.0.0
dotnet new uninstall CSharpInteractive.Templates
dotnet new install CSharpInteractive.Templates::2.0.0 --nuget-source %cd%/bin