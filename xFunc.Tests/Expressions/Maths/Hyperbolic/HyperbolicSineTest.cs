using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Hyperbolic;
using Xunit;

namespace xFunc.Test.Expressions.Maths.Hyperbolic
{
    
    public class HyperbolicSineTest
    {
        
        [Fact]
        public void CalculateTest()
        {
            var exp = new Sinh(new Number(1));

            Assert.Equal(Math.Sinh(1), exp.Calculate());
        }
        
    }

}
