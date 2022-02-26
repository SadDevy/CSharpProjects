using System.IO;
using System.Text;
using Data;
using NUnit.Framework;

namespace DataTests
{
    [TestFixture]
    public class FileDataSourceTests
    {
        private string filePath;
        private string expected;

        [SetUp]
        public void SetUpFields()
        {
            filePath = "testFile.txt";
            expected = "Any line";
        }

        [Test]
        public void TestWriteData_AnyLine_Success()
        {
            FileDataSource dataSource = new FileDataSource(filePath);
            dataSource.WriteData(expected);

            string actual;
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                byte[] data = new byte[fileStream.Length];
                fileStream.Read(data);

                actual = Encoding.UTF8.GetString(data);
            }

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestReadData_AnyLine_Success()
        {
            FileDataSource dataSource = new FileDataSource(filePath);
            dataSource.WriteData(expected);

            string actual = dataSource.ReadData();

            Assert.AreEqual(expected, actual);
        }
    }
}
