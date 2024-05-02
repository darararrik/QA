using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4_1_IDA_
{
    internal struct IDAState
    {
        public Puzzle Puzzle;//Сам пазл
        public int Depth;// Depth количество шагов сделанных в ветке

        public IDAState(Puzzle puzzle, int cost, int depth, string steps)
        {
            Puzzle = puzzle;
            Depth = depth;
        }
        // Другие параметры, которые могут понадобиться для работы алгоритма IDA*
    }
}
