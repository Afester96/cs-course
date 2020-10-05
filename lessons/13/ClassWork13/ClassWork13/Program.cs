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
    public abstract class AbstractLogWriter : ILogWriter
    {
        public abstract void LogInfo(string message);
        public abstract void LogWarning(string message);
        public abstract void LogError(string message);
    }
    public class FileLogWriter : AbstractLogWriter
    {
        public override void LogInfo(string message)
        {
            File.WriteAllText(@"Test.txt", $"{DateTimeOffset.Now} {message.GetType()} {message}");
        }
        public override void LogWarning(string message)
        {
            File.WriteAllText(@"Test.txt", $"{DateTimeOffset.Now} {GetType()} {message}");
        }
        public override void LogError(string message)
        {
            File.WriteAllText(@"Test.txtt", $"{DateTimeOffset.Now} {GetType()} {message}");
        }
    }
    public class ConsoleLogWriter : AbstractLogWriter
    {
        public override void LogInfo(string message)
        {
            Console.WriteLine($"{DateTimeOffset.Now} {message.GetType()} {message}");
        }
        public override void LogWarning(string message)
        {
            Console.WriteLine($"{DateTimeOffset.Now} {GetType()} {message}");
        }
        public override void LogError(string message)
        {
            Console.WriteLine($"{DateTimeOffset.Now} {GetType()} {message}");
        }
    }
    public class MultipleLogWriter : AbstractLogWriter
    {
        public override void LogInfo(string message)
        {
            Console.WriteLine($"{DateTimeOffset.Now} {GetType()} {message}");
            File.WriteAllText(@"Test.txt", $"{DateTimeOffset.Now} {GetType()} {message}");
        }
        public override void LogWarning(string message)
        {
            Console.WriteLine($"{DateTimeOffset.Now} {GetType()} {message}");
            File.WriteAllText(@"Test.txt", $"{DateTimeOffset.Now} {GetType()} {message}");
        }
        public override void LogError(string message)
        {
            Console.WriteLine($"{DateTimeOffset.Now} {GetType()} {message}");
            File.WriteAllText(@"Test.txt", $"{DateTimeOffset.Now} {GetType()} {message}");
        }
    }

}
