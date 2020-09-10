using System;
using System.Collections.Generic;

namespace HomeWork1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please, enter [] or ().");
            var entered = Console.ReadLine().Trim(); //Вводятся данные, удаляем все пробелы
            var dictionary = new Dictionary<char, char> //У нас имеется словарь
            {
                {'(',')'},
                {'[',']'}
            };
            var entered2 = entered.ToCharArray(); //Переводим введённые значения в массив типа чар
            var list = new List<char>(); //Создаём список
            for (int i = 0; i < entered2.Length; i++) //Заполняем его значениями
            {
                list.Add(entered2[i]);
            }
            while (true)
            {
                if (entered2.Length == 1) //Если одно значение, то всегда будет фолс
                {
                    Console.WriteLine("False");
                    break;
                }
                else if (list.Count == 0) //Если список пуст, пускаем тру, так как всё взаимоуничтожилось
                {
                    Console.WriteLine("True");
                    break;
                }
                else if (list.Count == entered2.Length) //Если количество в листе равняется количеству введённых данных,
                {
                    for (int i = 0; i < entered2.Length; i++) //Проходимся циклом сначала по ключам словаря
                    {
                        if (dictionary.ContainsKey(entered2[i]) & list.Count > 1) //Если есть совпадение, прыгаем ниже. Если значений остаётся меньше одного, то цикл дальше не идёт
                        {
                            for (int j = 0; j < entered2.Length; j++) //Далее проходимся циклом по значениям словаря
                            {
                                if (dictionary.ContainsValue(entered2[j]) & dictionary.ContainsKey(entered2[i])) //Перебираем значения словаря по ключу который задали выше
                                {
                                    list.Remove(entered2[j]); //Если есть совпадение, удаляем из листа значение которое подошло
                                }
                            }
                            list.Remove(entered2[i]);//Если есть сопадение, удаляем из листа ключ который подошёл
                        }
                    }
                }
                else //Если никакой вариант не подошёл, выводит фолс. Из-за удаления всегда остётся одно значение, которое и вызывает else
                {
                    Console.WriteLine("False");
                    break;
                }
            }
        }
    }
}
