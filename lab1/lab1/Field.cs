using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Claims;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace lab1
{
    internal class Field
    {
        private char[,] field;
        private int n;
        private int m;
        public Field(int n, int m)
        {
            this.n = n;
            this.m = m;
            field = new char[n, m];
        }
        public void FillField()
        {
            for (int i = 0; i < n; i++)
            {
                string row = Console.ReadLine();

                while (!IsValidInput(row))
                {
                    Console.WriteLine("Ошибка ввода. Пожалуйста вводите только '*' или '.'");
                    row = Console.ReadLine();
                }

                for (int j = 0; j < m; j++)
                {
                    field[i, j] = row[j];
                }
            }
        }
        public void FinalField(int number)
        {
            StringBuilder res = new StringBuilder();
            res.AppendLine($"Field #{number}");
            for (int i = 0; i < n; i++)
            {

                for (int j = 0; j < m; j++)
                {
                    if (field[i, j] == '*')
                        res.Append('*');
                    else res.Append(CountField(i, j));
                }
                res.AppendLine();
            }
            Console.WriteLine();
            Console.WriteLine(res);
        }
        private int CountField(int x, int y)
        {
            int count = 0;
            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (i >= 0 && i < n && j >= 0 && j < m && field[i, j] == '*')
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        static bool IsValidInput(string input)
        {
            foreach (char c in input)
            {
                if (c != '*' && c != '.')
                {
                    return false;
                }
            }
            return true;
        }
    }

}