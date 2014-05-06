using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Hyperbolic;

namespace xFunc.Test.Expressions.Maths.Hyperbolic
{

    [TestClass]
    public class HyperbolicArcosineTest
    {

        [TestMethod]
        public void CalculateTest()
        {
            var exp = new Arcosh(new Number(1));

            Assert.AreEqual(MathExtentions.Acosh(1), exp.Calculate());
        }

    }

}
