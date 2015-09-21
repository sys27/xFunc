using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Hyperbolic;
using Xunit;

namespace xFunc.Test.Expressions.Maths.Hyperbolic
{
    
    public class HyperbolicTangentTest
    {

        [Fact]
        public void CalculateTest()
        {
            var exp = new Tanh(new Number(1));

            Assert.Equal(Math.Tanh(1), exp.Calculate());
        }
        
    }

}
