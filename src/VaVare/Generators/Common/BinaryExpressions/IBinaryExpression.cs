using Microsoft.CodeAnalysis.CSharp.Syntax;
#pragma warning disable 1591

namespace VaVare.Generators.Common.BinaryExpressions
{
    public interface IBinaryExpression
    {
        /// <summary>
        /// Get the generated binary expression.
        /// </summary>
        /// <returns>The generated binary expression.</returns>
        ExpressionSyntax GetBinaryExpression();
    }
}
