using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace VaVare.Generators.Common.Patterns
{
    public class OrPattern : Pattern
    {
        private readonly PatternSyntax _left;
        private readonly PatternSyntax _right;

        public OrPattern(PatternSyntax left, PatternSyntax right)
        {
            _left = left;
            _right = right;
        }

        public OrPattern(Pattern left, Pattern right)
        {
            _left = left.GetPatternSyntax();
            _right = right.GetPatternSyntax();
        }

        public override PatternSyntax GetPatternSyntax()
        {
            return BinaryPattern(SyntaxKind.OrPattern, _left, _right);
        }
    }
}
