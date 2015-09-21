using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using Xunit;

namespace xFunc.Test.Expressions.Maths.Bitwise
{
    
    public class AndTest
    {
        
        [Fact]
        public void CalculateTest1()
        {
            var exp = new And(new Number(1), new Number(3));

            Assert.Equal(1, exp.Calculate());
        }

        [Fact]
        public void CalculateTest2()
        {
            var exp = new And(new Number(1.5), new Number(2.5));

            Assert.Equal(2, exp.Calculate());
        }

        [Fact]
        public void CalculateTest3()
        {
            var exp = new And(new Bool(true), new Bool(false));

            Assert.Equal(false, exp.Calculate());
        }

        [Fact]
        public void CalculateTest4()
        {
            var exp = new And(new Bool(true), new Bool(true));

            Assert.Equal(true, exp.Calculate());
        }

    }

}
