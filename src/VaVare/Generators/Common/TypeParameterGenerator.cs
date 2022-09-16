using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using VaVare.Models;

namespace VaVare.Generators.Common;

public class TypeParameterGenerator
{
    public static TypeParameterListSyntax Create(params TypeParameter[] typeParameter)
    {
        return Create((IEnumerable<TypeParameter>)typeParameter);
    }

    public static TypeParameterListSyntax Create(IEnumerable<TypeParameter> typeParameter)
    {
        return SyntaxFactory.TypeParameterList(SyntaxFactory.SeparatedList(typeParameter.Select(CreateSyntax)));
    }

    public static TypeParameterListSyntax Create(IEnumerable<TypeParameterSyntax> typeParameter)
    {
        return SyntaxFactory.TypeParameterList(SyntaxFactory.SeparatedList(typeParameter));
    }

    public static TypeParameterSyntax CreateSyntax(TypeParameter typeParameter)
    {
        var tp = SyntaxFactory.TypeParameter(typeParameter.Name);
        switch (typeParameter.Variance)
        {
            case Variance.None:
                break;
            case Variance.Covariant:
                tp = tp.WithVarianceKeyword(SyntaxFactory.Token(SyntaxKind.OutKeyword));
                break;
            case Variance.Contravariant:
                tp = tp.WithVarianceKeyword(SyntaxFactory.Token(SyntaxKind.InKeyword));
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return tp.WithAttributeLists(AttributeGenerator.Create(typeParameter.Attributes));
    }
}