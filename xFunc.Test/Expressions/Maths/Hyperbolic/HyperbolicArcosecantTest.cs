using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Hyperbolic;

namespace xFunc.Test.Expressions.Maths.Hyperbolic
{

    [TestClass]
    public class HyperbolicArcosecantTest
    {

        [TestMethod]
        public void CalculateTest()
        {
            var exp = new HyperbolicArcosecant(new Number(1));

            Assert.AreEqual(MathExtentions.Acsch(1), exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest()
        {
            IMathExpression exp = new HyperbolicArcosecant(new Multiplication(new Number(2), new Variable('x')));
            IMathExpression deriv = exp.Differentiation();

            Assert.AreEqual("-((2 * 1) / (abs(2 * x) * sqrt(1 + ((2 * x) ^ 2))))", deriv.ToString());
        }

    }

}
