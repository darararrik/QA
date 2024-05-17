namespace Ex4
{
    public class Program
    {
        public static void Main()
        {
            // Пример использования весовой функции
            List<Document> documents =
            [
                new Document { Title = "Long Document", Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum." },
            new Document { Title = "Short Document", Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry." },
            new Document { Title = "Medium Document", Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book." }
            ];

            SearchDocument documentSearch = new();

            // Вычисляем минимальное и максимальное количество слов среди документов
            documentSearch.CalculateMinMaxWordCount(documents);
            // 1 - любишь длинные доки , 0 - не любишь длинные доки
            double relevanceLongDocument = documentSearch.CalculateRelevance(documents[0], preference: 1);
            Console.WriteLine($"Relevance of long document: {relevanceLongDocument}");

            double relevanceShortDocument = documentSearch.CalculateRelevance(documents[1], preference: 0.76);
            Console.WriteLine($"Relevance of short document: {relevanceShortDocument}");

            double relevanceMediumDocument = documentSearch.CalculateRelevance(documents[2], preference: 0.5);
            Console.WriteLine($"Relevance of medium document: {relevanceMediumDocument}");
        }
    }
}