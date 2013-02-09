using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Hyperbolic;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class HyperbolicArcosecantMathExpressionTest
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
            var exp = parser.Parse("arcsch(1)");

            Assert.AreEqual(MathExtentions.Acsch(1), exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest()
        {
            IMathExpression exp = parser.Parse("deriv(arcsch(2x), x)").Differentiation();

            Assert.AreEqual("-(2 / (abs(2 * x) * sqrt(1 + ((2 * x) ^ 2))))", exp.ToString());
        }

    }

}
