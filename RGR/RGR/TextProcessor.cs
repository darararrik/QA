using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Ex1
{
    static internal class TextProcessor
    {
        public static string[] SeparateWords(this string Text)
        {
            Regex splitter = new Regex(@"\b\w+\b");
            string[] words = splitter.Matches(Text)
                            .Select(w => w.Value.ToLower())
                            .ToArray();

            return words;
        }
    }
}
