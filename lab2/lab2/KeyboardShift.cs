
namespace lab2
{
    internal static class KeyboardShift
    {
        //создаем коллекцию ключ значение
     
        private static Dictionary<char, char> keyboardMapping = new Dictionary<char, char>
        {
            {'Q','Q'},{'W','Q'},{'E','W'},{'R','E'},{'T','R'},{'Y','T'},
            {'U','Y'},{'I','U'},{'O','I'},{'P', 'O'},{'[','P'},{']','['},
            {'\\',']'},{'A','A'},{'S','A'},{'D','S'},{'F','D'},{'G','F'},
            {'H','G'},{'J','H'},{'K','J'},{'L','K'},{';','L'},{'\'',';'},
            {'Z','Z'},{'X','Z'},{'C','X'},{'V','C'},{'B','V'},{'N','B'},
            {'M','N'},{',','M'},{'.',','},{'/','.'},{'1','`'},{'2','1'},
            {'3','2'},{'4','3'},{'5','4'},{'6','5'},{'7','6'},{'8','7'},{'9','8'},
            {'0','9'},{'-','0'},{'=','-'}
        };
        public static string ShiftString(string str)
        {
            char[] shiftedChars = new char[str.Length];

            for (int i = 0; i < str.Length; i++)
            {
                // Получаем символ из строки
                char originalChar = str[i];

                // Если символ есть в словаре, используем его позицию для сдвига
                if (keyboardMapping.ContainsKey(originalChar))
                {
                    shiftedChars[i] = keyboardMapping[originalChar];
                }
                // Если символа нет в словаре (например, пробел или другой символ), оставляем его без изменений
                else
                {
                    shiftedChars[i] = originalChar;
                }
            }

            return new string(shiftedChars);
        }
    }
}
