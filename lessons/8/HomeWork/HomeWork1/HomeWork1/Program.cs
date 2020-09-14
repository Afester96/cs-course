using System;
using System.Collections.Generic;

namespace HomeWork1
{
    class Program
    {
        static void Main(string[] args)
        {
            var entered = EnteredText("Please, enter [] or ().");
            var entered2 = entered.ToCharArray(); //Переводим введённые значения в массив типа чар
            var stack = new Stack<char>(); //Создаём список
            var open = 0; //Открытые скобки счётчик
            var close = 0; //Закрытые скобки счётчик
            var counter = 0; //Счётчик значений
            while (counter < entered2.Length)
            {
                if (entered2[counter] == '(' | entered2[counter] == '[') //Смотрим есть ли открытые
                {
                    stack.Push(entered2[counter]); //Если есть, то пушим
                    counter++; //Добавляем в счётчик
                    open++; //Добавляем открытую
                    continue;
                }
                else if (entered2[counter] == ')' | entered2[counter] == ']') //Смотрим есть ли закрытые
                {
                    if (stack.TryPop(out entered2[counter]) == true) //Если содержат открытые
                    {
                        counter++;
                        close++; //Добавляем счётчик закрытых
                        continue;
                    }
                    else if (stack.TryPop(out entered2[counter]) == false) //Если не содержат открытые
                    {
                        counter++;
                        close++;
                        continue;
                    }
                }
            }
            if (open == close) //Если совпало значение открытых и закрытых, то выводим тру
            {
                Console.WriteLine("True");
            }
            else if (open != close) //Если не совпало значение открытых и закрытых, то выводим фолс
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
                else if (enteredText.Contains("(") | enteredText.Contains(")") | enteredText.Contains("[") | enteredText.Contains("]"))
                {
                    return enteredText;
                }
                else
                {
                    Console.WriteLine("Error. You enter wrong value!");
                    continue;
                }

            }
        }
    }
}
