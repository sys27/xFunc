using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using Xunit;

namespace xFunc.Test.Expressions.Maths.Bitwise
{
    
    public class OrTest
    {

        [Fact]
        public void CalculateTest1()
        {
            IExpression exp = new Or(new Number(1), new Number(2));

            Assert.Equal(3, exp.Calculate());
        }

        [Fact]
        public void CalculateTest2()
        {
            IExpression exp = new Or(new Number(4), new Number(2.5));

            Assert.Equal(7, exp.Calculate());
        }

        [Fact]
        public void CalculateTest3()
        {
            var exp = new Or(new Bool(true), new Bool(false));

            Assert.Equal(true, exp.Calculate());
        }

        [Fact]
        public void CalculateTest4()
        {
            var exp = new Or(new Bool(false), new Bool(false));

            Assert.Equal(false, exp.Calculate());
        }

    }

}
