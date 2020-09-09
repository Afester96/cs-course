using System;
using System.Text;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new StringBuilder("  lorem   ipsum   dolor    sit    amet   ", 50)
                //.Remove(0, 2) ////Удаление пробелов между строк так
                //.Remove(36, 3)
                //.Remove(29,3)
                //.Remove(6,2)
                //.Remove(12, 2)
                //.Remove(18, 3)
                .Replace("  ", " ") //Или так
                .Replace("  ", " ")
                .Remove(0, 1)//Удаляем первый пробел
                .Remove(26, 1)//Удаляем последний пробел
                .Replace("ipsum", "IPSUM") //Поднимаем регистр
                .ToString();
            Console.WriteLine(test);
        }
    }
}
