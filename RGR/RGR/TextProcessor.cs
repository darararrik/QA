
using System.Text.RegularExpressions;


namespace Ex1
{
    static internal class TextProcessor
    {
        public static string[] SeparateWords(this string Text)
        {
            Regex splitter = new(@"\b\w+\b");
            string[] words = splitter.Matches(Text)
                            .Select(w => w.Value.ToLower())
                            .ToArray();

            return words;
        }
    }
}
