
using FinalProject;

internal class Program
{
   static string[] texts = ["Python and C# are popular programming languages. Python is known for its simplicity and readability, making it ideal for beginners and rapid development. C# is a powerful, statically-typed language developed by Microsoft,widely used for building Windows applications and games with Unity. Both languages have strong community support and extensive libraries.",
            "While Python and C# are distinct from C++, they each have their strengths. Python code is easy to write and P.HD.DD understand, making it perfect for rapid development and testing. C# is used widely for enterprise-level applications and game development. Developers using Apple devices can find a list of useful programming tools, including code editors and searcher utilities, for both languages. Effective test cases are crucial in ensuring reliable program performance.",
        "Python code is versatile and often used in data science and web development, while C# excels in creating robust applications and games. Although different from C++, both Python and C# offer unique advantages. On Apple platforms, programmers can use a variety of tools to write, test, and search for code. A well-maintained list of resources and searcher utilities helps developers efficiently manage their projects and ensure high-quality program output."
         ,"Python and C# serve different purposes P.HD. DD in the programming world. Python is known for its easy-to-write code, making it a favorite for quick testing and scripting. C#, on the other hand, is preferred for developing complex applications and games, distinct from the low-level programming of C++. On Apple devices, a comprehensive list of development tools and searcher functions is available to aid in coding and program testing, ensuring high-quality software development.",
           "Programmers often debate the merits of Python and C# compared to languages like C++. Python's straightforward syntax makes coding and testing easier, while C# provides powerful features for application development. Developers working on Apple devices can use specialized code editors and searcher tools to enhance productivity. Maintaining a curated list of libraries and tools is essential for efficient programming and thorough testing."];
    static List<Document> documents = [new Document("Python and C#1", texts[0]), new Document("Python and C#2", texts[1]), new Document("Python and C#3", texts[2]), new Document("Python and C#4", texts[3]), new Document("Python and C#5", texts[4])];


    private static void Main()
    {

        string[] words = Searcher.SeparateWords(documents[0].Content);
        foreach (string word in words)
        {
            Console.WriteLine(word);
        }


        #region Ex3
        //Упражнение 3
        string query = "C++";
        List<Document> lists = Searcher.GetRows(documents,query);
        foreach(var list in lists)
        {
            Console.WriteLine(list.Adress);
        }    

        #endregion



        //       #region Ex1
        //        //Упражнение 1
        //        string sampleText = "My phone number is 617-555-1212.";
        //        string[] words = Crawler.SeparateWords(sampleText);
        //        foreach (string word in words)
        //        {Console.WriteLine(word);}
        //        #endregion

        //        #region Ex2
        //        //Упражнение 2
        //        string query = "list AND apple";
        //        //Пример условного списка документов в которых есть слово(ключ) и лист с номерами документов
        //        //Или условная БД
        //        Dictionary<string, List<int>> Data = new()
        //        {
        //            { "C++", new List<int> { 1, 2, 3,4 } },
        //            { "list", new List<int> { 2, 3 } },
        //            { "apple", new List<int> { 3, 4, 5 } }
        //        };

        //        List<int> res = Crawler.ExecuteQuery(Data, query);


        //        Console.WriteLine("Результаты запроса:");
        //        foreach (int docId in res)
        //        {
        //            Console.WriteLine($"Документ {docId}");
        //        }
        //        #endregion


        //        #region Ex4
        //        //Упражнение 4

        //         //Пример использования весовой функции
        //        List<Document> documents =
        //        [
        //            new Document { Title = "Long Document", Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. " +
        //            "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and " +
        //            "scrambled it to make a type specimen book. " +
        //            "It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. " +
        //            "It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, " +
        //            "and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum." },


        //            new Document { Title = "Short Document", Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry." },


        //            new Document { Title = "Medium Document", Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. " +
        //            "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type " +
        //            "and scrambled it to make a type specimen book." }
        //        ];

        //        // Вычисляем минимальное и максимальное количество слов среди документов
        //        Crawler.CalculateMinMaxWordCount(documents);
        //        // 1 - любишь длинные доки , 0 - не любишь длинные доки
        //        double relevanceLongDocument = Crawler.CalculateRelevance(documents[0], preference: 0);
        //        Console.WriteLine($"Relevance of long searcher: {relevanceLongDocument}");

        //        double relevanceShortDocument = Crawler.CalculateRelevance(documents[1], preference: 0);
        //        Console.WriteLine($"Relevance of short searcher: {relevanceShortDocument}");

        //        double relevanceMediumDocument = Crawler.CalculateRelevance(documents[2], preference: 0);
        //        Console.WriteLine($"Relevance of medium searcher: {relevanceMediumDocument}");
        //        #endregion

        //        #region Ex5
        //        //Упражнение 5

        //        documents = new List<Document> {
        //            new Document{
        //            Title = "Art of Painting",
        //            Content = "Description of painting techniques and styles, history of art development."
        //        }};
        //        List<string> keywords = new List<string> { "art", "painting" };


        //        double frequency = Crawler.CalculateFrequency(documents, keywords);


        //        if (frequency == -1)
        //        {
        //            return;
        //        }
        //        else
        //        {
        //            Console.WriteLine($"Frequency of keywords in the document: {frequency}");
        //        }
        //        #endregion

        //        #region Ex6
        //        //Упражнение 6

        //        documents = new List<Document> {
        //    new Document {
        //        Title = "Programming Resources",
        //        Content = "Resources for learning programming.",
        //        ExternalLinks = new List<string> { "https://example.com/programming_tutorials", "https://example.com/code_samples" }
        //    },
        //    new Document {
        //        Title = "Software Development Blog",
        //        Content = "Articles and insights on software development.",
        //        ExternalLinks = new List<string> { "https://example.com/software_blog", "https://example.com/programming_guides" }
        //    }
        //};


        //        string[] keywords_for_ex6 = { "programming", "development" };

        //        List<Document> results = Crawler.Search(documents, keywords_for_ex6);

        //        if (results.Count() == 0)
        //        {
        //            return;
        //        }
        //        else
        //        {
        //            Console.WriteLine("Search Results:");
        //            foreach (var result in results)
        //            {
        //                Console.WriteLine($"Title: {result.Title}");
        //            }
        //        }
        //        #endregion
        //        //Упражнени 7
        //        NeuralNetwork nn = new NeuralNetwork();

        //        //Console.WriteLine(nn.hiddenIds[0]);
        //        double[] c;
        //        nn.TrainQery(new string[] { "World", "Bank" }, new string[] { "River", "World Bank", "Earth" }, new string[] { "World Bank", "Earth" }, new double[] { 5.0, 2.0 });
        //        c = nn.GetResult(new string[] { "World", "Bank" }, new string[] { "World Bank", "River", "Earth" });
        //        foreach (double item in c)
        //        {
        //            Console.WriteLine(item);
        //        }
    }
}