using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace LinqProvider
{
    public static class Provider
    {
        public static IEnumerable<DateTime> GetWorkingDays(DateTime start)
        {
            while (true)
            {
                if (start.DayOfWeek != DayOfWeek.Saturday && start.DayOfWeek != DayOfWeek.Sunday)
                    yield return start;

                start = start.AddDays(1);
            }
        }

        public static string GetCommonestWord(string line)
        {
            CheckNull(line, nameof(line));

            char[] charsSeaparator = new char[] { ' ', ',', '.', '(', ')', '"', '!', '?' };
            string[] words = line.Split(charsSeaparator);

            var a = words.GroupBy(n => n, (word, words) => new { Word = word, Count = words.Count() });
            return a.First(n => n.Count == a.Max(n => n.Count)).Word;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void CheckNull<AItem>(AItem a, string aName)
            where AItem : class
        {
            if (a == null)
                throw new ArgumentNullException(aName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void CheckNull<AItem, BItem>(AItem a, string aName, BItem b, string bName)
            where AItem : class
            where BItem : class
        {
            CheckNull(a, aName);
            CheckNull(b, bName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void CheckNull<AItem, BItem, CItem>(AItem a, string aName, BItem b, string bName, CItem c, string cName)
            where AItem : class
            where BItem : class
            where CItem : class
        {
            CheckNull(a, aName, b, bName);
            CheckNull(c, cName);
        }

        public static double Summarize(double[] values)
        {
            CheckNull(values, nameof(values));

            if (values.Length == 0)
                return default;

            return values.Aggregate((value, result) => result + value);
        }

        public static string Join(string separator, string[] values)
        {
            CheckNull(separator, nameof(separator), values, nameof(values));

            return Join(separator, values, (value, result) => string.Concat(value, separator, result));
        }

        public static string JoinWithStringBuilder(string separator, string[] values)
        {
            CheckNull(separator, nameof(separator), values, nameof(values));

            return Join(separator, values, (value, result) => new StringBuilder()
                                                              .AppendJoin(separator, value, result)
                                                              .ToString());
        }

        private static string Join(string separator, string[] values, Func<string, string, string> func)
        {
            CheckNull(separator, nameof(separator), values, nameof(values), func, nameof(func));

            if (values.Length == 0)
                return default;

            return values.Aggregate(func);
        }

        public static string Reverse(string line)
        {
            CheckNull(line, nameof(line));

            const string separator = " ";

            string[] lineParts = line.Split(separator);
            return lineParts.Aggregate((value, result) => result + separator + value);
        }

        public static bool PinIsValid(string line)
        {
            CheckNull(line, nameof(line));

            const int elementsCount = 4;
            if (line.Length != elementsCount)
                return false;

            if (!int.TryParse(line, out int result))
                return false;

            const string pattern = @"(.)\1{2}";
            if (Regex.IsMatch(line, pattern))
                return false;

            int inRowCount = line.Where((n, i) => Math.Abs(n - line.First()) == i).Count();
            return !(inRowCount == elementsCount);
        }

        public static IEnumerable GetElements()
        {
            while (true)
                yield return default;
        }

        public static Action CreateAction()
        {
            int count = 0;
            return () =>
            {
                Console.WriteLine("Меня вызвали {0} раз", ++count);
            };
        }

        public static Action<double> CreateCounter()
        {
            double count = 0;
            return (increment) =>
            {
                count += increment;
                Console.WriteLine("Count = {0}", count);
            };
        }

        public static IEnumerable<string> FindSeries(this string word, char symbol)
        {
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == symbol)
                    yield return word.GetSeries(symbol, ref i);
            }
        }

        private static string GetSeries(this string word, char symbol, ref int index)
        {
            StringBuilder result = new StringBuilder();
            while (++index < word.Length && word[index] == symbol)
            {
                result.Append(word[index]);
            }

            return result.ToString();
        }

        public static IEnumerable<string> GetSeries(this IEnumerable<string> data, char symbol)
        {
            return data.SelectMany(n => n.FindSeries(symbol))
                       .Where(n => n.Length > 0);
        }

        public static Func<char, string> CreatePredicate(char symbol)
        {
            char previous = default;
            string result = string.Empty;
            return (current) =>
            {
                bool areEqual = current == symbol && previous == symbol;
                if (areEqual)
                    result += current;

                previous = current;

                if (!areEqual && result.Length > 0)
                {
                    string r = result;
                    result = string.Empty;
                    return r;
                }

                return result;
            };
        }

        public static IEnumerable<string> GetSymbols(this string line, Func<char, string> predicate)
        {
            const char separator = ' ';
            //return string.Join(separator, line.Where(n => predicate(n)))
            //             .Split(separator)
            //             .Select(n => n.Trim(separator));

            return line.Select(n => predicate(n)).Where(n => n.Length > 0);
        }

        //public static IEnumerable<IEnumerable<string>> GetSymbols(this string[] data, Predicate<char> predicate)
        //{
        //    return data.Select(n => n.GetSymbols(predicate)
        //               .Where(n => n.Length > 0));
        //}
    }
}
