using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Hyperbolic;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class HyperbolicArcosineMathExpressionTest
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
            var exp = parser.Parse("arcosh(1)");

            Assert.AreEqual(MathExtentions.Acosh(1), exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest()
        {
            IMathExpression exp = parser.Parse("deriv(arcosh(2x), x)").Derivative();

            Assert.AreEqual("2 / sqrt(((2 * x) ^ 2) - 1)", exp.ToString());
        }

    }

}
