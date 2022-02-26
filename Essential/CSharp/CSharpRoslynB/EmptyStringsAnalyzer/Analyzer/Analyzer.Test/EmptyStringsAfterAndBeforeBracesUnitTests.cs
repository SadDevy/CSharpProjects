using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestHelper;

namespace Analyzer.Test
{
    [TestClass]
    public class EmptyStringsAfterAndBeforeBracesUnitTests : CodeFixVerifier
    {
        private const string ThereIsNoEmptyString = @"
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

        private const string NotEmptyStringAfterOpenBrace = @"
using System;

namespace AnalyzerTest
{
    class Program
    {
        private string str = string.Empty;        

        static void Main(string[] args)
        {
            const int i = 0;
            Console.WriteLine(i);
        }
    }
}";

        private const string NotEmptyStringBeforeCloseBrace = @"
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

        private string str = string.Empty;        
    }
}";

        //

        private const string EmptyStringAfterOpenBraceCouldBeRemoved = @"
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

        private const string EmptyStringAfterOpenBraceCouldBeRemovedFixed = @"
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

        private const string EmptyStringBeforeCloseBraceCouldBeRemoved = @"
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

        private const string EmptyStringBeforeCloseBraceCouldBeRemovedFixed = @"
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
         DataRow(ThereIsNoEmptyString),
         DataRow(NotEmptyStringAfterOpenBrace),
         DataRow(NotEmptyStringBeforeCloseBrace)]
        public void WhenTestCodeIsValidNoDiagnosticIsTriggered(string testCode)
        {
            VerifyCSharpDiagnostic(testCode);
        }

        [DataTestMethod]
        [DataRow(EmptyStringAfterOpenBraceCouldBeRemoved, EmptyStringAfterOpenBraceCouldBeRemovedFixed, 8, 1),
         DataRow(EmptyStringBeforeCloseBraceCouldBeRemoved, EmptyStringBeforeCloseBraceCouldBeRemovedFixed, 12, 1)]
        public void WhenDiagosticIsRaisedFixUpdatesCode(
            string test,
            string fixTest,
            int line,
            int column)
        {
            var expected = new DiagnosticResult
            {
                Id = EmptyStringsAfterAndBeforeBracesAnalyzer.DiagnosticId,
                Message = new LocalizableResourceString(nameof(Analyzer.Resources.EmptyStringsAfterAndBeforeBracesAnalyzerMessageFormat), Analyzer.Resources.ResourceManager, typeof(Analyzer.Resources)).ToString(),
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
            return new EmptyStringsAfterAndBeforeBracesCodeFixProvider();
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new EmptyStringsAfterAndBeforeBracesAnalyzer();
        }
    }
}
