using System;
using NumberConverter;

namespace TaskB
{
    /// <summary>
    /// Запускает консольное приложение.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Запускает приложение в режиме обработки данных из консоли.
        /// </summary>
        static void Main()
        {
            int number = InputInteger("Исходное число:", "Введите целое положительное число:");

            foreach (BaseSystem baseSystem in GetBaseSystems())
            {
                string baseValue = Converter.ToBase(number, baseSystem);
                string baseValueStandard = Converter.ToBaseStandard(number, baseSystem);

                ShowResult(baseSystem, baseValue, baseValueStandard);
            }
        }

        private static BaseSystem[] GetBaseSystems()
        {
            return (BaseSystem[])Enum.GetValues(typeof(BaseSystem));
        }

        private static int InputInteger(string inputMessage, string failureMessage, int minBorder = 0)
        {
            Console.WriteLine(inputMessage);

            int value;
            while (!int.TryParse(Console.ReadLine(), out value) || value < minBorder)
            {
                Console.WriteLine(failureMessage);
            }

            return value;
        }

        private static void ShowResult(BaseSystem toBase, string value, string standardValue)
        {
            Console.WriteLine("Система счисления: {0}", toBase);
            Console.WriteLine("По алгоритму: {0}", value);
            if (standardValue != null)
            {
                Console.WriteLine("Стандартными средствами: {0}", standardValue);

                string equalityMessage = (value == standardValue) ? "Результаты совпадают." : "Результаты не совпадают.";
                Console.WriteLine(equalityMessage);
            }

            Console.WriteLine();
        }
    }
}
