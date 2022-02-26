using System;
using Entities.Models;
using NUnit.Framework;
using Utilities;

namespace UtilitiesTests
{
    [TestFixture]
    public class SerializerTests
    {
        private static string filePath;

        [SetUp]
        public void Setup()
        {
            filePath = "TestFile.xml";
        }

        [Test]
        public void TestSerialize_TestAndFilePathNotNull_Success()
        {
            const int testId = 100;
            Test expected = new Test() { Id = testId };

            Serializer.Serialize(expected, testId, filePath);

            Test actual = Serializer.Deserialize(filePath);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestSerialize_TestIsNull_Exception()
        {
            static void A()
            {
                const int testId = 100;
                Test expected = null;

                Serializer.Serialize(expected, testId, filePath);
            }

            Assert.Throws<ArgumentNullException>(A);
        }

        [Test]
        public void TestSerialize_FilePathIsNull_Exception()
        {
            static void A()
            {
                const int testId = 100;
                Test expected = new Test() { Id = testId };

                Serializer.Serialize(expected, testId, null);
            }

            Assert.Throws<ArgumentNullException>(A);
        }

        [Test]
        public void TestSerialize_FilePathIsEmpty_Exception()
        {
            static void A()
            {
                const int testId = 100;
                Test expected = new Test() { Id = testId };

                Serializer.Serialize(expected, testId, string.Empty);
            }

            Assert.Throws<ArgumentNullException>(A);
        }

        [Test]
        public void TestDeserialize_FilePathExists_Success()
        {
            Test test = Serializer.Deserialize(filePath);

            Assert.IsNotNull(test);
        }

        [Test]
        public void TestDeserialize_FilePathNotExists_Exception()
        {
            static void A()
            {
                Test test = Serializer.Deserialize("somePath");
            }

            Assert.Throws<InvalidOperationException>(A);
        }

        [Test]
        public void TestDeserialize_FilePathIsNull_Exception()
        {
            static void A()
            {
                Test test = Serializer.Deserialize(null);
            }

            Assert.Throws<ArgumentNullException>(A);
        }

        [Test]
        public void TestDeserialize_FilePathIsEmpty_Exception()
        {
            static void A()
            {
                Test test = Serializer.Deserialize(string.Empty);
            }

            Assert.Throws<ArgumentNullException>(A);
        }
    }
}
