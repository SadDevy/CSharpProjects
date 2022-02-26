using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using BinarySearch;
using NUnit.Framework;

namespace BinsaryTreeTests
{
    [TestFixture]
    public class StringIterativeTreeTests
    {
        [TestCaseSource(nameof(GetTestSerializationTestCases))]
        public List<string> TestSerialization(List<string> values, string added)
        {
            static void A(StringIterativeTree a)
            {
                using (Stream stream = new MemoryStream())
                {
                    using (XmlWriter writer = XmlWriter.Create(stream, new XmlWriterSettings()))
                    {
                        a.WriteXml(writer);
                    }

                    stream.Position = 0;

                    using (XmlReader reader = XmlReader.Create(stream, new XmlReaderSettings()))
                    {
                        a.ReadXml(reader);
                    }
                }
            }

            StringIterativeTree a = new StringIterativeTree(values);

            A(a);

            a.Add(added);

            A(a);

            List<string> result = new List<string>();
            foreach (string value in a)
                result.Add(value);

            return result;
        }

        private static IEnumerable GetTestSerializationTestCases
        {
            get
            {
                yield return new TestCaseData(new List<string>(), "something")
                                             .Returns(new List<string>() { "something" });
                yield return new TestCaseData(new List<string>() { "b", "a", "c" }, "something")
                                             .Returns(new List<string>() { "a", "b", "c", "something" });
            }
        }
    }
}
