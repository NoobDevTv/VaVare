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
            Assert.IsTrue(RecordBuilder.Build().ToString().Contains("TestRecord"));
        }

        [Test]
        public void Build_WhenGivenPropertyList_CodeShouldContainProperties()
        {
            Assert.IsTrue(RecordBuilder.Build().ToString().Contains("TestRecord"));
        }

        [Test]
        public void Build_WhenGivenNamespace_CodeShouldContainNamespace()
        {
            Assert.IsTrue(RecordBuilder.Build().ToString().Contains("MyNamespace"));
        }

        [Test]
        public void Build_WhenGivenField_CodeShouldContainField()
        {
            Assert.IsTrue(RecordBuilder.WithFields(new Field("myField", typeof(int), new List<Modifiers>() { Modifiers.Public})) .Build().ToString().Contains("publicintmyField;"));
        }

        [Test]
        public void Build_WhenGivenAttributes_CodeShouldContainAttributes()
        {
            Assert.IsTrue(RecordBuilder.WithAttributes(new Attribute("MyAttribute")).Build().ToString().Contains("[MyAttribute]"));
        }

        [Test]
        public void Build_WhenGivenParameterList_CodeShouldContainParameters()
        {
            Assert.IsTrue(RecordBuilder.WithParameterList(new Parameter("Bbq", typeof(int))).Build().ToString().Contains("TestRecord(intBbq)"));
            Assert.IsTrue(RecordBuilder.WithParameterList(new Parameter("Bbq", typeof(int)), new Parameter("Foo", typeof(string))).Build().ToString().Contains("TestRecord(intBbq,stringFoo)"));
        }

        [Test]
        public void Build_WhenGivenProperty_CodeShouldContainProperty()
        {
            Assert.IsTrue(RecordBuilder.WithProperties(new AutoProperty("MyProperty", typeof(int), PropertyTypes.GetAndSet)).Build().ToString().Contains("intMyProperty{get;set;}"));
        }

        [Test]
        public void Build_WhenGivenUsing_CodeShouldContainUsing()
        {
            Assert.IsTrue(RecordBuilder.WithUsings("some.namespace").Build().ToString().Contains("some.namespace"));
        }

        [Test]
        public void Build_CodeShouldContainRecordSpecifier()
        {
            Assert.IsTrue(RecordBuilder.Build().ToString().Contains($"{RecordSpecifier}TestRecord;"));
        }

        [Test]
        public void Build_WhenGivenModifiers_CodeShouldContainModifiers()
        {
            Assert.IsTrue(RecordBuilder.WithModifiers(Modifiers.Public, Modifiers.Abstract).Build().ToString().Contains($"publicabstract{RecordSpecifier}TestRecord"));
        }

        [Test]
        public void Build_WhenGivenInheritance_CodeShouldContainInheritance()
        {
            Assert.IsTrue(RecordBuilder.ThatInheritFrom(typeof(int)).Build().ToString().Contains("TestRecord:int"));
        }
    }
}
