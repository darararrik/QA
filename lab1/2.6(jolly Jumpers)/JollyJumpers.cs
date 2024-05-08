
using System.Numerics;

namespace jolly_Jumpers
{
    internal static class JollyJumpers
    {
        public static bool IsJollyJumpers(string input)
        {
            int d;
            string[] Numbers = input.Split(' ');
            int[] NumbersInt = new int[Numbers.Length];
            bool parsedSuccessfully = false;
            //Проверка на ввод
            for (int i = 0;i<NumbersInt.Length;i++)
            {
                parsedSuccessfully = false;

                while (!parsedSuccessfully)
                {
                    try
                    {
                        NumbersInt[i] = int.Parse(Numbers[i]);
                        parsedSuccessfully = true;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine($"Ошибка: Ваш ввод: {Numbers[i]}. Пожалуйста повторите ввод числа:");
                        Numbers[i] = Console.ReadLine(); // Обнуляем только некорректный элемент
                    }
                }

            }

            //Создаем булевой массив
            Boolean[] array = new Boolean[Numbers.Length];
            for (int i = 0;i<NumbersInt.Length - 1;i++)
            {
                d = Math.Abs(NumbersInt[i] - NumbersInt[i+1]);
                if (d == 0 || d > NumbersInt.Length - 1 || array[d] == true) 
                    return false;
                else
                    array[d] = true;


            }
            return true;
        }

    }
}
