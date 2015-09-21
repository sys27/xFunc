using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Hyperbolic;
using Xunit;

namespace xFunc.Test.Expressions.Maths.Hyperbolic
{
    
    public class HyperbolicCotangentTest
    {

        [Fact]
        public void CalculateTest()
        {
            var exp = new Coth(new Number(1));

            Assert.Equal(MathExtentions.Coth(1), exp.Calculate());
        }

    }

}
