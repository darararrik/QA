using System;
namespace LR3_2
{
    internal class Program
    {
        internal static int[] f;

        public static void Main()
        {
            string stroke;
            while ((stroke = Console.ReadLine()) != "Stop")
            {
                int n = int.Parse(stroke);

                if (f == null || f.Length <= n)
                    f = new int[n+1];

                var fn = new Golomb(n);
                Console.WriteLine(fn.ShowGolomb(n));
            }
        }
    }
}




