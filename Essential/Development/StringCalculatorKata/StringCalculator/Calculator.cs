using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class Calculator
    {
        private List<string> delimiters = new List<string> { ",", "\n" };
        private string failureMessage = "Error: negatives are not allowed";

        public Calculator() { }

        public int Add(string numbers)
        {
            if (!numbers.Any())
                return 0;

            AddDelimiter(numbers);

            return SumNumbers(numbers);
        }

        private void AddDelimiter(string numbers)
        {
            const string delimiterFlag = "//";

            string optionalLine = GetOptionalLine(numbers);
            if (optionalLine.StartsWith(delimiterFlag))
                Array.ForEach(ExtractDelimiters(optionalLine).ToArray(), delimiters.Add);
        }

        private string GetOptionalLine(string numbers)
        {
            const char delimiter = '\n';

            return numbers.Split(delimiter)
                          .First();
        }

        private IEnumerable<string> ExtractDelimiters(string optionalLine)
        {
            const int delimiterIndex = 2;
            char[] separators = new char[] { '[', ']' };

            string delimitersLine = optionalLine.Substring(delimiterIndex);
            return delimitersLine.Split(separators)
                                 .Where(s => !string.IsNullOrEmpty(s));
        }

        private int SumNumbers(string numbers)
        {
            List<int> values = ExctractValues(numbers);

            int sum = 0;
            values.ForEach(value => sum += value);

            return sum;
        }

        private List<int> ExctractValues(string numbers)
        {
            List<int> values = new List<int>();

            Array.ForEach(numbers.Split(delimiters.ToArray(), StringSplitOptions.None), ExctractValue(values));

            CheckNegatives(values);

            return values;
        }

        private Action<string> ExctractValue(List<int> values)
        {
            return s =>
            {
                const int ignoredValue = 1000;
                if (int.TryParse(s, out int value) && value <= ignoredValue)
                    values.Add(value);
            };
        }

        private void CheckNegatives(List<int> values)
        {
            List<int> negatives = ExctractNegatives(values);

            string failureMessage = GetFailureMessage(negatives);

            if (string.IsNullOrEmpty(failureMessage))
                return;

            throw new InvalidOperationException(failureMessage);
        }

        private List<int> ExctractNegatives(List<int> values)
        {
            List<int> negatives = new List<int>();

            values.ForEach(value =>
            {
                if (IsNegative(value))
                    negatives.Add(value);
            });

            return negatives;
        }

        private string GetFailureMessage(List<int> values)
        {
            const int one = 1;
            if (!values.Any())
                return string.Empty;

            if (values.Count == one)
                return failureMessage;

            string result = failureMessage + ": ";
            values.ForEach(value => result += value + ",");
            return result;
        }

        private bool IsNegative(int value) => value < 0;
    }
}
