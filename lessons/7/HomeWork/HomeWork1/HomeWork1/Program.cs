using System;

namespace HomeWork1
{
    class Program
    {
        static void Main(string[] args)
        {
            var enteredText = TempletString("Enter string with few words").ToLower();
            var enteredTextArray = enteredText.Split(" ");
            var counter = 0;
            for (int i = 0; i < enteredTextArray.Length; i++)
                if (enteredTextArray[i].StartsWith("a"))
                    counter++;

            Console.WriteLine(counter);
        }
        static string TempletString(string text)
        {
            while (true)
            {
                Console.WriteLine(text);

                var enteredText = Console.ReadLine();
                
                if (String.IsNullOrWhiteSpace(enteredText))
                {
                    Console.WriteLine("Please enter few words");
                    continue;
                }
                else if (enteredText.Contains(" "))
                    return enteredText;
                else
                {
                    Console.WriteLine("Please enter few words");
                    continue;
                }
            }
        }
    }
}
