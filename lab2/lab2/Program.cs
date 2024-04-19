using lab2;
class Program
{
    static void Main(string[] args)
    {
      
        Console.WriteLine("Введите строку:");
        string input = Console.ReadLine().ToUpper();

        // Сдвигаем строку
        string shiftedString = KeyboardShift.ShiftString(input);

        Console.WriteLine($"Сдвинутая строка: {shiftedString}");
    }

    
}
