using System;
using xFunc.Maths.Expressions;
using Xunit;

namespace xFunc.Test.Expressions.Maths
{
    
    public class LCMTest
    {

        [Fact]
        public void CalculateTest1()
        {
            var exp = new LCM(new Number(12), new Number(16));

            Assert.Equal(48.0, exp.Calculate());
        }

        [Fact]
        public void CalculateTest2()
        {
            var exp = new LCM(new IExpression[] { new Number(4), new Number(16), new Number(8) }, 3);

            Assert.Equal(16.0, exp.Calculate());
        }

    }

}
