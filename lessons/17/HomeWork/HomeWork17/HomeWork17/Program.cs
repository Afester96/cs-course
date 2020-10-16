using System;

namespace HomeWork17
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileWriter = new FileWriterWithProgress();
            var random = new Random();
            var write = new Result();
            byte[] data = new byte[5];

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (byte)random.Next(0, 9);
            }


            fileWriter.WritingPerformed += write.WriteResultData;
            fileWriter.WritingCompleted += write.WriteResultFinished;
            fileWriter.WriteBytes("log.txt", data, 0.1f);
        }
    }
    
}
