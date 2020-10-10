using System;

namespace ClassWork13
{
    public class ConsoleLogWriter : AbstractLogWriter
    {
        protected override void Write(string message, Type logType)
        {
            var text = Message(message, logType);
            Console.WriteLine(text);
        }
    }
}
