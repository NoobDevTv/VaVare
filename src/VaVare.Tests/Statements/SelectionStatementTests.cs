using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;
using VaVare.Generators.Common;
using VaVare.Generators.Common.Arguments.ArgumentTypes;
using VaVare.Generators.Common.BinaryExpressions;
using VaVare.Generators.Common.Patterns;
using VaVare.Models.References;
using VaVare.Statements;
using Assert = NUnit.Framework.Assert;

namespace VaVare.Tests.Statements
{
    [TestFixture]
    public class SelectionStatementTests
    {
        private SelectionStatement _conditional;

        [OneTimeSetUp]
        public void SetUp()
        {
            _conditional = new SelectionStatement();
        }

        [Test]
        public void If_WhenCreatingAnIfWithEqual_ShouldGenerateCorrectIfStatement()
        {
            Assert.AreEqual("if(2==3){}",
                _conditional.If(new ValueArgument(2), new ValueArgument(3), ConditionalStatements.Equal, BodyGenerator.Create()).ToString());
        }

        [Test]
        public void If_WhenCreatingAnIfWithEqualAndExpressionStatement_ShouldGenerateCorrectIfStatementWithoutBraces()
        {
            Assert.AreEqual("if(2==3)MyMethod();",
                _conditional.If(new ValueArgument(2), new ValueArgument(3), ConditionalStatements.Equal, Statement.Expression.Invoke("MyMethod").AsStatement()).ToString());
        }

        [Test]
        public void If_WhenCreatingAnIfWithNotEqual_ShouldGenerateCorrectIfStatement()
        {
            Assert.AreEqual("if(2!=3){}",
                _conditional.If(new ValueArgument(2), new ValueArgument(3), ConditionalStatements.NotEqual, BodyGenerator.Create()).ToString());
        }

        [Test]
        public void If_WhenCreatingAnIfWithGreaterThan_ShouldGenerateCorrectIfStatement()
        {
            Assert.AreEqual("if(2>3){}",
                _conditional.If(new ValueArgument(2), new ValueArgument(3), ConditionalStatements.GreaterThan, BodyGenerator.Create()).ToString());
        }

        [Test]
        public void If_WhenCreatingAnIfWithGreaterThanOrEqual_ShouldGenerateCorrectIfStatement()
        {
            Assert.AreEqual("if(2>=3){}",
                _conditional.If(new ValueArgument(2), new ValueArgument(3), ConditionalStatements.GreaterThanOrEqual, BodyGenerator.Create()).ToString());
        }

        [Test]
        public void If_WhenCreatingAnIfWithLessThan_ShouldGenerateCorrectIfStatement()
        {
            Assert.AreEqual("if(2<3){}",
                _conditional.If(new ValueArgument(2), new ValueArgument(3), ConditionalStatements.LessThan, BodyGenerator.Create()).ToString());
        }

        [Test]
        public void If_WhenCreatingAnIfWithLessThanOrEqual_ShouldGenerateCorrectIfStatement()
        {
            Assert.AreEqual("if(2<=3){}",
                _conditional.If(new ValueArgument(2), new ValueArgument(3), ConditionalStatements.LessThanOrEqual, BodyGenerator.Create()).ToString());
        }

        [Test]
        public void If_WhenCreatingAnBinaryExpression_ShouldGenerateCorrectIfStatement()
        {
            Assert.AreEqual("if(2<=3){}",
                _conditional.If(new ConditionalBinaryExpression(new ConstantReference(2), new ConstantReference(3), ConditionalStatements.LessThanOrEqual), BodyGenerator.Create()).ToString());
        }

        [Test]
        public void If_WhenCreatingAnComplexBinaryExpression_ShouldGenerateCorrectIfStatement()
        {
            var leftBinaryExpression = new ConditionalBinaryExpression(
                new ConstantReference(1),
                new ConstantReference(2),
                ConditionalStatements.Equal);

            var rightBinaryExpression = new ConditionalBinaryExpression(
                new ConstantReference(1),
                new ConstantReference(2),
                ConditionalStatements.LessThan);

            var orBinaryExpression = new OrBinaryExpression(
                leftBinaryExpression,
                rightBinaryExpression);

            var binaryExpression = new OrBinaryExpression(
                leftBinaryExpression,
                orBinaryExpression);

            Assert.AreEqual("if(1==2||1==2||1<2){}",
                _conditional.If(binaryExpression, BodyGenerator.Create()).ToString());
        }

        [Test]
        public void If_WhenCreatingAnIfWithBinaryExpressionAndExpressionStatement_ShouldGenerateCorrectIfStatementWithoutBraces()
        {
            Assert.AreEqual("if(2==3)MyMethod();",
                _conditional.If(new ConditionalBinaryExpression(new ConstantReference(2), new ConstantReference(3), ConditionalStatements.Equal), Statement.Expression.Invoke("MyMethod").AsStatement()).ToString());
        }

        [Test]
        public void If_ConditionalIsExpression()
        {
            var expected = "if(a is string){}".Replace(" ", "");
            var sut = conditional.If(new ValueArgument("a", false), new ValueArgument("string", false), ConditionalStatements.Is, BodyGenerator.Create());

            Assert.That(sut.ToFullString(), Is.EqualTo(expected));
        }

        [Test]
        public void If_DeclarationPattern()
        {
            var expected = "if(a is string b){}".Replace(" ", "");

            var declarationPattern = new DeclarationPattern(SyntaxFactory.ParseTypeName("string"), "b");
            var sut = conditional.If(new ValueArgument("a", false), declarationPattern, BodyGenerator.Create());

            Assert.That(sut.ToFullString(), Is.EqualTo(expected));
        }

        [Test]
        public void If_NotTypePattern()
        {
            var expected = "if(a is not string){}".Replace(" ", "");

            var typePattern = new TypePattern(SyntaxFactory.ParseTypeName("string"));
            var pattern = new NotPattern(typePattern);
            var sut = conditional.If(new ValueArgument("a", false), pattern, BodyGenerator.Create());

            Assert.That(sut.ToFullString(), Is.EqualTo(expected));
        }

        [Test]
        public void If_NotConstantPattern()
        {
            var expected = "if(a is not 12){}".Replace(" ", "");

            var typePattern = new ConstantPattern(new ValueArgument(12));
            var pattern = new NotPattern(typePattern);
            var sut = conditional.If(new ValueArgument("a", false), pattern, BodyGenerator.Create());

            Assert.That(sut.ToFullString(), Is.EqualTo(expected));
        }

        [Test]
        public void If_NotAndRelationalPattern()
        {
            var expected = "if(a is not 12 and > 15){}".Replace(" ", "");

            var typePattern = new ConstantPattern(new ValueArgument(12));
            var relationPattern = new RelationalPattern(ConditionalStatements.GreaterThan, new ValueArgument(15));
            var and = new AndPattern(typePattern, relationPattern);
            var pattern = new NotPattern(and);
            var sut = conditional.If(new ValueArgument("a", false), pattern, BodyGenerator.Create());

            Assert.That(sut.ToFullString(), Is.EqualTo(expected));
        }

        [Test]
        public void If_NotAndRelationalPatternGreaterEquals()
        {
            var expected = "if(a is not 12 and >= 15){}".Replace(" ", "");

            var typePattern = new ConstantPattern(new ValueArgument(12));
            var relationPattern = new RelationalPattern(ConditionalStatements.GreaterThanOrEqual, new ValueArgument(15));
            var and = new AndPattern(typePattern, relationPattern);
            var pattern = new NotPattern(and);
            var sut = conditional.If(new ValueArgument("a", false), pattern, BodyGenerator.Create());

            Assert.That(sut.ToFullString(), Is.EqualTo(expected));
        }

        [Test]
        public void If_NotOrRelationalPatternGreaterEquals()
        {
            var expected = "if(a is not 12 or >= 15){}".Replace(" ", "");

            var typePattern = new ConstantPattern(new ValueArgument(12));
            var relationPattern = new RelationalPattern(ConditionalStatements.GreaterThanOrEqual, new ValueArgument(15));
            var and = new OrPattern(typePattern, relationPattern);
            var pattern = new NotPattern(and);
            var sut = conditional.If(new ValueArgument("a", false), pattern, BodyGenerator.Create());

            Assert.That(sut.ToFullString(), Is.EqualTo(expected));
        }
    }
}
