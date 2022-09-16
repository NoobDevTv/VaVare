﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using VaVare.Models;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace VaVare.Generators.Special
{
    /// <summary>
    /// Extension methods to CsharpSyntaxNode.
    /// </summary>
    public static class CsharpSyntaxNodeExtensions
    {
        /// <summary>
        /// Create summary to syntax node.
        /// </summary>
        /// <typeparam name="T">Syntax type.</typeparam>
        /// <param name="syntax">The syntax.</param>
        /// <param name="summary">Summary text.</param>
        /// <param name="parameterSummaries">Parameters in the summary.</param>
        /// <returns>Return syntax node with summary.</returns>
        public static T WithSummary<T>(this T syntax, string summary, IEnumerable<ParameterSummary> parameterSummaries)
            where T : CSharpSyntaxNode
        {
            parameterSummaries = parameterSummaries ?? new List<ParameterSummary>();

            if (string.IsNullOrEmpty(summary))
            {
                return syntax;
            }

            var content = List<XmlNodeSyntax>();

            content = CreateSummaryDocumentation(content, summary);

            foreach (var parameter in parameterSummaries)
            {
                content = CreateParameterDocumentation(content, parameter);
            }

            content = content.Add(XmlText().WithTextTokens(TokenList(XmlTextNewLine(TriviaList(), "\n", "\n", TriviaList()))));

            var trivia = Trivia(
                DocumentationCommentTrivia(
                    SyntaxKind.SingleLineDocumentationCommentTrivia,
                    content));
            return syntax.WithLeadingTrivia(trivia);
        }

        /// <summary>
        /// Create summary to syntax node.
        /// </summary>
        /// <typeparam name="T">Syntax type.</typeparam>
        /// <param name="syntax">The syntax.</param>
        /// <param name="summary">Summary text.</param>
        /// <param name="parameters">Parameter summaries.</param>
        /// <param name="typeParameters">Type parameter summaries.</param>
        /// <returns>Return syntax node with summary.</returns>
        public static T WithSummary<T>(this T syntax, string summary, IEnumerable<Parameter> parameters = null, IEnumerable<TypeParameter> typeParameters = null)
            where T : CSharpSyntaxNode
        {
            var paramSums = parameters?.Where(p => p.XmlDocumentation != null).Select(x => new ParameterSummary(x));
            var typeParamSums = typeParameters?.Where(p => p.XmlDocumentation != null)
                .Select(x => new ParameterSummary(x));
            return WithSummary(syntax, summary, (paramSums is not null && typeParamSums is not null) ? typeParamSums.Concat(paramSums) : (paramSums ?? typeParamSums));
        }

        private static SyntaxList<XmlNodeSyntax> CreateSummaryDocumentation(SyntaxList<XmlNodeSyntax> content, string text)
        {
            var summary = new List<SyntaxToken>();
            summary.Add(XmlTextNewLine(TriviaList(), "\n", "\n", TriviaList()));
            var commentLines = text.Split(new[] { "\n" }, StringSplitOptions.None);
            for (int n = 0; n < commentLines.Length; n++)
            {
                var fixedCommentLine = $" {commentLines[n]}";
                if (n != commentLines.Length - 1)
                {
                    fixedCommentLine += "\n";
                }

                summary.Add(XmlTextLiteral(TriviaList(DocumentationCommentExterior("///")), fixedCommentLine, fixedCommentLine, TriviaList()));
            }

            summary.Add(XmlTextNewLine(TriviaList(), "\n", "\n", TriviaList()));
            summary.Add(XmlTextLiteral(TriviaList(DocumentationCommentExterior("///")), " ", " ", TriviaList()));

            return content.AddRange(new List<XmlNodeSyntax>
            {
                XmlText().WithTextTokens(TokenList(XmlTextLiteral(TriviaList(DocumentationCommentExterior("///")), " ", " ", TriviaList()))),
                XmlElement(XmlElementStartTag(XmlName(Identifier("summary"))), XmlElementEndTag(XmlName(Identifier("summary"))))
                    .WithContent(SingletonList<XmlNodeSyntax>(XmlText().WithTextTokens(TokenList(summary)))),
            });
        }

        private static SyntaxList<XmlNodeSyntax> CreateParameterDocumentation(SyntaxList<XmlNodeSyntax> content, ParameterSummary parameter)
        {
            string paramTag = parameter.IsTypeParameter ? "typeparam" : "param";
            return content.AddRange(new List<XmlNodeSyntax>
            {
                XmlText().WithTextTokens(
                        TokenList(
                            new[]
                            {
                                XmlTextNewLine(
                                    TriviaList(),
                                    "\n",
                                    "\n",
                                    TriviaList()),
                                XmlTextLiteral(
                                    TriviaList(
                                        DocumentationCommentExterior("///")),
                                    " ",
                                    " ",
                                    TriviaList()),
                            })),
                XmlExampleElement(SingletonList<XmlNodeSyntax>(
                        XmlText().WithTextTokens(
                                    TokenList(
                                        XmlTextLiteral(
                                            TriviaList(),
                                            parameter.Summary,
                                            parameter.Summary,
                                            TriviaList())))))
                    .WithStartTag(XmlElementStartTag(
                                XmlName(
                                    Identifier(paramTag)))
                            .WithAttributes(
                                SingletonList<XmlAttributeSyntax>(
                                    XmlNameAttribute(
                                        XmlName(
                                            Identifier(" name")),
                                        Token(SyntaxKind.DoubleQuoteToken),
                                        IdentifierName(parameter.ParameterName),
                                        Token(SyntaxKind.DoubleQuoteToken)))))
                    .WithEndTag(
                        XmlElementEndTag(
                            XmlName(
                                Identifier(paramTag)))),
            });
        }
    }
}
