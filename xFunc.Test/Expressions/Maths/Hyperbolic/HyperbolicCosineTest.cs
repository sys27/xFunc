using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Hyperbolic;

namespace xFunc.Test.Expressions.Maths.Hyperbolic
{

    [TestClass]
    public class HyperbolicCosineTest
    {

        [TestMethod]
        public void CalculateTest()
        {
            var exp = new Cosh(new Number(1));

            Assert.AreEqual(Math.Cosh(1), exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest()
        {
            IMathExpression exp = new Cosh(new Multiplication(new Number(2), new Variable("x")));
            IMathExpression deriv = exp.Differentiate();

            Assert.AreEqual("(2 * 1) * sinh(2 * x)", deriv.ToString());
        }

    }

}
