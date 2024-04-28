using System;
using System.Collections.Generic;

class Puzzle
{
    public int[,] Board { get; set; }

    public Puzzle(int[,] board)
    {
        Board = board;
    }

    public bool IsGoal()
    {
        // Проверка, является ли текущее состояние решением
        int[,] goal = {
            { 1, 2, 3, 4 },
            { 5, 6, 7, 8 },
            { 9, 10, 11, 12 },
            { 13, 14, 15, 0 }
        };

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (Board[i, j] != goal[i, j])
                    return false;
            }
        }

        return true;
    }

    public int Heuristic()
    {
        // Пример эвристической функции - возвращаем количество неправильных плиток
        int count = 0;
        int[,] goal = {
            { 1, 2, 3, 4 },
            { 5, 6, 7, 8 },
            { 9, 10, 11, 12 },
            { 13, 14, 15, 0 }
        };

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (Board[i, j] != goal[i, j])
                    count++;
            }
        }

        return count;
    }
}

class Program
{
    static List<Puzzle> SolvePuzzle(Puzzle puzzle, HashSet<string> visited)
    {
        if (puzzle.IsGoal())
        {
            // Если текущее состояние решение, возвращаем его
            return new List<Puzzle> { puzzle };
        }

        // Если текущее состояние не решение, пробуем все возможные ходы
        List<Puzzle> solution = null;
        List<Puzzle> moves = GenerateMoves(puzzle);

        foreach (var move in moves)
        {
            // Проверяем, было ли уже посещено это состояние
            if (!visited.Contains(ToString(move)))
            {
                visited.Add(ToString(move));
                solution = SolvePuzzle(move, visited);
                if (solution != null)
                {
                    solution.Insert(0, puzzle);
                    break;
                }
            }
        }

        return solution;
    }

    static List<Puzzle> GenerateMoves(Puzzle puzzle)
    {
        List<Puzzle> moves = new List<Puzzle>();
        int emptyRow = -1, emptyCol = -1;

        // Находим позицию пустой клетки
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (puzzle.Board[i, j] == 0)
                {
                    emptyRow = i;
                    emptyCol = j;
                    break;
                }
            }
            if (emptyRow != -1) break;
        }

        // Генерируем ходы
        int[,] directions = { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };
        for (int i = 0; i < 4; i++)
        {
            int newRow = emptyRow + directions[i, 0];
            int newCol = emptyCol + directions[i, 1];

            if (newRow >= 0 && newRow < 4 && newCol >= 0 && newCol < 4)
            {
                int[,] newBoard = (int[,])puzzle.Board.Clone();
                newBoard[emptyRow, emptyCol] = newBoard[newRow, newCol];
                newBoard[newRow, newCol] = 0;
                moves.Add(new Puzzle(newBoard));
            }
        }

        return moves;
    }

    static string ToString(Puzzle puzzle)
    {
        // Преобразуем состояние доски в строку для хранения в HashSet
        string str = "";
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                str += puzzle.Board[i, j].ToString();
            }
        }
        return str;
    }

    static void Main(string[] args)
    {
        int[,] initialBoard = {
            { 1, 2, 3, 4 },
            { 5, 6, 7, 8 },
            { 9, 10, 11, 12 },
            { 13, 0, 14, 15 }
        };

        Puzzle initialPuzzle = new Puzzle(initialBoard);
        HashSet<string> visited = new HashSet<string>();

        List<Puzzle> solution = SolvePuzzle(initialPuzzle, visited);

        if (solution != null)
        {
            Console.WriteLine("Решение найдено:");
            foreach (var puzzle in solution)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        Console.Write(puzzle.Board[i, j] + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("Решение не найдено.");
        }
    }
}
