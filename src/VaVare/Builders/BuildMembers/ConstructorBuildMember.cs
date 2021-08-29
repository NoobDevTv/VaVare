using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace VaVare.Builders.BuildMembers
{
    public class ConstructorBuildMember : IBuildMember
    {
        private readonly IEnumerable<ConstructorDeclarationSyntax> _constructorDeclarationSyntax;

        public ConstructorBuildMember(IEnumerable<ConstructorDeclarationSyntax> constructorDeclarationSyntax)
        {
            _constructorDeclarationSyntax = constructorDeclarationSyntax;
        }

        public SyntaxList<MemberDeclarationSyntax> AddMember(SyntaxList<MemberDeclarationSyntax> members)
        {
            foreach (var constructorDeclarationSyntax in _constructorDeclarationSyntax)
            {
                members = members.Add(constructorDeclarationSyntax);
            }

            return members;
        }
    }
}
