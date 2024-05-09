using System;

class Program
{   static int[] numbers = new int[2];
    static string[] StringNumbers;
    static void Main()
    {

        while (true)
        {
            Console.WriteLine("Введите два целых числа.");
            Console.WriteLine("Для завершения программы введите \"0 0\".");
            StringNumbers = Console.ReadLine().Split();

            bool Correct = false;
            for (int i = 0;i<2;i++)
            {
                Correct = false;
                while (!Correct)
                {
                    try
                    {
                        if (!int.TryParse(StringNumbers[i], out numbers[i]))
                            throw new FormatException($"Не удалось преобразовать {StringNumbers[i]} в число.");

                        if (numbers[i] < 0) 
                            throw new FormatException("Введите беззнаковые числа.");
                        
                        Correct = true;
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine($"Ошибка ввода. {e.Message}");
                        Console.WriteLine($"Повторите ввод числа {i+1}");
                        StringNumbers[i] = Console.ReadLine();

                    }
                }
            }
            

            Console.WriteLine($"\nВы ввели {numbers[0]} и {numbers[1]}.");
            if (numbers[0] == 0 && numbers[1]== 0) 
            {
                Console.Write("Программа завершена.");
                break;
            }

            int c = 0, ans = 0;
            for (int i = 9; i >= 0; i--)
            {
                c = (numbers[0] % 10 + numbers[1] % 10 + c) > 9 ? 1 : 0;
                ans += c;
                numbers[0] /= 10; numbers[1] /= 10;
            }

            if (ans == 0) Console.WriteLine("Нету переносов.");
            else Console.WriteLine($"Всего переносов: {ans}.\n");


        }
    }
}
