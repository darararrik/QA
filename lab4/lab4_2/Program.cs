using System;

class Program
{
    static int T;
    static int[,] DP;
    static int[] L;
    //массив ошибки для всех пар палочек
    static int[] bad;

    static int FindDP(int k, int n)
    {
        // Если результат уже был вычислен, возвращаем его из кэша
        if (DP[k, n] != -1)
            return DP[k, n];
        // Если палочек недостаточно для размещения k гостей
        if (n < 3 * k)
        {
            // Устанавливаем значение в int.MaxValue, чтобы обозначить невозможность размещения
            DP[k, n] = int.MaxValue;
        }
        //если больше нет гостей для размещения
        else if (k == 0)
        {
            DP[k, n] = 0;
        }
        else
        {
            // Рекурсивно вычисляем 
            DP[k, n] = Math.Min(FindDP(k, n - 1), FindDP(k - 1, n - 2) + bad[n]);
            // FindDP(k, n - 1) -  если текущая палочка не используется
            // FindDP(k - 1, n - 2) + bad[n] - если текущая палочка используется
        }

        return DP[k, n];
    }

    static void Main()
    {
        bool parsedSuccessfully = false;
        Console.WriteLine("Введите количество тестовых блоков.");
        while (!parsedSuccessfully)
        {
            try
            {
                if (!int.TryParse(Console.ReadLine().Trim(), out T))
                    throw new FormatException("Введена буква. Пожалуйста, введите только целые числа.");

                if (T <= 0)
                    throw new FormatException("Число должно быть положительными. Пожалуйста, введите положительное число.");


                parsedSuccessfully = true;

            }
            catch (FormatException e)
            {
                Console.WriteLine($"Ошибка ввода: {e.Message}");
                Console.Write($"Повторите ввод элемента T: ");
            }

        }
        while (T-- > 0)
        {
            int k = 0;
            int n = 0;
            parsedSuccessfully = false;
            Console.WriteLine("Введите количество гостей и количество палочек через пробел.");

            while (!parsedSuccessfully)
            {
                string[] inputs = Console.ReadLine().Split();

                try
                {
                    if (inputs.Length != 2)
                        throw new FormatException("Неправильный формат ввода. Пожалуйста, введите два числа через пробел.");
                    
                    if (!int.TryParse(inputs[0], out k) || !int.TryParse(inputs[1], out n))
                        throw new FormatException("Введена буква. Пожалуйста, введите только целые числа.");


                    if (k <= 0 || n <= 0)
                        throw new FormatException("Числа должны быть положительными. Пожалуйста, введите положительных числа.");

                    parsedSuccessfully = true;
                }
                catch (FormatException e)
                {
                    Console.WriteLine($"Ошибка ввода {e.Message}");
                }
            }
            L = new int[n + 1];
            bad = new int[n + 1];
            DP = new int[k + 9, n + 1];
            Console.WriteLine("Введите длины палочек через пробел:"); 
            string[] lengths = Console.ReadLine().Split();

            for (int i = 1; i <= n; i++)
            {
                parsedSuccessfully = false;

                while (!parsedSuccessfully)
                {
                    try
                    {

                        if (!int.TryParse(lengths[i -1 ], out L[i]))
                            throw new FormatException("Введена буква. Пожалуйста, введите только целые числа.");


                        if (L[i] <= 0)
                            throw new FormatException("Число должно быть положительными. Пожалуйста, введите положительное число.");
                        parsedSuccessfully = true;

                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine($"Ошибка ввода {e.Message}");
                        Console.Write($"Вы ввели {lengths[i-1]}, введите еще раз: ");
                        lengths[i - 1] = Console.ReadLine();
                    }
                }
            }
            // Сортируем массив L в порядке убывания, начиная с индекса 1 и с учетом первых n элементов.
             // Это позволит упорядочить длины палочек таким образом, чтобы наибольшие значения были в начале массива,
             // что важно для последующего расчета "плохих" пар палочек.
            Array.Sort(L, 1, n, Comparer<int>.Create((a, b) => b.CompareTo(a)));

                for (int i = 2; i <= n; i++)
                {
                    bad[i] = (L[i] - L[i - 1]) * (L[i] - L[i - 1]);
                }

                for (int i = 0; i <= k + 8; i++)
                {
                    for (int j = 0; j <= n; j++)
                    {
                        DP[i, j] = -1;
                    }
                }

                Console.WriteLine($"Суммарная минимальная ошибка наборов: {FindDP(k + 8, n)}");;

            
        }
    }
}
