using System;

namespace HomeWork4
{
    [Flags]
    enum bottles
    {
        oneIter = 0b1,
        fiveIter = 0b1 <<1,
        twentyIiter = 0b1 <<2
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Какой объём сока (в литрах) требуетется упаковать?");
            var enteredAmount = double.Parse(Console.ReadLine()); //Дабл число которое ввели
            var dCeilingEnteredAmount = Math.Ceiling(enteredAmount); //Округление, так как это литры, идёт вверх
            //int fceilingEnteredAmount = int.Parse(dCeilingEnteredAmount); Почему-то не работает, возвращает тип не Int, а system read only span char.
            int ceilingEnteredAmount = Convert.ToInt32(dCeilingEnteredAmount);//Перевели в инт

            //Вместимость контейнеров
            var bAllBottlesAvaible = bottles.oneIter | bottles.fiveIter | bottles.twentyIiter;
            var bOneLiterBottleUnAvaible = bAllBottlesAvaible ^ bottles.oneIter;
            var bFiveLiterBottleUnAvaible = bAllBottlesAvaible ^ bottles.fiveIter;
            var bTwentyLiterBottleUnAvaible = bAllBottlesAvaible ^ bottles.twentyIiter;
            //Перевод в int
            int allBottlesAvaible = Convert.ToInt32(bAllBottlesAvaible);
            int oneLiterBottleUnAvaible = Convert.ToInt32(bOneLiterBottleUnAvaible);
            int fiveLiterBottleUnAvaible = Convert.ToInt32(bFiveLiterBottleUnAvaible);
            int twentyLiterBottleUnAvaible = Convert.ToInt32(bTwentyLiterBottleUnAvaible);

            if (ceilingEnteredAmount <= 0)
            {
                Console.WriteLine("Введите число больше нуля");
                return;
            }
            else if (allBottlesAvaible == 7) //Все бутылки
            {
                double finalTwentyLiterBottleResult = ceilingEnteredAmount / 20; //Делим введённое число пользователем на объем 20 литрового контейнера,
                int iFinalTwentyLiterBottleResult = (int)finalTwentyLiterBottleResult;   //Округляем до интом
                int minusTwentyLiterBottleResult = ceilingEnteredAmount - iFinalTwentyLiterBottleResult * 20; // Узнаём остаток
                double finalFiveLiterBottleResult = minusTwentyLiterBottleResult / 5; //Делим введённое число пользователем на оъём 5 литрового контейнера,
                int iFinalFiveLiterBottleResult = (int)finalFiveLiterBottleResult;       //Может выйти не целое число, поэтому добавляем дабл и округляем интом
                int minusFiveBottleResult = minusTwentyLiterBottleResult - iFinalFiveLiterBottleResult * 5; //Узнаем остаточное значение
                int finalOneLiterBottleResult = minusFiveBottleResult / 1; //Высчитываем значение литровых
                Console.WriteLine("Вам понадобятся: " + finalOneLiterBottleResult + "шт. литровых контейнеров " + finalFiveLiterBottleResult + "шт. пятилитровых контейнеров "
                    + finalTwentyLiterBottleResult + "шт. двадцатилитровых контейнеров");
            }
            else if (allBottlesAvaible == twentyLiterBottleUnAvaible) //1 и 5
            {
                double finalFiveLiterBottleResult = ceilingEnteredAmount / 5; //Делим введённое число пользователем на оъём 5 литрового контейнера,
                int iFinalFiveLiterBottleResult = (int)finalFiveLiterBottleResult;              //Может выйти не целое число, поэтому добавляем дабл и округляем интом
                int minusBottleResult = ceilingEnteredAmount - iFinalFiveLiterBottleResult * 5; //Узнаем остаточное значение
                int finalOneLiterBottleResult = minusBottleResult / 1; //Высчитываем значение литровых
                Console.WriteLine("Вам понадобятся: " + finalOneLiterBottleResult + "шт. литровых контейнеров " + finalFiveLiterBottleResult + "шт. пятилитровых контейнеров");
            }
            else if (allBottlesAvaible == fiveLiterBottleUnAvaible) //1 и 20
            {
                double finalTwentyLiterBottleResult = ceilingEnteredAmount / 20; //Делим введённое число пользователем на оъём 20 литрового контейнера,
                int iFinalTwentyLiterBottleResult = (int)finalTwentyLiterBottleResult;              //Может выйти не целое число, поэтому добавляем дабл
                int minusBottleResult = ceilingEnteredAmount - iFinalTwentyLiterBottleResult * 20; //Узнаем остаточное значение
                int finalOneLiterBottleResult = minusBottleResult / 1; //Высчитываем значение литровых
                Console.WriteLine("Вам понадобятся: " + finalOneLiterBottleResult + "шт. литровых контейнеров " + finalTwentyLiterBottleResult + "шт. двадцатилитровых контейнеров");
            }
            else if (allBottlesAvaible == oneLiterBottleUnAvaible) //5 и 20
            {
                double finalTwentyLiterBottleResult = ceilingEnteredAmount / 20; //Делим введённое число пользователем на оъём 20 литрового контейнера,
                int iFinalTwentyLiterBottleResult = (int)finalTwentyLiterBottleResult;              //Может выйти не целое число, поэтому добавляем дабл
                int minusBottleResult = ceilingEnteredAmount - iFinalTwentyLiterBottleResult * 20; //Узнаем остаточное значение
                int finalFiveLiterBottleResult = minusBottleResult / 5; //Высчитываем значение литровых
                Console.WriteLine("Вам понадобятся: " + finalFiveLiterBottleResult + "шт. пятилитровых контейнеров " + finalTwentyLiterBottleResult + "шт. двадцатилитровых контейнеров");
            }
        }
    }
}
