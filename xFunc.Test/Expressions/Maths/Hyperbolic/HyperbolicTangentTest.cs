using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Hyperbolic;

namespace xFunc.Test.Expressions.Maths.Hyperbolic
{

    [TestClass]
    public class HyperbolicTangentTest
    {

        [TestMethod]
        public void CalculateTest()
        {
            var exp = new Tanh(new Number(1));

            Assert.AreEqual(Math.Tanh(1), exp.Calculate());
        }

        [TestMethod]
        public void DerivativeTest()
        {
            IMathExpression exp = new Tanh(new Mul(new Number(2), new Variable("x")));
            IMathExpression deriv = exp.Differentiate();

            Assert.AreEqual("(2 * 1) / (cosh(2 * x) ^ 2)", deriv.ToString());
        }

    }

}
