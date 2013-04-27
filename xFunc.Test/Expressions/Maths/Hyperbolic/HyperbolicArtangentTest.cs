using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Hyperbolic;

namespace xFunc.Test.Expressions.Maths.Hyperbolic
{

    [TestClass]
    public class HyperbolicArtangentTest
    {

        [TestMethod]
        public void CalculateTest()
        {
            var exp = new Artanh(new Number(1));

            Assert.AreEqual(MathExtentions.Atanh(1), exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest()
        {
            IMathExpression exp = new Artanh(new Multiplication(new Number(2), new Variable("x")));
            IMathExpression deriv = exp.Differentiate();

            Assert.AreEqual("(2 * 1) / (1 - ((2 * x) ^ 2))", deriv.ToString());
        }

    }

}
