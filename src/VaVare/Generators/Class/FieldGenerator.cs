using System;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using VaVare.Generators.Common;
using VaVare.Generators.Special;
using VaVare.Models;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace VaVare.Generators.Class
{
    /// <summary>
    /// Provides the functionality to generate fields.
    /// </summary>
    public static class FieldGenerator
    {
        /// <summary>
        /// Create the syntax for a field of a class.
        /// </summary>
        /// <param name="field">Field to create.</param>
        /// <returns>The declaration syntax for a field.</returns>
        public static FieldDeclarationSyntax Create(Field field)
        {
            if (field == null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            var variableDeclarator = VariableDeclarator(Identifier(field.Name));

            if (field.InitializeWith != null)
            {
                variableDeclarator = variableDeclarator.WithInitializer(EqualsValueClause(field.InitializeWith));
            }

            var fieldDeclaration = FieldDeclaration(VariableDeclaration(TypeGenerator.Create(field.Type), SeparatedList(new[] { variableDeclarator })));

            if (field.Modifiers != null)
            {
                fieldDeclaration = fieldDeclaration.WithModifiers(ModifierGenerator.Create(field.Modifiers.ToArray()));
            }

            if (field.Attributes != null)
            {
                fieldDeclaration = fieldDeclaration.WithAttributeLists(AttributeGenerator.Create(field.Attributes.ToArray()));
            }

            if (!string.IsNullOrEmpty(field.Summary))
            {
                fieldDeclaration = fieldDeclaration.WithSummary(field.Summary);
            }

            return fieldDeclaration;
        }
    }
}
