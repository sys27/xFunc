using System;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using Xunit;

namespace xFunc.Test.Expressions.Maths.Bitwise
{
    
    public class NOrTest
    {

        [Fact]
        public void CalculateTest1()
        {
            var nor = new NOr(new Bool(false), new Bool(true));

            Assert.Equal(false, nor.Calculate());
        }

        [Fact]
        public void CalculateTest2()
        {
            var nor = new NOr(new Bool(false), new Bool(false));

            Assert.Equal(true, nor.Calculate());
        }

    }

}
