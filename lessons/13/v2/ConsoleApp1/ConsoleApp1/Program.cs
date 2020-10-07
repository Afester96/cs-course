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
                test.LogError("test", Type.Error);
                test.LogInfo("test2", Type.Info);
                test.LogWarning("test3", Type.Warning);
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
        void LogInfo(string message, Type type);
        void LogWarning(string message, Type type);
        void LogError(string message, Type type);
    }
   
    public class FileLogWriter : ILogWriter
    {
        public void LogInfo(string message, Type type)
        {
            File.WriteAllText(@$"{message}.txt", $"{DateTimeOffset.Now} {type} {message}");
        }
        public void LogWarning(string message, Type type)
        {
            File.WriteAllText(@$"{message}.txt", $"{DateTimeOffset.Now} {type} {message}");
        }
        public void LogError(string message, Type type)
        {
            File.WriteAllText(@$"{message}.txt", $"{DateTimeOffset.Now} {type} {message}");
        }
    }
    public class ConsoleLogWriter : ILogWriter
    {
        
        public void LogInfo(string message, Type type)
        {
            Console.WriteLine($"{DateTimeOffset.Now} {type} {message}");
        }
        public void LogWarning(string message, Type type)
        {
            Console.WriteLine($"{DateTimeOffset.Now} {type} {message}");
        }
        public void LogError(string message, Type type)
        {
            Console.WriteLine($"{DateTimeOffset.Now} {type} {message}");
        }
    }
    public class MultipleLogWriter : ILogWriter
    {
        public ILogWriter[] LogWriters { get; private set; }
        public MultipleLogWriter(params ILogWriter[] logWriters)
        {
            LogWriters = logWriters;
        }
        public void LogInfo(string message, Type type)
        {
            foreach (var log in LogWriters)
            {
                log.LogInfo(message, type);
            }
        }
        public void LogWarning(string message, Type type)
        {
            foreach (var log in LogWriters)
            {
                log.LogInfo(message, type);
            }
        }
        public void LogError(string message, Type type)
        {
            foreach (var log in LogWriters)
            {
                log.LogInfo(message, type);
            }
        }
    }
}
