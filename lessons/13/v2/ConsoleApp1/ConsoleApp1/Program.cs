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
                var test = new ConsoleLogWriter();
                test.LogInfo("test");
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
            File.WriteAllText(@$"{message}.txt", $"{DateTimeOffset.Now} {message.GetType()} {message}");
        }
        public void LogWarning(string message)
        {
            File.WriteAllText(@$"{message}.txt", $"{DateTimeOffset.Now} {message.GetType()} {message}");
        }
        public void LogError(string message)
        {
            File.WriteAllText(@$"{message}.txt", $"{DateTimeOffset.Now} {message.GetType()} {message}");
        }
    }
    public class ConsoleLogWriter : ILogWriter
    {
        public void LogInfo(string message)
        {
            Console.WriteLine($"{DateTimeOffset.Now} {message.GetType()} {message}");
        }
        public void LogWarning(string message)
        {
            Console.WriteLine($"{DateTimeOffset.Now} {message.GetType()} {message}");
        }
        public void LogError(string message)
        {
            Console.WriteLine($"{DateTimeOffset.Now} {message.GetType()} {message}");
        }
    }
    public class MultipleLogWriter : ILogWriter
    {
        
    }
}
