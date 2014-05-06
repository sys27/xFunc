using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class LogTest
    {

        [TestMethod]
        public void CalculateTest()
        {
            IExpression exp = new Log(new Number(10), new Number(2));

            Assert.AreEqual(Math.Log(10, 2), exp.Calculate());
        }

    }

}
