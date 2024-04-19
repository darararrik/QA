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
            int n, m;
            if (sizes.Length != 2 || !int.TryParse(sizes[0], out n) || !int.TryParse(sizes[1], out m))
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите два целых числа через пробел.");
                continue;
            }
            if (n < 0 || m < 0)
            {
                Console.WriteLine("Размеры поля не могут быть отрицательными числами.");
                continue;
            }
            if (n == 0 && m == 0)
                break;

            Field field = new Field(n, m);
            Console.WriteLine($"Введите строки поля #{fieldNumber} (используйте '*' для обозначения мины и '.' для обозначения пустой клетки):");
            field.FillField();
            field.FinalField(fieldNumber);
           
            fieldNumber++;
        }


    }
}