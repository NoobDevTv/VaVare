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
    /// Provides a builder to generate a class.
    /// </summary>
    public class ClassBuilder : TypeBuilderBase<ClassBuilder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClassBuilder"/> class.
        /// </summary>
        /// <param name="name">Name of the class.</param>
        /// <param name="namespace">Name of the class namespace.</param>
        public ClassBuilder(string name, string @namespace)
         : base(name, @namespace)
        {
        }

        /// <summary>
        /// Add class fields.
        /// </summary>
        /// <param name="fields">A set of wanted fields.</param>
        /// <returns>The current class builder.</returns>
        public ClassBuilder WithFields(params Field[] fields)
        {
            return With(new FieldBuildMember(fields.Select(FieldGenerator.Create)));
        }

        /// <summary>
        /// Add class fields.
        /// </summary>
        /// <param name="fields">An array of already declared fields.</param>
        /// <returns>The current class builder.</returns>
        public ClassBuilder WithFields(params FieldDeclarationSyntax[] fields)
        {
            return With(new FieldBuildMember(fields));
        }

        /// <summary>
        /// Add class constructor.
        /// </summary>
        /// <param name="constructor">An already generated constructor.</param>
        /// <returns>The current class builder.</returns>
        public ClassBuilder WithConstructor(params ConstructorDeclarationSyntax[] constructor)
        {
            return With(new ConstructorBuildMember(constructor));
        }

        protected override TypeDeclarationSyntax BuildBase()
        {
            return SyntaxFactory.ClassDeclaration(Name).WithBaseList(CreateBaseList()).WithModifiers(CreateModifiers());
        }
    }
}
