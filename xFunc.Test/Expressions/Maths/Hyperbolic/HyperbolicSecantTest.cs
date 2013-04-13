using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Hyperbolic;

namespace xFunc.Test.Expressions.Maths.Hyperbolic
{

    [TestClass]
    public class HyperbolicSecantTest
    {

        [TestMethod]
        public void CalculateTest()
        {
            var exp = new HyperbolicSecant(new Number(1));

            Assert.AreEqual(MathExtentions.Sech(1), exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest()
        {
            IMathExpression exp = new HyperbolicSecant(new Multiplication(new Number(2), new Variable("x")));
            IMathExpression deriv = exp.Differentiate();

            Assert.AreEqual("-((2 * 1) * (tanh(2 * x) * sech(2 * x)))", deriv.ToString());
        }

    }

}
