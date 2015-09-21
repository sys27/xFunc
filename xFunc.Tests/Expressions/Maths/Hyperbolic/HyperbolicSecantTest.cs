using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Hyperbolic;
using Xunit;

namespace xFunc.Test.Expressions.Maths.Hyperbolic
{
    
    public class HyperbolicSecantTest
    {

        [Fact]
        public void CalculateTest()
        {
            var exp = new Sech(new Number(1));

            Assert.Equal(MathExtentions.Sech(1), exp.Calculate());
        }

    }

}
