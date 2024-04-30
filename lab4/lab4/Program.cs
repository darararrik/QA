using System;
using System.Collections.Generic;

class FifteenPuzzleSolver
{
    static int[,] goalState = { {1, 2, 3, 4},
                                {5, 6, 7, 8},
                                {9, 10, 11, 12},
                                {13, 14, 15, 0} };

    static Dictionary<char, Tuple<int, int>> moves = new Dictionary<char, Tuple<int, int>>
    {
        {'R', new Tuple<int, int>(0, 1)},
        {'L', new Tuple<int, int>(0, -1)},
        {'U', new Tuple<int, int>(-1, 0)},
        {'D', new Tuple<int, int>(1, 0)}
    };

    static bool IsSolvable(int[,] puzzle)
    {
        int sum = 0, row = 0;

        for (int i = 0; i < 16; i++)
        {
            if (puzzle[i / 4, i % 4] == 0)
            {
                row = i / 4 + 1;
                continue;
            }

            for (int j = i + 1; j < 16; j++)
            {
                if (puzzle[j / 4, j % 4] < puzzle[i / 4, i % 4])
                {
                    if (puzzle[j / 4, j % 4] != 0)
                    {
                        sum++;
                    }
                }
            }
        }

        return (1 - (sum + row) % 2) == 1;
    }


    static bool IsGoalState(int[,] puzzle)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (puzzle[i, j] != goalState[i, j])
                {
                    return false;
                }
            }
        }
        return true;
    }

    static Tuple<int, int> FindBlankPosition(int[,] puzzle)
    {
        Tuple<int, int> position = new Tuple<int, int>(-1, -1);
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (puzzle[i, j] == 0)
                {
                    position = new Tuple<int, int>(i, j);
                    break;
                }
            }
        }
        return position;
    }

    static bool IsValidMove(int row, int col)
    {
        return row >= 0 && row < 4 && col >= 0 && col < 4;
    }

    static string SolvePuzzle(int[,] puzzle)
    {
        if (!IsSolvable(puzzle))
        {
            return "This puzzle is not solvable";
        }

        HashSet<int[,]> visited = new HashSet<int[,]>();
        Queue<Tuple<int[,], string>> queue = new Queue<Tuple<int[,], string>>();
        queue.Enqueue(new Tuple<int[,], string>(puzzle, ""));

        while (queue.Count > 0)
        {
            Tuple<int[,], string> current = queue.Dequeue();
            int[,] currentPuzzle = current.Item1;
            string currentMoves = current.Item2;

            if (IsGoalState(currentPuzzle))
            {
                return currentMoves;
            }

            Tuple<int, int> blankPosition = FindBlankPosition(currentPuzzle);
            int blankRow = blankPosition.Item1;
            int blankCol = blankPosition.Item2;

            foreach (var move in moves)
            {
                int newRow = blankRow + move.Value.Item1;
                int newCol = blankCol + move.Value.Item2;

                if (IsValidMove(newRow, newCol))
                {
                    int[,] newPuzzle = (int[,])currentPuzzle.Clone();
                    newPuzzle[blankRow, blankCol] = newPuzzle[newRow, newCol];
                    newPuzzle[newRow, newCol] = 0;

                    if (!visited.Contains(newPuzzle))
                    {
                        visited.Add(newPuzzle);
                        queue.Enqueue(new Tuple<int[,], string>(newPuzzle, currentMoves + move.Key));
                    }
                }
            }
        }

        return "This puzzle is not solvable";
    }

    static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            int[,] puzzle = new int[4, 4];
            for (int j = 0; j < 4; j++)
            {
                string[] row = Console.ReadLine().Split();
                for (int k = 0; k < 4; k++)
                {
                    puzzle[j, k] = int.Parse(row[k]);
                }
            }

            string solution = SolvePuzzle(puzzle);
            Console.WriteLine(solution);
        }
    }
}
