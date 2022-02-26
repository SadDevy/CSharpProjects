using Analyzer.CodeFixes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestHelper;

namespace Analyzer.Test
{
    [TestClass]
    public class UnitTest : CodeFixVerifier
    {

        // This section contains code to analyze where no diagnostic should e reported

        // <SnippetVariableAssigned>
        private const string VariableAssigned = @"
using System;

namespace MakeConstTest
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 0;
            Console.WriteLine(i++);
        }
    }
}";
        // </SnippetDeclarationIsntString>

        // This section contains code to analyze where the diagnostic should trigger,
        // followed by the code after the fix has been applied.

        //<SnippetFirstFixTest>
        private const string StringDeclarationUsesVar = @"
using System;

namespace MakeConstTest
{
    class Program
    {
        static void Main(string[] args)
        {
            const string item = ""abc"";
            
            if (true)
                return;
        }
    }
}";
        private const string StringDeclarationUsesVarFixedHasType = @"
using System;

namespace MakeConstTest
{
    class Program
    {
        static void Main(string[] args)
        {
            const string item = ""abc"";
            
            if (true)
            {
                return;
            }
        }
    }
}";
        // </SnippetVarDeclarations>

        // <SnippetFinishedTests>
        //No diagnostics expected to show up
        [DataTestMethod]
        [DataRow(""),
         DataRow(VariableAssigned)]
        public void WhenTestCodeIsValidNoDiagnosticIsTriggered(string testCode)
        {
            VerifyCSharpDiagnostic(testCode);
        }

        [DataTestMethod]
        [DataRow(StringDeclarationUsesVar, StringDeclarationUsesVarFixedHasType, 13, 17)]
        public void WhenDiagosticIsRaisedFixUpdatesCode(
            string test,
            string fixTest,
            int line,
            int column)
        {
            var expected = new DiagnosticResult
            {
                Id = AnalyzerAnalyzer.DiagnosticId,
                Message = new LocalizableResourceString(nameof(Analyzer.Resources.AnalyzerMessageFormat), Analyzer.Resources.ResourceManager, typeof(Analyzer.Resources)).ToString(),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", line, column)
                        }
            };

            VerifyCSharpDiagnostic(test, expected);

            VerifyCSharpFix(test, fixTest);
        }
        // </SnippetFinishedTests>

        protected override CodeFixProvider GetCSharpCodeFixProvider()
        {
            return new AnalyzerCodeFixProvider();
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new AnalyzerAnalyzer();
        }
    }
}
