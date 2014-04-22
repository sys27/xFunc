using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class FloorTest
    {

        [TestMethod]
        public void FloorCalculate()
        {
            var floor = new Floor(new Number(5.55555555));
            var result = floor.Calculate();
            var expected = 5.0;

            Assert.AreEqual(expected, result);
        }

    }

}
