using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Bitwise;

namespace xFunc.Test.Expressions.Maths.Bitwise
{

    [TestClass]
    public class OrTest
    {

        [TestMethod]
        public void CalculateTest1()
        {
            IExpression exp = new Or(new Number(1), new Number(2));

            Assert.AreEqual(3, exp.Calculate());
        }

        [TestMethod]
        public void CalculateTest2()
        {
            IExpression exp = new Or(new Number(4), new Number(2.5));

            Assert.AreEqual(7, exp.Calculate());
        }

    }

}
