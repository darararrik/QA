using lab1;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        int fieldNumber = 1;


        while (true)
        {
            Console.WriteLine("Введите размер поля через пробел (N M)");
            Console.WriteLine("Если ввести 0 0, программа завершится");
            string[] sizes = Console.ReadLine().Split(' ');
            int n = Convert.ToInt32(sizes[0]);
            int m = Convert.ToInt32(sizes[1]); 
            if (n < 0 || m < 0) 
            {
                Console.WriteLine("Введены не правильные значения.");
                continue;
            }
            if (n == 0 && m == 0)
                break;

            Field field = new Field(n, m);
            field.FillField();
            field.FinalField(fieldNumber);
           
            fieldNumber++;
        }


    }
}