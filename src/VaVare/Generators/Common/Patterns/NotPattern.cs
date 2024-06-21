using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using VaVare.Factories;
using VaVare.Generators.Common.Arguments.ArgumentTypes;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace VaVare.Generators.Common.Patterns
{
    public class NotPattern : Pattern
    {
        private readonly PatternSyntax _pattern;

        public NotPattern(PatternSyntax pattern)
        {
            _pattern = pattern;
        }

        public NotPattern(Pattern pattern)
        {
            _pattern = pattern.GetPatternSyntax();
        }

        public override PatternSyntax GetPatternSyntax()
        {
            return UnaryPattern(_pattern);
        }
    }
}
