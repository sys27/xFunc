using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;
using Xunit;

namespace xFunc.Test.Expressions.Maths.Trigonometric
{
    
    public class SineTest
    {

        [Fact]
        public void CalculateRadianTest()
        {
            IExpression exp = new Sin(new Number(1));

            Assert.Equal(Math.Sin(1), exp.Calculate(AngleMeasurement.Radian));
        }

        [Fact]
        public void CalculateDegreeTest()
        {
            IExpression exp = new Sin(new Number(1));

            Assert.Equal(Math.Sin(1 * Math.PI / 180), exp.Calculate(AngleMeasurement.Degree));
        }

        [Fact]
        public void CalculateGradianTest()
        {
            IExpression exp = new Sin(new Number(1));

            Assert.Equal(Math.Sin(1 * Math.PI / 200), exp.Calculate(AngleMeasurement.Gradian));
        }

    }

}
