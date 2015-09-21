using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using Xunit;

namespace xFunc.Test.Expressions.Maths.Bitwise
{
    
    public class EqualityTest
    {

        [Fact]
        public void CalculateTest1()
        {
            var eq = new Equality(new Bool(true), new Bool(true));

            Assert.Equal(true, eq.Calculate());
        }

        [Fact]
        public void CalculateTest2()
        {
            var eq = new Equality(new Bool(true), new Bool(false));

            Assert.Equal(false, eq.Calculate());
        }

    }

}
