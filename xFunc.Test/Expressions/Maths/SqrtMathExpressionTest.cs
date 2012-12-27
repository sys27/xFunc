using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Library;
using xFunc.Library.Expressions.Maths;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class SqrtMathExpressionTest
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
            IMathExpression exp = parser.Parse("sqrt(4)");

            Assert.AreEqual(Math.Sqrt(4), exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest()
        {
            IMathExpression exp = MathParser.Derivative(parser.Parse("sqrt(2x)"));

            Assert.AreEqual("1 / sqrt(2 * x)", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = parser.Parse("deriv(sqrt(2xy), x)").Derivative();
            Assert.AreEqual("(2 * y) / (2 * sqrt((2 * x) * y))", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = parser.Parse("deriv(sqrt(y), x)").Derivative();
            Assert.AreEqual("0", exp.ToString());
        }

        [TestMethod]
        public void ToStringTest()
        {
            IMathExpression exp = parser.Parse("sqrt(8)");

            Assert.AreEqual("sqrt(8)", exp.ToString());
        }

    }

}
