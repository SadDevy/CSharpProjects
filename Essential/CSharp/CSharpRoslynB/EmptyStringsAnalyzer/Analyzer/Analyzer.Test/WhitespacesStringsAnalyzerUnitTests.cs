using Analyzer.CodeFixes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestHelper;

namespace Analyzer.Test
{
    [TestClass]
    public class WhitespacesStringsAnalyzerUnitTests : CodeFixVerifier
    {
        private const string SpacesBeforeCode = @"
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

        private const string ThereIsNoSpacesStrings = @"
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

        private const string SpacesAfterComment = @"
using System;

namespace AnalyzerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            const int i = 0;
            //Coment            
            Console.WriteLine(i);
        }
    }
}";

        private const string SpacesBeforeComment = @"
using System;

namespace AnalyzerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            const int i = 0;
                    //Coment
            Console.WriteLine(i);
        }
    }
}";

        private const string OnlySpacesCouldBeRemoved = @"
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

        private const string OnlySpacesCouldBeRemovedFixed = @"
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

        private const string SpacesAfterCodeCouldBeRemoved = @"
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

        private const string SpacesAfterCodeCouldBeRemovedFixed = @"
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
         DataRow(SpacesBeforeCode),
         DataRow(ThereIsNoSpacesStrings),
         DataRow(SpacesAfterComment),
         DataRow(SpacesBeforeComment)]
        public void WhenTestCodeIsValidNoDiagnosticIsTriggered(string testCode)
        {
            VerifyCSharpDiagnostic(testCode);
        }

        [DataTestMethod]
        [DataRow(OnlySpacesCouldBeRemoved, OnlySpacesCouldBeRemovedFixed, 8, 1),
         DataRow(SpacesAfterCodeCouldBeRemoved, SpacesAfterCodeCouldBeRemovedFixed, 10, 29)]
        public void WhenDiagosticIsRaisedFixUpdatesCode(
            string test,
            string fixTest,
            int line,
            int column)
        {
            var expected = new DiagnosticResult
            {
                Id = WhitespacesStringsAnalyzer.DiagnosticId,
                Message = new LocalizableResourceString(nameof(Analyzer.Resources.WhitespacesStringsAnalyzerMessageFormat), Analyzer.Resources.ResourceManager, typeof(Analyzer.Resources)).ToString(),
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
            return new WhiteSpacesStringsCodeFixProvider();
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new WhitespacesStringsAnalyzer();
        }
    }
}
