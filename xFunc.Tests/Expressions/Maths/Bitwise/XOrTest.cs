using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using Xunit;

namespace xFunc.Test.Expressions.Maths.Bitwise
{
    
    public class XOrTest
    {

        [Fact]
        public void CalculateTest1()
        {
            IExpression exp = new XOr(new Number(1), new Number(2));

            Assert.Equal(3, exp.Calculate());
        }

        [Fact]
        public void CalculateTest2()
        {
            IExpression exp = new XOr(new Number(1), new Number(2.5));

            Assert.Equal(2, exp.Calculate());
        }

        [Fact]
        public void CalculateTest3()
        {
            var exp = new XOr(new Bool(true), new Bool(true));

            Assert.Equal(false, exp.Calculate());
        }

        [Fact]
        public void CalculateTest4()
        {
            var exp = new XOr(new Bool(false), new Bool(true));

            Assert.Equal(true, exp.Calculate());
        }

    }

}
