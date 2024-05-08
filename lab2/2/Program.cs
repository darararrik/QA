using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks.Sources;

class Program
{
    static int FindPos(List<int> seqNum, int num)
    {
        for (int i = 0; i < seqNum.Count; i++)
        {
            if (seqNum[i] == num)
                return i;
        }
        return -1;
    }

    static void MoveTop(List<int> array, int j)
    {
        int temp = array[j];
        for (int i = j; i > 0; i--)
        {
            array[i] = array[i - 1];
        }
        array[0] = temp;
    }

    static void SortSequence(List<string> startOrder, List<string> endOrder, List<string> result)
    {
        int n = startOrder.Count;
        Dictionary<string, int> endDict = new Dictionary<string, int>();
        for (int i = 0; i < endOrder.Count; i++)
        {
            endDict[endOrder[i]] = i;
        }

        List<int> seqNum = new List<int>();
        foreach (var s in startOrder)
        {
            seqNum.Add(endDict[s]);
        }
        
        for (int curr = n - 1; curr > 0; curr--)
        {//Если индекс текущего числа(curr) меньше чем индекс предыдущего мы перемещаем предыдущее число в начало
            int posCurr = FindPos(seqNum, curr);
            int posNext = FindPos(seqNum, curr - 1);
            if (posCurr < posNext)
            {
                MoveTop(seqNum, posNext);
                result.Add(endOrder[curr - 1]);
      
            }

           

        }
      



    }

    static void Main()
    {
        Console.WriteLine("Введите количество тестовых блоков.");
        int numOfTests = int.Parse(Console.ReadLine().Trim());
        for (int t = 0; t < numOfTests; t++)
        {
            Console.WriteLine("Введите количество черепах в груде.");
            bool CorrectInput = false;
            int lines =0;
            while(!CorrectInput) 
            {
                try
                {
                    lines = int.Parse(Console.ReadLine().Trim());
                    CorrectInput=true;
                }
                catch(FormatException)
                {
                    Console.WriteLine("Не верный формат. Введите пожалуйста количество черепах в груде.");

                }
            }

            List<string> startOrder = new List<string>();
            HashSet<string> uniqueNames = new HashSet<string>();

            Console.WriteLine("Введите начальный порядок груды.");
            for (int i = 0; i < lines; i++)
            {
                string turtle;
                bool validTurtle = false;

                do
                {
                    turtle = Console.ReadLine().Trim();

                    // Проверяем, что введенное имя является уникальным
                    if (uniqueNames.Contains(turtle))
                    {
                        Console.WriteLine($"Черепаха с именем '{turtle}' уже была введена. Пожалуйста, введите уникальное имя.");
                    }
                    else
                    {
                        validTurtle = true;
                        uniqueNames.Add(turtle); // Добавляем имя в множество уникальных имен
                    }
                } while (!validTurtle);

                startOrder.Add(turtle);
            }
            Console.WriteLine("Введите желаемый порядок груды.");
            List<string> endOrder = new List<string>();
            uniqueNames.Clear();
            for (int i = 0; i < lines; i++)
            {
                string turtle;
                bool validTurtle = false;

                do
                {
                    turtle = Console.ReadLine().Trim();

                    // Проверяем, что введенное имя является уникальным
                    if (!startOrder.Contains(turtle))
                    {
                        Console.WriteLine($"Черепаха с именем '{turtle}' отсутствует в начальной груде. Пожалуйста, введите имя снова.");
                    }
                    else if (uniqueNames.Contains(turtle))
                    {
                        Console.WriteLine($"Черепаха с именем '{turtle}' уже была введена. Пожалуйста, введите уникальное имя.");
                    }
                    else
                    {
                        validTurtle = true;
                        uniqueNames.Add(turtle); // Добавляем имя в множество уникальных имен
                    }
                } while (!validTurtle);

                endOrder.Add(turtle);
            }

            List<string> result = new List<string>();

            SortSequence(startOrder, endOrder, result);
            Console.WriteLine("\nОтвет:");
            if (result.Count > 0)
            {
                Console.WriteLine(string.Join(Environment.NewLine, result));
            }
            else 
            {
                Console.WriteLine("Желаемый порядок уже был достиг");
            }
            Console.WriteLine();
        }
    }
}
