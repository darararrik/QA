
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FinalProject
{
    internal static class Crawler
    {

        static string[] separators = { " ", ".", ",", ";", ":", "-", "!", "?", "...", "_", "/", "//" };
        #region Ex1

        //Упражнение 1
        public static string[] SeparateWords(string Text)
        {
            Regex splitter = new(@"\b\w+\b");
            string[] words = splitter.Matches(Text)
                            .Select(w => w.Value.ToLower())
                            .ToArray();

            return words;
        }
        #endregion

        #region Ex2


        public static List<int> ExecuteQuery(Dictionary<string, List<int>> Data, string Query)
        {
            string[] tokens = Query.Split(new char[] { ' ', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            Stack<List<int>> stack = [];
            Stack<string> operators = [];
            foreach (string token in tokens)
            {
                if (token == "AND" || token == "OR")
                {
                    while (operators.Count > 0 && Priority(operators.Peek()) >= Priority(token))
                        ExecuteOperation(stack, operators, Data,tokens);
                    operators.Push(token);
                }
                else
                    stack.Push(Data[token]);
            }
            while (operators.Count > 0)
                ExecuteOperation(stack, operators, Data,tokens);
            return stack.Peek();
        }

        private static int Priority(string op)
        {
            return op switch
            {
                "AND" => 2,
                "OR" => 1,
                _ => 0
            };
        }
        private static void ExecuteOperation(Stack<List<int>> stack, Stack<string> operators, Dictionary<string, List<int>> Data, string[] tokens)
        {
            string op = operators.Pop();
            List<int> operand2 = stack.Pop();
            List<int> operand1 = stack.Pop();

            if (op == "AND")
            {
                stack.Push(Intersect(operand1, operand2));
            }
            else if (op == "OR")
            {
                stack.Push(Union(operand1, operand2, Data,tokens));
            }
        }

        private static List<int> Intersect(List<int> list1, List<int> list2)
        {
            List<int> result = [];
            foreach (int item in list1)
            {
                if (list2.Contains(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        private static List<int> Union(List<int> list1, List<int> list2, Dictionary<string, List<int>> Data, string[] tokens)
        {
            List<int> list3 = Data[tokens[0]];//лежат все доки где есть питон

            List<int> result = new(list1);
            foreach (int item2 in list3)
            {
                if (!list1.Contains(item2))
                {
                    if (list2.Contains(item2))
                        result.Add(item2);
                }

            }
            return result;
        }
        #endregion
        #region Ex3
        public static List<string> GetRows(List<string> data, string query)
        {
            if (data == null || query == null)
            {
                return []; // возвращаем пустой список, если входные параметры null
            }

            List<string> exactMatchRows = [];

            foreach (string row in data)
            {
                if (row.Contains(query))
                {
                    string[] wordsInRow = row.Split(' ');
                    List<string> wordsInSearchString = new(query.Split(' '));
                    int index = 0;

                    foreach (string word in wordsInRow)
                    {
                        if (word == wordsInSearchString[index])
                        {
                            index++;
                            if (index == wordsInSearchString.Count)
                            {
                                exactMatchRows.Add(row);
                                break;
                            }
                        }
                    }
                }
            }

            return exactMatchRows;
        }
        #endregion
        #region Ex4
        private static int minWordCount;
        private static int maxWordCount;

        // Метод для вычисления минимального и максимального количества слов среди документов
        public static void CalculateMinMaxWordCount(IEnumerable<Document> documents)
        {
            if (documents == null || !documents.Any())
            {
                throw new ArgumentException("Document list cannot be null or empty");
            }
            //для нахождения min\max
            minWordCount = documents.Min(doc => doc.GetWordCount());
            maxWordCount = documents.Max(doc => doc.GetWordCount());
        }

        // Весовая функция для оценки релевантности документов с учетом их длины
        public static double CalculateRelevance(Document document, double preference)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            // Получаем количество слов в документе
            int documentWordCount = document.GetWordCount();

            // Нормализуем количество слов в документе в диапазоне от 0 до 1
            double normalizedLength = (double)(documentWordCount - minWordCount) / (maxWordCount - minWordCount);
            normalizedLength = Math.Max(0, Math.Min(1, normalizedLength)); // Ограничим значение от 0 до 1

            // Вычисляем вес документа в зависимости от предпочтений пользователя
            if (preference < 0 || preference > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(preference), "Preference should be between 0 and 1");
            }

            // Вес рассчитывается как линейная комбинация нормализованной длины и её дополнения
            double weight = preference * normalizedLength + (1 - preference) * (1 - normalizedLength);
            return weight;
        }
        #endregion
        #region Ex5
        public static double CalculateFrequency(List<Document> documents, List<string> keywords)
        {
            try
            {
                if (documents == null || documents.Count() == 0)
                {
                    throw new Exception("No document");
                }
                double frequency = 0.0;
                int totalWords = 0;
                int keywordCount = 0;
                if (keywords == null || keywords.Count() == 0)
                {
                    throw new Exception("Keywords cannot be null or empty.");
                }
                foreach (var document in documents)
                {
                    string[] words = document.Content.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                    totalWords += words.Length;

                    keywordCount = words.Count(w => keywords.Contains(w.Trim().ToLower()));

                }
                frequency = (double)keywordCount / totalWords;
                return frequency;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }
        #endregion
        #region Ex6
        public static List<Document> Search(List<Document> documents, string[] keywords)
        {
            List<Document> results = new List<Document>();
            try
            {
                if (documents == null || documents.Count() == 0)
                {
                    throw new Exception("No documents");
                }

                if (keywords == null || keywords.Length == 0)
                {
                    throw new Exception("Keywords cannot be null or empty.");
                }


                foreach (var document in documents)
                {
                    bool containsKeywords = document.Content.Split(separators, StringSplitOptions.RemoveEmptyEntries)
                                                            .Intersect(keywords).Any();

                    if (containsKeywords)
                    {
                        results.Add(document);
                    }
                    else
                    {
                        // Проверяем наличие ключевых слов во внешних ссылках
                        foreach (var link in document.ExternalLinks)
                        {
                            if (link.Split(separators, StringSplitOptions.RemoveEmptyEntries)
                                    .Intersect(keywords).Any())
                            {
                                results.Add(document);
                                break;
                            }
                        }
                    }
                }

                if (!results.Any())
                {
                    throw new Exception("No documents");
                }
                return results;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return results;
            }
        }
        #endregion

    }
}
