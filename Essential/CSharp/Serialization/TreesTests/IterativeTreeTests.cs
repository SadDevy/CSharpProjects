using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using BinaryTee;
using NUnit.Framework;
namespace BinsaryTreeTests
{
    [TestFixture]
    public class IterativeTreeTests
    {
        [TestCaseSource(nameof(GetTestSerializationIntegersTestCases))]
        public List<int> TestSerializationIntegers(int[] values)
        {
            IterativeTree<int> a = new IterativeTree<int>(values);

            Assert.IsNotNull(a);

            using (Stream s = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(s, a);
                s.Position = 0;
                a = (IterativeTree<int>)formatter.Deserialize(s);
            }

            List<int> result = new List<int>();
            foreach (int item in a)
                result.Add(item);

            return result;
        }

        private static IEnumerable GetTestSerializationIntegersTestCases
        {
            get
            {
                yield return new TestCaseData(new int[0]).Returns(new List<int>());
                yield return new TestCaseData(new int[] { 5, 4, 6, 3, 5 }).Returns(new List<int>() { 3, 4, 5, 5, 6 });
            }
        }

        [TestCaseSource(nameof(GetTestSerializationStudentsTestCases))]
        public List<StudentTestInfo> TestSerializationStudents(List<StudentTestInfo> values)
        {
            IterativeTree<StudentTestInfo> a = new IterativeTree<StudentTestInfo>(values);

            Assert.IsNotNull(a);

            using (Stream s = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(s, a);
                s.Position = 0;
                a = (IterativeTree<StudentTestInfo>)formatter.Deserialize(s);
            }

            List<StudentTestInfo> result = new List<StudentTestInfo>();
            foreach (StudentTestInfo item in a)
                result.Add(item);

            return result;
        }

        private static IEnumerable GetTestSerializationStudentsTestCases
        {
            get
            {
                StudentTestInfo a = new StudentTestInfo("Павел", "Павлов", "Test", new DateTime(2020, 06, 14), 1);
                StudentTestInfo b = new StudentTestInfo("Семен", "Семенов", "Test", new DateTime(2020, 06, 14), 2);
                StudentTestInfo c = new StudentTestInfo("Петр", "Петров", "Test", new DateTime(2020, 06, 14), 3);
                StudentTestInfo d = new StudentTestInfo("Андрей", "Андреев", "Test", new DateTime(2020, 06, 14), 4);
                StudentTestInfo e = new StudentTestInfo("Игорь", "Игорев", "Test", new DateTime(2020, 06, 14), 5);

                yield return new TestCaseData(new List<StudentTestInfo>()).Returns(new StudentTestInfo[0]);
                yield return new TestCaseData(new List<StudentTestInfo>() { a, b, c, d, e })
                                             .Returns(new StudentTestInfo[] { a, b, c, d, e });
            }
        }
    }
}
