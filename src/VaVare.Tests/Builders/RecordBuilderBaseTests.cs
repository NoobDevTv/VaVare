using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;
using NUnit.Framework;
using VaVare.Builders;
using VaVare.Models;
using VaVare.Models.Properties;

namespace VaVare.Tests.Builders
{
    public abstract class RecordBuilderBaseTests
    {
        protected RecordBuilder RecordBuilder;

        protected abstract string RecordSpecifier { get; }

        [Test]
        public void Build_WhenGivenRecordName_CodeShouldContainRecordName()
        {
            Assert.That(RecordBuilder.Build().ToString().Contains("TestRecord"), Is.True);
        }

        [Test]
        public void Build_WhenGivenNamespace_CodeShouldContainNamespace()
        {
            Assert.That(RecordBuilder.Build().ToString().Contains("MyNamespace"), Is.True);
        }

        [Test]
        public void Build_WhenGivenField_CodeShouldContainField()
        {
            var field = new Field("myField", typeof(int), new List<Modifiers>() { Modifiers.Public });
            Assert.That(RecordBuilder.WithFields(field).Build().ToString().Contains("publicintmyField;"), Is.True);
        }

        [Test]
        public void Build_WhenGivenAttributes_CodeShouldContainAttributes()
        {
            Assert.That(RecordBuilder.WithAttributes(new Attribute("MyAttribute"))
                .Build().ToString().Contains("[MyAttribute]"), Is.True);
        }

        [Test]
        public void Build_WhenGivenParameterList_CodeShouldContainParameters()
        {
            var parameter1 = new Parameter("Bbq", typeof(int));
            var parameter2 = new Parameter("Foo", typeof(string));
            Assert.That(RecordBuilder.WithParameterList(parameter1)
                .Build().ToString().Contains("TestRecord(intBbq)"), Is.True);
            Assert.That(RecordBuilder.WithParameterList(parameter1, parameter2)
                    .Build().ToString().Contains("TestRecord(intBbq,stringFoo)"), Is.True);
        }

        [Test]
        public void Build_WhenGivenProperty_CodeShouldContainProperty()
        {
            var property = new AutoProperty("MyProperty", typeof(int), PropertyTypes.GetAndSet);
            Assert.That(RecordBuilder.WithProperties(property).Build().ToString().Contains("intMyProperty{get;set;}"), Is.True);
        }

        [Test]
        public void Build_WhenGivenUsing_CodeShouldContainUsing()
        {
            Assert.That(RecordBuilder.WithUsings("some.namespace").Build().ToString().Contains("some.namespace"), Is.True);
        }

        [Test]
        public void Build_CodeShouldContainRecordSpecifier()
        {
            Assert.That(RecordBuilder.Build().ToString().Contains($"{RecordSpecifier}TestRecord;"), Is.True);
        }

        [Test]
        public void Build_WhenGivenModifiers_CodeShouldContainModifiers()
        {
            Assert.That(RecordBuilder.WithModifiers(Modifiers.Public, Modifiers.Abstract)
                .Build().ToString().Contains($"publicabstract{RecordSpecifier}TestRecord"), Is.True);
        }

        [Test]
        public void Build_WhenGivenInheritance_CodeShouldContainInheritance()
        {
            Assert.That(RecordBuilder.ThatInheritFrom(typeof(int))
                .Build().ToString().Contains("TestRecord:int"), Is.True);
        }
    }
}
