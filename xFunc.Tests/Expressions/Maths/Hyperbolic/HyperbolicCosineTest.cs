using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Hyperbolic;
using Xunit;

namespace xFunc.Test.Expressions.Maths.Hyperbolic
{
    
    public class HyperbolicCosineTest
    {

        [Fact]
        public void CalculateTest()
        {
            var exp = new Cosh(new Number(1));

            Assert.Equal(Math.Cosh(1), exp.Calculate());
        }

    }

}
