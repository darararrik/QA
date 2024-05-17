

namespace Ex3
{
    internal class Searcher
    {
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
    }
}
