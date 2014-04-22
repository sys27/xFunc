using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class CeilTest
    {

        [TestMethod]
        public void CeilCalculate()
        {
            var ceil = new Ceil(new Number(5.55555555));
            var result = ceil.Calculate();
            var expected = 6.0;

            Assert.AreEqual(expected, result);
        }

    }

}
