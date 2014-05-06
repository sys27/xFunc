using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        
    }

}
