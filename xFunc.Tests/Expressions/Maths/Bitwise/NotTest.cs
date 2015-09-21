using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using Xunit;

namespace xFunc.Test.Expressions.Maths.Bitwise
{
    
    public class NotTest
    {
        
        [Fact]
        public void CalculateTest1()
        {
            var exp = new Not(new Number(2));

            Assert.Equal(-3, exp.Calculate());
        }

        [Fact]
        public void CalculateTest2()
        {
            var exp = new Not(new Number(2.5));

            Assert.Equal(-4, exp.Calculate());
        }

        [Fact]
        public void CalculateTest3()
        {
            var exp = new Not(new Bool(true));

            Assert.Equal(false, exp.Calculate());
        }

    }

}
