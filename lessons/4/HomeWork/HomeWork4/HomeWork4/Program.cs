using System;

namespace HomeWork4
{
    [Flags]
    enum bottles
    {
        zero = 0,
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

            //Контейнеры на складе
            int amountOneLiterBottle = 10;
            int amountFiveLiterBottle = 5;
            int amountTwentyLiterBottle = 4;

            //Вместимость контейнеров
            int valueOneLiterBottle = 1;
            int valueFiveLiterBottle = 5;
            int valueTwentyLiterBottle = 20;

            if (ceilingEnteredAmount <= 0)
            {
                Console.WriteLine("Введите число больше нуля");
                return;
            }

            else if (amountOneLiterBottle > 0 & amountFiveLiterBottle > 0 & amountTwentyLiterBottle > 0) //все бутылки
            {
                if (ceilingEnteredAmount >= 20)
                {
                    double finalTwentyLiterBottleResult = ceilingEnteredAmount / valueTwentyLiterBottle; //Делим введённое число пользователем на объем 20 литрового контейнера,
                    int iFinalTwentyLiterBottleResult = (int)finalTwentyLiterBottleResult;   //Округляем до интом
                    int minusTwentyLiterBottleResult = ceilingEnteredAmount - iFinalTwentyLiterBottleResult * valueTwentyLiterBottle; // Узнаём остаток
                    double finalFiveLiterBottleResult = minusTwentyLiterBottleResult / valueFiveLiterBottle; //Делим введённое число пользователем на оъём 5 литрового контейнера,
                    int iFinalFiveLiterBottleResult = (int)finalFiveLiterBottleResult;       //Может выйти не целое число, поэтому добавляем дабл и округляем интом
                    int minusFiveBottleResult = minusTwentyLiterBottleResult - iFinalFiveLiterBottleResult * valueFiveLiterBottle; //Узнаем остаточное значение
                    int finalOneLiterBottleResult = minusFiveBottleResult / valueOneLiterBottle; //Высчитываем значение литровых
                    Console.WriteLine("Вам понадобятся: " + finalOneLiterBottleResult + "шт. литровых контейнеров " + finalFiveLiterBottleResult + "шт. пятилитровых контейнеров "
                        + finalTwentyLiterBottleResult + "шт. двадцатилитровых контейнеров");
                }
                else if (ceilingEnteredAmount < 20 & ceilingEnteredAmount >= 5)
                {
                    double finalFiveLiterBottleResult = ceilingEnteredAmount / valueFiveLiterBottle; //Делим введённое число пользователем на оъём 5 литрового контейнера,
                    int iFinalFiveLiterBottleResult = (int)finalFiveLiterBottleResult;              //Может выйти не целое число, поэтому добавляем дабл и округляем интом
                    int minusBottleResult = ceilingEnteredAmount - iFinalFiveLiterBottleResult * valueFiveLiterBottle; //Узнаем остаточное значение
                    int finalOneLiterBottleResult = minusBottleResult / valueOneLiterBottle; //Высчитываем значение литровых
                    Console.WriteLine("Вам понадобятся: " + finalOneLiterBottleResult + "шт. литровых контейнеров " + finalFiveLiterBottleResult + "шт. пятилитровых контейнеров");
                }
                else if (ceilingEnteredAmount < 5)
                {
                    int finalOneLiterBottleResult = ceilingEnteredAmount / valueOneLiterBottle;
                    Console.WriteLine("Вам понадобятся " + finalOneLiterBottleResult + "шт. литровых контейнеров");
                }
            }
            else if (amountOneLiterBottle > 0 & amountFiveLiterBottle > 0 & amountTwentyLiterBottle <= 0) //1 и 5
            {
                if (ceilingEnteredAmount >= 5)
                {
                    double finalFiveLiterBottleResult = ceilingEnteredAmount / valueFiveLiterBottle; //Делим введённое число пользователем на оъём 5 литрового контейнера,
                    int iFinalFiveLiterBottleResult = (int)finalFiveLiterBottleResult;              //Может выйти не целое число, поэтому добавляем дабл и округляем интом
                    int minusBottleResult = ceilingEnteredAmount - iFinalFiveLiterBottleResult * valueFiveLiterBottle; //Узнаем остаточное значение
                    int finalOneLiterBottleResult = minusBottleResult / valueOneLiterBottle; //Высчитываем значение литровых
                    Console.WriteLine("Вам понадобятся: " + finalOneLiterBottleResult + "шт. литровых контейнеров " + finalFiveLiterBottleResult + "шт. пятилитровых контейнеров");
                }
                else if (ceilingEnteredAmount < 5)
                {
                    int finalOneLiterBottleResult = ceilingEnteredAmount / valueOneLiterBottle;
                    Console.WriteLine("Вам понадобятся: " + finalOneLiterBottleResult + "шт. литровых контейнеров");
                }
            }
            else if (amountOneLiterBottle > 0 & amountFiveLiterBottle <= 0 & amountTwentyLiterBottle > 0) //1 и 20
            {
                if (ceilingEnteredAmount >= 20)
                {
                    double finalTwentyLiterBottleResult = ceilingEnteredAmount / valueTwentyLiterBottle; //Делим введённое число пользователем на оъём 20 литрового контейнера,
                    int iFinalTwentyLiterBottleResult = (int)finalTwentyLiterBottleResult;              //Может выйти не целое число, поэтому добавляем дабл
                    int minusBottleResult = ceilingEnteredAmount - iFinalTwentyLiterBottleResult * valueTwentyLiterBottle; //Узнаем остаточное значение
                    int finalOneLiterBottleResult = minusBottleResult / valueOneLiterBottle; //Высчитываем значение литровых
                    Console.WriteLine("Вам понадобятся: " + finalOneLiterBottleResult + "шт. литровых контейнеров " + finalTwentyLiterBottleResult + "шт. двадцатилитровых контейнеров");
                }
                else if (ceilingEnteredAmount < 20)
                {
                    int finalOneLiterBottleResult = ceilingEnteredAmount / valueOneLiterBottle;
                    Console.WriteLine("Вам понадобятся: " + finalOneLiterBottleResult + "шт. литровых контейнеров");
                }
            }
            else if (amountOneLiterBottle <= 0 & amountFiveLiterBottle > 0 & amountTwentyLiterBottle > 0 & (ceilingEnteredAmount % amountFiveLiterBottle) == 0) //5 и 20. Используется если число кратно 5.
            {
                if (ceilingEnteredAmount >= 20)
                {
                    double finalTwentyLiterBottleResult = ceilingEnteredAmount / valueTwentyLiterBottle; //Делим введённое число пользователем на оъём 20 литрового контейнера,
                    int iFinalTwentyLiterBottleResult = (int)finalTwentyLiterBottleResult;              //Может выйти не целое число, поэтому добавляем дабл
                    int minusBottleResult = ceilingEnteredAmount - iFinalTwentyLiterBottleResult * valueTwentyLiterBottle; //Узнаем остаточное значение
                    int finalFiveLiterBottleResult = minusBottleResult / valueFiveLiterBottle; //Высчитываем значение литровых
                    Console.WriteLine("Вам понадобятся: " + finalFiveLiterBottleResult + "шт. пятилитровых контейнеров " + finalTwentyLiterBottleResult + "шт. двадцатилитровых контейнеров");
                }
                else if (ceilingEnteredAmount < 20)
                {
                    int finalFiveLiterBottleResult = ceilingEnteredAmount / valueFiveLiterBottle;
                    Console.WriteLine("Вам понадобятся: " + finalFiveLiterBottleResult + "шт. пятилитровых контейнеров");
                }
            }
            else if (amountOneLiterBottle > 0 & amountFiveLiterBottle <= 0 & amountTwentyLiterBottle <= 0 & amountOneLiterBottle >= ceilingEnteredAmount) //Если можно всё покрыть однолитровыми контейнерами
            {
                int finalOneLiterBottleResult = ceilingEnteredAmount / valueOneLiterBottle;
                Console.WriteLine("Вам понадобятся " + finalOneLiterBottleResult + "шт. литровых контейнеров");
            }
        }  
    }
}
