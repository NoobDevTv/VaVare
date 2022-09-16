namespace VaVare.Models;

public enum Variance
{
    None,
    Covariant = 1,
    Contravariant = 2,
    Out = Covariant,
    In = Contravariant,
}

public class TypeParameter
{
    public TypeParameter(string name, Variance variance = Variance.None, string xmlDocumentation = null, params Attribute[] attributes)
    {
        Name = name;
        Variance = variance;
        XmlDocumentation = xmlDocumentation;
        Attributes = attributes;
    }

    public string Name { get; }

    public Variance Variance { get; }

    public Attribute[] Attributes { get; }

    /// <summary>
    /// Gets the xml documentation.
    /// </summary>
    public string XmlDocumentation { get; }
}