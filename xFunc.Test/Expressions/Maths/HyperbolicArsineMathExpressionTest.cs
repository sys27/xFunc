using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class HyperbolicArsineMathExpressionTest
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
            var exp = parser.Parse("arsinh(1)");

            Assert.AreEqual(MathExtentions.Asinh(1), exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest()
        {
            IMathExpression exp = parser.Parse("deriv(arsinh(2x), x)").Derivative();

            Assert.AreEqual("2 / sqrt(((2 * x) ^ 2) + 1)", exp.ToString());
        }

    }

}
