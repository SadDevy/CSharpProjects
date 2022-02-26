using System;
using GCDCalculator;

namespace GCDCalculatorUI
{
    /// <summary>
    /// Отображает НОД чисел в консоли.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Отображает НОД нескольких чисел, и время его нахожденя в консоли.
        /// </summary>
        static void Main()
        {
            int[] numbers = InputIntegers("Ввод чисел:");

            TimeSpan timeEuclidean;
            int gcdEuclidean = GCD.CalculateEuclidean(out timeEuclidean, numbers);
            TimeSpan timeStein;
            int gcdStein = GCD.CalculateStein(out timeStein, numbers);

            ShowResult(gcdEuclidean, gcdStein, timeEuclidean, timeStein);
        }

        private static int[] InputIntegers(string inputMessage)
        {
            Console.WriteLine(inputMessage);

            int numbersCount = InputInteger("Введите количество чисел:");

            int[] numbers = new int[numbersCount];
            for (int i = 0; i < numbers.Length; i++)
            {
                string numberFormat = string.Format("{0} - е число:", i);
                numbers[i] = InputInteger(numberFormat);
            }

            return numbers;
        }

        private static int InputInteger(string inputMessage)
        {
            Console.WriteLine(inputMessage);

            int value;
            while (!int.TryParse(Console.ReadLine(), out value) || value <= 0)
            {
                Console.WriteLine("Введите целое положительное число!");
            }

            return value;
        }

        private static void ShowResult(int gcdEuclidean, int gcdStein, TimeSpan timeEuclidean, TimeSpan timeStein)
        {
            Console.WriteLine("НОД по алгоритму Евклида: {0}, время работы алгоритма: {1} мс.", gcdEuclidean, timeEuclidean.TotalMilliseconds);
            Console.WriteLine("НОД по алгоритму Стейна: {0}, время работы алгоритма: {1} мс.", gcdStein, timeStein.TotalMilliseconds);

            string equlityMessage = (gcdEuclidean == gcdStein) ? "Результаты совпадают." : "Результаты не совпадают.";
            Console.WriteLine(equlityMessage);
        }
    }
}
