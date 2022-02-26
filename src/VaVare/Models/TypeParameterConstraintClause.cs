using System.Collections.Generic;

namespace VaVare.Models;

public class TypeParameterConstraintClause
{
    public TypeParameterConstraintClause(string identifier, params TypeParameterConstraint[] constraints)
    {
        Identifier = identifier;
        Constraints = new ();
        _ = WithConstraints(constraints);
    }

    public string Identifier { get; }

    public List<TypeParameterConstraint> Constraints { get; }

    public TypeParameterConstraintClause WithConstraints(IEnumerable<TypeParameterConstraint> constraints)
    {
        Constraints.Clear();
        Constraints.AddRange(constraints);
        return this;
    }

    public TypeParameterConstraintClause WithConstraints(params TypeParameterConstraint[] constraints)
    {
        return WithConstraints((IEnumerable<TypeParameterConstraint>)constraints);
    }

    public TypeParameterConstraintClause AddConstraints(IEnumerable<TypeParameterConstraint> constraints)
    {
        Constraints.AddRange(constraints);
        return this;
    }

    public TypeParameterConstraintClause AddConstraints(params TypeParameterConstraint[] constraints)
    {
        return AddConstraints((IEnumerable<TypeParameterConstraint>)constraints);
    }
}