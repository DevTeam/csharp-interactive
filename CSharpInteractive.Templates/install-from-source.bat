dotnet pack -c Release CSharpInteractive.Templates.csproj -p:version=1.1.0
dotnet new uninstall CSharpInteractive.Templates
dotnet new install CSharpInteractive.Templates::1.1.0 --nuget-source %cd%/bin