using System;
using xFunc.Maths.Expressions;
using Xunit;

namespace xFunc.Test.Expressions.Maths
{
    
    public class LnTest
    {

        [Fact]
        public void CalculateTest()
        {
            IExpression exp = new Ln(new Number(2));

            Assert.Equal(Math.Log(2), exp.Calculate());
        }

    }

}
