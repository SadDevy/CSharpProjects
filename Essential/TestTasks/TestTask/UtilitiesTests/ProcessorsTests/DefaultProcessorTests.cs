using System.Collections;
using NUnit.Framework;
using Utilities.Formatters;
using Utilities.Processors;
using Utilities.Writers;

namespace UtilitiesTests.ProcessorsTests
{
    [TestFixture]
    public class DefaultProcessorTests
    {
        [TestCaseSource(nameof(GetTestProcessTestCases))]
        public string TestProcess(string data, IFormatter formatter, IWriter writer)
        {
            DefaultProcessor processor = new DefaultProcessor(data, formatter, writer);
            
            using (ConsoleOutput actual = new ConsoleOutput())
            {
                processor.Process();

                Assert.IsNotNull(processor);
                return actual.GetOutput();
            }
        }

        private static IEnumerable GetTestProcessTestCases
        {
            get
            {
                yield return new TestCaseData("", new DefaultFormatter(), new ConsoleWriter())
                    .Returns("");

                yield return new TestCaseData("Some data", new DefaultFormatter(), new ConsoleWriter())
                    .Returns("");

                yield return new TestCaseData(
@"Shopping Basket 1:
1 16lb bag of Skittles at 16.00
1 Walkman at 99.99
1 bag of microwave Popcorn at 0.99

Shopping Basket 2:
1 imported bag of Vanilla-Hazelnut Coffee at 11.00
1 Imported Vespa at 15,001.25

Shopping Basket 3:
1 imported crate of Almond Snickers at 75.99
1 Discman at 55.00
1 Imported Bottle of Wine at 10.00
1 300# bag of Fair-Trade Coffee at 997.99", 
                    new DefaultFormatter(), 
                    new ConsoleWriter())
                    .Returns(
@"Output 1:
1 16lb bag of Skittles: 16.0
1 Walkman: 109.99
1 bag of microwave Popcorn: 0.99
Sales Taxes: 10.0
Total: 126.98

Output 2:
1 imported bag of Vanilla-Hazelnut Coffee: 11.55
1 Imported Vespa: 17,251.45
Sales Taxes: 2,250.75
Total: 17,263.0

Output 3:
1 imported crate of Almond Snickers: 79.79
1 Discman: 60.5
1 Imported Bottle of Wine: 11.5
1 300# bag of Fair-Trade Coffee: 997.99
Sales Taxes: 10.8
Total: 1,149.78

");
                yield return new TestCaseData(
@"Shopping Basket 1:
1 16lb bag of Skittles at 16.00
1 Walkman at 99.99
1 bag of microwave Popcorn at 0.99

Shopping Basket 2:
1 imported bag of Vanilla-Hazelnut Coffee at 11.00
1 Imported Vespa at 15,001.25

Shopping Basket 3:
1 imported crate of Almond Snickers at 75.99
1 Discman at 55.00
1 Imported Bottle of Wine at 10.00
1 300# bag of Fair-Trade Coffee at 997.99",
      null,
      new ConsoleWriter())
      .Returns("");
            }
        }
    }
}
