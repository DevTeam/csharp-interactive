// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMethodReturnValue.Global

namespace CSharpInteractive.Core;

using HostApi;

[ExcludeFromCodeCoverage]
internal static class LogExtensions
{
    extension<T>(ILog<T> log)
    {
        public ILog<T> Error(ErrorId id, params string[] error)
        {
            log.Error(id, error.Select(i => new Text(i, Color.Error)).ToArray());
            return log;
        }

        public ILog<T> Error(ErrorId id, Exception error)
        {
            log.Error(id, error.ToText());
            return log;
        }

        public ILog<T> Info(params string[] message)
        {
            log.Info(message.Select(i => new Text(i)).ToArray());
            return log;
        }

        public ILog<T> Warning(params string[] warning)
        {
            log.Warning(warning.Select(i => new Text(i, Color.Warning)).ToArray());
            return log;
        }

        public ILog<T> Summary(params string[] summary)
        {
            log.Summary(summary.Select(i => new Text(i, Color.Highlighted)).ToArray());
            return log;
        }
    }

}