using System;
using System.Collections.Generic;

namespace HomeWork1
{
    class Program
    {
        static void Main(string[] args)
        {
            var entered = EnteredText("Please, enter [] or ().");
            //var entered3 = entered.Split(' ', StringSplitOptions.RemoveEmptyEntries); ;
            //Console.WriteLine(entered3.Length);
            var entered2 = entered.ToCharArray();//Переводим введённые значения в массив типа чар
            var stack = new Stack<char>(); //Создаём список
            var dictionary = new Dictionary<char, char>
            {
                { '(',')' },
                {'[',']' },
                {'{','}' }
            };

            for (int i = 0; i < entered2.Length; i++)
            {
                if (dictionary.ContainsKey(entered2[i])) //Смотрим есть ли открытые
                {
                    stack.Push(entered2[i]); //Если есть, то пушим
                    continue;
                }
                else if (dictionary.ContainsValue(entered2[i])) //Смотрим есть ли закрытые
                {
                    try
                    {
                        stack.Pop();
                        continue;
                    }
                    catch (InvalidOperationException)
                    {
                        Console.WriteLine("False");
                        stack.Push(entered2[i]);
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
