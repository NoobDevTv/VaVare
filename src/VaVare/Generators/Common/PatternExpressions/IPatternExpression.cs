using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace VaVare.Generators.Common.PatternExpressions
{
    public interface IPatternExpression
    {
        /// <summary>
        /// Get the generated pattern expression.
        /// </summary>
        /// <returns>The generated pattern expression.</returns>
        ExpressionSyntax GetPatternExpression();
    }
}
