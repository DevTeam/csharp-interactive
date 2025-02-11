using HostApi;

WriteLine("Hello World!");
Trace("My trace");
Info("My info");
Summary("My summary message");
Summary("My summary ", "message".WithColor(Color.Header));
Warning("My warning");
Error("My error");