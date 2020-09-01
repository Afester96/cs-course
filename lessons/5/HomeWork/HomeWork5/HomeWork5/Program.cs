using System;

namespace HomeWork5
{
    class Program
    {
        [Flags]
        enum Figures
        {
            Круг,
            Треугольник,
            Прямоугольник
        }
        static void Main(string[] args)
        {
            Figures enteredType = ReadType("Введите название фигуры: Круг, Треугольник, Прямоугольник");
            Console.WriteLine(enteredType);
            {
                switch (enteredType)
                {
                    case Figures.Круг:
                        double circleANumber = ReadNumber("Введите радиус " + enteredType + "а");
                        var circleS = circleANumber * circleANumber * 3.14;
                        var circleP = 2 * 3.14 * circleANumber;
                        Console.WriteLine("Площадь круга = " + circleS);
                        Console.WriteLine("Периметр круга = " + circleP);
                        break;
                    case Figures.Треугольник:
                        double firstNumber = ReadNumber("Введите основание " + enteredType + "а");
                        double secondNumber = ReadNumber("Введите высоту " + enteredType + "а");
                        double thirdNumber = ReadNumber("Введите сторону a " + enteredType + "а");
                        double fourthNumber = ReadNumber("Введите сторону b " + enteredType + "а");
                        var resultS = firstNumber * secondNumber / 2;
                        var resultP = secondNumber + thirdNumber + fourthNumber;
                        Console.WriteLine("Площадь треугольника = " + resultS);
                        Console.WriteLine("Периметр треугольника = " + resultP);
                        break;
                    case Figures.Прямоугольник:
                        double rectangleANumber = ReadNumber("Введите сторону a " + enteredType + "а");
                        double rectangleBNumber = ReadNumber("Введите сторону b " + enteredType + "а");
                        var rectangleS = rectangleANumber * rectangleBNumber;
                        var rectangleP = (rectangleANumber + rectangleBNumber) * 2;
                        Console.WriteLine("Площадь прямоугольника = " + rectangleS);
                        Console.WriteLine("Периметр прямоугольника = " + rectangleP);
                        break;
                }
            } 
        }
        static double ReadNumber (string caption)
        {
            for (;;)
            {
                try
                {
                    Console.WriteLine(caption);
                    var enteredNumber = double.Parse(Console.ReadLine());
                    return enteredNumber;
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("ОШИБКА! Введите не нулевое значение");
                }
                catch (FormatException)
                {
                    Console.WriteLine("ОШИБКА! Введите числовое значение");
                }
            }
        }
        static Figures ReadType (string caption)
        {
            for (;;)
            {
                try
                {
                    Console.WriteLine(caption);
                    Figures readEnteredType = (Figures)Enum.Parse(typeof(Figures), Console.ReadLine());
                    return readEnteredType;
                }
                catch (FormatException)
                {
                    Console.WriteLine("ОШИБКА! Введите текстовое значение");
                }
                catch (System.ArgumentException)
                {
                    Console.WriteLine("ОШИБКА! Введите значение из списка ниже");
                }
            }
        }
    }
}
