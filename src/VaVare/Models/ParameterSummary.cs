namespace VaVare.Models;

/// <summary>
/// Summary for a parameter.
/// </summary>
/// <param name="ParameterName">The parameter name of the parameter the summary is for.</param>
/// <param name="Summary">The summary text.</param>
/// <param name="IsTypeParameter">Whether the referenced parameter is a type parameter.</param>
public record struct ParameterSummary(string ParameterName, string Summary, bool IsTypeParameter)
{
    public ParameterSummary(Parameter parameter)
        : this(parameter.Name, parameter.XmlDocumentation, false)
    {
    }

    public ParameterSummary(TypeParameter parameter)
        : this(parameter.Name, parameter.XmlDocumentation, true)
    {
    }
}