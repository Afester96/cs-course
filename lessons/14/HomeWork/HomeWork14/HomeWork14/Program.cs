using System;

namespace HomeWork14
{
    class Program
    {
        static void Main(string[] args)
        {
            var fibonacciYield = new FibonacciYield(20);
            var fibonacci = new Fibonacci(10);

            foreach (var i in fibonacciYield)
            {
                Console.WriteLine(i);
            }
            foreach (var i in fibonacci)
            {
                Console.WriteLine(i);
            }
        }
    }
}
