using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using VaVare.Factories;
using VaVare.Generators.Common.Arguments.ArgumentTypes;
using VaVare.Generators.Common.BinaryExpressions;
using VaVare.Generators.Common.PatternExpressions;
using VaVare.Generators.Common.Patterns;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace VaVare.Statements
{
    /// <summary>
    /// Provides the functionality to generate selection statements.
    /// </summary>
    public class SelectionStatement
    {
        /// <summary>
        /// Create the statement syntax for a if-conditional.
        /// </summary>
        /// <param name="leftArgument">The left argument of the if-statement.</param>
        /// <param name="rightArgument">The right argument of the if-statement.</param>
        /// <param name="conditional">The conditional.</param>
        /// <param name="block">The block containing all statements.</param>
        /// <returns>The declared statement syntax.</returns>
        public StatementSyntax If(IArgument leftArgument, IArgument rightArgument, ConditionalStatements conditional, BlockSyntax block)
        {
            if (leftArgument == null)
            {
                throw new ArgumentNullException(nameof(leftArgument));
            }

            if (rightArgument == null)
            {
                throw new ArgumentNullException(nameof(rightArgument));
            }

            return
                IfStatement(
                    BinaryExpression(
                        ConditionalFactory.GetSyntaxKind(conditional),
                        leftArgument.GetArgumentSyntax().Expression,
                        rightArgument.GetArgumentSyntax().Expression),
                    block);
        }

        /// <summary>
        /// Create the statement syntax for a if-conditional with a single statement.
        /// </summary>
        /// <param name="leftArgument">The left argument of the if-statement.</param>
        /// <param name="rightArgument">The right argument of the if-statement.</param>
        /// <param name="conditional">The conditional.</param>
        /// <param name="expressionStatement">Statement inside the if.</param>
        /// <returns>The declared statement syntax.</returns>
        public StatementSyntax If(IArgument leftArgument, IArgument rightArgument, ConditionalStatements conditional, ExpressionStatementSyntax expressionStatement)
        {
            if (leftArgument == null)
            {
                throw new ArgumentNullException(nameof(leftArgument));
            }

            if (rightArgument == null)
            {
                throw new ArgumentNullException(nameof(rightArgument));
            }

            return
                IfStatement(
                    BinaryExpression(
                        ConditionalFactory.GetSyntaxKind(conditional),
                        leftArgument.GetArgumentSyntax().Expression,
                        rightArgument.GetArgumentSyntax().Expression),
                    expressionStatement);
        }

        /// <summary>
        /// Create the statement syntax for a if-conditional with a single statement.
        /// </summary>
        /// <param name="leftArgument">The left argument of the if-statement.</param>
        /// <param name="pattern">The right pattern of the if-statement.</param>
        /// <param name="expressionStatement">Statement inside the if.</param>
        /// <returns>The declared statement syntax.</returns>
        public StatementSyntax If(IArgument leftArgument, IPattern pattern, ExpressionStatementSyntax expressionStatement)
        {
            if (leftArgument == null)
            {
                throw new ArgumentNullException(nameof(leftArgument));
            }

            if (pattern == null)
            {
                throw new ArgumentNullException(nameof(pattern));
            }

            return
                IfStatement(
                    IsPatternExpression(
                        leftArgument.GetArgumentSyntax().Expression,
                        pattern.GetPatternSyntax()),
                    expressionStatement);
        }

        /// <summary>
        /// Create the statement syntax for a if-conditional with a single statement.
        /// </summary>
        /// <param name="leftArgument">The left argument of the if-statement.</param>
        /// <param name="pattern">The right pattern of the if-statement.</param>
        /// <param name="block">The block containing all statements.</param>
        /// <returns>The declared statement syntax.</returns>
        public StatementSyntax If(IArgument leftArgument, IPattern pattern, BlockSyntax block)
        {
            if (leftArgument == null)
            {
                throw new ArgumentNullException(nameof(leftArgument));
            }

            if (pattern == null)
            {
                throw new ArgumentNullException(nameof(pattern));
            }

            return
                IfStatement(
                    IsPatternExpression(
                        leftArgument.GetArgumentSyntax().Expression,
                        pattern.GetPatternSyntax()),
                    block);
        }

        /// <summary>
        /// Create the statement syntax for a if-conditional.
        /// </summary>
        /// <param name="binaryExpression">The binary expression to generate.</param>
        /// <param name="block">The block containing all statements.</param>
        /// <returns>The declared statement syntax.</returns>
        public StatementSyntax If(IBinaryExpression binaryExpression, BlockSyntax block)
        {
            if (binaryExpression == null)
            {
                throw new ArgumentNullException(nameof(binaryExpression));
            }

            return IfStatement(binaryExpression.GetBinaryExpression(), block);
        }

        /// <summary>
        /// Create the statement syntax for a if-conditional with pattern.
        /// </summary>
        /// <param name="patternExpression">The pattern expression to generate.</param>
        /// <param name="block">The block containing all statements.</param>
        /// <returns>The declared statement syntax.</returns>
        public StatementSyntax If(IPatternExpression patternExpression, BlockSyntax block)
        {
            if (patternExpression == null)
            {
                throw new ArgumentNullException(nameof(patternExpression));
            }

            return IfStatement(patternExpression.GetPatternExpression(), block);
        }

        /// <summary>
        /// Create the statement syntax for a if-conditional with a single statement.
        /// </summary>
        /// <param name="binaryExpression">The binary expression to generate.</param>
        /// <param name="expressionStatement">Statement inside the if.</param>
        /// <returns>The declared statement syntax.</returns>
        public StatementSyntax If(IBinaryExpression binaryExpression, ExpressionStatementSyntax expressionStatement)
        {
            if (binaryExpression == null)
            {
                throw new ArgumentNullException(nameof(binaryExpression));
            }

            return IfStatement(binaryExpression.GetBinaryExpression(), expressionStatement);
        }

        /// <summary>
        /// Create the statement syntax for a if-conditional with a single statement and a pattern expression.
        /// </summary>
        /// <param name="patternExpression">The pattern expression to generate.</param>
        /// <param name="expressionStatement">Statement inside the if.</param>
        /// <returns>The declared statement syntax.</returns>
        public StatementSyntax If(IPatternExpression patternExpression, ExpressionStatementSyntax expressionStatement)
        {
            if (patternExpression == null)
            {
                throw new ArgumentNullException(nameof(patternExpression));
            }

            return IfStatement(patternExpression.GetPatternExpression(), expressionStatement);
        }
    }
}
