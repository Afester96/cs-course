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
                return new FileLogWriter(parameters as string);
            }

            else if (typeof(T) == typeof(MultipleLogWriter))
            {
                return new MultipleLogWriter(parameters as ILogWriter[]);
            }

            throw new NotSupportedException($"\"{typeof(T)}\" is not correct");
        }
    }
}
