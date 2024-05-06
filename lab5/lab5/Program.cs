using System;
using System.Collections.Generic;

class Program
{
    static List<(int, int, int)> FindMinSquares(int N)
    {
        int[] dp = new int[N + 1];
        List<(int, int, int)> squares = new List<(int, int, int)>();

        for (int i = 1; i <= N; i++)
        {
            dp[i] = i * i;
            for (int j = 1; j < i; j++)
            {
                if (j * j > i)
                    break;
                dp[i] = Math.Min(dp[i], 1 + dp[i - j * j]);
            }
        }

        while (N > 0)
        {
            int side = (int)Math.Sqrt(dp[N] - 1) + 1;
            squares.Add((1, 1, side));
            N -= side * side;
        }

        return squares;
    }

    static void Main(string[] args)
    {
        int T = int.Parse(Console.ReadLine());
        for (int t = 0; t < T; t++)
        {
            int N = int.Parse(Console.ReadLine());
            List<(int, int, int)> result = FindMinSquares(N);
            Console.WriteLine(result.Count);
            foreach (var square in result)
            {
                Console.WriteLine($"{square.Item1} {square.Item2} {square.Item3}");
            }
        }
    }
}
