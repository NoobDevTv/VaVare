using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using VaVare.Builders.Base;
using VaVare.Builders.BuildMembers;
using VaVare.Generators.Class;
using VaVare.Models;

namespace VaVare.Builders
{
    /// <summary>
    /// Provides a builder to generate a struct.
    /// </summary>
    public class StructBuilder : TypeBuilderBase<StructBuilder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StructBuilder"/> class.
        /// </summary>
        /// <param name="name">Name of the struct.</param>
        /// <param name="namespace">Name of the struct namespace.</param>
        public StructBuilder(string name, string @namespace)
         : base(name, @namespace)
        {
        }

        /// <summary>
        /// Add struct fields.
        /// </summary>
        /// <param name="fields">A set of wanted fields.</param>
        /// <returns>The current struct builder.</returns>
        public StructBuilder WithFields(params Field[] fields)
        {
            return With(new FieldBuildMember(fields.Select(FieldGenerator.Create)));
        }

        /// <summary>
        /// Add struct fields.
        /// </summary>
        /// <param name="fields">An array of already struct fields.</param>
        /// <returns>The current struct builder</returns>
        public StructBuilder WithFields(params FieldDeclarationSyntax[] fields)
        {
            return With(new FieldBuildMember(fields));
        }

        /// <summary>
        /// Add struct constructor.
        /// </summary>
        /// <param name="constructor">An already generated constructor.</param>
        /// <returns>The current struct builder.</returns>
        public StructBuilder WithConstructor(params ConstructorDeclarationSyntax[] constructor)
        {
            return With(new ConstructorBuildMember(constructor));
        }

        protected override TypeDeclarationSyntax BuildBase()
        {
            return SyntaxFactory.StructDeclaration(Name).WithBaseList(CreateBaseList()).WithModifiers(CreateModifiers());
        }
    }
}
