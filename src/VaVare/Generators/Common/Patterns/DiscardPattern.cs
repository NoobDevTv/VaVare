using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace VaVare.Generators.Common.Patterns
{
    public class DiscardPattern : Pattern
    {
        public override PatternSyntax GetPatternSyntax()
        {
            return DiscardPattern();
        }
    }
}
