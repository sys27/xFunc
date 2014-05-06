using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class PowTest
    {

        [TestMethod]
        public void CalculateTest()
        {
            IExpression exp = new Pow(new Number(2), new Number(10));

            Assert.AreEqual(1024.0, exp.Calculate());
        }

    }

}
