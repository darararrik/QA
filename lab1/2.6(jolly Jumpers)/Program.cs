using jolly_Jumpers;
internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Введите последовательность целых чисел через пробел:");
        string input = Console.ReadLine();
        if (JollyJumpers.IsJollyJumpers(input))
        {
            Console.WriteLine("Это jolly jumper.");
        }
        else
        {
            Console.WriteLine("Это не jolly jumper.");
        }

    }
}