using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using System;

namespace VaVare.Generators.Common.Arguments.ArgumentTypes
{
    public class StringValueArgument : Argument
    {
        private readonly IdentifierNameSyntax identifierName;

        /// <summary>
        /// Gets the value sent in as an argument.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringValueArgument"/> class.
        /// </summary>
        /// <param name="value">String value to send in as an argument.</param>
        /// <param name="stringType">The type of string.</param>
        /// <param name="namedArgument">Specificy the argument for a partical parameter.</param>
        public StringValueArgument(string value, StringType stringType = StringType.Normal, string namedArgument = null)
            : base(namedArgument)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Value = stringType == StringType.Verbatim ? $"@\"{value}\"" : $"\"{value}\"";

            identifierName = CreateIdentifierNameSyntax(value);
        }

        protected override ArgumentSyntax CreateArgumentSyntax()
            => SyntaxFactory.Argument(identifierName);

        public static StringValueArgument Parse(string value, StringType stringType = StringType.Normal)
            => new StringValueArgument(value, stringType: stringType);
        public static StringValueArgument Parse(string value, StringType stringType, string namedArgument)
            => new StringValueArgument(value, stringType: stringType, namedArgument: namedArgument);

        private static IdentifierNameSyntax CreateIdentifierNameSyntax(object value)
        {
            var name = value.ToString();

            if (value is bool)
            {
                name = name.ToLower();
            }

            return SyntaxFactory.IdentifierName(name);
        }
    }
}
