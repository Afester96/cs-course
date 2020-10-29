using System;
using System.IO;

namespace HomeWork17
{
    public class FileWriterWithProgress
    {
        public event EventHandler<WritingPerformedEventArgs> WritingPerformed;
        public event EventHandler<WritingCompletedEventArgs> WritingCompleted;

        public void WriteBytes(string fileName, byte[] data, float percentageToFireEvent)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (i != 0 && i % ((int)(data.Length * percentageToFireEvent / 1.0)) == 0)
                {
                    using (FileStream fs = File.OpenWrite(fileName))
                    {
                        fs.Write(data, 0, i);
                    }
                    WritingPerformed?.Invoke(this, new WritingPerformedEventArgs($"{fileName}", data, i / 100));
                }
            }
            WritingCompleted?.Invoke(this, new WritingCompletedEventArgs("Writing Complited!"));
            using (FileStream fs = File.OpenWrite(fileName))
            {
                fs.Write(data, 0, data.Length);
            }
        }
    }
}
