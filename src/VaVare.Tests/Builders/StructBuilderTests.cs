using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using Microsoft.CodeAnalysis.Formatting;
using NUnit.Framework;
using VaVare.Builders;
using VaVare.Models;
using VaVare.Models.Properties;

namespace VaVare.Tests.Builders
{
    [TestFixture]
    public class StructBuilderTests
    {
        private const string TypeName = "TestStruct";
        private StructBuilder _structBuilder;

        [SetUp]
        public void SetUp()
        {
            _structBuilder = new StructBuilder(TypeName, "MyNamespace");
        }

        [Test]
        public void Build_WhenGivenClassName_CodeShouldContainClassName()
        {
            Assert.That(_structBuilder.Build().ToString().Contains(TypeName), Is.True);
        }

        [Test]
        public void Build_WhenGivenNamespace_CodeShouldContainNamespace()
        {
            Assert.That(_structBuilder.Build().ToString().Contains("MyNamespace"), Is.True);
        }

        [Test]
        public void Build_WhenGivenField_CodeShouldContainField()
        {
            var withField = _structBuilder
                .WithFields(new Field("myField", typeof(int), new List<Modifiers>() { Modifiers.Public })).Build();
            Assert.That(withField.ToString().Contains("publicintmyField;"), Is.True);
        }

        [Test]
        public void Build_WhenGivenAttributes_CodeShouldContainAttributes()
        {
            var withAttrs = _structBuilder.WithAttributes(new Attribute("MyAttribute")).Build();
            Assert.That(withAttrs.ToString().Contains("[MyAttribute]"), Is.True);
        }


        [Test]
        public void Build_WhenGivenProperty_CodeShouldContainProperty()
        {
            var withProps = _structBuilder
                .WithProperties(new AutoProperty("MyProperty", typeof(int), PropertyTypes.GetAndSet)).Build();
            Assert.That(withProps.ToString().Contains("intMyProperty{get;set;}"), Is.True);
        }

        [Test]
        public void Build_WhenGivenUsing_CodeShouldContainUsing()
        {
            Assert.That(_structBuilder.WithUsings("some.namespace").Build().ToString().Contains("some.namespace"), Is.True);
        }


        [Test]
        public void Build_WhenGivenModifiers_CodeShouldContainModifiers()
        {
            var withModifiers = _structBuilder.WithModifiers(Modifiers.Public, Modifiers.Abstract).Build();
            Assert.That(withModifiers.ToString().Contains($"publicabstractstruct{TypeName}"), Is.True);
        }

        [Test]
        public void Build_WhenGivenInheritance_CodeShouldContainInheritance()
        {
            Assert.That(_structBuilder.ThatInheritFrom(typeof(int)).Build().ToString().Contains($"{TypeName}:int"), Is.True);
        }
    }
}
