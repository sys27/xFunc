using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Hyperbolic;

namespace xFunc.Test.Expressions.Maths.Hyperbolic
{

    [TestClass]
    public class HyperbolicCosecantTest
    {

        [TestMethod]
        public void CalculateTest()
        {
            var exp = new Csch(new Number(1));

            Assert.AreEqual(MathExtentions.Csch(1), exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest()
        {
            IMathExpression exp = new Csch(new Multiplication(new Number(2), new Variable("x")));
            IMathExpression deriv = exp.Differentiate();

            Assert.AreEqual("-((2 * 1) * (coth(2 * x) * csch(2 * x)))", deriv.ToString());
        }

    }

}
