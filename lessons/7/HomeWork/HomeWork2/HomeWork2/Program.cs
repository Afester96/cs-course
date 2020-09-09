using System;

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
                try
                {
                    var enteredText = Console.ReadLine();
                    int myInt;
                    bool isNumerical = int.TryParse(enteredText, out myInt);
                    if (string.IsNullOrWhiteSpace(enteredText))
                    {
                        Console.WriteLine("Error! You entered empty values.");
                        continue;
                    }
                    else if (string.IsNullOrEmpty(enteredText))
                    {
                        Console.WriteLine("Error! You entered empty values.");
                        continue;
                    }
                    else if (isNumerical)
                    {
                        Console.WriteLine("Error! You entered numerable values.");
                        continue;
                    }
                    else
                        return enteredText;    
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error! Please enter correct values.");
                    continue;
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("Error! Please enter correct values.");
                    continue;
                }
            }
        }
    }
}
