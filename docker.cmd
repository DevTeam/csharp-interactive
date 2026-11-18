docker.exe pull nikolayp/dotnetsdk:latest
docker.exe run --rm -it -v D:\Projects\csharp-interactive:/src -w /src nikolayp/dotnetsdk:latest dotnet run -f net11.0 --project Build