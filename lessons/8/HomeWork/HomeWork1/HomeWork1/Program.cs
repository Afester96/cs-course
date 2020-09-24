using System;
using System.Collections.Generic;

namespace HomeWork1
{
    class Program
    {
        static void Main(string[] args)
        {
            var entered = EnteredText("Please, enter [] or ().");
            var stack = new Stack<char>(); //Создаём список
            var dictionary = new Dictionary<char, char>
            {
                { '(',')' },
                {'[',']' },
                {'{','}' }
            };
            foreach (char i in entered)
            {
                if (dictionary.ContainsKey(i)) //Смотрим есть ли открытые
                {
                    stack.Push(i); //Если есть, то пушим
                    continue;
                }
                else if (dictionary.ContainsValue(i)) //Смотрим есть ли закрытые
                {
                    try
                    {
                        stack.Pop();
                        continue;
                    }
                    catch (InvalidOperationException)
                    {
                        stack.Push(i);
                        break;
                    }
                }
            }
            if (stack.Count == 0) 
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
    }
}
