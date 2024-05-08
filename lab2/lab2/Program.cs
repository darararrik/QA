using lab2;
class Program
{
    static void Main()
    {
      
        Console.WriteLine("Введите строку:");
        string input = Console.ReadLine().ToUpper();

        // Сдвигаем строку вызвав метод
        string shiftedString = KeyboardShift.ShiftString(input);

        Console.WriteLine($"Сдвинутая строка: {shiftedString}");
    }

    
}
