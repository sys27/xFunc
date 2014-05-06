using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var exp = new Sinh(new Number(1));

            Assert.AreEqual(Math.Sinh(1), exp.Calculate());
        }
        
    }

}
