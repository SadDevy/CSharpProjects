using Data;
using Data.Compression;
using NUnit.Framework;

namespace DataTests.CompressionTests
{
    [TestFixture]
    public class ZippedDataSourceTests
    {
        [Test]
        public void TestWriteAndReadData_AnyLine_Success()
        {
            const string expected = "Any line";

            StringDataSource dataSource = new StringDataSource();
            ZippedDataSource zipped = new ZippedDataSource(dataSource);

            zipped.WriteData(expected);

            string actual = zipped.ReadData();

            Assert.AreEqual(expected, actual);
        }
    }
}
