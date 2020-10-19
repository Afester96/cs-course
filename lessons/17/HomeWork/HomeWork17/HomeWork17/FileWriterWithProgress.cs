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
            var dataFirstValue = 0;
            var dataArrayCount = 0;
            byte[] dataArray = new byte[(int)(data.Length * percentageToFireEvent / 1.0)];
            for (int i = 0; i < data.Length; i++)
            {
                if (i != 0 && i % ((int)(data.Length * percentageToFireEvent / 1.0)) == 0)
                {
                    var dataSecondValue = i;
                    dataArrayCount = 0;
                    for (int j = dataFirstValue; j < dataSecondValue; j++)
                    {
                        dataArray[dataArrayCount] = data[j];
                        dataArrayCount++;
                    }
                    File.WriteAllText($"{i / 100 + "% " + fileName}", String.Join(" ", dataArray));
                    dataFirstValue = dataSecondValue;
                    WritingPerformed?.Invoke(this, new WritingPerformedEventArgs($"{fileName}", data, i / 100));
                }
            }
            WritingCompleted?.Invoke(this, new WritingCompletedEventArgs("Writing Complited!"));
            File.WriteAllText($"{100 + "% " + fileName}", String.Join(" ", data));
        }
    }
}
