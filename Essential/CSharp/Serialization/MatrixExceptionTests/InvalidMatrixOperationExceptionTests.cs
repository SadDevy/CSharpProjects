using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using MatrixException;
using NUnit.Framework;

namespace MatrixExceptionTests
{
    [TestFixture]
    public class InvalidMatrixOperationExceptionTests
    {
        [Test]
        public void TestBinarySerialization()
        {
            const int expectedRowCount = 2;
            const int expectedColumnCount = 2;
            MatrixSize size = new MatrixSize(expectedRowCount, expectedColumnCount);

            InvalidMatrixOperationException e = new InvalidMatrixOperationException(size, size);

            using (Stream s = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(s, e);
                s.Position = 0;
                e = (InvalidMatrixOperationException)formatter.Deserialize(s);
            }

            Assert.AreEqual(expectedRowCount, e.ARowCount); 
            Assert.AreEqual(expectedColumnCount, e.AColumnCount);
            Assert.AreEqual(expectedRowCount, e.BRowCount);
            Assert.AreEqual(expectedColumnCount, e.BRowCount);
        }
    }
}
