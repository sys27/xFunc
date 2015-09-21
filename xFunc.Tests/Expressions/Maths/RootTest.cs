using System;
using xFunc.Maths.Expressions;
using Xunit;

namespace xFunc.Test.Expressions.Maths
{
    
    public class RootTest
    {
        
        [Fact]
        public void CalculateRootTest1()
        {
            IExpression exp = new Root(new Number(8), new Number(3));

            Assert.Equal(Math.Pow(8, 1.0 / 3.0), exp.Calculate());
        }

        [Fact]
        public void CalculateRootTest2()
        {
            IExpression exp = new Root(new Number(-8), new Number(3));

            Assert.Equal(-2.0, exp.Calculate());
        }

        [Fact]
        public void NegativeNumberCalculateTest()
        {
            var exp = new Root(new Number(-25), new Number(2));

            Assert.Equal(double.NaN, exp.Calculate());
        }
                
    }

}
