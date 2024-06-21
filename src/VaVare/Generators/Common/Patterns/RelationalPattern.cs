using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using VaVare.Factories;
using VaVare.Generators.Common.Arguments.ArgumentTypes;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace VaVare.Generators.Common.Patterns
{
    public class RelationalPattern : Pattern
    {
        private readonly SyntaxToken _syntaxToken;
        private readonly ExpressionSyntax _expressionSyntax;

        public RelationalPattern(SyntaxToken syntaxToken, ExpressionSyntax expressionSyntax)
        {
            _syntaxToken = syntaxToken;
            _expressionSyntax = expressionSyntax;
        }

        public RelationalPattern(ConditionalStatements conditionalStatement, ExpressionSyntax expressionSyntax)
        {
            _syntaxToken = Token(ConditionalFactory.GetSyntaxKindToken(conditionalStatement));
            _expressionSyntax = expressionSyntax;
        }

        public RelationalPattern(ConditionalStatements conditionalStatement, IArgument argument)
        {
            _syntaxToken = Token(ConditionalFactory.GetSyntaxKindToken(conditionalStatement));
            _expressionSyntax = argument.GetArgumentSyntax().Expression;
        }

        public override PatternSyntax GetPatternSyntax()
        {
            return RelationalPattern(_syntaxToken, _expressionSyntax);
        }
    }
}
