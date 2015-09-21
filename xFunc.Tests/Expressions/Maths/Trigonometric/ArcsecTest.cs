using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;
using Xunit;

namespace xFunc.Test.Expressions.Maths.Trigonometric
{
    
    public class ArcsecTest
    {
        
        [Fact]
        public void CalculateRadianTest()
        {
            IExpression exp = new Arcsec(new Number(1));

            Assert.Equal(MathExtentions.Asec(1), exp.Calculate(AngleMeasurement.Radian));
        }

        [Fact]
        public void CalculateDegreeTest()
        {
            IExpression exp = new Arcsec(new Number(1));

            Assert.Equal(MathExtentions.Asec(1) / Math.PI * 180, exp.Calculate(AngleMeasurement.Degree));
        }

        [Fact]
        public void CalculateGradianTest()
        {
            IExpression exp = new Arcsec(new Number(1));

            Assert.Equal(MathExtentions.Asec(1) / Math.PI * 200, exp.Calculate(AngleMeasurement.Gradian));
        }

    }

}
