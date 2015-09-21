using System;
using xFunc.Maths.Expressions;
using Xunit;

namespace xFunc.Test.Expressions.Maths
{

    public class MulTest
    {

        [Fact]
        public void CalculateTest()
        {
            IExpression exp = new Mul(new Number(2), new Number(2));

            Assert.Equal(4.0, exp.Calculate());
        }

    }

}
