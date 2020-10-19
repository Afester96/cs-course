using System;

namespace HomeWork17
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileWriter = new FileWriterWithProgress();
            var write = new Result();
            byte[] data = new byte[10000];
            var random = new Random();

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (byte)random.Next(255);
            }

            fileWriter.WritingPerformed += write.WriteResultData;
            fileWriter.WritingCompleted += write.WriteResultFinished;
            fileWriter.WriteBytes($"log.txt", data, 0.1f);
        }
    }

}
