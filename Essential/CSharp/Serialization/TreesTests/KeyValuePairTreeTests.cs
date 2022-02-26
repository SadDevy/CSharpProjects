using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;
using BinarySearch;
using BinaryTee;
using NUnit.Framework;

namespace BinsaryTreeTests
{
    [TestFixture]
    public class KeyValuePairTreeTests
    {
        [TestCaseSource(nameof(GetTestNodeSerializationTestCases))]
        public void TestNodeSerialization(
            KeyValue<string, int> data,
            Node<KeyValue<string, int>> right,
            Node<KeyValue<string, int>> left
            )
        {
            Node<KeyValue<string, int>> a = new Node<KeyValue<string, int>>(data, right, left);

            Assert.IsNotNull(a);

            using (Stream s = new MemoryStream())
            {
                XmlSerializer formatter = new XmlSerializer(typeof(Node<KeyValue<string, int>>));
                formatter.Serialize(s, a);
                s.Position = 0;
                a = formatter.Deserialize(s) as Node<KeyValue<string, int>>;
            }

            Assert.AreEqual(data, a.Data);
            Assert.AreEqual(left, a.Left);
            Assert.AreEqual(right, a.Right);
        }

        private static IEnumerable GetTestNodeSerializationTestCases
        {
            get
            {
                yield return new TestCaseData(new KeyValue<string, int>("Танк", 100), null, null);
                yield return new TestCaseData(new KeyValue<string, int>("Танк", 100),
                                              new Node<KeyValue<string, int>>(
                                                  new KeyValue<string, int>("Самолет", 140)), null);
                yield return new TestCaseData(new KeyValue<string, int>("Танк", 100),
                                              new Node<KeyValue<string, int>>(
                                                  new KeyValue<string, int>("Самолет", 140)),
                                              new Node<KeyValue<string, int>>(
                                                  new KeyValue<string, int>("Авианосец", 600)));
            }
        }

        [TestCaseSource(nameof(GetTestTreeSerializationTestCases))]
        public List<KeyValue<string, int>> TestTreeSerialization(List<KeyValue<string, int>> values)
        {
            KeyValuePairTree<string, int> a = new KeyValuePairTree<string, int>(values);

            Assert.IsNotNull(a);

            using (Stream s = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(s, a);
                s.Position = 0;
                a = formatter.Deserialize(s) as KeyValuePairTree<string, int>;
            }

            List<KeyValue<string, int>> result = new List<KeyValue<string, int>>();
            foreach (KeyValue<string, int> value in a)
                result.Add(value);

            return result;
        }

        private static IEnumerable GetTestTreeSerializationTestCases
        {
            get
            {
                yield return new TestCaseData(new List<KeyValue<string, int>>())
                                             .Returns(new List<KeyValue<string, int>>());
                yield return new TestCaseData(new List<KeyValue<string, int>>()
                {
                    new KeyValue<string, int>("Танк", 100),
                    new KeyValue<string, int>("Самолет", 140),
                    new KeyValue<string, int>("Авианосец", 600),
                    new KeyValue<string, int>("Гаубица", 80)
                }).Returns(new List<KeyValue<string, int>>()
                {
                    new KeyValue<string, int>("Авианосец", 600),
                    new KeyValue<string, int>("Гаубица", 80),
                    new KeyValue<string, int>("Самолет", 140),
                    new KeyValue<string, int>("Танк", 100)
                });
            }
        }

    }
}
