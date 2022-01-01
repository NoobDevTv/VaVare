using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace VaVare.Generators.Common.Patterns
{
    public class AndPattern : Pattern
    {
        private readonly PatternSyntax _left;
        private readonly PatternSyntax _right;

        public AndPattern(PatternSyntax left, PatternSyntax right)
        {
            _left = left;
            _right = right;
        }

        public AndPattern(Pattern left, Pattern right)
        {
            _left = left.GetPatternSyntax();
            _right = right.GetPatternSyntax();
        }

        public override PatternSyntax GetPatternSyntax()
        {
            return BinaryPattern(SyntaxKind.AndPattern, _left, _right);
        }
    }
}
