using System;
using System.Linq;

namespace HomeWork1
{
    class Program
    {
        static void Main(string[] args)
        {
            var enteredText = TempletString("Enter string with few words").ToLower();
            var charArray = enteredText.ToCharArray();
            Array.Reverse(charArray);

            for (int i = 0; i < charArray.Length; i++)
            {
                Console.Write(charArray[i]);
            }
        }
        static string TempletString(string text)
        {
            while (true)
            {
                Console.WriteLine(text);

                var enteredText = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(enteredText))
                {
                    Console.WriteLine("Error! You entered empty values.");
                    continue;
                }
                return enteredText;
            }
        }
    }
}
