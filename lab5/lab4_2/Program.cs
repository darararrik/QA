﻿using System;
using System.Collections.Generic;

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

class FireStationPlacement
{
    static int I;
    static int f;
    static int[,] distances = new int[I, I];
    static int[] depoNumber = new int[f];

    static void ShowArray()
    {
        Console.Write("\t");
        for (int i = 0; i < I; i++)
        {
            Console.Write($"\t{i+1}");
        }
        Console.WriteLine();
        Console.Write("\t");
        for (int i = 0; i < I; i++)
        {
            Console.Write("---------");
        }
        Console.WriteLine();

        for (int i = 0; i < I; i++)
        {
            Console.Write($"\t{i + 1}|");

            for (int j = 0; j < I; j++)
            {
                Console.Write($"\t{distances[i, j]}");
            }
            Console.WriteLine($"|");
        }
    }
    static void Main()
    {
        int t = int.Parse(Console.ReadLine());
        while (t-- > 0)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            f = int.Parse(inputs[0]); // кол-во депо
            I = int.Parse(inputs[1]); // кол-во перекрестков

            List<Intersections> intersections = new List<Intersections>();

            depoNumber = new int[f];
            for (int i = 0; i < f; i++)
            {
                depoNumber[i] = int.Parse(Console.ReadLine()) - 1;
            }

            for (int i = 0; i < I; i++)
            {
                string[] line = Console.ReadLine().Split(' ');
                intersections.Add(new Intersections(int.Parse(line[0]), int.Parse(line[1]), int.Parse(line[2])));
            }

            // Устанавливаем флаги депо для перекрестков
            foreach (int depoIndex in depoNumber)
            {
                intersections[depoIndex].hasDepo = true;
            }

            // Применяем алгоритм Флойда-Уоршелла
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

            ShowArray();

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

            ShowArray();





            int minValue = int.MaxValue;

            int ff = depoNumber.Count();
            Console.WriteLine(ff);
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
            Console.WriteLine();
            Console.WriteLine($"minValue = {minValue}");
            Console.WriteLine();

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
            Console.WriteLine($"maxValue 1 = {maxValue} ");

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
            ShowArray();


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



            Console.WriteLine();
            Console.WriteLine($"maxValue 2 = {maxValue}");
            Console.WriteLine();


           Console.WriteLine(optimal);
        }
    }
}
