using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;
// Класс, представляющий информацию о перекрестках
class Intersections
{
    public int from;
    public int to;
    public int distance;
    public bool hasDepo;

    public Intersections(int from, int to, int distance)
    {
        this.from = from;
        this.to = to;
        this.distance = distance;
        this.hasDepo = false;
    }
}

class Program
{
    static int I;// кол-во перекрестков
    static int f; // кол-во депо
    static int[,] distances = new int[I, I]; // Массив для хранения расстояний между перекрестками
    static int[] depoNumber = new int[f];  // Массив для хранения номеров перекрестков с депо
    static int t;// Количество тестовых блоков


    static void Main()
    {
        bool Correct = false;
        int[] line = new int[3];
        // Запрос на ввод количества тестовых блоков
        Console.WriteLine("Введите количество тестовых блоков.");
        while (!Correct)
        {
            try
            {
                if (!int.TryParse(Console.ReadLine(), out t))
                    throw new FormatException("Неправильный формат ввода. Пожалуйста, введите одно число.");
                if (t == 0 || t < 0)
                    throw new FormatException("Введите число >0");

                Correct = true;

            }
            catch (FormatException e)
            {

                Console.WriteLine($"Ошибка ввода: {e.Message}");
            }
        }
        Console.WriteLine();
        while (t-- > 0)
        {
            Console.WriteLine("Введите число существующих пожарных депо и число перекрестков через пробел.");
            string[] input = Console.ReadLine().Split();

            Correct = false;

            while (!Correct)
            {
                try
                {
                    if(input.Length !=2)
                        throw new FormatException($"Введите два числа!");

                    if (!int.TryParse(input[0], out f) || !int.TryParse(input[1], out I))
                        throw new FormatException($"Неправильный формат ввода. Пожалуйста повторите ввод депо и перекрестков.");
                    if (f == 0 || f < 0 || I == 0 || I < 0)
                        throw new FormatException("Неправильный формат ввода. Пожалуйста повторите ввод депо и перекрестков. Числа должны быть больше 0.");
                    Correct = true;
                }
                catch (FormatException e)
                {
                    Console.WriteLine($"Ошибка ввода: {e.Message}");
                    input = Console.ReadLine().Split();

                }
            }
            List<Intersections> intersections = new List<Intersections>();

            depoNumber = new int[f];
            Console.WriteLine($"Введите номер перекрестка, на котором расположено пожарное депо.");
            for (int i = 0; i < f; i++)
            {
                Correct = false;
                while (!Correct)
                {
                    try
                    {

                        if (!int.TryParse(Console.ReadLine() , out depoNumber[i]))
                            throw new FormatException("Неправильный формат ввода. Пожалуйста, введите одно число.");
                        depoNumber[i]--;
                        if (depoNumber[i] < 0)
                            throw new FormatException("Введите число >0");
                        Correct = true;

                    }
                    catch (FormatException e)
                    {

                        Console.WriteLine($"Ошибка ввода: {e.Message}");
                    }
                }
            }
            // Запрос на ввод информации о дорогах между перекрестками
            Console.WriteLine("Введите номер перекрестка, номер другого перекрестка и длину дороги между ними.");
            for (int i = 0; i < I; i++)
            {
                input = Console.ReadLine().Split(' ');
                Correct = false;
                while (!Correct)
                {
                    try
                    {
                        if (input.Length != 3)
                            throw new FormatException($"Введите три числа!");
                        if (!int.TryParse(input[0], out line[0]) || !int.TryParse(input[1], out line[1]) || !int.TryParse(input[2], out line[2]))
                            throw new FormatException("Не удалось преобразовать в числа. Скорее всего были введены буквы.");
                        if (line[0] <= 0 || line[1] <= 0|| line[2] <= 0)
                            throw new FormatException("Номер перекреста и длина дороги не могут быть меньше или равны 0"); 
                        intersections.Add(new Intersections(line[0], line[1], line[2]));
                        Correct = true;

                    }
                    catch (FormatException e)
                    {

                        Console.WriteLine($"Неправильный формат ввода. Пожалуйста повторите ввод, номер первого перекрестка, номер второго перекрестка и длину дороги между ними.\nОшибка: {e.Message}");
                        input = Console.ReadLine().Split(' ');
                    }
                }
            }

            // Устанавливаем значения наличия депо для перекрестков
            foreach (int depoIndex in depoNumber)
            {
                intersections[depoIndex].hasDepo = true;
            }

            //Заполняем матрицу расстояний
            distances = new int[I,I];
            for (int i = 0; i < I; i++)
            {
                for (int j = 0; j < I; j++)
                {
                    if (i == j)
                        distances[i, j] = 0;
                    else
                        distances[i, j] = -1;
                }
            }
            // Заполняем массив distances информацией о дорогах
            for (int i = 1; i <= I; i++)
            {
                for (int j = 1; j <= I; j++)
                {
                    if (i != j)
                    {
                        foreach (Intersections intersection in intersections)
                        {
                            distances[intersection.from - 1, intersection.to - 1] = intersection.distance;
                            distances[intersection.to - 1, intersection.from - 1] = intersection.distance;


                        }
                    }

                }
            }

            // Поиск кратчайших путей между всеми парами перекрестков с использованием алгоритма Флойда-Уоршелла
            for (int k = 0; k < I; k++)
            {
                for (int i = 0; i < I; i++)
                {
                    for (int j = 0; j < I; j++)
                    {
                        // Если существует путь от i к j через вершину k
                        if (distances[i, k] != -1 && distances[k, j] != -1)
                        {
                            // Если distance[i, j] еще не установлено или новый путь короче
                            if (distances[i, j] == -1 || distances[i, k] + distances[k, j] < distances[i, j])
                            {
                                distances[i, j] = distances[i, k] + distances[k, j];
                            }
                        }
                    }
                }
            }






            int minValue = int.MaxValue;

            int ff = depoNumber.Count();
            for (int i = 0; i < ff; i++)
            {
                for (int j = 0; j < I; j++)
                {
                    for(int k = 0; k < I; k++)
                    {
                        if (distances[depoNumber[i], k] < minValue && distances[depoNumber[i], k] != 0)
                        {
                            minValue = distances[depoNumber[i], k];
                        }
                    }
                }
               
            }

            int maxValue = 0;
            int optimal = 0;
            for (int j = 0; j < ff; j++)
            {
                for (int i = 0; i < I; i++)
                {
                    // Проверяем, что перекресток i не является депо
                    if (!depoNumber.Contains(i))
                    {
                        for (int k = 0; k < I; k++)
                        {
                            if (distances[i, k] > maxValue)
                            {
                                maxValue = distances[i, k];
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < ff; i++)
            {
                for (int j = 0; j < I; j++)
                {
                    for (int k = 0; k < I; k++)
                    {

                        if (distances[depoNumber[i], k] == minValue && distances[depoNumber[i], k] != maxValue)
                        {
                            distances[depoNumber[i], k] = 0;
                            distances[k, depoNumber[i]] = 0;
                            for (int l = 0; l < I; l++)
                            {
                                distances[k, l] = -1;
                                distances[l, k] = -1;

                            }
                        }


                    }
                }
            }

            maxValue = 0;


            // Находим максимальное значение, исключая перекрестки, в которых уже есть депо

            for (int j = 0; j < ff; j++)
            {
                for (int i = 0; i < I; i++)
                {
                    // Проверяем, что перекресток i не является депо
                    if (!depoNumber.Contains(i))
                    {
                        for (int k = 0; k < I; k++)
                        {
                            if (distances[i, k] > maxValue )
                            {
                                maxValue = distances[i, k];
                                optimal = i + 1; // Используем индекс i + 1, чтобы получить номер перекрестка
                            }
                        }
                    }
                }
            }
           Console.WriteLine($"Оптимальное расположение нового депо:{optimal}");
        }
    }
}
