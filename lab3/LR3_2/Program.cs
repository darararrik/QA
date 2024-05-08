using System;
namespace LR3_2
{
    internal class Program
    {
        static int n;
        public static void Main()
        {
            string stroke = "";
            bool Correct=false;
            Console.WriteLine("Чтобы завершить программу введите 0");

            while (true)
            {
                Correct = false;
           
                while (!Correct) 
                {
                    Console.WriteLine("Введите число:");
                    try
                    {
                        stroke = Console.ReadLine();
                        if (!int.TryParse(stroke, out n))
                            throw new FormatException($"Не удалось преобразовать {stroke} в число.");
                        if (n < 0)
                            throw new FormatException("Введите беззнаковое число.");

                        Correct = true;

                    }
                    catch (FormatException e) 
                    {
                        Console.WriteLine($"Ошибка ввода. {e.Message}");
                    }
                }
                if(n == 0)
                    break;

                var fn = new Golomb(n);
                Console.WriteLine($"fn = {fn.ShowGolomb(n)}");
            }
        }
    }
}




