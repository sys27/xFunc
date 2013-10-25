using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class GCDTest
    {

        [TestMethod]
        public void CalcucateTest1()
        {
            var exp = new GCD(new Number(12), new Number(16));

            Assert.AreEqual(4, exp.Calculate());
        }

        [TestMethod]
        public void CalcucateTest2()
        {
            var exp = new GCD(new IMathExpression[] { new Number(64), new Number(16), new Number(8) }, 3);

            Assert.AreEqual(8, exp.Calculate());
        }

    }

}
