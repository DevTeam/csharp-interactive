docker.exe pull nikolayp/dotnetsdk:latest
docker.exe run --rm -it -v D:\Projects\csharp-interactive:/src -w /src nikolayp/dotnetsdk:latest dotnet build