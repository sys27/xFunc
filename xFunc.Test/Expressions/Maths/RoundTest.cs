using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class RoundTest
    {

        [TestMethod]
        public void CalculateRoundWithoutDigits()
        {
            var round = new Round(new Number(5.555555));
            var result = round.Calculate();
            var expected = 6.0;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CalculateRoundWithDigits()
        {
            var round = new Round(new Number(5.555555), new Number(2));
            var result = round.Calculate();
            var expected = 5.56;

            Assert.AreEqual(expected, result);
        }

    }

}
