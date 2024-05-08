using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR3_2
{
    internal class Golomb
    {
        private int[] f;

        public Golomb(int n)
        {
            this.f = new int[n + 1];
            CalculateGolomb(n);
        }
        //https://en.wikipedia.org/wiki/Golomb_sequence алгоритм взят отсюда
        private void CalculateGolomb(int n)
        {
            f[1] = 1;
            for (int i = 2; i <= n; i++)
            {
                f[i] = 1 + f[i - f[f[i - 1]]];
            }
        }

        public int ShowGolomb(int n)
        {
            return f[n];
        }
    }
}
