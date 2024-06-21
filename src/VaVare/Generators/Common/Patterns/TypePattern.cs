using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace VaVare.Generators.Common.Patterns
{
    public class TypePattern : Pattern
    {
        private readonly TypeSyntax _typeSyntax;

        public TypePattern(TypeSyntax typeSyntax)
        {
            _typeSyntax = typeSyntax;
        }

        public override PatternSyntax GetPatternSyntax()
        {
            return TypePattern(_typeSyntax);
        }
    }
}
