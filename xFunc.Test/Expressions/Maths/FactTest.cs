using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class FactTest
    {

        [TestMethod]
        public void CalculateTest1()
        {
            var fact = new Fact(new Number(4));

            Assert.AreEqual(24, fact.Calculate());
        }

        [TestMethod]
        public void CalculateTest2()
        {
            var fact = new Fact(new Number(0));

            Assert.AreEqual(1, fact.Calculate());
        }

        [TestMethod]
        public void CalculateTest3()
        {
            var fact = new Fact(new Number(1));

            Assert.AreEqual(1, fact.Calculate());
        }

        [TestMethod]
        public void CalculateTest4()
        {
            var fact = new Fact(new Number(-1));

            Assert.AreEqual(double.NaN, fact.Calculate());
        }

    }

}
