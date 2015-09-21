using System;
using xFunc.Maths.Expressions;
using Xunit;

namespace xFunc.Test.Expressions.Maths
{
    
    public class PowTest
    {

        [Fact]
        public void CalculateTest()
        {
            IExpression exp = new Pow(new Number(2), new Number(10));

            Assert.Equal(1024.0, exp.Calculate());
        }

        [Fact]
        public void NegativeCalculateTest()
        {
            IExpression exp = new Pow(new Number(-8), new Number(1 / 3.0));

            Assert.Equal(-2.0, exp.Calculate());
        }

        [Fact]
        public void NegativeNumberCalculateTest()
        {
            var exp = new Pow(new Number(-25), new Number(1 / 2.0));

            Assert.Equal(double.NaN, exp.Calculate());
        }

    }

}
