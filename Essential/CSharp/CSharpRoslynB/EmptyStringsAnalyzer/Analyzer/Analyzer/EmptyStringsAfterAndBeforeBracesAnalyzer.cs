using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Analyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class EmptyStringsAfterAndBeforeBracesAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = nameof(EmptyStringsAfterAndBeforeBracesAnalyzer);

        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.EmptyStringsAfterAndBeforeBracesAnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.EmptyStringsAfterAndBeforeBracesAnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.EmptyStringsAfterAndBeforeBracesAnalyzerDescription), Resources.ResourceManager, typeof(Resources));
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
                    Diagnostic diagnostic = null;
                    if (token.IsKind(SyntaxKind.OpenBraceToken))
                    {
                        var nextToken = token.GetNextToken();
                        var trivias = nextToken.GetAllTrivia().Where(n => n.IsKind(SyntaxKind.EndOfLineTrivia));

                        if (!trivias.Any())
                            continue;

                        SyntaxTrivia afterBraceTrivia = trivias.First();
                        if (!LeadingIsTriviaKind(root, afterBraceTrivia, SyntaxKind.EndOfLineTrivia))
                            continue;

                        diagnostic = Diagnostic.Create(Rule, afterBraceTrivia.GetLocation());
                    }

                    if (token.IsKind(SyntaxKind.CloseBraceToken))
                    {
                        var trivias = token.GetAllTrivia().Where(n => n.IsKind(SyntaxKind.EndOfLineTrivia) && n != token.GetAllTrivia().Last());

                        if (!trivias.Any())
                            continue;

                        SyntaxTrivia beforeBraceTrivia = trivias.Last();
                        if (!LeadingIsTriviaKind(root, beforeBraceTrivia, SyntaxKind.EndOfLineTrivia))
                            continue;

                        SyntaxTrivia leadingBeforeBraceTrivia = root.FindTrivia(beforeBraceTrivia.SpanStart - 1);
                        if (!leadingBeforeBraceTrivia.IsKind(SyntaxKind.EndOfLineTrivia))
                            continue;

                        diagnostic = Diagnostic.Create(Rule, beforeBraceTrivia.GetLocation());
                    }

                    if (diagnostic != null)
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
