using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class LCMTest
    {

        [TestMethod]
        public void CalculateTest1()
        {
            var exp = new LCM(new Number(12), new Number(16));

            Assert.AreEqual(48, exp.Calculate());
        }

        [TestMethod]
        public void CalculateTest2()
        {
            var exp = new LCM(new IExpression[] { new Number(4), new Number(16), new Number(8) }, 3);

            Assert.AreEqual(16, exp.Calculate());
        }

    }

}
