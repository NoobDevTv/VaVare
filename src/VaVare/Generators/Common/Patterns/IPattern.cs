using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace VaVare.Generators.Common.Patterns
{
    public interface IPattern
    {
        PatternSyntax GetPatternSyntax();
    }
}
