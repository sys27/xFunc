using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Bitwise;

namespace xFunc.Test.Expressions.Maths.Bitwise
{

    [TestClass]
    public class XOrTest
    {

        [TestMethod]
        public void CalculateTest1()
        {
            IMathExpression exp = new XOr(new Number(1), new Number(2));

            Assert.AreEqual(3, exp.Calculate());
        }

        [TestMethod]
        public void CalculateTest2()
        {
            IMathExpression exp = new XOr(new Number(1), new Number(2.5));

            Assert.AreEqual(2, exp.Calculate());
        }

    }

}
