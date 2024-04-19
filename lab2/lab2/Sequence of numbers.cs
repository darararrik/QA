using System;

namespace lab2
{
    internal class Sequence_of_numbers
    {
        private int[] numbers;

        public Sequence_of_numbers()
        {
            Console.WriteLine("Введите последовательность чисел через пробел:");
            string[] input = Console.ReadLine().Split(' ');

            numbers = Array.ConvertAll(input, int.TryParse);
            Console.WriteLine(JollyJumpersCheck());
        }

        public string JollyJumpersCheck()
        {
            int n = numbers.Length;
            bool[] visited = new bool[n];

            for (int i = 0; i < n - 1; i++)
            {
                int diff = Math.Abs(numbers[i] - numbers[i + 1]);
                if (diff < 1 || diff >= n || visited[diff]) 
                    return "NO";
                visited[diff] = true;
            }

            for (int i = 1; i < n; i++)
            {
                if (!visited[i])
                    return "NO";
            }

            return "YES";
        }
    }
}
