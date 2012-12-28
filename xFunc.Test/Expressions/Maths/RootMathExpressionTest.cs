using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Library.Maths;
using xFunc.Library.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class RootMathExpressionTest
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
            IMathExpression exp = parser.Parse("root(8, 3)");

            Assert.AreEqual(Math.Pow(8, 1.0 / 3.0), exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest()
        {
            IMathExpression exp = MathParser.Derivative(parser.Parse("root(x, 3)"));

            Assert.AreEqual("(1 / 3) * (x ^ ((1 / 3) - 1))", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = parser.Parse("deriv(root(xy, 3), x)").Derivative();
            Assert.AreEqual("y * ((1 / 3) * ((x * y) ^ ((1 / 3) - 1)))", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = parser.Parse("deriv(root(y, 3), x)").Derivative();
            Assert.AreEqual("0", exp.ToString());
        }

        [TestMethod]
        public void ToStringTest()
        {
            IMathExpression exp = parser.Parse("root(8, 3)");

            Assert.AreEqual("root(8, 3)", exp.ToString());
        }

    }

}
