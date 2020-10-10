using System;

namespace HomeWork14
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new FibonacciYield(20);
            var test2 = new Fibonacci(10);

            foreach (var i in test)
            {
                Console.WriteLine(i);
            }
            foreach (var i in test2)
            {
                Console.WriteLine(i);
            }
        }
    }
}
