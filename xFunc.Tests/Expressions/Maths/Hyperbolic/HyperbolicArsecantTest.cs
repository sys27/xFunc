using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Hyperbolic;
using Xunit;

namespace xFunc.Test.Expressions.Maths.Hyperbolic
{
    
    public class HyperbolicArsecantTest
    {

        [Fact]
        public void CalculateTest()
        {
            var exp = new Arsech(new Number(1));

            Assert.Equal(MathExtentions.Asech(1), exp.Calculate());
        }

    }

}
