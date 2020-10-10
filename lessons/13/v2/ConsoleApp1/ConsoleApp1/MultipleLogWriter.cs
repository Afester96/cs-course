namespace ClassWork13
{
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
