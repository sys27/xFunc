using System;
using xFunc.Maths.Expressions;
using Xunit;

namespace xFunc.Test.Expressions.Maths
{
    
    public class DivTest
    {

        [Fact]
        public void CalculateTest()
        {
            IExpression exp = new Div(new Number(1), new Number(2));

            Assert.Equal(1.0 / 2.0, exp.Calculate());
        }

    }

}
