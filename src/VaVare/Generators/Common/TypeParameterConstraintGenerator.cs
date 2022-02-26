using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using VaVare.Models;

namespace VaVare.Generators.Common;

public static class TypeParameterConstraintGenerator
{
    public static SyntaxList<TypeParameterConstraintClauseSyntax> Create(params TypeParameterConstraintClause[] clauses)
    {
        return Create((IEnumerable<TypeParameterConstraintClause>)clauses);
    }

    public static SyntaxList<TypeParameterConstraintClauseSyntax> Create(IEnumerable<TypeParameterConstraintClause> clauses)
    {
        return SyntaxFactory.List(clauses.Select(Create));
    }

    public static TypeParameterConstraintClauseSyntax Create(TypeParameterConstraintClause clause)
    {
        return Create(clause.Identifier, clause.Constraints);
    }

    public static TypeParameterConstraintClauseSyntax Create(string identifier, params TypeParameterConstraint[] constraints)
    {
        return Create(identifier, (IEnumerable<TypeParameterConstraint>)constraints);
    }

    public static TypeParameterConstraintClauseSyntax Create(string identifier, IEnumerable<TypeParameterConstraint> constraints)
    {
        return SyntaxFactory.TypeParameterConstraintClause(identifier).WithConstraints(SyntaxFactory.SeparatedList(constraints.Select(Create)));
    }

    private static TypeParameterConstraintSyntax Create(TypeParameterConstraint constraint)
    {
        return constraint.Type switch
        {
            TypeParameterConstraint.ConstraintType.Constructor
                => SyntaxFactory.ConstructorConstraint(),
            TypeParameterConstraint.ConstraintType.Class
                => SyntaxFactory.ClassOrStructConstraint(SyntaxKind.ClassConstraint),
            TypeParameterConstraint.ConstraintType.Struct
                => SyntaxFactory.ClassOrStructConstraint(SyntaxKind.StructConstraint),
            TypeParameterConstraint.ConstraintType.Default
                => SyntaxFactory.DefaultConstraint(),
            TypeParameterConstraint.ConstraintType.Type
                => SyntaxFactory.TypeConstraint(SyntaxFactory.IdentifierName(constraint.ConstraintIdentifier)),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}