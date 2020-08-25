using System;

namespace HomeWork3
{
    class Program
    {
        static void Main(string[] args)
        {
            //User greeting
            var computerName = Environment.MachineName.ToString();
            Console.WriteLine("Hello " + computerName + "!");

            //Create string arrays for further use in for loops.
            //namePresent is responsible for the name entered into the console.
            //agePresent is responsible for the age entered in the console.
            string[] namePresent = new string[3];
            int[] agePresent = new int[namePresent.Length];
            int[] ageFuture = new int[agePresent.Length];
            

            //Creating a for loop to add data to array
            for (int i = 0; i < namePresent.Length; i++)
            {
                Console.WriteLine("Enter name № " + (i+1) + "."); 
                namePresent[i] = Console.ReadLine();
                Console.WriteLine("Enter age " + namePresent[i] + ".");
                agePresent[i] = int.Parse(Console.ReadLine());
            }
            
            //Convert string to int and then sum with sumAge
            for (int i = 0; i < namePresent.Length; i++)
            {
                Console.WriteLine("How much do you want to increase " + namePresent[i] + " age?");
                int ageNumber = int.Parse(Console.ReadLine());
                ageFuture[i] = ageNumber + agePresent[i]; 
                Console.WriteLine("After " + ageNumber + " years " + namePresent[i] + " will be " + ageFuture[i] + " years old.");                
            }

            //End of the program
            Console.WriteLine("Press anything to end the program.");
            Console.ReadKey(true);
        }
    }
}
