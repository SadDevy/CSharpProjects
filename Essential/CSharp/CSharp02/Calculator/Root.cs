using System;

namespace Calculator
{
    /// <summary>
    /// Находит корень N-ой степени числа.
    /// </summary>
    public static class Root
    {
        /// <summary>
        /// Вычисляет корень методом Ньютона.
        /// </summary>
        /// <param name="value">Число.</param>
        /// <param name="n">Степень.</param>
        /// <param name="accuracy">Точность.</param>
        /// <returns>Корень степени n для value с точностью accuracy.</returns>
        public static double CalculateRootNewton(double value, double n, double accuracy)
        {
            double val1 = value / n;
            double val2 = CalculateNewValue(value, val1, n);
            while (!Equals(val2, val1, accuracy))
            {
                val1 = val2;
                val2 = CalculateNewValue(value, val1, n);
            }

            return val2;
        }

        public static bool Equals(double a, double b, double accuracy)
        {
            return Math.Abs(a - b) < accuracy;
        }

        /// <summary>
        /// Вычисляет новое значение в методе Ньютона.
        /// </summary>
        /// <param name="oldValue">Старое значение числа.</param>
        /// <param name="n">Степень корня.</param>
        /// <returns>Новое значение числа.</returns>
        private static double CalculateNewValue(double number, double oldValue, double n)
        {
            return (1 / n) * ((n - 1) * oldValue + (number / (Math.Pow(oldValue, n - 1))));
        }

        /// <summary>
        /// Вычисляет корень стандартными средствами.
        /// </summary>
        /// <param name="value">Число.</param>
        /// <param name="n">Степень.</param>
        /// <returns>Корень степени n для value.</returns>
        public static double CalculateRoot(double value, double n)
        {
            return Math.Pow(value, 1 / n);
        }
    }
}
