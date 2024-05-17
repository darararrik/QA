using Ex1;

class Program
{
    static void Main()
    {
        string sampleText = "C++ is a programming language. $20 is the price. Ph.D is an abbreviation. 617-555-1212 is a phone number.";

        string[] words = sampleText.SeparateWords();
        foreach (string word in words)
        {
            Console.WriteLine(word);
        }
    }
}