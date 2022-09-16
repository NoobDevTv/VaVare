using System;
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
        /// <param name="returnsSummary">Summary for the return value.</param>
        /// <returns>Return syntax node with summary.</returns>
        public static T WithSummary<T>(this T syntax, string summary, IEnumerable<ParameterSummary> parameterSummaries, string returnsSummary = null)
            where T : CSharpSyntaxNode
        {
            parameterSummaries = parameterSummaries ?? new List<ParameterSummary>();

            if (string.IsNullOrEmpty(summary))
            {
                return syntax;
            }

            var nodes = new List<XmlNodeSyntax>();
            nodes.Add(CreateSummaryDocumentation(summary));
            foreach (var parameter in parameterSummaries)
            {
                nodes.Add(XmlNewLine("\n"));
                nodes.Add(CreateParameterDocumentation(parameter));
            }

            if (returnsSummary is not null)
            {
                nodes.Add(XmlNewLine("\n"));
                nodes.Add(XmlReturnsElement(CreateMultilineXmlTextContent(returnsSummary)));
            }

            nodes.Add(CreateXmlNewLine());

            var trivia = Trivia(DocumentationComment(nodes.ToArray()));
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
        /// <param name="returnsSummary">Summary for the return value.</param>
        /// <returns>Return syntax node with summary.</returns>
        public static T WithSummary<T>(this T syntax, string summary, IEnumerable<Parameter> parameters = null, IEnumerable<TypeParameter> typeParameters = null, string returnsSummary = null)
            where T : CSharpSyntaxNode
        {
            var paramSums = parameters?.Where(p => p.XmlDocumentation != null).Select(x => new ParameterSummary(x));
            var typeParamSums = typeParameters?.Where(p => p.XmlDocumentation != null)
                .Select(x => new ParameterSummary(x));
            return WithSummary(syntax, summary, (paramSums is not null && typeParamSums is not null) ? typeParamSums.Concat(paramSums) : (paramSums ?? typeParamSums), returnsSummary);
        }

        private static XmlTextSyntax CreateXmlNewLine(bool continueComment = false)
        {
            var xmlText = continueComment
                ? XmlTextNewLine("\n", true)
                : XmlTextNewLine(TriviaList(), "\n", "\n", TriviaList());
            return XmlText().WithTextTokens(TokenList(xmlText));
        }

        private static XmlElementSyntax CreateSummaryDocumentation(string text)
        {
            return XmlSummaryElement(CreateMultilineXmlTextContent(text, true));
        }

        private static XmlNodeSyntax[] CreateMultilineXmlTextContent(string content, bool forceMultiLine = false, bool surroundNewLinesIfMultiLine = true)
        {
            var commentLines = content.Split(new[] { "\n" }, StringSplitOptions.None);
            if (commentLines.Length == 1 && !forceMultiLine)
            {
                return new XmlNodeSyntax[] { XmlText(content) };
            }

            var res = new XmlNodeSyntax[(commentLines.Length * 2) - 1 + (surroundNewLinesIfMultiLine ? 2 : 0)];

            var index = 0;

            if (surroundNewLinesIfMultiLine)
            {
                res[index++] = XmlNewLine("\n");
            }

            for (int i = 0; i < commentLines.Length; i++)
            {
                var l = commentLines[i];
                res[index++] = XmlText(l);
                if (i < commentLines.Length - 1)
                {
                    res[index++] = XmlNewLine("\n");
                }
            }

            if (surroundNewLinesIfMultiLine)
            {
                res[index] = XmlNewLine("\n");
            }

            return res;
        }

        private static XmlElementSyntax CreateParameterDocumentation(ParameterSummary parameter)
        {
            var content = CreateMultilineXmlTextContent(parameter.Summary);
            return parameter.IsTypeParameter
                    ? XmlTypeParamElement(parameter.ParameterName, content)
                    : XmlParamElement(parameter.ParameterName, content);
        }

        /// <summary>
        /// Creates the syntax representation of a param element within xml documentation comments (e.g. for
        /// documentation of method parameters).
        /// </summary>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="content">
        /// A list of syntax nodes that represents the content of the param element
        /// (e.g. the description and meaning of the parameter).
        /// </param>
        /// <returns>The created xml element.</returns>
        public static XmlElementSyntax XmlTypeParamElement(string parameterName, params XmlNodeSyntax[] content)
        {
            return XmlParamElement(parameterName, List(content));
        }
    }
}
