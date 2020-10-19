using System;

namespace HomeWork17
{
    public class WritingPerformedEventArgs : EventArgs
    {
        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public float PercentageToFireEvent { get; set; }
        public WritingPerformedEventArgs(string fileName, byte[] data, float percentageToFireEvent)
        {
            FileName = fileName;
            Data = data;
            PercentageToFireEvent = percentageToFireEvent;
        }
    }
}
