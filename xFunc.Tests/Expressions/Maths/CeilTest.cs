using System;
using xFunc.Maths.Expressions;
using Xunit;

namespace xFunc.Test.Expressions.Maths
{
    
    public class CeilTest
    {

        [Fact]
        public void CeilCalculate()
        {
            var ceil = new Ceil(new Number(5.55555555));
            var result = ceil.Calculate();
            var expected = 6.0;

            Assert.Equal(expected, result);
        }

    }

}
