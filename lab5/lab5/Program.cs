using System.ComponentModel;

internal class Program
{
    static int[,] adjancyMatrix;
    //graphs[0] - Вершины
    //graphs[1] - Ребра
    static int[] graphs;
    static int Tests;
    static int S, D, T;

    static void Matrix()
    {
        for (int i = 0; i < graphs[1]; i++) 
        {
            for(int j = 0; j < graphs[1]; j++) 
            {
                adjancyMatrix[i, j] = -1;
            }
        }
    }
    static void MatrixFloyd()
    {
        for (int k = 0; k < graphs[1]; k++)
        {
            for (int i = 0; i < graphs[1]; i++)
            {
                for (int j = 0; j < graphs[1]; j++)
                {
                    adjancyMatrix[i, j] = Math.Max(adjancyMatrix[i, j], Math.Min(adjancyMatrix[i,k], adjancyMatrix[k,j]));
                }
            }
        }

    }
    private static void Main()
    {
        bool Correct = false;
        int C1, C2, P;
        graphs = new int[2];

        Console.WriteLine("Введите количество тестовых блоков.");
        while (!Correct) 
        {
            try
            {
                if (!int.TryParse(Console.ReadLine(), out Tests))
                    throw new FormatException("Неправильный формат ввода. Пожалуйста, введите одно число.");
                if(Tests == 0 || Tests<0)
                    throw new FormatException("Введите число >0");

                Correct = true;

            }
            catch (FormatException e)
            {

                Console.WriteLine($"Ошибка ввода: {e.Message}");
            }
        }
        int scenario = 0 ;
        while (Tests-->0) 
        {
            Console.WriteLine("Введите число городов и число дорожных сегментов через пробел.");
            string[] input = Console.ReadLine().Split();

            Correct = false;

            while (!Correct)
            {
                try
                {
                    if (!int.TryParse(input[0], out graphs[0]) || !int.TryParse(input[1], out graphs[1]))
                        throw new FormatException($"Неправильный формат ввода. Пожалуйста повторите ввод городов и дорог.");
                    if (graphs[0] == 0 || graphs[0] < 0 || graphs[1] == 0 || graphs[1] < 0)
                        throw new FormatException("Неправильный формат ввода. Пожалуйста повторите ввод городов и дорог. Числа должны быть больше 0.");
                    Correct = true;
                }
                catch (FormatException e)
                {
                    Console.WriteLine($"Ошибка ввода: {e.Message}");
                    input = Console.ReadLine().Split();

                }
            }
            adjancyMatrix = new int[graphs[1], graphs[1]];

            Matrix();
            Console.WriteLine("Введите номера городов (откуда - куда) и число пассажиров, которые могут перевозиться между двумя городами.\nЧерез пробел.");
            Console.WriteLine("Пример: 1 2 30");
            for (int i = 0; i < graphs[1]; i++)
            {
                Correct = false;

                input = Console.ReadLine().Split();

                while (!Correct)
                {
                    try
                    {
                        if (!int.TryParse(input[0], out C1) || !int.TryParse(input[1], out C2) || !int.TryParse(input[2], out P))
                            throw new FormatException($"Не удалось преобразовать входные данные в числа.");
                        if (C1 < 0 || C2 < 0 || P < 0)
                            throw new FormatException($"Номера городов и пассажиров должны быть больше 0.");
                        adjancyMatrix[C1,C2] = P;
                        adjancyMatrix[C2, C1] = P;

                        Correct = true;
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine($"Неправильный формат ввода. Пожалуйста повторите ввод.\nОшибка: {e.Message}");
                        input = Console.ReadLine().Split();

                    }
                }
            }

            Correct = false;
            Console.WriteLine("Введите начальный город, конечный город и число туристов, которых необходимо перевезти.\nЧерез пробел.");
            input = Console.ReadLine().Split();

            while (!Correct)
            {
                try
                {
                    if (!int.TryParse(input[0], out S) || !int.TryParse(input[1], out D) || !int.TryParse(input[2], out T))
                        throw new FormatException($"Не удалось преобразовать входные данные в числа.");
                    if (S < 0 || D < 0 || T < 0)
                        throw new FormatException($"Номера городов и пассажиров должны быть больше 0.");


                    Correct = true;
                }
                catch (FormatException e)
                {
                    Console.WriteLine($"Неправильный формат ввода. Пожалуйста повторите ввод.\n Ошибка: {e.Message}");
                    input = Console.ReadLine().Split();

                }
            }
            MatrixFloyd();


            Console.WriteLine($"Scenario {++scenario}");
            //- 1 потому что нужно считать еще себя(мистера Ж.)
            int answer = T / (adjancyMatrix[S, D] - 1);
            if (T % adjancyMatrix[S,D] > 0)
            {
                //Для оставшихся 
                answer++;
            }
            Console.WriteLine($"Minimum Number of Trips = {answer}");
        }



    }
  
}
