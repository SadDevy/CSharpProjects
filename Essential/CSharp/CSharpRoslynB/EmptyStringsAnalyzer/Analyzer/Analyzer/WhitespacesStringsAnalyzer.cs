using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Analyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class WhitespacesStringsAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = nameof(WhitespacesStringsAnalyzer);

        public static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.WhitespacesStringsAnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.WhitespacesStringsAnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.WhitespacesStringsAnalyzerDescription), Resources.ResourceManager, typeof(Resources));
        private const string Category = "Naming";

        private static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxTreeAction(syntaxTreeContext =>
            {
                var root = syntaxTreeContext.Tree.GetRoot(syntaxTreeContext.CancellationToken);
                foreach (var trivia in root.DescendantTrivia())
                {
                    if (!trivia.IsKind(SyntaxKind.EndOfLineTrivia))
                        continue;

                    SyntaxTrivia leadingTrivia = root.FindTrivia(trivia.SpanStart - 1);
                    if (!LeadingIsTriviaKind(root, trivia, SyntaxKind.WhitespaceTrivia))
                        continue;

                    var diagnostic = Diagnostic.Create(Rule, leadingTrivia.GetLocation());
                    syntaxTreeContext.ReportDiagnostic(diagnostic);
                }
            });
        }

        private bool LeadingIsTriviaKind(SyntaxNode root, SyntaxTrivia trivia, SyntaxKind kind)
        {
            SyntaxTrivia leadingBeforeBraceTrivia = root.FindTrivia(trivia.SpanStart - 1);
            return leadingBeforeBraceTrivia.IsKind(kind);
        }
    }
}
