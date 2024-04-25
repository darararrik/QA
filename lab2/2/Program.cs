using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static int FindPos(List<int> seqNum, int num)
    {
        for (int i = 0; i < seqNum.Count; i++)
        {
            if (seqNum[i] == num)
                return i;
        }
        return -1;
    }

    static void MoveTop(List<int> array, int j)
    {
        int temp = array[j];
        for (int i = j; i > 0; i--)
        {
            array[i] = array[i - 1];
        }
        array[0] = temp;
    }

    static void SortSequence(List<string> startOrder, List<string> endOrder, List<string> result)
    {
        int n = startOrder.Count;
        Dictionary<string, int> endDict = new Dictionary<string, int>();
        for (int i = 0; i < endOrder.Count; i++)
        {
            endDict[endOrder[i]] = i;
        }

        List<int> seqNum = new List<int>();
        foreach (var s in startOrder)
        {
            seqNum.Add(endDict[s]);
        }
        
        for (int curr = n - 1; curr > 0; curr--)
        {//Если индекс текущего числа(curr) меньше чем индекс предыдущего мы перемещаем предыдущее число в начало
            int posCurr = FindPos(seqNum, curr);
            int posNext = FindPos(seqNum, curr - 1);
            if (posCurr < posNext)
            {
                MoveTop(seqNum, posNext);
                result.Add(endOrder[curr - 1]);
      
            }

           

        }
      



    }

    static void Main(string[] args)
    {
        StreamReader reader = new StreamReader("input.txt");
        int numOfTests = int.Parse(reader.ReadLine());

        for (int t = 0; t < numOfTests; t++)
        {
            int lines = int.Parse(reader.ReadLine());
            List<string> startOrder = new List<string>();
            for (int i = 0; i < lines; i++)
            {
                startOrder.Add(reader.ReadLine().Trim());
            }

            List<string> endOrder = new List<string>();
            for (int i = 0; i < lines; i++)
            {
                endOrder.Add(reader.ReadLine().Trim());
            }

            List<string> result = new List<string>();

     
            SortSequence(startOrder, endOrder, result);
            Console.WriteLine(string.Join(Environment.NewLine, result));
            Console.WriteLine();
        }
        reader.Close();
    }
}
