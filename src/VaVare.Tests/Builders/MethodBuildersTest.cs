using NUnit.Framework;
using VaVare.Builders;
using VaVare.Generators.Common;
using VaVare.Models;

namespace VaVare.Tests.Builders
{
    [TestFixture]
    public class MethodBuildersTest
    {
        [Test]
        public void Build_WhenGivingMethodName_CodeShouldContainName()
        {
            var builder = new MethodBuilder("MyMethod");
            Assert.IsTrue(builder.Build().ToString().Contains("MyMethod()"));
        }

        [Test]
        public void Build_WhenGivingAttribute_CodeShouldContainAttribute()
        {
            var builder = new MethodBuilder("MyMethod");
            Assert.IsTrue(builder.WithAttributes(new Attribute("MyAttribute")).Build().ToString()
                .Contains("[MyAttribute]"));
        }

        [Test]
        public void Build_WhenGivingModifier_CodeShouldContainModifiers()
        {
            var builder = new MethodBuilder("MyMethod");
            Assert.IsTrue(builder.WithModifiers(Modifiers.Public, Modifiers.Abstract).Build().ToString()
                .Contains("publicabstract"));
        }

        [Test]
        public void Build_WhenGivingParameters_CodeShouldContainParamters()
        {
            var builder = new MethodBuilder("MyMethod");
            Assert.IsTrue(builder.WithParameters(new Parameter("myParamter", typeof(int))).Build().ToString()
                .Contains("intmyParamter"));
        }

        [Test]
        public void Build_WhenGivingParameterWithModifier_CodeShouldContainParamters()
        {
            var builder = new MethodBuilder("MyMethod");
            Assert.IsTrue(builder.WithParameters(new Parameter("myParamter", typeof(int), ParameterModifiers.This))
                .Build().ToString().Contains("thisintmyParamter"));
        }

        [Test]
        public void Build_WhenGivingReturnType_CodeShouldContainReturn()
        {
            var builder = new MethodBuilder("MyMethod");
            Assert.IsTrue(builder.WithReturnType(typeof(int)).Build().ToString().Contains("intMyMethod()"));
        }

        [Test]
        public void Build_WhenGivingNullBody_CodeShouldContainMethodWithSemicolonAtTheEnd()
        {
            var builder = new MethodBuilder("MyMethod");
            Assert.IsTrue(builder.WithBody(null).Build().ToString().Contains("MyMethod();"));
        }

        [Test]
        public void Build_WhenHavingOperatorOverloading_ShouldGenerateOverloading()
        {
            var builder = new MethodBuilder("MyMethod")
                .WithModifiers(Modifiers.Public, Modifiers.Static)
                .WithOperatorOverloading(Operators.Equal)
                .WithBody(BodyGenerator.Create());

            StringAssert.Contains("publicstaticMyMethodoperator==(){}",builder.Build().ToString());
        }
        
        [Test]
        public void Build_WhenGivenTypeParameter_CodeShouldContainTypeParameter()
        {
            var builder = new MethodBuilder("MyMethod");
            Assert.IsTrue(builder.WithTypeParameters(new TypeParameter("T", Variance.In)).Build().ToString().Contains("MyMethod<inT>"));
        }

        [Test]
        public void Build_WhenGivenTypeParameters_CodeShouldContainTypeParameters()
        {
            var builder = new MethodBuilder("MyMethod");
            Assert.IsTrue(builder.WithTypeParameters(new TypeParameter("T", Variance.In), new TypeParameter("U", Variance.Out)).Build().ToString().Contains("MyMethod<inT,outU>"));
        }

        [Test]
        public void Build_WhenGivenTypeParameterConstraints_CodeShouldContainTypeParameterConstraints()
        {
            var builder = new MethodBuilder("MyMethod");
            Assert.IsTrue(builder.WithTypeParameters(new TypeParameter("T")).WithTypeConstraintClauses(new TypeParameterConstraintClause("T", new TypeParameterConstraint(TypeParameterConstraint.ConstraintType.Constructor), new TypeParameterConstraint(TypeParameterConstraint.ConstraintType.Class))).Build().ToString().Contains("MyMethod<T>()whereT:new(),class"));
        }
    }
}