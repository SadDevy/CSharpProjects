using Data;
using NUnit.Framework;

namespace DataTests
{
    [TestFixture]
    public class StringDataSourceTests
    {
        [Test]
        public void TestWriteAndReadData_AnyLine_Success()
        {
            const string expected = "Any line";

            StringDataSource dataSource = new StringDataSource();
            dataSource.WriteData(expected);

            string actual = dataSource.ReadData();

            Assert.AreEqual(expected, actual);
        }
    }
}
