using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

            Assert.AreEqual(Math.Cosh(1), exp.Calculate());
        }

    }

}
