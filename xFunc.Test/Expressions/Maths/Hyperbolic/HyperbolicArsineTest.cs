using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Hyperbolic;

namespace xFunc.Test.Expressions.Maths.Hyperbolic
{

    [TestClass]
    public class HyperbolicArsineTest
    {

        [TestMethod]
        public void CalculateTest()
        {
            var exp = new Arsinh(new Number(1));

            Assert.AreEqual(MathExtentions.Asinh(1), exp.Calculate());
        }

        [TestMethod]
        public void DerivativeTest()
        {
            IExpression exp = new Arsinh(new Mul(new Number(2), new Variable("x")));
            IExpression deriv = exp.Differentiate();

            Assert.AreEqual("(2 * 1) / sqrt(((2 * x) ^ 2) + 1)", deriv.ToString());
        }

    }

}
