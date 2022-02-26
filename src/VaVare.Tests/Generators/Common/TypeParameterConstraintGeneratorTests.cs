using NUnit.Framework;
using VaVare.Generators.Common;
using VaVare.Models;

namespace VaVare.Tests.Generators.Common
{
    [TestFixture]
    public class TypeParameterConstraintGeneratorTests
    {
        [Test]
        public void Build_TypeParameterConstraintType_CodeShouldContainCorrectTypeParameterConstraintType()
        {
            Assert.IsTrue(TypeParameterConstraintGenerator.Create(new TypeParameterConstraintClause("T", new TypeParameterConstraint(TypeParameterConstraint.ConstraintType.Constructor))).ToString().Contains("whereT:new()"));
            Assert.IsTrue(TypeParameterConstraintGenerator.Create(new TypeParameterConstraintClause("T", new TypeParameterConstraint(TypeParameterConstraint.ConstraintType.Class))).ToString().Contains("whereT:class"));
            Assert.IsTrue(TypeParameterConstraintGenerator.Create(new TypeParameterConstraintClause("T", new TypeParameterConstraint(TypeParameterConstraint.ConstraintType.Struct))).ToString().Contains("whereT:struct"));
            Assert.IsTrue(TypeParameterConstraintGenerator.Create(new TypeParameterConstraintClause("T", new TypeParameterConstraint(TypeParameterConstraint.ConstraintType.Default))).ToString().Contains("whereT:default"));
            Assert.IsTrue(TypeParameterConstraintGenerator.Create(new TypeParameterConstraintClause("T", new TypeParameterConstraint("SomeClass"))).ToString().Contains("whereT:SomeClass"));
        }

        [Test]
        public void Build_TypeParameterConstraintTypes_CodeShouldContainCorrectTypeParameterConstraintTypes()
        {
            Assert.IsTrue(TypeParameterConstraintGenerator.Create(new TypeParameterConstraintClause("T", new TypeParameterConstraint(TypeParameterConstraint.ConstraintType.Constructor), new TypeParameterConstraint("SomeClass"))).ToString().Contains("whereT:new(),SomeClass"));
        }
        
        [Test]
        public void Build_MultipleConstraintClauses_CodeShouldContainTypeClauses()
        {
            Assert.IsTrue(TypeParameterConstraintGenerator.Create(new []
            {
                new TypeParameterConstraintClause("T", new TypeParameterConstraint(TypeParameterConstraint.ConstraintType.Constructor), new TypeParameterConstraint("SomeClass")),
                new TypeParameterConstraintClause("U", new TypeParameterConstraint(TypeParameterConstraint.ConstraintType.Default), new TypeParameterConstraint("SomeOtherClass"))
            }).ToString().Contains("whereT:new(),SomeClasswhereU:default,SomeOtherClass"));
        }
    }
}