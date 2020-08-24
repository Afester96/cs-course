
using System;

namespace ClassWork3
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] array = new string[5];

            for (int i = 0; i < 5; i++)
            {
                array[i] = Console.ReadLine();
            }
           
            for (int i = 0; i < 5; i++)
            {
               Console.WriteLine(array[i]);
            }
        }
    }
}
