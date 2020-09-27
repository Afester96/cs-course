using System;
using System.Collections.Generic;

namespace HomeWork1
{
    class Program
    {
        static void Main(string[] args)
        {
            var entered = EnteredText("Please, enter [] or ().");
            if (IsBool(entered))
            {
                Console.WriteLine("True");
            }
            else
            {
                Console.WriteLine("False");
            }
        }
        static string EnteredText(string text)
        {
            while (true)
            {
                Console.WriteLine(text);
                var enteredText = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(enteredText))
                {
                    Console.WriteLine("Error. You enter Null or white space!");
                    continue;
                }
                else
                {
                    return enteredText;
                }
            }
        }
        static bool IsBool(string test)
        {
            var dictionary = new Dictionary<char, char>
            {
                { '(',')' },
                {'[',']' },
                {'{','}' }
            };
            var stack = new Stack<char>(); //Создаём список
            foreach (char i in test)
            {
                if (dictionary.ContainsKey(i)) //Смотрим есть ли открытые
                {
                    stack.Push(i); //Если есть, то пушим
                }
                else if (dictionary.ContainsValue(i)) //Смотрим есть ли закрытые
                {
                    try
                    {
                        stack.Pop();
                    }
                    catch (InvalidOperationException)
                    {
                        return false;
                    }
                }
            }
            return stack.Count == 0;
        }
    }
}
