using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.LogicalAndBitwise;

namespace xFunc.Test.Expressions.Maths.Bitwise
{

    [TestClass]
    public class NotTest
    {
        
        [TestMethod]
        public void CalculateTest1()
        {
            var exp = new Not(new Number(2));

            Assert.AreEqual(-3, exp.Calculate());
        }

        [TestMethod]
        public void CalculateTest2()
        {
            var exp = new Not(new Number(2.5));

            Assert.AreEqual(-4, exp.Calculate());
        }

    }

}
