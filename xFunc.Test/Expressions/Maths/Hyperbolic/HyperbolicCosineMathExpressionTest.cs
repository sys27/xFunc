using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Hyperbolic;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class HyperbolicCosineMathExpressionTest
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
            var exp = parser.Parse("cosh(1)");

            Assert.AreEqual(Math.Cosh(1), exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest()
        {
            IMathExpression exp = parser.Parse("deriv(cosh(2x), x)").Differentiation();

            Assert.AreEqual("2 * sinh(2 * x)", exp.ToString());
        }

    }

}
