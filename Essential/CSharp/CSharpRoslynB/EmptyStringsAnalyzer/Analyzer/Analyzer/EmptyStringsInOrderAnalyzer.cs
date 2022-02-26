using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Analyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class EmptyStringsInOrderAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "EmnptyStringsInOrder";

        public static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.EmptyStringsInOrderAnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.EmptyStringsInOrderAnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.EmptyStringsInOrderAnalyzerDescription), Resources.ResourceManager, typeof(Resources));
        private const string Category = "Naming";

        private static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxTreeAction(syntaxTreeContext =>
            {
                var root = syntaxTreeContext.Tree.GetRoot(syntaxTreeContext.CancellationToken);
                foreach (var token in root.DescendantTokens())
                {
                    List<Location> trivias = new List<Location>();

                    foreach (var trivia in token.GetAllTrivia())
                    {
                        if (!trivia.IsKind(SyntaxKind.EndOfLineTrivia))
                            continue;

                        if (token.Span.End == trivia.Span.Start)
                            continue;

                        SyntaxTrivia leadingTrivia = root.FindTrivia(trivia.SpanStart - 1);

                        if (LeadingIsTriviaKind(root, trivia, SyntaxKind.WhitespaceTrivia))
                            continue;

                        if (!LeadingIsTriviaKind(root, leadingTrivia, SyntaxKind.EndOfLineTrivia))
                            continue;

                        var diagnostic = Diagnostic.Create(Rule, trivia.GetLocation());
                        syntaxTreeContext.ReportDiagnostic(diagnostic);
                    }
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
