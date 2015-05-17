using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions.LogicalAndBitwise;

namespace xFunc.Test.Expressions.Maths.Bitwise
{

    [TestClass]
    public class NOrTest
    {

        [TestMethod]
        public void CalculateTest1()
        {
            var nor = new NOr(new Bool(false), new Bool(true));

            Assert.AreEqual(false, nor.Calculate());
        }

        [TestMethod]
        public void CalculateTest2()
        {
            var nor = new NOr(new Bool(false), new Bool(false));

            Assert.AreEqual(true, nor.Calculate());
        }

    }

}
