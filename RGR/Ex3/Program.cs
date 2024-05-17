namespace Ex3
{
    public class Program
    {

        public static void Main()
        {
            List<string> data = ["this is a test document", "another test document", "this document is another test"];
            string query = "test document";

            List<string> exactMatches = Searcher.GetRows(data, query);

            Console.WriteLine("Точные совпадения:");
            foreach (string match in exactMatches)
            {
                Console.WriteLine(match);
            }
        }
    }
}