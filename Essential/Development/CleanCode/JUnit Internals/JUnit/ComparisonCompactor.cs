using System;
using System.Text;

namespace ComparisonCompacting
{
    public class ComparisonCompactor
    {
        private const string elipsis = "...";
        private const string deltaEnd = "]";
        private const string deltaStart = "[";

        private int сontextLength;
        private string expected;
        private string actual;

        private int preffixLength;
        private int suffixLength;

        public ComparisonCompactor(int сontextLength, string expected, string actual)
        {
            this.сontextLength = сontextLength;
            this.expected = expected;
            this.actual = actual;
        }

        public string FormatCompactedComparison(string message)
        {
            string compactExpected = expected;
            string compactActual = actual;
            if (ShouldBeCompacted())
            {
                FindCommonPrefixAndSuffix();
                compactExpected = Compact(expected);
                compactActual = Compact(actual);
            }

            return Format(message, compactExpected, compactActual);
        }

        private bool ShouldBeCompacted() => !ShouldNotBeCompacted();

        private bool ShouldNotBeCompacted() => expected == null || actual == null || expected.Equals(actual);

        private void FindCommonPrefixAndSuffix()
        {
            FindCommonPrefix();
            suffixLength = 0;
            for (; !SuffixOverlapsPrefix(suffixLength); suffixLength++)
            {
                if (CharFromEnd(expected, suffixLength) != CharFromEnd(actual, suffixLength))
                    break;
            }
        }

        private char CharFromEnd(string s, int i) => s[s.Length - i - 1];

        private bool SuffixOverlapsPrefix(int suffixLength)
        {
            return actual.Length - suffixLength <= preffixLength ||
                expected.Length - suffixLength <= preffixLength;
        }

        private void FindCommonPrefix()
        {
            preffixLength = 0;
            int end = Math.Min(expected.Length, actual.Length);
            for (; preffixLength < end; preffixLength++)
            {
                if (expected[preffixLength] != actual[preffixLength])
                    break;
            }
        }

        private string Compact(string source)
        {
            return new StringBuilder()
                .Append(StartingEllipsis())
                .Append(StartingContext())
                .Append(deltaStart)
                .Append(GetDelta(source))
                .Append(deltaEnd)
                .Append(EndingContext())
                .Append(EndingEllipsis())
                .ToString();
        }

        private string StartingEllipsis() => preffixLength > сontextLength ? elipsis : string.Empty;

        private string StartingContext()
        {
            int contextStart = Math.Max(0, preffixLength - сontextLength);
            int contextEnd = preffixLength - Math.Max(0, preffixLength - сontextLength);
            return expected.Substring(contextStart, contextEnd);
        }

        private string GetDelta(string s)
        {
            int deltaStart = preffixLength;
            int deltaEnd = s.Length - suffixLength - preffixLength;
            return s.Substring(deltaStart, deltaEnd);
        }

        private string EndingContext()
        {
            int contextStart = expected.Length - suffixLength;
            int contextEnd = Math.Min(expected.Length - suffixLength + сontextLength, expected.Length) 
                                    - (expected.Length - suffixLength);

            return expected.Substring(contextStart, contextEnd);
        }

        private string EndingEllipsis() => expected.Length - suffixLength < expected.Length - сontextLength ? elipsis : string.Empty;

        private string Format(string message, object expected, object actual)
        {
            return string.Format("{0}{1}expected:<{2}> but was:<{3}>", message, string.IsNullOrEmpty(message) ? null : " ",
                expected ?? "null", actual ?? "null");
        }
    }
}
