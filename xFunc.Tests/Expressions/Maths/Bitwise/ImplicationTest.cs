using System;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using Xunit;

namespace xFunc.Test.Expressions.Maths.Bitwise
{
    
    public class ImplicationTest
    {

        [Fact]
        public void CalculateTest1()
        {
            var impl = new Implication(new Bool(true), new Bool(false));

            Assert.Equal(false, impl.Calculate());
        }

        [Fact]
        public void CalculateTest2()
        {
            var impl = new Implication(new Bool(true), new Bool(true));

            Assert.Equal(true, impl.Calculate());
        }

    }

}
