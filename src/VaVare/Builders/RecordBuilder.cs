using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using VaVare.Builders.Base;
using VaVare.Builders.BuildMembers;
using VaVare.Generators.Class;
using VaVare.Generators.Common;
using VaVare.Models;

namespace VaVare.Builders
{
    /// <summary>
    /// Provides a builder to generate a record.
    /// </summary>
    public class RecordBuilder : TypeBuilderBase<RecordBuilder>
    {
        private readonly bool _isRecordStruct;
        private readonly List<ParameterSyntax> _parameters;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordBuilder"/> class.
        /// </summary>
        /// <param name="name">Name of the record.</param>
        /// <param name="namespace">Name of the record namespace.</param>
        /// <param name="isRecordStruct">Whether the record should be a record struct or not.</param>
        public RecordBuilder(string name, string @namespace, bool isRecordStruct = false)
         : base(name, @namespace)
        {
            _isRecordStruct = isRecordStruct;
            _parameters = new List<ParameterSyntax>();
        }

        /// <summary>
        /// Add record fields.
        /// </summary>
        /// <param name="fields">A set of wanted fields.</param>
        /// <returns>The current record builder.</returns>
        public RecordBuilder WithFields(params Field[] fields)
        {
            return With(new FieldBuildMember(fields.Select(FieldGenerator.Create)));
        }

        /// <summary>
        /// Add record fields.
        /// </summary>
        /// <param name="fields">An array of already declared fields.</param>
        /// <returns>The current record builder.</returns>
        public RecordBuilder WithFields(params FieldDeclarationSyntax[] fields)
        {
            return With(new FieldBuildMember(fields));
        }

        /// <summary>
        /// Add record constructor.
        /// </summary>
        /// <param name="constructor">An already generated constructor.</param>
        /// <returns>The current record builder.</returns>
        public RecordBuilder WithConstructor(params ConstructorDeclarationSyntax[] constructor)
        {
            return With(new ConstructorBuildMember(constructor));
        }

        /// <summary>
        /// Add record properties and parameters.
        /// </summary>
        /// <param name="parameters">The record properties and parameters.</param>
        /// <returns>The current record builder.</returns>
        public RecordBuilder WithParameterList(params Parameter[] parameters)
        {
            _parameters.Clear();
            foreach (var parameter in parameters)
            {
                _parameters.Add(ParameterGenerator.Create(parameter));
            }

            return this;
        }

        /// <summary>
        /// Add record properties and parameters.
        /// </summary>
        /// <param name="parameters">The record properties and parameters.</param>
        /// <returns>The current record builder.</returns>
        public RecordBuilder WithParameterList(params ParameterSyntax[] parameters)
        {
            _parameters.Clear();
            _parameters.AddRange(parameters);
            return this;
        }

        protected override TypeDeclarationSyntax BuildSurroundingTokens(TypeDeclarationSyntax typeDeclarationSyntax)
        {
            return typeDeclarationSyntax.Members.Count == 0
                ? typeDeclarationSyntax.WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))
                : typeDeclarationSyntax.WithOpenBraceToken(SyntaxFactory.Token(SyntaxKind.OpenBraceToken)).WithCloseBraceToken(SyntaxFactory.Token(SyntaxKind.CloseBraceToken));
        }

        protected override TypeDeclarationSyntax BuildBase()
        {
            var recordDeclaration = SyntaxFactory.RecordDeclaration(SyntaxFactory.Token(SyntaxKind.RecordKeyword), Name).WithBaseList(CreateBaseList()).WithModifiers(CreateModifiers());
            if (_isRecordStruct)
            {
                recordDeclaration = recordDeclaration.WithClassOrStructKeyword(SyntaxFactory.Token(SyntaxKind.StructKeyword));
            }

            if (_parameters.Count > 0)
            {
                var paramList = SyntaxFactory.SeparatedList(_parameters);
                var paramListSyntax = SyntaxFactory.ParameterList(paramList);
                recordDeclaration = recordDeclaration.WithParameterList(paramListSyntax);
            }

            return recordDeclaration;
        }
    }
}
