
namespace jolly_Jumpers
{
    internal static class JollyJumpers
    {
        public static bool IsJollyJumpers(string input)
        {
            int count = 0;
            string[] Numbers = input.Split(' ');
            int[] NumbersInt = new int[Numbers.Length];
            if (Numbers.Length > 3000 || Numbers.Length <= 1)
            {
                Console.WriteLine("В строке должно быть чисел < 3000 и > 1.");
                return false;

            }

            for (int i = 0; i < Numbers.Length; i++)
            {
                if (!int.TryParse(Numbers[i], out NumbersInt[i]))
                {
                    Console.WriteLine("Неправильный формат числа.");
                    return false;
                }

            }
            Boolean[] array = new Boolean[Numbers.Length];
            int d;
            for (int i = 0;i<NumbersInt.Length - 1;i++)
            {
                d = Math.Abs(NumbersInt[i] - NumbersInt[i+1]);
                if (d == 0 || d > NumbersInt.Length - 1 || array[d] == true) 
                    return false;
                else
                    array[d] = true;


            }
            return true;
        }

    }
}
