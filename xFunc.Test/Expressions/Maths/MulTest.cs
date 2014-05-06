using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class MulTest
    {

        [TestMethod]
        public void CalculateTest()
        {
            IExpression exp = new Mul(new Number(2), new Number(2));

            Assert.AreEqual(4.0, exp.Calculate());
        }

    }

}
