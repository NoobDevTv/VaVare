using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using VaVare.Builders.BuildMembers;

namespace VaVare.Builders.BuilderHelpers
{
    internal class MemberHelper
    {
        public MemberHelper()
        {
            Members = new List<IBuildMember>();
        }

        public List<IBuildMember> Members { get; }

        public MemberHelper AddMembers(params IBuildMember[] members)
        {
            Members.AddRange(members);
            return this;
        }

        public TypeDeclarationSyntax BuildMembers(TypeDeclarationSyntax type)
        {
            return type.WithMembers(BuildSyntaxList());
        }

        public CompilationUnitSyntax BuildMembers(CompilationUnitSyntax compilationUnitSyntax)
        {
            return compilationUnitSyntax.WithMembers(BuildSyntaxList());
        }

        public SyntaxList<MemberDeclarationSyntax> BuildSyntaxList()
        {
            var members = default(SyntaxList<MemberDeclarationSyntax>);

            foreach (var member in Members)
            {
                members = member.AddMember(members);
            }

            return members;
        }
    }
}
