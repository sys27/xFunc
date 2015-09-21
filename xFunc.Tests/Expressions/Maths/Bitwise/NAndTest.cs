using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using Xunit;

namespace xFunc.Test.Expressions.Maths.Bitwise
{
    
    public class NAndTest
    {

        [Fact]
        public void CalculateTest1()
        {
            var nand = new NAnd(new Bool(true), new Bool(true));

            Assert.Equal(false, nand.Calculate());
        }

        [Fact]
        public void CalculateTest2()
        {
            var nand = new NAnd(new Bool(false), new Bool(true));

            Assert.Equal(true, nand.Calculate());
        }

    }

}
