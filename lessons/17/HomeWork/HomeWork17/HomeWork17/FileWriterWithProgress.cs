using System;

namespace HomeWork17
{
    public delegate void PercentageToFire(string fileName, byte[] data, float percentageToFireEvent);
    public delegate void PercentageToFireFinished(string text);
    public class FileWriterWithProgress
    {
        public event PercentageToFire WritingPerformed;
        public event PercentageToFireFinished WritingCompleted;
        public void WriteBytes(string fileName, byte[] data, float percentageToFireEvent)
        {
            float percentStart = 0.001f;
            float percentRunning = 0.001f;
            while (percentStart < 1)
            {
                if (percentStart % percentageToFireEvent == 0)
                {
                    WritingPerformed?.Invoke($"{fileName}", data, percentStart * 100);
                }
                percentStart += percentRunning;
                Console.WriteLine(percentStart % percentageToFireEvent == 0);
                Console.WriteLine(percentStart);
            }
            WritingCompleted?.Invoke("Writing Complited!");
        }
    }
}
