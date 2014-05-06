using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class LgTest
    {

        [TestMethod]
        public void CalculateTest()
        {
            IExpression exp = new Lg(new Number(2));

            Assert.AreEqual(Math.Log10(2), exp.Calculate());
        }

    }

}
