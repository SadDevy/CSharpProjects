using System;
using System.Diagnostics;

namespace GCDCalculator
{
    /// <summary>
    /// Находит НОД целых положительных чисел.
    /// </summary>
    public static class GCD
    {
        /// <summary>
        /// Вычисляет НОД двух целых положительных чисел по алгоритму Эвклида.
        /// </summary>
        /// <param name="a">Первое число.</param>
        /// <param name="b">Второе число.</param>
        /// <returns>НОД двух чисел.</returns>
        public static int CalculateEuclidean(int a, int b)
        {
            int smallest = Math.Min(a, b);
            int biggest = Math.Max(a, b);

            if (smallest == 0)
                return biggest;
            else
                return CalculateEuclidean(smallest, biggest - smallest);
        }

        public static int CalculateEuclidean(params int[] values)
        {
            if (values == null)
                throw new ArgumentNullException("values");

            if (values.Length < 2)
                throw new InvalidOperationException("НОД вычисляется минимум для двух чисел.");
            
            int gcd = values[0];
            for (int i = 1; i < values.Length; i++)
                gcd = CalculateEuclidean(gcd, values[i]);

            return gcd;
        }

        public static int CalculateEuclidean(int a, int b, int c)
        {
            return CalculateEuclidean(new int[] { a, b, c });
        }

        public static int CalculateEuclidean(int a, int b, int c, int d)
        {
            return CalculateEuclidean(new int[] { a, b, c, d });
        }

        public static int CalculateEuclidean(int a, int b, int c, int d, int e)
        {
            return CalculateEuclidean(new int[] { a, b, c, d, e });
        }

        /// <summary>
        /// Вычисляет НОД двух целых положительных чисел по алгоритму Стейна.
        /// </summary>
        /// <param name="a">Первое число.</param>
        /// <param name="b">Второе число.</param>
        /// <returns>НОД двух чисел.</returns>
        public static int CalculateStein(int a, int b)
        {
            int smallest = Math.Min(a, b);
            int biggest = Math.Max(a, b);

            if (smallest == biggest)
                return smallest;

            if (smallest == 0)
                return biggest;

            if (biggest == 0)
                return smallest;

            if (smallest % 2 == 0 && biggest % 2 == 0)
                return 2 * CalculateStein(smallest / 2, biggest / 2);

            if (smallest % 2 == 0 && biggest % 2 != 0)
                return CalculateStein(smallest / 2, biggest);

            if (biggest % 2 == 0 && smallest % 2 != 0)
                return CalculateStein(smallest, biggest / 2);

            return CalculateStein((biggest - smallest) / 2, smallest);
        }

        public static int CalculateStein(params int[] values)
        {
            if (values == null)
                throw new ArgumentNullException("values");

            if (values.Length < 2)
                throw new InvalidOperationException("НОД вычисляется минимум для двух чисел.");

            int gcd = values[0];
            for (int i = 1; i < values.Length; i++)
                gcd = CalculateStein(gcd, values[i]);

            return gcd;
        }

        public static int CalculateStein(out TimeSpan elapsed, params int[] values)
        {
            Stopwatch timer = Stopwatch.StartNew();

            int gcd = CalculateStein(values);
            timer.Stop();

            elapsed = timer.Elapsed;
            return gcd;
        }

        public static int CalculateEuclidean(out TimeSpan elapsed, params int[] values)
        {
            Stopwatch timer = Stopwatch.StartNew();

            int gcd = CalculateEuclidean(values);
            timer.Stop();

            elapsed = timer.Elapsed;
            return gcd;
        }
    }
}
