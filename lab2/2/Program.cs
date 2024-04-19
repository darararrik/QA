using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Считываем количество тестов
        int tests = int.Parse(Console.ReadLine());

        // Цикл по каждому тесту
        for (int t = 0; t < tests; t++)
        {
            Console.WriteLine($"Тест {t + 1}:");
            Console.WriteLine("Введите количество черепах в груде:");
            // Считываем количество черепах в груде для текущего теста
            int turtlesCount = int.Parse(Console.ReadLine());

            List<string> initialOrder = new List<string>();
            Console.WriteLine("Введите начальный порядок черепах:");
            // Считываем имена черепах в начальном порядке
            for (int i = 0; i < turtlesCount; i++)
            {
                initialOrder.Add(Console.ReadLine());
            }

            List<string> desiredOrder = new List<string>();
            Console.WriteLine("Введите желаемый порядок черепах:");
            // Считываем имена черепах в желаемом порядке
            for (int i = 0; i < turtlesCount; i++)
            {
                desiredOrder.Add(Console.ReadLine());
            }

            // Находим и выводим последовательность операций для текущего теста
            List<string> operations = FindOperations(initialOrder, desiredOrder);
            Console.WriteLine("Последовательность операций для преобразования:");
            foreach (var op in operations)
            {
                Console.WriteLine(op);
            }
            Console.WriteLine();
        }
    }

    // Функция для нахождения последовательности операций
    static List<string> FindOperations(List<string> initialOrder, List<string> desiredOrder)
    {
        List<string> operations = new List<string>();

        // Словарь для хранения позиций черепах в желаемом порядке
        Dictionary<string, int> desiredPositions = new Dictionary<string, int>();
        for (int i = 0; i < desiredOrder.Count; i++)
        {
            desiredPositions[desiredOrder[i]] = i;
        }

        // Проходимся по начальному порядку
        for (int i = 0; i < initialOrder.Count; i++)
        {
            string turtle = initialOrder[i];
            int desiredIndex = desiredPositions[turtle]; // Находим позицию черепахи в желаемом порядке
            while (i != desiredIndex)
            {
                string movedTurtle = initialOrder[i]; // Получаем черепаху, которую нужно переместить
                operations.Add(movedTurtle); // Добавляем её в последовательность операций
                initialOrder.RemoveAt(i); // Удаляем черепаху с текущей позиции
                initialOrder.Insert(desiredIndex, movedTurtle); // Вставляем её на нужную позицию
                i = desiredIndex; // Обновляем текущую позицию
                desiredIndex = desiredPositions[movedTurtle]; // Обновляем желаемую позицию для следующей итерации
            }
        }

        return operations;
    }
}
