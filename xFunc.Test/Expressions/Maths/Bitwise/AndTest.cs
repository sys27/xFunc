using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.LogicalAndBitwise;

namespace xFunc.Test.Expressions.Maths.Bitwise
{

    [TestClass]
    public class AndTest
    {
        
        [TestMethod]
        public void CalculateTest1()
        {
            var exp = new And(new Number(1), new Number(3));

            Assert.AreEqual(1, exp.Calculate());
        }

        [TestMethod]
        public void CalculateTest2()
        {
            var exp = new And(new Number(1.5), new Number(2.5));

            Assert.AreEqual(2, exp.Calculate());
        }

    }

}
