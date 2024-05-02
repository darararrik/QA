using lab4_1_IDA_;
using System;

internal class Program
{
    private static void Main(string[] args)
    {
        int[,] array = new int[4, 4];
        int N = 0;
        bool parsedSuccessfully = false;
        Console.WriteLine("Введите количество тестовых блоков.");
        while (!parsedSuccessfully)
        {
            try
            {

                N = int.Parse(Console.ReadLine());
                parsedSuccessfully = true;
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: Введено не число. Пожалуйста, введите число.");
                Console.Write($"Повторите ввод элемента N: ");
            }
        
        }

        for (; N > 0; N--)
        {
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine($"Введите {i+1} строку");
                string[] str = Console.ReadLine().Split(' ');
                for (int j = 0; j < 4; j++)
                {
                    parsedSuccessfully = false;
                    while (!parsedSuccessfully)
                    {
                        try
                        {
                            int inputValue = int.Parse(str[j]);
                            if (inputValue < 0 || inputValue > 15)
                            {
                                Console.WriteLine($"Ошибка: Введенное число должно быть в диапазоне от 0 до 15. Пожалуйста, введите корректное число.");
                                Console.Write($"Повторите ввод элемента [{i + 1},{j + 1}]: ");
                                str[j] = Console.ReadLine(); // Обнуляем только некорректный элемент
                            }
                            else
                            {
                                array[i, j] = inputValue;
                                parsedSuccessfully = true;
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Ошибка: Введено не число. Пожалуйста, введите число.");
                            Console.Write($"Повторите ввод элемента [{i + 1},{j + 1}]: ");
                            str[j] = Console.ReadLine(); // Обнуляем только некорректный элемент
                        }
                    }


                }
              
            }
            Puzzle initialPuzzle = new Puzzle(array);
            if (initialPuzzle.IsSolvable(array))
            {
                List<Puzzle> solutionPath = initialPuzzle.SolvePuzzleIDAStar(initialPuzzle);
                if (solutionPath != null)
                {

                    Puzzle.PrintSolution(solutionPath);
                }

            }
            else { Console.WriteLine("This puzzle is not solvable"); }





        }

    }
}
/*

       int[,] initialBoard = {
               {2, 3, 4, 0},
               {1, 5, 7, 8},
               {9, 6, 10, 12},
               {13, 14, 11, 15}
           };

       int[,] initialBoard = {
               {13, 1, 2, 4},
               {5, 0, 3, 7},
               {9, 6, 10, 12},
               {15, 8, 11, 14}
           };      */