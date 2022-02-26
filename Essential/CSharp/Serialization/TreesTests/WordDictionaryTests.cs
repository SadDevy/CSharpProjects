using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using BinarySearch;
using NUnit.Framework;

namespace BinsaryTreeTests
{
    [TestFixture]
    public class WordDictionaryTests
    {
        [TestCaseSource(nameof(GetTestSerializationTestCases))]
        public List<KeyValue<string, KeyValuePairTree<string, string[]>>> TestSerialization(
            List<KeyValue<string, KeyValuePairTree<string, string[]>>> values
            )
        {
            WordDictionary a = new WordDictionary(values);

            using (Stream s = new MemoryStream())
            {
                XmlSerializer formatter = new XmlSerializer(typeof(WordDictionary));
                formatter.Serialize(s, a);
                s.Position = 0;
                a = (WordDictionary)formatter.Deserialize(s);
            }

            List<KeyValue<string, KeyValuePairTree<string, string[]>>> result =
                new List<KeyValue<string, KeyValuePairTree<string, string[]>>>();
            foreach (var value in a)
                result.Add(value);

            return result;
        }

        private static IEnumerable GetTestSerializationTestCases
        {
            get
            {
                yield return new TestCaseData(new List<KeyValue<string, KeyValuePairTree<string, string[]>>>())
                                             .Returns(new List<KeyValue<string, KeyValuePairTree<string, string[]>>>());
                yield return new TestCaseData(new List<KeyValue<string, KeyValuePairTree<string, string[]>>>()
                {
                    new KeyValue<string, KeyValuePairTree<string, string[]>>(
                        "Hello", new KeyValuePairTree<string, string[]>(
                            new List<KeyValue<string, string[]>>()
                            {
                                new KeyValue<string, string[]>("fr", new string[] { "bonjour" }),
                                new KeyValue<string, string[]>("ru", new string[] { "Привет", "Здравствуй" })
                            })
                        )
                }).Returns(
                    new List<KeyValue<string, KeyValuePairTree<string, string[]>>>()
                    {
                        new KeyValue<string, KeyValuePairTree<string, string[]>>(
                            "Hello", new KeyValuePairTree<string, string[]>(
                                new List<KeyValue<string, string[]>>()
                                {
                                    new KeyValue<string, string[]>("fr", new string[] { "bonjour" }),
                                    new KeyValue<string, string[]>("ru", new string[] { "Привет", "Здравствуй" })
                                })
                            )
                    });
            }
        }

    }
}
