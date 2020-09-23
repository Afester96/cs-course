using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeWork9
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Hello {Environment.MachineName}!");
            var persons = new List<Person>(); //Создаётся список людей
            var counter = IntEnteredText("How many names you want to enter?"); //Сколько нужно добавить людей
            for (int i = 0; i < counter; i++) //В каждого закидываем данные
            {
                persons.Add(new Person()
                {
                    Name = StringEnteredText("Ener the name."),
                    Age = IntEnteredText("Enter the age.")
                });
            }
            for (int i = 0; i < persons.Count; i++) //Выводим
            {
                Console.WriteLine(persons[i].Info);
            }
        }

        static string StringEnteredText(string text) //Строковый текст
        {
            while (true)
            {
                Console.WriteLine(text);
                var enteredText = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(enteredText))
                {
                    Console.WriteLine("ERROR. You entered null value");
                    continue;
                }
                else if (enteredText.Contains(" "))
                {
                    Console.WriteLine("ERROR. You entered more then one value");
                    continue;
                }
                else
                {
                    return enteredText;
                }
            }
        }

        static int IntEnteredText(string text) //Числовой текст
        {
            while (true)
            {
                Console.WriteLine(text);

                try
                {
                    var enteredText = int.Parse(Console.ReadLine());
                    return enteredText;
                }
                catch (FormatException)
                {
                    Console.WriteLine("ERROR. You entered wrong value.");
                    continue;
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("ERROR. You entered more then one value");
                    continue;
                }
            }
        }
    }
}

