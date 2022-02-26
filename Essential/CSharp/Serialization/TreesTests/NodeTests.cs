using System;
using System.Collections;
using System.IO;
using System.Xml.Serialization;
using BinaryTee;
using NUnit.Framework;

namespace BinsaryTreeTests
{
    [TestFixture]
    public class NodeTests
    {
        [TestCaseSource(nameof(GetTestXmlSerializationTestCases))]
        public void TestXmlSerialization(int data, Node<int> left, Node<int> right)
        {
            Node<int> a = new Node<int>(data, right, left);

            Assert.IsNotNull(a);

            using (Stream s = new MemoryStream())
            {
                XmlSerializer formatter = new XmlSerializer(typeof(Node<int>));
                formatter.Serialize(s, a);
                s.Position = 0;
                a = (Node<int>)formatter.Deserialize(s);
            }

            Assert.AreEqual(data, a.Data);
            Assert.AreEqual(left, a.Left);
            Assert.AreEqual(right, a.Right);
        }

        private static IEnumerable GetTestXmlSerializationTestCases
        {
            get
            {
                yield return new TestCaseData(1, null, null);
                yield return new TestCaseData(1, new Node<int>(0), null);
                yield return new TestCaseData(1, new Node<int>(0), new Node<int>(2));
            }
        }
    }
}
