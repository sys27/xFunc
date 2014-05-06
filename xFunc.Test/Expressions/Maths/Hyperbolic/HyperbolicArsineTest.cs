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

    }

}
