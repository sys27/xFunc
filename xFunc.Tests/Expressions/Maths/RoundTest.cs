using System;
using xFunc.Maths.Expressions;
using Xunit;

namespace xFunc.Test.Expressions.Maths
{
    
    public class RoundTest
    {

        [Fact]
        public void CalculateRoundWithoutDigits()
        {
            var round = new Round(new Number(5.555555));
            var result = round.Calculate();
            var expected = 6.0;

            Assert.Equal(expected, result);
        }

        [Fact]
        public void CalculateRoundWithDigits()
        {
            var round = new Round(new Number(5.555555), new Number(2));
            var result = round.Calculate();
            var expected = 5.56;

            Assert.Equal(expected, result);
        }

    }

}
