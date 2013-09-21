using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class BinaryTest
    {

        [TestMethod]
        public void EqualsTest1()
        {
            var add1 = new Add(new Number(2), new Number(3));
            var add2 = new Add(new Number(2), new Number(3));

            Assert.AreEqual(add1, add2);
        }

        [TestMethod]
        public void EqualsTest2()
        {
            var add = new Add(new Number(2), new Number(3));
            var sub = new Sub(new Number(2), new Number(3));

            Assert.AreNotEqual(add, sub);
        }

    }

}
