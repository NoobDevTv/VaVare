using Microsoft.CodeAnalysis.CSharp.Syntax;

using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace VaVare.Generators.Common.PatternExpressions
{
    public class IsPatternExpression : IPatternExpression
    {
        private readonly ExpressionSyntax _expressionSyntax;
        private readonly PatternSyntax _patternSyntax;

        public IsPatternExpression(ExpressionSyntax expressionSyntax, PatternSyntax patternSyntax)
        {
            _expressionSyntax = expressionSyntax;
            _patternSyntax = patternSyntax;
        }

        public ExpressionSyntax GetPatternExpression()
        {
            return IsPatternExpression(_expressionSyntax, _patternSyntax);
        }
    }
}
