using System;

namespace HomeWork17
{
    public class FileWriterWithProgress
    {
        public event EventHandler<WritingPerformedEventArgs> WritingPerformed;
        public event EventHandler<WritingCompletedEventArgs> WritingCompleted;

        public void WriteBytes(string fileName, byte[] data, float percentageToFireEvent)
        {
            var random = new Random();

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (byte)random.Next(255);
                if (i % (data.Length * percentageToFireEvent / 1.0) == 0)
                {
                    WritingPerformed?.Invoke(this, new WritingPerformedEventArgs($"{fileName}", data, i / 100));
                }
            }
            WritingCompleted?.Invoke(this, new WritingCompletedEventArgs("Writing Complited!"));
        }
    }
}
