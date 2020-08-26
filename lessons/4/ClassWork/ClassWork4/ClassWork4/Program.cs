using System;

namespace ClassWork4
{
    enum colours
    {
        black = 0b1,
        blue = 0b1 << 1,
        cyan = 0b1 << 2,
        grey = 0b1 << 3,
        green = 0b1 << 4,
        magenta = 0b1 << 5,
        red = 0b1 << 6,
        white = 0b1 << 7,
        yellow = 0b1 << 8
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Вот все значения. выбери какой нравится");
            var coloursArray = (colours[]) Enum.GetValues(typeof(colours));
            for (int i = 0; i < coloursArray.Length; i++)
            {
                Console.WriteLine(coloursArray[i]);
            }
            for (int i = 0; i < 4; i++)
            {
                colours colour = (colours) Enum.Parse (typeof(colours), Console.ReadLine());
                colours  = colour | coloursArray[i];
                Console.WriteLine();
            }
            
            
        }
    }
}
