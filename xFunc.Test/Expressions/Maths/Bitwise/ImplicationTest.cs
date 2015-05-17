using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions.LogicalAndBitwise;

namespace xFunc.Test.Expressions.Maths.Bitwise
{

    [TestClass]
    public class ImplicationTest
    {

        [TestMethod]
        public void CalculateTest1()
        {
            var impl = new Implication(new Bool(true), new Bool(false));

            Assert.AreEqual(false, impl.Calculate());
        }

        [TestMethod]
        public void CalculateTest2()
        {
            var impl = new Implication(new Bool(true), new Bool(true));

            Assert.AreEqual(true, impl.Calculate());
        }

    }

}
