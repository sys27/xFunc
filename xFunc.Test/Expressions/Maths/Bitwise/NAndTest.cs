using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.LogicalAndBitwise;

namespace xFunc.Test.Expressions.Maths.Bitwise
{

    [TestClass]
    public class NAndTest
    {

        [TestMethod]
        public void CalculateTest1()
        {
            var nand = new NAnd(new Bool(true), new Bool(true));

            Assert.AreEqual(false, nand.Calculate());
        }

        [TestMethod]
        public void CalculateTest2()
        {
            var nand = new NAnd(new Bool(false), new Bool(true));

            Assert.AreEqual(true, nand.Calculate());
        }

    }

}
