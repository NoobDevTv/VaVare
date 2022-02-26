using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using VaVare.Factories;
using VaVare.Generators.Common;
using VaVare.Generators.Special;
using VaVare.Models;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using Attribute = VaVare.Models.Attribute;

namespace VaVare.Builders
{
    /// <summary>
    /// Provides a builder to generate a method
    /// </summary>
    public class MethodBuilder
    {
        private readonly string _name;
        private readonly List<ParameterSyntax> _parameters;
        private readonly List<Modifiers> _modifiers;
        private readonly List<TypeParameter> _typeParameters;
        private readonly List<TypeParameterConstraintClause> _constraintClauses;
        private readonly List<Parameter> _parameterXmlDocumentation;

        private TypeSyntax _returnType;
        private BlockSyntax _body;
        private string _summary;
        private SyntaxKind? _overrideOperator;

        private SyntaxList<AttributeListSyntax> _attributes;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodBuilder"/> class.
        /// </summary>
        /// <param name="name">Name of the method</param>
        public MethodBuilder(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }

            _name = name.Replace(" ", "_");
            _parameters = new List<ParameterSyntax>();
            _modifiers = new List<Modifiers>();
            _typeParameters = new List<TypeParameter>();
            _constraintClauses = new List<TypeParameterConstraintClause>();
            _parameterXmlDocumentation = new List<Parameter>();
            _body = BodyGenerator.Create();
        }

        /// <summary>
        /// Set method parameters.
        /// </summary>
        /// <param name="parameters">A set of wanted parameters.</param>
        /// <returns>The current method builder</returns>
        public MethodBuilder WithParameters(params Parameter[] parameters)
        {
            _parameters.Clear();
            _parameterXmlDocumentation.Clear();

            foreach (var parameter in parameters)
            {
                _parameters.Add(ParameterGenerator.Create(parameter));

                if (parameter.XmlDocumentation != null)
                {
                    _parameterXmlDocumentation.Add(parameter);
                }
            }

            return this;
        }

        /// <summary>
        /// Set method parameters.
        /// </summary>
        /// <param name="parameters">A set of already generated parameters.</param>
        /// <returns>The current method builder</returns>
        public MethodBuilder WithParameters(params ParameterSyntax[] parameters)
        {
            _parameters.Clear();
            _parameters.AddRange(parameters);
            return this;
        }

        /// <summary>
        /// Set method return type
        /// </summary>
        /// <param name="type">The wanted return type</param>
        /// <returns>The current method builder</returns>
        public MethodBuilder WithReturnType(Type type)
        {
            _returnType = TypeGenerator.Create(type);
            return this;
        }

        /// <summary>
        /// Set method return type
        /// </summary>
        /// <param name="type">The wanted return type</param>
        /// <returns>The current method builder</returns>
        public MethodBuilder WithReturnType(TypeSyntax type)
        {
            _returnType = type;
            return this;
        }

        /// <summary>
        /// Set method attributs.
        /// </summary>
        /// <param name="attributes">A set of wanted attributes</param>
        /// <returns>The current method builder</returns>
        public MethodBuilder WithAttributes(params Attribute[] attributes)
        {
            _attributes = AttributeGenerator.Create(attributes);
            return this;
        }

        /// <summary>
        /// Set method attributes.
        /// </summary>
        /// <param name="attributes">A set of already generated attributes </param>
        /// <returns>The current method builder</returns>
        public MethodBuilder WithAttributes(SyntaxList<AttributeListSyntax> attributes)
        {
            _attributes = attributes;
            return this;
        }

        /// <summary>
        /// Set method body.
        /// </summary>
        /// <param name="body">The method body.</param>
        /// <returns>The current method builder</returns>
        public MethodBuilder WithBody(BlockSyntax body)
        {
            _body = body;
            return this;
        }

        /// <summary>
        /// Set method xml summary.
        /// </summary>
        /// <param name="summary">The method summary.</param>
        /// <returns>The current method builder</returns>
        public MethodBuilder WithSummary(string summary)
        {
            _summary = summary;
            return this;
        }

        /// <summary>
        /// Set method modifiers.
        /// </summary>
        /// <param name="modifiers">A set of wanted modifiers.</param>
        /// <returns>The current method builder</returns>
        public MethodBuilder WithModifiers(params Modifiers[] modifiers)
        {
            _modifiers.Clear();
            _modifiers.AddRange(modifiers);
            return this;
        }

        /// <summary>
        /// Add generic type parameters.
        /// </summary>
        /// <param name="typeParameters">The type parameters.</param>
        /// <returns>The current builder.</returns>
        public MethodBuilder WithTypeParameters(params TypeParameter[] typeParameters)
        {
            _typeParameters.Clear();
            _typeParameters.AddRange(typeParameters);
            return this;
        }

        /// <summary>
        /// Add constraint clauses on the generic parameters.
        /// </summary>
        /// <param name="constraintClauses">The constraint clauses on the generic parameters.</param>
        /// <returns>The current builder.</returns>
        public MethodBuilder WithTypeConstraintClauses(params TypeParameterConstraintClause[] constraintClauses)
        {
            _constraintClauses.Clear();
            return AddTypeConstraintClauses(constraintClauses);
        }

        /// <summary>
        /// Add constraint clauses on the generic parameters.
        /// </summary>
        /// <param name="constraintClauses">The constraint clauses on the generic parameters.</param>
        /// <returns>The current builder.</returns>
        public MethodBuilder AddTypeConstraintClauses(params TypeParameterConstraintClause[] constraintClauses)
        {
            _constraintClauses.AddRange(constraintClauses);
            return this;
        }

        /// <summary>
        /// Set operator overloading.
        /// </summary>
        /// <param name="operator">Operator to overload</param>
        /// <returns>The current method builder.</returns>
        public MethodBuilder WithOperatorOverloading(Operators @operator)
        {
            _overrideOperator = OperatorFactory.GetSyntaxKind(@operator);
            return this;
        }

        /// <summary>
        /// Set operator overloading.
        /// </summary>
        /// <param name="operator">Operator to overload</param>
        /// <returns>The current method builder.</returns>
        public MethodBuilder WithOperatorOverloading(SyntaxKind @operator)
        {
            _overrideOperator = @operator;
            return this;
        }

        /// <summary>
        /// Build method and return the generated code.
        /// </summary>
        /// <returns>The generated method.</returns>
        public BaseMethodDeclarationSyntax Build()
        {
            var method = BuildMethodBase();
            method = BuildModifiers(method);
            method = BuildAttributes(method);
            method = method.WithSummary(_summary, _parameterXmlDocumentation);
            method = BuildParameters(method);
            method = BuildBody(method);
            return method;
        }

        private BaseMethodDeclarationSyntax BuildMethodBase()
        {
            if (_overrideOperator != null)
            {
                return OperatorDeclaration(IdentifierName(_name), Token(_overrideOperator.Value));
            }

            if (_returnType != null)
            {
                return BuildTypeParameterConstraintClauses(BuildTypeParameters(MethodDeclaration(_returnType, Identifier(_name))));
            }
            else
            {
                return BuildTypeParameterConstraintClauses(BuildTypeParameters(MethodDeclaration(
                        PredefinedType(Token(SyntaxKind.VoidKeyword)),
                        Identifier(_name))));
            }
        }

        private BaseMethodDeclarationSyntax BuildModifiers(BaseMethodDeclarationSyntax method)
        {
            if (_modifiers == null || !_modifiers.Any())
            {
                return method;
            }

            return method.WithModifiers(ModifierGenerator.Create(_modifiers.ToArray()));
        }

        private MethodDeclarationSyntax BuildTypeParameters(MethodDeclarationSyntax method)
        {
            return _typeParameters.Count == 0 ? method : method.WithTypeParameterList(TypeParameterGenerator.Create(_typeParameters));
        }

        protected MethodDeclarationSyntax BuildTypeParameterConstraintClauses(MethodDeclarationSyntax method)
        {
            return _constraintClauses.Count == 0 ? method : method.WithConstraintClauses(TypeParameterConstraintGenerator.Create(_constraintClauses));
        }

        private BaseMethodDeclarationSyntax BuildAttributes(BaseMethodDeclarationSyntax method)
        {
            return !_attributes.Any() ? method : method.WithAttributeLists(_attributes);
        }

        private BaseMethodDeclarationSyntax BuildParameters(BaseMethodDeclarationSyntax method)
        {
            return !_parameters.Any() ? method : method.WithParameterList(ParameterGenerator.ConvertParameterSyntaxToList(_parameters.ToArray()));
        }

        private BaseMethodDeclarationSyntax BuildBody(BaseMethodDeclarationSyntax method)
        {
            if (_body == null)
            {
                return method.WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
            }

            return method.WithBody(_body);
        }
    }
}
