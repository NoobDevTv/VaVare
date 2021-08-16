using Microsoft.CodeAnalysis.CSharp.Syntax;
#pragma warning disable 1591

namespace VaVare.Generators.Common.Arguments.ArgumentTypes
{
    public interface IArgument
    {
        ArgumentSyntax GetArgumentSyntax();
    }
}
