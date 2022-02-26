using Analyzer.CodeFixes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestHelper;

namespace Analyzer.Test
{
    [TestClass]
    public class EmptyStringsInOrderUnitTests : CodeFixVerifier
    {
        private const string OneEmptyString = @"
using System;

namespace AnalyzerTest
{
    class Program
    {

        static void Main(string[] args)
        {
            const int i = 0;
            Console.WriteLine(i);
        }
    }
}";

        private const string NotEmptyStringBetweenTwoEmpties = @"
using System;

namespace AnalyzerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            const int i = 0;

            //Hello World

            Console.WriteLine(i);
        }
    }
}";

        private const string WhitespacesStringBetweenTwoEmpties = @"
using System;

namespace AnalyzerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            const int i = 0;

                        

            Console.WriteLine(i);
        }
    }
}";

        //

        private const string TwoEmptyStringsCouldBeRemoved = @"
using System;

namespace AnalyzerTest
{
    class Program
    {


        static void Main(string[] args)
        {
            const int i = 0;
            Console.WriteLine(i);
        }
    }
}";

        private const string TwoEmptyStringsCouldBeRemovedFixed = @"
using System;

namespace AnalyzerTest
{
    class Program
    {

        static void Main(string[] args)
        {
            const int i = 0;
            Console.WriteLine(i);
        }
    }
}";

        [DataTestMethod]
        [DataRow(""),
         DataRow(OneEmptyString),
         DataRow(NotEmptyStringBetweenTwoEmpties),
         DataRow(WhitespacesStringBetweenTwoEmpties)]
        public void WhenTestCodeIsValidNoDiagnosticIsTriggered(string testCode)
        {
            VerifyCSharpDiagnostic(testCode);
        }

        [DataTestMethod]
        [DataRow(TwoEmptyStringsCouldBeRemoved, TwoEmptyStringsCouldBeRemovedFixed, 9, 1)]
        public void WhenDiagosticIsRaisedFixUpdatesCode(
            string test,
            string fixTest,
            int line,
            int column)
        {
            var expected = new DiagnosticResult
            {
                Id = EmptyStringsInOrderAnalyzer.DiagnosticId,
                Message = new LocalizableResourceString(nameof(Analyzer.Resources.EmptyStringsInOrderAnalyzerMessageFormat), Analyzer.Resources.ResourceManager, typeof(Analyzer.Resources)).ToString(),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", line, column)
                        }
            };

            VerifyCSharpDiagnostic(test, expected);

            VerifyCSharpFix(test, fixTest);
        }

        protected override CodeFixProvider GetCSharpCodeFixProvider()
        {
            return new EmptyStringsInOrderCodeFixProvider();
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new EmptyStringsInOrderAnalyzer();
        }
    }
}
