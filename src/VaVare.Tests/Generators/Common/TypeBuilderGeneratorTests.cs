using NUnit.Framework;
using VaVare.Builders;
using VaVare.Generators.Common;
using VaVare.Generators.Common.Arguments.ArgumentTypes;
using VaVare.Models;

namespace VaVare.Tests.Generators.Common
{
    [TestFixture]
    public class TypeBuilderGeneratorTests
    {
        [Test]
        public void Create_WhenNotProvidingAnyTypeParameters_ShouldGetEmptyGenericList()
        {
            Assert.AreEqual("<>", TypeParameterGenerator.Create().ToString());
        }
        
        [Test]
        public void Create_WhenCreatingWithSingleTypeParameter_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("<Test>", TypeParameterGenerator.Create(new TypeParameter("Test")).ToString());
        }
        
        [Test]
        public void Create_WhenCreatingWithMultipleTypeParameters_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("<Test1,Test2>", TypeParameterGenerator.Create(new TypeParameter("Test1"), new TypeParameter("Test2")).ToString());
        }
        
        [Test]
        public void Create_WhenCreatingWithSingleTypeParameterWithVariance_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("<inTest1>", TypeParameterGenerator.Create(new TypeParameter("Test1", Variance.In)).ToString());
            Assert.AreEqual("<outTest1>", TypeParameterGenerator.Create(new TypeParameter("Test1", Variance.Out)).ToString());
        }

        [Test]
        public void Create_WhenCreatingWithSingleTypeParameterWithAttributes_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("<[SomeAttribute]Test1>", TypeParameterGenerator.Create(new TypeParameter("Test1", attributes: new Attribute("SomeAttribute"))).ToString());
        }
        
        [Test]
        public void Create_WhenCreatingWithSingleTypeParameterWithAttributesAndVariance_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("<[SomeAttribute]inTest1>", TypeParameterGenerator.Create(new TypeParameter("Test1", Variance.In, null, new Attribute("SomeAttribute"))).ToString());
        }
    }
}