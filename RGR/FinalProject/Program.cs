
using FinalProject;
using System.Collections.Generic;

internal class Program
{
    private static void Main()
    {

        #region Ex1
        //Упражнение 1
        string sampleText = "My phone number is 617-555-1212.";
        string[] words = Crawler.SeparateWords(sampleText);
        foreach (string word in words)
        {Console.WriteLine(word);}
        #endregion

        #region Ex2
        //Упражнение 2
        string query = "list AND apple";
        //Пример условного списка документов в которых есть слово(ключ) и лист с номерами документов
        //Или условная БД
        Dictionary<string, List<int>> Data = new()
        {
            { "C++", new List<int> { 1, 2, 3,4 } },
            { "list", new List<int> { 2, 3 } },
            { "apple", new List<int> { 3, 4, 5 } }
        };

        List<int> res = Crawler.ExecuteQuery(Data, query);


        Console.WriteLine("Результаты запроса:");
        foreach (int docId in res)
        {
            Console.WriteLine($"Документ {docId}");
        }
        #endregion

        #region Ex3
        //Упражнение 3
        List<string> data = ["searcher test", "test", "this is a test", "test searcher"];
        query = "a test";

        List<string> exactMatches = Crawler.GetRows(data, query);

        Console.WriteLine("Точные совпадения:");
        foreach (string match in exactMatches)
        {
            Console.WriteLine(match);
        }
        #endregion

        #region Ex4
        //Упражнение 4

         //Пример использования весовой функции
        List<Document> documents =
        [
            new Document { Title = "Long Document", Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. " +
            "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and " +
            "scrambled it to make a type specimen book. " +
            "It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. " +
            "It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, " +
            "and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum." },


            new Document { Title = "Short Document", Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry." },


            new Document { Title = "Medium Document", Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. " +
            "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type " +
            "and scrambled it to make a type specimen book." }
        ];

        // Вычисляем минимальное и максимальное количество слов среди документов
        Crawler.CalculateMinMaxWordCount(documents);
        // 1 - любишь длинные доки , 0 - не любишь длинные доки
        double relevanceLongDocument = Crawler.CalculateRelevance(documents[0], preference: 0);
        Console.WriteLine($"Relevance of long searcher: {relevanceLongDocument}");

        double relevanceShortDocument = Crawler.CalculateRelevance(documents[1], preference: 0);
        Console.WriteLine($"Relevance of short searcher: {relevanceShortDocument}");

        double relevanceMediumDocument = Crawler.CalculateRelevance(documents[2], preference: 0);
        Console.WriteLine($"Relevance of medium searcher: {relevanceMediumDocument}");
        #endregion

        #region Ex5
        //Упражнение 5

        documents = new List<Document> {
            new Document{
            Title = "Art of Painting",
            Content = "Description of painting techniques and styles, history of art development."
        }};
        List<string> keywords = new List<string> { "art", "painting" };


        double frequency = Crawler.CalculateFrequency(documents, keywords);


        if (frequency == -1)
        {
            return;
        }
        else
        {
            Console.WriteLine($"Frequency of keywords in the document: {frequency}");
        }
        #endregion

        #region Ex6
        //Упражнение 6

        documents = new List<Document> {
    new Document {
        Title = "Programming Resources",
        Content = "Resources for learning programming.",
        ExternalLinks = new List<string> { "https://example.com/programming_tutorials", "https://example.com/code_samples" }
    },
    new Document {
        Title = "Software Development Blog",
        Content = "Articles and insights on software development.",
        ExternalLinks = new List<string> { "https://example.com/software_blog", "https://example.com/programming_guides" }
    }
};


        string[] keywords_for_ex6 = { "programming", "development" };

        List<Document> results = Crawler.Search(documents, keywords_for_ex6);

        if (results.Count() == 0)
        {
            return;
        }
        else
        {
            Console.WriteLine("Search Results:");
            foreach (var result in results)
            {
                Console.WriteLine($"Title: {result.Title}");
            }
        }
        #endregion
        //Упражнени 7
        NeuralNetwork nn = new NeuralNetwork();

        //Console.WriteLine(nn.hiddenIds[0]);
        double[] c;
        nn.TrainQery(new string[] { "World", "Bank" }, new string[] { "River", "World Bank", "Earth" }, new string[] { "World Bank", "Earth" }, new double[] { 5.0, 2.0 });
        c = nn.GetResult(new string[] { "World", "Bank" }, new string[] { "World Bank", "River", "Earth" });
        foreach (double item in c)
        {
            Console.WriteLine(item);
        }
    }
}