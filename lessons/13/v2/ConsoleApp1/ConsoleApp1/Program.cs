namespace ClassWork13
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var consoleLogWriter = LogWriterFactory.Instance.GetLogWriter<ConsoleLogWriter>(null);
                var fileLogWriter = LogWriterFactory.Instance.GetLogWriter<FileLogWriter>("something.txt");
                var multipleLogWriter = LogWriterFactory.Instance.GetLogWriter<MultipleLogWriter>(new ILogWriter[] { consoleLogWriter, fileLogWriter });

                consoleLogWriter.LogError("Console Error");
                consoleLogWriter.LogInfo("Console Info");
                consoleLogWriter.LogWarning("Console Warning!");

                fileLogWriter.LogError("File Error");
                fileLogWriter.LogInfo("File Info");
                fileLogWriter.LogWarning("File Warning!");

                multipleLogWriter.LogError("Multiple Error");
                multipleLogWriter.LogInfo("Multiple Info");
                multipleLogWriter.LogWarning("Multiple Warning");
            }
        }
    }
}
