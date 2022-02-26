using Data;
using Data.Encryption;
using NUnit.Framework;
using System.Collections;

namespace DataTests.EncryprionTests
{
    [TestFixture]
    public class ShiftEncryptionDataSourceTests
    {
        private static string filePath;
        [SetUp]
        public void Init() => filePath = "test.txt";

        [Test]
        public void TestWriteAndReadData_AnyLine_Success()
        {
            const string expected = "Any line";
            const int offset = 1;

            StringDataSource dataSource = new StringDataSource();
            ShiftEncryptionDataSource shiftEncryption = new ShiftEncryptionDataSource(dataSource, offset);

            shiftEncryption.WriteData(expected);

            string actual = shiftEncryption.ReadData();

            Assert.AreEqual(expected, actual);
        }

        [TestCaseSource(nameof(GetTestEncodeTestCases))]
        public string TestEncode(string data, int offset)
        {
            FileDataSource dataSource = new FileDataSource(filePath);
            ShiftEncryptionDataSource shiftEncryption = new ShiftEncryptionDataSource(dataSource, offset);

            shiftEncryption.WriteData(data);

            return dataSource.ReadData();
        }

        private static IEnumerable GetTestEncodeTestCases
        {
            get
            {
                yield return new TestCaseData("hello", 2).Returns("jgnnq");
                yield return new TestCaseData("What", 3).Returns("Zkdw");
                yield return new TestCaseData("я", 1).Returns("а");
            }
        }

        [TestCaseSource(nameof(GetTestDecodeTestCases))]
        public string TestDecode(string data, int offset)
        {
            FileDataSource dataSource = new FileDataSource(filePath);
            ShiftEncryptionDataSource shiftEncryption = new ShiftEncryptionDataSource(dataSource, offset);

            dataSource.WriteData(data);

            return shiftEncryption.ReadData();
        }

        private static IEnumerable GetTestDecodeTestCases
        {
            get
            {
                yield return new TestCaseData("jgnnq", 2).Returns("hello");
                yield return new TestCaseData("Zkdw", 3).Returns("What");
                yield return new TestCaseData("а", 1).Returns("я");
            }
        }
    }
}
