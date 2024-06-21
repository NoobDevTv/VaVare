using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using VaVare.Factories;
using VaVare.Generators.Common.Arguments.ArgumentTypes;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace VaVare.Generators.Common.Patterns
{
    public class DeclarationPattern : Pattern
    {
        private readonly TypeSyntax _typeSyntax;
        private readonly VariableDesignationSyntax _variableDesignationSyntax;

        public DeclarationPattern(TypeSyntax syntaxToken, VariableDesignationSyntax expressionSyntax)
        {
            _typeSyntax = syntaxToken;
            _variableDesignationSyntax = expressionSyntax;
        }

        public DeclarationPattern(TypeSyntax syntaxToken, string identifier)
        {
            _typeSyntax = syntaxToken;
            _variableDesignationSyntax = SingleVariableDesignation(Identifier(identifier));
        }

        public DeclarationPattern(TypeSyntax syntaxToken, SyntaxToken identifier)
        {
            _typeSyntax = syntaxToken;
            _variableDesignationSyntax = SingleVariableDesignation(identifier);
        }

        public override PatternSyntax GetPatternSyntax()
        {
            return DeclarationPattern(_typeSyntax, _variableDesignationSyntax);
        }
    }
}
