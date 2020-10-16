using System;
using System.IO;

namespace HomeWork17
{
    public class Result
    {
        public void WriteResultData(string path, byte[] data, float percent)
        {
            File.WriteAllText($"{path}", String.Join(" ", data));
            Console.WriteLine($"{(int)percent} is done");
        }
        public void WriteResultFinished(string text)
        {
            Console.WriteLine(text);
        }
    }
}
