using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class GCDTest
    {

        [TestMethod]
        public void CalcucateTest()
        {
            var exp = new GCD(new Number(12), new Number(16));

            Assert.AreEqual(4, exp.Calculate());
        }

    }

}
