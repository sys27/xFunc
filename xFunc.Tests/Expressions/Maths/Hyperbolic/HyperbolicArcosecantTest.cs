using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Hyperbolic;
using Xunit;

namespace xFunc.Test.Expressions.Maths.Hyperbolic
{
    
    public class HyperbolicArcosecantTest
    {

        [Fact]
        public void CalculateTest()
        {
            var exp = new Arcsch(new Number(1));

            Assert.Equal(MathExtentions.Acsch(1), exp.Calculate());
        }

    }

}
