using System;
using Microsoft.CodeAnalysis.CSharp;

namespace VaVare.Factories
{
    internal static class ConditionalFactory
    {
        public static SyntaxKind GetSyntaxKind(ConditionalStatements conditional)
        {
            switch (conditional)
            {
                case ConditionalStatements.Equal:
                    return SyntaxKind.EqualsExpression;
                case ConditionalStatements.NotEqual:
                    return SyntaxKind.NotEqualsExpression;
                case ConditionalStatements.GreaterThan:
                    return SyntaxKind.GreaterThanExpression;
                case ConditionalStatements.GreaterThanOrEqual:
                    return SyntaxKind.GreaterThanOrEqualExpression;
                case ConditionalStatements.LessThan:
                    return SyntaxKind.LessThanExpression;
                case ConditionalStatements.LessThanOrEqual:
                    return SyntaxKind.LessThanOrEqualExpression;
                case ConditionalStatements.Is:
                    return SyntaxKind.IsExpression;
                default:
                    throw new ArgumentOutOfRangeException(nameof(conditional), conditional, null);
            }
        }

        public static SyntaxKind GetSyntaxKindToken(ConditionalStatements conditional)
        {
            switch (conditional)
            {
                case ConditionalStatements.Equal:
                    return SyntaxKind.EqualsToken;
                case ConditionalStatements.NotEqual:
                    return SyntaxKind.ExclamationEqualsToken;
                case ConditionalStatements.GreaterThan:
                    return SyntaxKind.GreaterThanToken;
                case ConditionalStatements.GreaterThanOrEqual:
                    return SyntaxKind.GreaterThanEqualsToken;
                case ConditionalStatements.LessThan:
                    return SyntaxKind.LessThanToken;
                case ConditionalStatements.LessThanOrEqual:
                    return SyntaxKind.LessThanEqualsToken;
                default:
                    throw new ArgumentOutOfRangeException(nameof(conditional), conditional, null);
            }
        }
    }
}
