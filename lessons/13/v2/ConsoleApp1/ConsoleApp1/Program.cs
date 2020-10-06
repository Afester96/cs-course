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
                test.Multiple("test");
            }
            //var test = new ConsoleLogWriter();
            //test.LogInfo("test");
            //var test2 = new FileLogWriter();
            //test2.LogWarning("test2");
            //var test3 = new MultipleLogWriter();
            //test3.LogError("test3");
            //var log = new AbstractLogWriter[]
            //{
            //    test,
            //    test2,
            //    test3
            //};
            //foreach (var s in log)
            //{
            //    s.LogInfo("test4");
            //    s.LogError("test5");
            //    s.LogWarning("test6");
            //}
        }
    }
    enum Type
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
   
    public class FileLogWriter : ILogWriter
    {
        public void LogInfo(string message)
        {
            File.WriteAllText(@$"{message}.txt", $"{DateTimeOffset.Now} {Type.Info} {message}");
        }
        public void LogWarning(string message)
        {
            File.WriteAllText(@$"{message}.txt", $"{DateTimeOffset.Now} {Type.Warning} {message}");
        }
        public void LogError(string message)
        {
            File.WriteAllText(@$"{message}.txt", $"{DateTimeOffset.Now} {Type.Error} {message}");
        }
    }
    public class ConsoleLogWriter : ILogWriter
    {
        public void LogInfo(string message)
        {
            Console.WriteLine($"{DateTimeOffset.Now} {Type.Info} {message}");
        }
        public void LogWarning(string message)
        {
            Console.WriteLine($"{DateTimeOffset.Now} {Type.Warning} {message}");
        }
        public void LogError(string message)
        {
            Console.WriteLine($"{DateTimeOffset.Now} {Type.Error} {message}");
        }
    }
    public class MultipleLogWriter
    {
        public ILogWriter[] LogWriters { get; private set; }
        public MultipleLogWriter(params ILogWriter[] logWriters)
        {
            LogWriters = logWriters;
        }
        public void Multiple(string message)
        {
            foreach (var log in LogWriters)
            {
                log.LogInfo(message);
                log.LogError(message);
                log.LogWarning(message);
            }
        }
    }
}
