using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Hyperbolic;
using Xunit;

namespace xFunc.Test.Expressions.Maths.Hyperbolic
{
    
    public class HyperbolicArcotangentTest
    {

        [Fact]
        public void CalculateTest()
        {
            var exp = new Arcoth(new Number(1));

            Assert.Equal(MathExtentions.Acoth(1), exp.Calculate());
        }

    }

}
