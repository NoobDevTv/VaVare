using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;
using VaVare.Generators.Common.Arguments.ArgumentTypes;
using VaVare.Models.Types;

namespace VaVare.Tests.Generators.Common.Arguments.ArgumentTypes
{
    [TestFixture]
    public class ObjectInitializationArgumentTests
    {
        [Test]
        public void GetArgumentSyntax_WhenInitializeObject_ShouldGetCorrectCode()
        {
            var argument = new ObjectInitializationArgument(
                CustomType.Create("CustomClass"),
                new Dictionary<string, IArgument> { ["Property"] = new StringValueArgument("hello") });

            var syntax = argument.GetArgumentSyntax();

            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("newCustomClass{Property=\"hello\"}", syntax.ToString());
        }

        [Test]
        public void GetArgumentSyntax_WhenInitializeObjectWithVariable_ShouldGetCorrectCode()
        {
            var argument = new ObjectInitializationArgument(
                CustomType.Create("CustomClass"),
                new Dictionary<string, IArgument>
                    {
                        ["Property"] = new StringValueArgument("hello"),
                        ["AnotherProperty"] = new VariableArgument("variable")
                    });

            var syntax = argument.GetArgumentSyntax();

            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("newCustomClass{Property=\"hello\",AnotherProperty=variable}", syntax.ToString());
        }

        [Test]
        public void GetArgumentSyntax_WhenInitializeObjectWithDictionary_ShouldGetCorrectCode()
        {
            var argument = new ObjectInitializationArgument(
                CustomType.Create("CustomClass"),
                new Dictionary<string, IArgument>
                    {
                        ["PropertyWithDictionaryType"] =
                        new DictionaryInitializationArgument<int, int>(
                            new Dictionary<int, IArgument>
                                {
                                    [1] = new ValueArgument(2)
                                })
                    });

            var syntax = argument.GetArgumentSyntax();

            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("newCustomClass{PropertyWithDictionaryType=newDictionary<int,int>{[1]=2}}", syntax.ToString());
        }

        [Test]
        public void GetArgumentSyntax_WhenInitializeObjectWithAnotherObjectInitialize_ShouldGetCorrectCode()
        {
            var argument = new ObjectInitializationArgument(
                CustomType.Create("CustomClass"),
                new Dictionary<string, IArgument>
                    {
                        ["CustomPropertyType"] = new ObjectInitializationArgument(
                            CustomType.Create("CustomPropertyType"),
                            new Dictionary<string, IArgument>
                                {
                                    ["Property"] =
                                    new StringValueArgument(
                                        "hello")
                                })
                    });

            var syntax = argument.GetArgumentSyntax();

            Assert.IsInstanceOf<ArgumentSyntax>(syntax);
            Assert.AreEqual("newCustomClass{CustomPropertyType=newCustomPropertyType{Property=\"hello\"}}", syntax.ToString());
        }
    }
}
