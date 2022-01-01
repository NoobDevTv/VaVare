using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Moq;
using NUnit.Framework;
using VaVare.Builders;
using VaVare.Builders.BuilderHelpers;
using VaVare.Models;
using VaVare.Models.Properties;
using Attribute = VaVare.Models.Attribute;

namespace VaVare.Tests.Builders
{
    [TestFixture]
    [TestOf(typeof(ClassBuilder))]
    public static class ClassBuilderTests
    {
        internal class WithFields : TestFixture
        {
            public override string Name => "WithFieldsClass";
            public override string Namespace => "VaVare.Test";

            [Test]
            public void Throws_an_ArgumentNullException_if_Field_array_parameter_is_null()
            {
                Assert.That(() => ClassBuilder.WithFields(null as Field[]), Throws.ArgumentNullException);
            }

            [Test]
            public void Throws_an_ArgumentNullException_if_FieldDeclarationSyntax_array_parameter_is_null()
            {
                Assert.That(() => ClassBuilder.WithFields(null as FieldDeclarationSyntax[]), Throws.ArgumentNullException);
            }
        }

        internal class OldTests : TestFixture
        {
            public override string Name => "TestClass";
            public override string Namespace => "MyNamespace";

            [Test]
            public void Build_WhenGivenClassName_CodeShouldContainClassName()
            {
                Assert.IsTrue(ClassBuilder.Build().ToString().Contains("TestClass"));
            }

            [Test]
            public void Build_WhenGivenNamespace_CodeShouldContainNamespace()
            {
                Assert.IsTrue(ClassBuilder.Build().ToString().Contains("MyNamespace"));
            }

            [Test]
            public void Build_WhenGivenField_CodeShouldContainField()
            {
                Assert.IsTrue(ClassBuilder.WithFields(new Field("myField", typeof(int), new List<Modifiers>() { VaVare.Modifiers.Public })).Build().ToString().Contains("publicintmyField;"));
            }

            [Test]
            public void Build_WhenGivenAttributes_CodeShouldContainAttributes()
            {
                Assert.IsTrue(ClassBuilder.WithAttributes(new Attribute("MyAttribute")).Build().ToString().Contains("[MyAttribute]"));
            }


            [Test]
            public void Build_WhenGivenProperty_CodeShouldContainProperty()
            {
                Assert.IsTrue(ClassBuilder.WithProperties(new AutoProperty("MyProperty", typeof(int), PropertyTypes.GetAndSet)).Build().ToString().Contains("intMyProperty{get;set;}"));
            }

            [Test]
            public void Build_WhenGivenUsing_CodeShouldContainUsing()
            {
                Assert.IsTrue(ClassBuilder.WithUsings("some.namespace").Build().ToString().Contains("some.namespace"));
            }


            [Test]
            public void Build_WhenGivenModifiers_CodeShouldContainModifiers()
            {
                Assert.IsTrue(ClassBuilder.WithModifiers(VaVare.Modifiers.Public, VaVare.Modifiers.Abstract).Build().ToString().Contains("publicabstractclassTestClass"));
            }

            [Test]
            public void Build_WhenGivenInheritance_CodeShouldContainInheritance()
            {
                Assert.IsTrue(ClassBuilder.ThatInheritFrom(typeof(int)).Build().ToString().Contains("TestClass:int"));
            }

        }



        public abstract class TestFixture
        {
            public abstract string Name { get;  }
            public abstract string Namespace { get; }

            public List<Type> Inheritance { get; set; }
            public List<Modifiers> Modifiers { get; set; }

            internal Mock<MemberHelper> MemberHelperMock { get; private set; }
            internal Mock<UsingHelper> UsingHelperMock { get; private set; }
            internal Mock<NamespaceHelper> NamespaceHelperMock { get; private set; }

            internal Mock<ClassBuilderTestClass> ClassBuilderMock { get; private set; }
            internal ClassBuilder ClassBuilder { get; private set; }

            protected virtual void SetupAdditionalDependencies() { }

            [SetUp]
            public virtual void SetUp()
            {
                Inheritance = new List<Type>();
                Modifiers = new List<Modifiers>();

                MemberHelperMock 
                    = new Mock<MemberHelper>
                    {
                        CallBase = true
                    };

                UsingHelperMock 
                    = new Mock<UsingHelper>
                    {
                        CallBase = true
                    };

                NamespaceHelperMock 
                    = new Mock<NamespaceHelper>(Namespace)
                    {
                        CallBase = true
                    };

                SetupAdditionalDependencies();

                ClassBuilderMock 
                    = new Mock<ClassBuilderTestClass>(
                        Name, 
                        Inheritance, 
                        Modifiers, 
                        MemberHelperMock.Object, 
                        UsingHelperMock.Object, 
                        NamespaceHelperMock.Object
                    )
                    {
                        CallBase = true
                    };

                ClassBuilder = ClassBuilderMock.Object;
            }

            [TearDown]
            public virtual void TearDown() { }
        }

        internal class ClassBuilderTestClass : ClassBuilder
        {
            public ClassBuilderTestClass(string name, List<Type> inheritance, List<Modifiers> modifiers, MemberHelper memberHelper, UsingHelper usingHelper, NamespaceHelper namespaceHelper)
            : base(name, inheritance, modifiers, memberHelper, usingHelper, namespaceHelper)
            {

            }
        }
    }
}
