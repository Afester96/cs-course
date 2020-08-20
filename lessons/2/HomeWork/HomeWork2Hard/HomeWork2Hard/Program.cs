using System;

namespace HomeWork2Hard
{
    class Program
    {
        static void Main(string[] args)
        {
            //Providing information to the user about numbers
            Console.WriteLine("Please, enter two numbers.");

            //Converting entered string numbers into double numbers
            double numberFirst = double.Parse(Console.ReadLine());
            double numberSecond = double.Parse(Console.ReadLine());

            //Providing information to the user about mathematical operation
            Console.WriteLine("Please, enter mathematical operation");
             
            //Storing entered mathematical operation value
            string mathematicalOpreation = Console.ReadLine();

            //Providing information about mathematical operations with if else construction
            if (mathematicalOpreation == "+") //addition operation
            {
                Console.WriteLine("addition");
                Console.WriteLine(numberFirst + numberSecond);
            }
            else if (mathematicalOpreation == "-") //subtraction operation
            {
                Console.WriteLine("subtraction");
                Console.WriteLine(numberFirst - numberSecond);
            }
            else if (mathematicalOpreation == "*") //multiplication operation
            {
                Console.WriteLine("multiplication");
                Console.WriteLine(numberFirst * numberSecond);
            }
            else if (mathematicalOpreation == "/") //division operation
            {
                Console.WriteLine("division");
                Console.WriteLine(numberFirst / numberSecond);
            }
            else //operation if user write wrong mathematical operation
            {
                Console.WriteLine("Plese enter correct mathematical operation (+, -, *, /)");
            }

            //Providing information about the end of program and opportunity to exit
            Console.WriteLine("Thank you for using our program, press any key for exit!");
            Console.ReadLine();
        }
    }
}
