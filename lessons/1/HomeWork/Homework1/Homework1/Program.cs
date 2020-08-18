using System;
using System.Threading;

namespace Homework1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Переменная для запоминания имени пользователя.
            string name;

            //Отправка сообщения пользователю.
            Console.WriteLine("Please, enter your name");

            //Ввод имени и его запоминание.
            name = Console.ReadLine();

            //Информирование пользователя о том, что программа не зависла.
            Console.WriteLine("Thank you! Please, await 5 seconds.");

            //Ожидание 5 секунд.
            Thread.Sleep(5000);

            //Приветствие пользателя. С использованием данных полученных при вводе.
            Console.WriteLine("Welcome, " + name + "! Please, await 5 seconds.");

            //Ожидание 5 секунд.
            Thread.Sleep(5000);

            //Прощание с пользователем. С использованием данных полученных при вводе.
            Console.WriteLine("Goodbye, " + name + "! Come back again.");

            //Ожидание нажатия кнопки пользователем.
            Console.ReadLine();
        }
    }
}
