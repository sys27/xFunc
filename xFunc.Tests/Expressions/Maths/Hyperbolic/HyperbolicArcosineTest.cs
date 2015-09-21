using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Hyperbolic;
using Xunit;

namespace xFunc.Test.Expressions.Maths.Hyperbolic
{
    
    public class HyperbolicArcosineTest
    {

        [Fact]
        public void CalculateTest()
        {
            var exp = new Arcosh(new Number(1));

            Assert.Equal(MathExtentions.Acosh(1), exp.Calculate());
        }

    }

}
