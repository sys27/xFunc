using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class SqrtTest
    {

        [TestMethod]
        public void CalculateTest()
        {
            IExpression exp = new Sqrt(new Number(4));

            Assert.AreEqual(Math.Sqrt(4), exp.Calculate());
        }

        [TestMethod]
        public void NegativeNumberCalculateTest()
        {
            var exp = new Sqrt(new Number(-25));

            Assert.AreEqual(double.NaN, exp.Calculate());
        }

    }

}
