using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class DivTest
    {

        [TestMethod]
        public void CalculateTest()
        {
            IExpression exp = new Div(new Number(1), new Number(2));

            Assert.AreEqual(1.0 / 2.0, exp.Calculate());
        }

    }

}
