using System.Drawing;

internal class Program
{
    class Puzzle
    {
        private static int[,] _directions = { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };
        private static int[,] _goal = {
            { 1, 2, 3, 4 },
            { 5, 6, 7, 8 },
            { 9, 10, 11, 12 },
            { 13, 14, 15, 0 } };
        public int[,] Board;
        public Puzzle(int[,] board) => this.Board = board;
        public bool IsGoal()
        {
            // Проверка, является ли текущее состояние РЕШЕНИЕМ
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (Board[i, j] != _goal[i, j])
                        return false;
                }
            }
            return true;
        }
        public List<Puzzle> GenerateMoves(Puzzle puzzle)
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
        public bool IsSolvable() => CountInversions() % 2 == 0 ? true : false;
        public int CountInversions()
        {
            int СountInversions = 0;
            int[] array = new int[16];
            int index = 0;
            for (int i = 0;i < 4;i++)
            {
                for (int j = 0;j < 4;j++)
                {
                    array[index] = Board[i,j];
                    index++;
                }
            }
            for (int i = 0; i < 16 - 1; i++)
            {
                for (int j = i + 1; j < 16; j++)
                {
                    if (array[i] != 0 && array[j] != 0 && array[i] > array[j])
                    {
                        СountInversions++;
                    }
                }
            }
            return СountInversions;
        }
    }
    private static void Main(string[] args)
    {
        int[,] initialBoard = {
            { 1, 2, 3, 4 },
            { 5, 6, 7, 8 },
            { 9, 10, 11, 12 },
            { 13, 0, 14, 15 }
        };
        Puzzle puzzle = new Puzzle(initialBoard);
        Console.WriteLine(puzzle.IsSolvable());

    }

}
   