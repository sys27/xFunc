using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Library;
using xFunc.Library.Expressions.Maths;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class DivisionMathExpressionTest
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
            IMathExpression exp = parser.Parse("1 / 2");

            Assert.AreEqual(1.0 / 2.0, exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest()
        {
            IMathExpression exp = MathParser.Derivative(parser.Parse("1 / x"));

            Assert.AreEqual("-1 / (x ^ 2)", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IMathExpression exp = MathParser.Derivative(parser.Parse("sin(x) / x"));

            Assert.AreEqual("((cos(x) * x) - sin(x)) / (x ^ 2)", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = parser.Parse("deriv((y + x ^ 2) / x, x)").Derivative();
            Assert.AreEqual("(((2 * x) * x) - (y + (x ^ 2))) / (x ^ 2)", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = parser.Parse("deriv(y / x, x)").Derivative();
            Assert.AreEqual("-y / (x ^ 2)", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IMathExpression exp = parser.Parse("deriv(y / x, y)").Derivative();
            Assert.AreEqual("1 / x", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest4()
        {
            IMathExpression exp = parser.Parse("deriv((x + 1) / x, y)").Derivative();
            Assert.AreEqual("0", exp.ToString());
        }

        [TestMethod]
        public void ToStringTest()
        {
            IMathExpression exp = parser.Parse("1 / 2");

            Assert.AreEqual("1 / 2", exp.ToString());
        }

    }

}
