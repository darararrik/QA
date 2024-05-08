using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4_1_IDA_
{
    internal class Puzzle
    {
        #region varibles
        private int[,] board;
        private int[,] pos = new int[17, 2];
        static int[,] goalState = {
            {1, 2, 3, 4},
            {5, 6, 7, 8},
            {9, 10, 11, 12},
            {13, 14, 15, 0}
        };
        char step ;
        #endregion

        public Puzzle(int[,] initialBoard)
        {
            board = initialBoard;
            InitializePositions();
        }
        #region methods
        private void InitializePositions()
        {
            int i, k, j;
            for (i = 0, k = 0; i < 4; i++)
            {
                for (j = 0; j < 4; j++)
                {
                    pos[++k, 0] = i; // Строка
                    pos[k,    1] = j; // Столбец
                }
            }
        }

        public bool IsSolvable(int[,] puzzle)
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
        public bool IsGoalState()
        {
           

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (board[i, j] != goalState[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public int Heuristic(int[,] board)
        {
            int distance = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int k = board[i, j];
                    if (k != 0)
                    {
                        distance += Math.Abs(i - pos[k,0]) + Math.Abs(j - pos[k, 1]);
                    }
                }
            }
          
            return distance;
        }
        public List<Puzzle> GetNextStates()// ШАги 
        {
            List<Puzzle> nextStates = new List<Puzzle>();

            // Поиск пустой плитки
            int emptyTileRow = -1;
            int emptyTileCol = -1;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (board[i, j] == 0)
                    {
                        emptyTileRow = i;
                        emptyTileCol = j;
                        break;
                    }
                }
                if (emptyTileRow != -1)
                    break;
            }

            // Генерация следующих состояний
            int[] dRow = { -1, 1, 0, 0 }; // Сдвиги для строк
            int[] dCol = { 0, 0, -1, 1 }; // Сдвиги для столбцов
            for (int k = 0; k < 4; k++)
            {

                int newRow = emptyTileRow + dRow[k];
                int newCol = emptyTileCol + dCol[k];


          
                if (newRow >= 0 && newRow < 4 && newCol >= 0 && newCol < 4)
                {
                    int[,] nextState = CloneBoard(board);
                    nextState[emptyTileRow, emptyTileCol] = nextState[newRow, newCol];
                    nextState[newRow, newCol] = 0;
                    var newPuzzle= new Puzzle(nextState);
                    nextStates.Add(newPuzzle);
                    CharStep(newPuzzle, dRow, dCol, k);
                }
            }

            return nextStates;
        }
        public void CharStep(Puzzle puzzle, int[] dRow, int[] dCol,int k)
        {
            if (dRow[k] == -1) puzzle.step = 'U'; // Вверх
            else if (dRow[k] == 1) puzzle.step = 'D'; // Вниз
            else if (dCol[k] == -1) puzzle.step = 'L'; // Влево
            else if (dCol[k] == 1) puzzle.step = 'R'; // Вправо

        }
        private int[,] CloneBoard(int[,] original)
        {
            int[,] clone = new int[4, 4];
            Array.Copy(original, clone, original.Length);
            return clone;
        }

        private Puzzle Search(IDAState state, int threshold, ref int nextThreshold, List<Puzzle> solutionPath)
        {


            Puzzle currentPuzzle = state.Puzzle;

            int f = state.Depth + currentPuzzle.Heuristic(currentPuzzle.board);
            //h - это сколько шагов надо сделать из данноо узла 
            //f - сумма шагов за все, скок сделал(g) + скок надо(h) до цели


            if (f > threshold)
            {




                nextThreshold = Math.Min(nextThreshold, f); // Обновляем порог для следующей итерации



                return null; // Возвращаем null, чтобы указать на то, что порог был превышен
            }

            if (currentPuzzle.IsGoalState())
            {
                solutionPath.Add(currentPuzzle); // Добавляем текущее состояние в путь к решению
                return currentPuzzle; // Возвращаем текущее состояние как решение
            }

            List<Puzzle> nextPuzzles = currentPuzzle.GetNextStates(); // Получаем следующие возможные состояния

            foreach (Puzzle nextPuzzle in nextPuzzles)
            {

                if (!solutionPath.Contains(nextPuzzle))
                {
                
                    IDAState nextState = new IDAState
                    {
                        Puzzle = nextPuzzle,
                        Depth = state.Depth + 1 // Depth количество шагов сделанных в ветке
                    };

                    Puzzle solution = Search(nextState, threshold, ref nextThreshold, solutionPath); // Рекурсивный вызов для следующего состояния

                    if (solution != null)
                    {
                        solutionPath.Insert(0, currentPuzzle); // Добавляем текущее состояние в начало пути к решению
                        return solution; // Возвращаем решение
                    }
                }
            }

            return null; // Возвращаем null, если решение не найдено
        }
       
        public List<Puzzle> SolvePuzzleIDAStar(Puzzle initialPuzzle)
        {
            //Мы начинаем с установки начального порога равным стоимости эвристической
            //функции для начального состояния initialPuzzle
            int threshold = initialPuzzle.Heuristic(initialPuzzle.board);
            List<Puzzle> solutionPath = new List<Puzzle>();

            while (true)
            {
                int nextThreshold = int.MaxValue;
                IDAState initialState = new IDAState
                {
                    Puzzle = initialPuzzle,
                    Depth = 0
                };

                Puzzle solution = Search(initialState, threshold, ref nextThreshold, solutionPath);

                if (solution != null)
                {
                    return solutionPath; // Возвращаем путь к решению
                }

                if (nextThreshold == int.MaxValue)
                {
                    return null; // Решение не найдено
                }

                threshold = nextThreshold;
            }
        }
        public static void PrintSolution(List<Puzzle> solutionPath)
        {
            Console.WriteLine("Путь к решению:");
            for (int i = 0; i < solutionPath.Count; i++)
            {
                Puzzle puzzle = solutionPath[i];
                Console.Write(puzzle.step);

            }
            Console.WriteLine();
        }
        #endregion
    }
}
