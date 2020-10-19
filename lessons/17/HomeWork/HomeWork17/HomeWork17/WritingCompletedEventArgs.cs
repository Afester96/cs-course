using System;

namespace HomeWork17
{
    public class WritingCompletedEventArgs : EventArgs
    {
        public string Text { get; set; }
        public WritingCompletedEventArgs(string text)
        {
            Text = text;
        }
    }
}
