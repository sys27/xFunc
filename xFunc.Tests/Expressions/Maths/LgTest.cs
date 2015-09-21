using System;
using xFunc.Maths.Expressions;
using Xunit;

namespace xFunc.Test.Expressions.Maths
{
    
    public class LgTest
    {

        [Fact]
        public void CalculateTest()
        {
            IExpression exp = new Lg(new Number(2));

            Assert.Equal(Math.Log10(2), exp.Calculate());
        }

    }

}
