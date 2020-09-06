using System;

namespace HomeWork1
{
    class Program
    {
        static void Main(string[] args)
        {
            int enteredNumber = ReadNumber("Введите положительное натуральное число не более 2 миллиардов");
            int evenCounter = 0;
            int forEnteredNumber = enteredNumber;
            for (int i = 0; i < enteredNumber.ToString().Length; i++)
            {
                int perNumber = forEnteredNumber % 10;
                forEnteredNumber /= 10;
                if (perNumber % 2 == 0)
                {
                    evenCounter++;
                }
            }
            Console.WriteLine("В числе " + enteredNumber + " содержится " + evenCounter + " чётных чисел.");
            Console.WriteLine("Нажмите любую клавишу для выхода");
            Console.ReadKey();
        }
        static int ReadNumber(string text)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine(text);
                    int readNumber = int.Parse(Console.ReadLine());
                    if (readNumber >= 0)
                    {
                        return readNumber;
                    }
                    else if (readNumber < 0)
                    {
                        Console.WriteLine("Ошибка! Введено неверное значение. Попробуйте ещё раз: ");
                        continue;
                    }
                    else if (readNumber < 2000000000)
                    {
                        Console.WriteLine("Ошибка! Введено неверное значение. Попробуйте ещё раз: ");
                        continue;
                    }
                }
                catch (FormatException exception)
                {
                    Console.WriteLine("Ошибка " + exception.Message + "! Попробуйте ещё раз: ");
                    continue;
                }
                catch (OverflowException exception)
                {
                    Console.WriteLine("Ошибка " + exception.Message + "! Попробуйте ещё раз: ");
                    continue;
                }
            }
        }
    }
}
