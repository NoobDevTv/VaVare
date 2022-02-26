using NUnit.Framework;
using VaVare.Models.References;
using VaVare.Statements;
using Assert = NUnit.Framework.Assert;

namespace VaVare.Tests.Statements
{
    [TestFixture]
    public class JumpStatementTests
    {
        private JumpStatement _return;

        [SetUp]
        public void SetUp()
        {
            _return = new JumpStatement();
        }

        [Test]
        public void ReturnTrue_WhenReturnTrue_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("returntrue;", _return.ReturnTrue().ToString());
        }

        [Test]
        public void ReturnFalse_WhenReturnFalse_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("returnfalse;", _return.ReturnFalse().ToString());
        }

        [Test]
        public void Return_WhenReturnReference_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("returni;", _return.Return(new VariableReference("i")).ToString());
        }

        [Test]
        public void Return_WhenReturnExpression_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("returntest();", _return.Return(Statement.Expression.Invoke("test").AsExpression()).ToString());
        }

        [Test]
        public void Return_WhenReturnThis_ShouldGenerateCorrectCode()
        {
            Assert.AreEqual("returnthis;", _return.ReturnThis().ToString());
        }
    }
}
