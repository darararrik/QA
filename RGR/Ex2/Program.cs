

namespace Ex2
{
    public class Program
    {
        public static void Main()
        {
            string query = "python AND (program OR code)";
            //Пример условного списка документов в которых есть слово(ключ) и лист с номерами документов
            Dictionary<string, List<int>> Data = new()
        {
            { "python", new List<int> { 1, 2, 3 } },
            { "program", new List<int> { 2, 3, 4 } },
            { "code", new List<int> { 3, 4, 5 } }
        };
            Searcher ob = new(Data, query);
            List<int> result = ob.ExecuteQuery();


            Console.WriteLine("Результаты запроса:");
            foreach (int docId in result)
            {
                Console.WriteLine($"Документ {docId}");
            }
        }


    }
}