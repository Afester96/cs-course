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
            Console.WriteLine("First number");
            double numberFirst = double.Parse(Console.ReadLine());
            Console.WriteLine("Second number");
            double numberSecond = double.Parse(Console.ReadLine());

            //Providing information to the user about mathematical operation
            Console.WriteLine("Please, enter mathematical operation");
             
            //Storing entered mathematical operation value
            string mathematicalOpreation = Console.ReadLine();

            //Providing information about mathematical operations with if else construction
            if (mathematicalOpreation == "+") //addition operation
            {
                Console.WriteLine("Addition");
                Console.WriteLine("Your result is " + (numberFirst + numberSecond));
            }
            else if (mathematicalOpreation == "-") //subtraction operation
            {
                Console.WriteLine("Subtraction");
                Console.WriteLine("Your result is " + (numberFirst - numberSecond));
            }
            else if (mathematicalOpreation == "*") //multiplication operation
            {
                Console.WriteLine("Multiplication");
                Console.WriteLine("Your result is " + (numberFirst * numberSecond));
            }
            else if (mathematicalOpreation == "/") //division operation
            {
                Console.WriteLine("Division");
                Console.WriteLine("Your result is " + (numberFirst / numberSecond));
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
