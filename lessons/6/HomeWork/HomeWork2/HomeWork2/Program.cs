using System;

namespace HomeWork2
{
    class Program
    {
        static void Main(string[] args)
        {
            var beforeNumber = ReadNumber("Введите сумму первоначального взноса в рублях:");
            var persentNumber = ReadNumber("Введите ежедневный процент дохода в виде десятичной дроби (1% = 0.01):");
            var afterNumber = ReadNumber("Введите желаемую сумму накопления в рублях:");
            int counterDays = 0;

            while (beforeNumber != afterNumber)
            {
                beforeNumber = beforeNumber + persentNumber;
                counterDays++;
            }
            Console.WriteLine("Вам необходимоd " + counterDays + " дней для накопления суммы.");
        }
        static double ReadNumber(string text)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine(text);
                    var readNumber = double.Parse(Console.ReadLine());
                    if (readNumber > 0)
                    {
                        return readNumber;
                    }
                    else if (readNumber <= 0 )
                    {
                        Console.WriteLine("Ошибка! Введите значение больше нуля.");
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
