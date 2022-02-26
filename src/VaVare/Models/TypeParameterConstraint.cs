using System;
using Microsoft.CodeAnalysis.CSharp;

namespace VaVare.Models;

public class TypeParameterConstraint
{
    public enum ConstraintType
    {
        Constructor,
        Class,
        Struct,
        Default,
        Type,
    }

    public TypeParameterConstraint(ConstraintType constraintType)
    {
        switch (constraintType)
        {
            case ConstraintType.Type:
                throw new ArgumentException("Use the overload with the identifier to constraint to a specific type.");
            case ConstraintType.Constructor:
            case ConstraintType.Class:
            case ConstraintType.Struct:
            case ConstraintType.Default:
                Type = constraintType;
                ConstraintIdentifier = null;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(constraintType));
        }
    }

    public TypeParameterConstraint(string identifier)
    {
        ConstraintIdentifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
        Type = ConstraintType.Type;
    }

    public ConstraintType Type { get; }

    public string ConstraintIdentifier { get; }
}