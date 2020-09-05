using System;

namespace HomeWork1
{
    class Program
    {
        static void Main(string[] args)
        {
            int enteredNumber = ReadNumber("Введите положительное натуральное число не более 2 миллиардов");
            var sEnteredNumber = enteredNumber.ToString();
            int evenCounter = 0;
            for (int i = 0; i < sEnteredNumber.Length; i++)
            {
                if ((int.Parse(sEnteredNumber[i].ToString()) % 2) == 0)
                    evenCounter++;
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
                }
                catch (OverflowException exception)
                {
                    Console.WriteLine("Ошибка " + exception.Message + "! Попробуйте ещё раз: ");
                }
            }
        }
    }
}
