using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.LogicalAndBitwise;

namespace xFunc.Test.Expressions.Maths.Bitwise
{

    [TestClass]
    public class EqualityTest
    {

        [TestMethod]
        public void CalculateTest1()
        {
            var eq = new Equality(new Bool(true), new Bool(true));

            Assert.AreEqual(true, eq.Calculate());
        }

        [TestMethod]
        public void CalculateTest2()
        {
            var eq = new Equality(new Bool(true), new Bool(false));

            Assert.AreEqual(false, eq.Calculate());
        }

    }

}
