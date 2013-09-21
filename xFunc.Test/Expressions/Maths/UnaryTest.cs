using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions.Trigonometric;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class UnaryTest
    {

        [TestMethod]
        public void EqualsTest1()
        {
            var sine1 = new Sin(new Number(2));
            var sine2 = new Sin(new Number(2));

            Assert.AreEqual(sine1, sine2);
        }

        [TestMethod]
        public void EqualsTest2()
        {
            var sine = new Sin(new Number(2));
            var ln = new Ln(new Number(2));

            Assert.AreNotEqual(sine, ln);
        }

    }

}
