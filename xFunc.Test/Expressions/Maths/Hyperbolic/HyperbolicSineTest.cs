using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Hyperbolic;

namespace xFunc.Test.Expressions.Maths.Hyperbolic
{

    [TestClass]
    public class HyperbolicSineTest
    {
        
        [TestMethod]
        public void CalculateTest()
        {
            var exp = new HyperbolicSine(new Number(1));

            Assert.AreEqual(Math.Sinh(1), exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest()
        {
            IMathExpression exp = new HyperbolicSine(new Multiplication(new Number(2), new Variable('x')));
            IMathExpression deriv = exp.Differentiation();

            Assert.AreEqual("(2 * 1) * cosh(2 * x)", deriv.ToString());
        }

    }

}
