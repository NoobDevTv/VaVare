using System;
using System.Collections.Generic;
using NUnit.Framework;
using VaVare.Builders;
using VaVare.Generators.Common;
using VaVare.Generators.Special;
using VaVare.Models.References;
using VaVare.Statements;
using Attribute = VaVare.Models.Attribute;

namespace VaVare.Tests.Integration
{
    [TestFixture]
    class NunitTest
    {
        [Test]
        public void Test_ArgumentNull()
        {
            var classBuilder = new ClassBuilder("NullTest", "MyTest");
            var @class = classBuilder
                .WithUsings("System", "NUnit.Framework")
                .WithModifiers(Modifiers.Public)
                .WithMethods(
                    new MethodBuilder("SetUp")
                        .WithAttributes(new Attribute("SetUp"))
                        .WithModifiers(Modifiers.Public)
                        .Build(),
                    new MethodBuilder("Test_WhenAddingNumber_ShouldBeCorrectSum")
                        .WithAttributes(new Attribute("Test"))
                        .WithModifiers(Modifiers.Public)
                        .WithBody(
                            BodyGenerator.Create(
                                Statement.Declaration.Declare("myList", typeof(List<int>)),
                                NunitAssertGenerator.Throws(new VariableReference("myList", new MethodReference("First")), typeof(ArgumentNullException))))
                                .Build())
                .Build();
            Assert.AreEqual(@"usingSystem;usingNUnit.Framework;namespaceMyTest{publicclassNullTest{[SetUp]publicvoidSetUp(){}[Test]publicvoidTest_WhenAddingNumber_ShouldBeCorrectSum(){List<int>myList;Assert.Throws<ArgumentNullException>(()=>myList.First(),"""");}}}", @class.ToString());
        }
    }
}
