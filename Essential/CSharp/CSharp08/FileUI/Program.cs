using File;
using System;

namespace FileUI
{
    class Program
    {
        const string filePath = @"..\..\..\Something.txt";
        const string greeting = "[01] Привет мир!";
        const int symbolIndex = 2;
        const char changedSymbol = '2';

        static void Main()
        {
            Write(filePath, greeting);
            Show(filePath);
            ChangeSymbol(filePath, symbolIndex, changedSymbol);
            Show(filePath);
        }

        private static void Write(string filePath, string value)
        {
            FileArray symbols = null;
            try
            {
                symbols = FileArray.Create(filePath, value.Length);
                for (int i = 0; i < value.Length; i++)
                {
                    symbols[i] = value[i];
                }
            }
            finally
            {
                symbols.Dispose();
            }
        }

        private static void Show(string filePath) 
        {
            using (FileArray symbols = FileArray.Read(filePath))
            {
                for (int i = 0; i < symbols.Length; i++)
                    Console.Write(symbols[i]);
            }
        }

        private static void ChangeSymbol(string filePath, int index, char symbol)
        {
            using (FileArray symbols = FileArray.Read(filePath))
            {
                symbols[index] = symbol;
            }
        }
    }
}
