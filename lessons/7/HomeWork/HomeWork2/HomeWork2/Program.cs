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
                try
                {
                    var enteredText = Console.ReadLine();
                    var charArray = enteredText.ToCharArray();
                    bool result;
                    int counter = 0;
                    for (int i = 0; i < charArray.Length; i++)
                    {
                        result = Char.IsDigit(charArray[i]);
                        if (result == true)
                        {
                            Console.WriteLine("Error! You entered numeric values.");
                            break;
                        }
                        else if (result == false)
                        {
                            counter++;
                            continue;
                        }
                        else if (result == false & counter == charArray.Length)
                        {
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
                            else
                                return enteredText;
                        }
                    }                       
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
