using Microsoft.CodeAnalysis.CSharp.Syntax;
using VaVare.Generators.Common.Arguments.ArgumentTypes;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace VaVare.Generators.Common.Patterns
{
    public class ConstantPattern : Pattern
    {
        private readonly ExpressionSyntax _expressionSyntax;

        public ConstantPattern(ExpressionSyntax expression)
        {
            _expressionSyntax = expression;
        }

        public ConstantPattern(IArgument argument)
        {
            _expressionSyntax = argument.GetArgumentSyntax().Expression;
        }

        public override PatternSyntax GetPatternSyntax()
        {
            return ConstantPattern(_expressionSyntax);
        }
    }
}
