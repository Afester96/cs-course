using System;
using System.IO;

namespace ClassWork13
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var test1 = new FileLogWriter();
                var test2 = new ConsoleLogWriter();
                var test = new MultipleLogWriter(test1, test2);
                test.LogError("test");
                test.LogInfo("test2");
                test.LogWarning("test3");
            }
        }
    }
    public enum Type
    {
        Info,
        Warning,
        Error
    }
    public interface ILogWriter
    {
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message);
    }
    public abstract class AbstractLogWriter : ILogWriter
    {
        protected abstract void Write(string message, Type logType);
        public void LogError(string message)
        {
            Write(message, Type.Error);
        }

        public void LogInfo(string message)
        {
            Write(message, Type.Info);
        }

        public void LogWarning(string message)
        {
            Write(message, Type.Warning);
        }

        protected string Message(string message, Type logType)
        {
            return $"{DateTimeOffset.Now} {logType} {message}";
        }
    }
    public class FileLogWriter : AbstractLogWriter
    {
        protected override void Write(string message, Type logType)
        {
            var text = Message(message, logType);
            switch (logType)
            {
                case Type.Info:
                    Console.WriteLine(text);
                    break;
                case Type.Warning:
                    Console.WriteLine(text);
                    break;
                case Type.Error:
                    Console.WriteLine(text);
                    break;
            }
        }
    }
    public class ConsoleLogWriter : AbstractLogWriter
    {
        protected override void Write(string message, Type logType)
        {
            var text = Message(message, logType);
            switch (logType)
            {
                case Type.Info:
                    File.WriteAllText($"{message}.txt", text);
                    break;
                case Type.Warning:
                    File.WriteAllText($"{message}.txt", text);
                    break;
                case Type.Error:
                    File.WriteAllText($"{message}.txt", text);
                    break;
            }
        }
    }
    public class MultipleLogWriter : ILogWriter
    {
        public ILogWriter[] LogWriters { get; private set; }
        public MultipleLogWriter(params ILogWriter[] logWriters)
        {
            LogWriters = logWriters;
        }
        public void LogInfo(string message)
        {
            foreach (var log in LogWriters)
            {
                log.LogInfo(message);
            }
        }
        public void LogWarning(string message)
        {
            foreach (var log in LogWriters)
            {
                log.LogInfo(message);
            }
        }
        public void LogError(string message)
        {
            foreach (var log in LogWriters)
            {
                log.LogInfo(message);
            }
        }
    }
}
