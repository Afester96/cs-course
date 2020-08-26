using System;

namespace ClassWork4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("a");
            var firstNumber = double.Parse(Console.ReadLine());
            Console.WriteLine("h");
            var secondNumber = double.Parse(Console.ReadLine());
            Console.WriteLine("S side = " + (3 * firstNumber * secondNumber));
            Console.WriteLine("S full = " + (((3.0 / 2) * firstNumber) * (firstNumber * (Math.Sqrt(3)) + (2 * secondNumber))));
            Console.WriteLine("V = " + (Math.Pow(firstNumber, 2) / 2.0 * secondNumber * Math.Sqrt(3)));
            Console.WriteLine("H = " + Math.Sqrt(Math.Pow(secondNumber,2) - Math.Pow(firstNumber,2)/12.0));
        }
    }
}
