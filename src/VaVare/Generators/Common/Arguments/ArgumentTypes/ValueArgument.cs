using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using VaVare.Extensions;
#pragma warning disable 1591

namespace VaVare.Generators.Common.Arguments.ArgumentTypes
{
    /// <summary>
    /// Provides the functionality to generate simple value arguments.
    /// </summary>
    public class ValueArgument : Argument
    {
        private readonly IdentifierNameSyntax identifierName;

        /// <summary>
        /// Gets the value sent in as an argument.
        /// </summary>
        public object Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueArgument"/> class.
        /// </summary>
        /// <param name="value">Value to send in as an argument.</param>
        /// <param name="namedArgument">Specify the argument for a particular parameter.</param>
        public ValueArgument(object value, string namedArgument = null)
            : base(namedArgument)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (!(value.IsNumeric() || value is bool || value is string))
            {
                throw new ArgumentException($"{nameof(value)} must be a number or boolean");
            }

            Value = value;
            identifierName = CreateIdentifierNameSyntax(value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueArgument"/> class.
        /// </summary>
        /// <param name="value">String value to send in as an argument.</param>
        /// <param name="stringType">The type of string.</param>
        /// <param name="namedArgument">Specify the argument for a particular parameter.</param>
        [Obsolete($"Please use {nameof(StringValueArgument)} instead")]
        public ValueArgument(string value, StringType stringType = StringType.Normal, string namedArgument = null)
            : this(value, true, stringType, namedArgument)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueArgument"/> class.
        /// </summary>
        /// <param name="value">String value to send in as an argument.</param>
        /// <param name="escapeValueAsString">If <see langword="true"/>, inverted commas are added to the <paramref name="value"/>.</param>
        /// <param name="stringType">The type of string.</param>
        /// <param name="namedArgument">Specify the argument for a particular parameter.</param>
        [Obsolete($"Please use {nameof(StringValueArgument)} instead")]
        public ValueArgument(string value, bool escapeValueAsString, StringType stringType = StringType.Normal, string namedArgument = null)
            : base(namedArgument)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (escapeValueAsString)
            {
                Value = stringType == StringType.Verbatim ? $"@\"{value}\"" : $"\"{value}\"";
            }
            else
            {
                Value = value;
            }

            identifierName = CreateIdentifierNameSyntax(Value);
        }

        protected override ArgumentSyntax CreateArgumentSyntax() 
            => SyntaxFactory.Argument(identifierName);

        public static ValueArgument Parse(string value)
            => new ValueArgument(value, false);
        public static ValueArgument Parse(string value, string namedArgument)
            => new ValueArgument(value, false, namedArgument: namedArgument);

        private static IdentifierNameSyntax CreateIdentifierNameSyntax(object value)
        {
            var name = value.ToString();

            if(value is bool)
            {
                name = name.ToLower();
            }

            return SyntaxFactory.IdentifierName(name);
        }
    }
}