using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class LnTest
    {

        [TestMethod]
        public void CalculateTest()
        {
            IExpression exp = new Ln(new Number(2));

            Assert.AreEqual(Math.Log(2), exp.Calculate());
        }

    }

}
