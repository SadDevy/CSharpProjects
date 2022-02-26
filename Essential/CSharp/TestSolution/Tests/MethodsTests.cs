using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class MethodsTests
    {
        [TestCaseSource(nameof(GetSourceNumbersCases))]
        public Dictionary<int, int> GetNumbersWithLinqTest(int[] values)
        {
            return Methods.Methods.GetNumbersWithLinq(values);
        }
        
        [TestCaseSource(nameof(GetSourceNumbersCases))]
        public Dictionary<int, int> GetNumbersWithArraysTest(int[] values)
        {
            return Methods.Methods.GetNumbersWithArrays(values);
        }
        
        [TestCaseSource(nameof(GetSourceNumbersCases))]
        public Dictionary<int, int> GetNumbersWithListsTest(int[] values)
        {
            return Methods.Methods.GetNumbersWithLists(values);
        }

        private static IEnumerable GetSourceNumbersCases
        {
            get
            {
                yield return new TestCaseData(new int[] {1, 2, 3, 0, -1, -2, 1, 18, 3}).Returns(
                    new Dictionary<int, int>() {{1, 2}, {2, 1}, {3, 2}, {18, 1}});
                yield return new TestCaseData(new int[] {0, 1, -10, 1, 2, -1, 10, 10, 0, 10}).Returns(
                    new Dictionary<int, int>() {{1, 2}, {2, 1}, {10, 3}});
                yield return new TestCaseData(new int[] {0, 2, -5, 0, -1, -2, 1, 0, 3}).Returns(
                    new Dictionary<int, int>() {{2, 1}, {1, 1}, {3, 1}});
            }
        }
    }
}