using System;
using xFunc.Maths.Expressions;
using Xunit;

namespace xFunc.Test.Expressions.Maths
{
    
    public class FloorTest
    {

        [Fact]
        public void FloorCalculate()
        {
            var floor = new Floor(new Number(5.55555555));
            var result = floor.Calculate();
            var expected = 5.0;

            Assert.Equal(expected, result);
        }

    }

}
