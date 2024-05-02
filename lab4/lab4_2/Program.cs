using System;

class Program
{
    static int[,] DP;
    static int[] L;
    static int[] bad;

    static int FindDP(int k, int n)
    {
        if (DP[k, n] != -1)
            return DP[k, n];

        if (n < 3 * k)
        {
            DP[k, n] = int.MaxValue;
        }
        else if (k == 0)
        {
            DP[k, n] = 0;
        }
        else
        {
            DP[k, n] = Math.Min(FindDP(k, n - 1), FindDP(k - 1, n - 2) + bad[n]);
        }

        return DP[k, n];
    }

    static void Main()
    {
        int t = int.Parse(Console.ReadLine());
        while (t-- > 0)
        {
            string[] inputs = Console.ReadLine().Split();
            int k = int.Parse(inputs[0]);
            int n = int.Parse(inputs[1]);

            L = new int[n + 1];
            bad = new int[n + 1];
            DP = new int[k + 9, n + 1];

            string[] lengths = Console.ReadLine().Split();
            for (int i = 1; i <= n; i++)
            {
                L[i] = int.Parse(lengths[i - 1]);
            }

            Array.Sort(L, 1, n, Comparer<int>.Create((a, b) => b.CompareTo(a)));

            for (int i = 2; i <= n; i++)
            {
                bad[i] = (L[i] - L[i - 1]) * (L[i] - L[i - 1]);
            }

            for (int i = 0; i <= k + 8; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    DP[i, j] = -1;
                }
            }

            Console.WriteLine(FindDP(k + 8, n));
            Console.ReadLine();

        }
    }
}
