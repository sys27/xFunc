using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Library;
using xFunc.Library.Expressions.Maths;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class ExponentiationMathExpressionTest
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
            IMathExpression exp = parser.Parse("2^10");

            Assert.AreEqual(1024, exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IMathExpression exp = MathParser.Derivative(parser.Parse("x^3"));

            Assert.AreEqual("3 * (x ^ 2)", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            IMathExpression exp = MathParser.Derivative(parser.Parse("2^(3x)"));

            Assert.AreEqual("(ln(2) * (2 ^ (3 * x))) * 3", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = parser.Parse("deriv((y * x) ^ 3, x)").Derivative();
            Assert.AreEqual("y * (3 * ((y * x) ^ 2))", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = parser.Parse("deriv((y * x) ^ 3, y)").Derivative();
            Assert.AreEqual("x * (3 * ((y * x) ^ 2))", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IMathExpression exp = parser.Parse("deriv(y ^ 3, x)").Derivative();
            Assert.AreEqual("0", exp.ToString());
        }

        [TestMethod]
        public void ToStringTest1()
        {
            IMathExpression exp = parser.Parse("x^10+1");

            Assert.AreEqual("(x ^ 10) + 1", exp.ToString());
        }

        [TestMethod]
        public void ToStringTest2()
        {
            IMathExpression exp = parser.Parse("2^(3x)");

            Assert.AreEqual("2 ^ (3 * x)", exp.ToString());
        }

    }

}
