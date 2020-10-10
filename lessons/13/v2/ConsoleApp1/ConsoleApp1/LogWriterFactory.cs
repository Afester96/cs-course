using System;

namespace ClassWork13
{
    class LogWriterFactory
    {
        private static LogWriterFactory _instance;
        public static LogWriterFactory Instance =>
            _instance ??= new LogWriterFactory();

        private LogWriterFactory() { }

        public ILogWriter GetLogWriter<T>(object parameters = null)
            where T : ILogWriter
        {
            if (typeof(T) == typeof(ConsoleLogWriter))
            {
                return new ConsoleLogWriter();
            }

            else if (typeof(T) == typeof(FileLogWriter))
            {
                return parameters is string path
                    ? new FileLogWriter(path)
                    : parameters == null
                        ? new FileLogWriter()
                        : throw new ArgumentException($"Parameter {nameof(parameters)} must be {typeof(string)}");
            }

            else if (typeof(T) == typeof(MultipleLogWriter))
            {
                return parameters is ILogWriter[] logWriters
                    ? new MultipleLogWriter(logWriters)
                    : throw new ArgumentException($"Parameter {nameof(parameters)} must be{typeof(ILogWriter[])}");
            }

            throw new NotSupportedException($"\"{typeof(T)}\" is not correct");
        }
    }
}
