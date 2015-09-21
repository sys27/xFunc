using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;
using Xunit;

namespace xFunc.Test.Expressions.Maths.Trigonometric
{
    
    public class SecantTest
    {
        
        [Fact]
        public void CalculateDegreeTest()
        {
            IExpression exp = new Sec(new Number(1));

            Assert.Equal(MathExtentions.Sec(Math.PI / 180), exp.Calculate(AngleMeasurement.Degree));
        }

        [Fact]
        public void CalculateRadianTest()
        {
            IExpression exp = new Sec(new Number(1));

            Assert.Equal(MathExtentions.Sec(1), (double)exp.Calculate(AngleMeasurement.Radian), 15);
        }

        [Fact]
        public void CalculateGradianTest()
        {
            IExpression exp = new Sec(new Number(1));

            Assert.Equal(MathExtentions.Sec(Math.PI / 200), exp.Calculate(AngleMeasurement.Gradian));
        }

    }
}
