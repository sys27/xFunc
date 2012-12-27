using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Library;
using xFunc.Library.Expressions.Maths;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class AdditionMathExpressionTest
    {

        private MathParser parser;

        [TestInitialize]
        public void TestInit()
        {
            parser = new MathParser();
        }

        [TestMethod]
        public void CalculateTest()
        {
            IMathExpression exp = parser.Parse("1+2");

            Assert.AreEqual(3, exp.Calculate(null));
        }

        [TestMethod]
        public void CalculateTest1()
        {
            IMathExpression exp = parser.Parse("-3+2");

            Assert.AreEqual(-1, exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest()
        {
            IMathExpression exp = MathParser.Derivative(parser.Parse("2x + 3"));

            Assert.AreEqual("2", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IMathExpression exp = MathParser.Derivative(parser.Parse("2x + 3x"));

            Assert.AreEqual("5", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = parser.Parse("deriv(xy + x + y, x)").Derivative();
            Assert.AreEqual("y + 1", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = parser.Parse("deriv(xy + x + y, y)").Derivative();
            Assert.AreEqual("x + 1", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IMathExpression exp = parser.Parse("deriv(x + 1, y)").Derivative();
            Assert.AreEqual("0", exp.ToString());
        }

        [TestMethod]
        public void ToStringTest()
        {
            IMathExpression exp = parser.Parse("sin(1)+2");

            Assert.AreEqual("sin(1) + 2", exp.ToString());
        }

        [TestMethod]
        public void ToStringTest1()
        {
            IMathExpression exp = parser.Parse("sin(1) + x + 2");

            Assert.AreEqual("(sin(1) + x) + 2", exp.ToString());
        }

    }

}
