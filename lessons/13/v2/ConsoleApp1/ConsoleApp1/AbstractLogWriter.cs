using System;

namespace ClassWork13
{
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
}
