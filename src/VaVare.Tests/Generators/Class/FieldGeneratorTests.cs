using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using NUnit.Framework;
using VaVare.Generators.Class;
using VaVare.Generators.Common;
using VaVare.Models;
using VaVare.Models.References;
using VaVare.Models.Types;
using Assert = NUnit.Framework.Assert;

namespace VaVare.Tests.Generators.Class
{
    [TestFixture]
    public class FieldGeneratorTests
    {
        [Test]
        public void Create_WhenCreatingField_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("intmyField;", FieldGenerator.Create(new Field("myField", typeof(int))).ToString());
        }

        [Test]
        public void Create_WhenCreatingFieldWithInitializer_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("ILogger_logger=LoggerService.Logger();", FieldGenerator.Create(new Field("_logger", CustomType.Create("ILogger"), initializeWith: ReferenceGenerator.Create(new VariableReference("LoggerService", new MethodReference( "Logger"))))).ToString());
        }

        [Test]
        public void Create_WhenCreatingFieldWithGenericType_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("List<int>myField;", FieldGenerator.Create(new Field("myField", typeof(List<int>))).ToString());
        }

        [Test]
        public void Create_WhenCreatingFieldWithMultipleGenericType_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("List<List<string>>myField;", FieldGenerator.Create(new Field("myField", typeof(List<List<string>>))).ToString());
        }

        [Test]
        public void Create_WhenCreatingFieldWithModifiers_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("publicintmyField;", FieldGenerator.Create(new Field("myField", typeof(int), new List<Modifiers>() { Modifiers.Public})).ToString());
        }

        [Test]
        public void Create_WhenCreatingFieldWithAttribute_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("[Test]intmyField;", FieldGenerator.Create(new Field("myField", typeof(int), attributes: new List<Attribute>() { new Attribute("Test") })).ToString());
        }
    }
}
