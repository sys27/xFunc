using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Hyperbolic;
using Xunit;

namespace xFunc.Test.Expressions.Maths.Hyperbolic
{
    
    public class HyperbolicCosecantTest
    {

        [Fact]
        public void CalculateTest()
        {
            var exp = new Csch(new Number(1));

            Assert.Equal(MathExtentions.Csch(1), exp.Calculate());
        }

    }

}
