using NUnit.Framework;
using VaVare.Generators.Common.BinaryExpressions;
using VaVare.Models.References;

namespace VaVare.Tests.Generators.Common.BinaryExpressions
{
    [TestFixture]
    public class ConditionalBinaryExpressionTests
    {
        [Test]
        public void GetBinaryExpression_WhenHavingTwoReferencesAndEqual_ShouldGenerateCode()
        {
            var binaryExpression = new ConditionalBinaryExpression(
                new ConstantReference(1), 
                new ConstantReference(2),
                ConditionalStatements.Equal);
            Assert.AreEqual("1==2", binaryExpression.GetBinaryExpression().ToString());
        }


    }
}
