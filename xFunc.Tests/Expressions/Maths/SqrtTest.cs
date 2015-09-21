using System;
using xFunc.Maths.Expressions;
using Xunit;

namespace xFunc.Test.Expressions.Maths
{

    public class SqrtTest
    {

        [Fact]
        public void CalculateTest()
        {
            IExpression exp = new Sqrt(new Number(4));

            Assert.Equal(Math.Sqrt(4), exp.Calculate());
        }

        [Fact]
        public void NegativeNumberCalculateTest()
        {
            var exp = new Sqrt(new Number(-25));

            Assert.Equal(double.NaN, exp.Calculate());
        }

    }

}
