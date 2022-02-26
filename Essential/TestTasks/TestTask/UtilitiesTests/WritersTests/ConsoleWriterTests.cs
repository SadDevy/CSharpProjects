using NUnit.Framework;
using Utilities.Writers;

namespace UtilitiesTests.WritersTests
{
    [TestFixture]
    public class ConsoleWriterTests
    {
        [Test]
        public void TestWrite()
        {
            string data = "Some data";
            const string expected = 
@"Some data
";

            using (ConsoleOutput actual = new ConsoleOutput())
            {
                IWriter writer = new ConsoleWriter();
                writer.Write(data);

                Assert.AreEqual(expected, actual.GetOutput());
            }
        }
    }
}
