using System;
using System.IO;

namespace HomeWork17
{
    public class Result
    {
        public void WriteResultData(object sender, WritingPerformedEventArgs args)
        {
            Console.WriteLine($"{args.PercentageToFireEvent} % is done");
        }
        public void WriteResultFinished(object sender, WritingCompletedEventArgs args)
        {
            Console.WriteLine(args.Text);
        }
    }
}
