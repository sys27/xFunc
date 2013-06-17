using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class LCMTest
    {

        [TestMethod]
        public void CalculateTest()
        {
            var exp = new LCM(new Number(12), new Number(16));

            Assert.AreEqual(48, exp.Calculate());
        }

    }

}
