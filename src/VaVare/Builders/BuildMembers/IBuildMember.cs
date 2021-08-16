using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace VaVare.Builders.BuildMembers
{
    public interface IBuildMember
    {
        SyntaxList<MemberDeclarationSyntax> AddMember(SyntaxList<MemberDeclarationSyntax> members);
    }
}
