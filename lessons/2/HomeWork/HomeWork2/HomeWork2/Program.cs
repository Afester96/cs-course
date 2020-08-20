using System;

namespace HomeWork2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Providing information to the user
            Console.WriteLine("Please, enter two numbers.");

            //Converting entered string into double
            double numberFirst = double.Parse(Console.ReadLine());
            double numberSecond = double.Parse(Console.ReadLine());

            //Providing information about mathematical operations
            Console.WriteLine("addition");
            Console.WriteLine(numberFirst + numberSecond);
            Console.WriteLine("subtraction");
            Console.WriteLine(numberFirst - numberSecond);
            Console.WriteLine("multiplication");
            Console.WriteLine(numberFirst * numberSecond);
            Console.WriteLine("division");
            Console.WriteLine(numberFirst / numberSecond);
        }
    }
}
