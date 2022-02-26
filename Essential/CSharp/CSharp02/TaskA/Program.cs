using System;
using System.Globalization;
using Calculator;

namespace TaskA
{
    class Program
    {
        const double accuracyMaxBorder = 0.11;

        static void Main()
        {
            double number = InputDouble("Число:", "Введите вещественное число:");
            int n = InputInteger("Степень корня:", "Введите целое число:");
            double accuracy = InputDouble("Точность:", "Введите вещественное число:", 0, accuracyMaxBorder);

            double newtonNRoot = Root.CalculateRootNewton(number, n, accuracy);
            double standardNRoot = Root.CalculateRoot(number, n);

            ShowResult(newtonNRoot, standardNRoot, accuracy);
        }

        private static double InputDouble(string inputMessage, string failureMessage, double minBorder = 0, double? maxBorder = null)
        {
            Console.WriteLine(inputMessage);

            double value;
            while (!double.TryParse(Console.ReadLine(), NumberStyles.Number, CultureInfo.InvariantCulture, out value) || (value < minBorder) || (maxBorder.HasValue && (value > maxBorder)))
            {
                Console.WriteLine(failureMessage);
                ShowBordersMessage(minBorder, maxBorder);
            }

            return value;
        }

        private static int InputInteger(string inputMessage, string failureMessage, int minBorder = 0, int? maxBorder = null)
        {
            Console.WriteLine(inputMessage);

            int value;
            while (!int.TryParse(Console.ReadLine(), out value) || (value < minBorder) || (maxBorder.HasValue && (value > maxBorder)))
            {
                Console.WriteLine(failureMessage);
                ShowBordersMessage(minBorder, maxBorder);
            }

            return value;
        }

        private static void ShowBordersMessage(double minBorder, double? maxBorder)
        {
            string borderFormat = maxBorder.HasValue ? "Число больше {0}:" : "Число больше {0} и меньше {1}:";
            Console.WriteLine(borderFormat, minBorder, maxBorder);
        }

        private static void ShowResult(double newtonRoot, double standardRoot, double accuracy)
        {
            Console.WriteLine("Результат методом Ньютона: {0}", newtonRoot);
            Console.WriteLine("Результат стандартным методом: {0}", standardRoot);

            bool areEqual = Root.Equals(newtonRoot, standardRoot, accuracy); 
            string equalityMessageFormat = areEqual ? "Результаты равны с точностью {0}." : "Результаты не равны с точностью {0}.";
            Console.WriteLine(equalityMessageFormat, accuracy);
        }
    }
}
