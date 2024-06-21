using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace VaVare.Generators.Common.Patterns
{
    /// <summary>
    /// Provides the base class from which the classes that represent pattern derived.
    /// </summary>
    public abstract class Pattern : IPattern
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Pattern"/> class.
        /// </summary>
        protected Pattern()
        {
        }

        public abstract PatternSyntax GetPatternSyntax();
    }
}
