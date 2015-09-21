using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;
using Xunit;

namespace xFunc.Test.Expressions.Maths.Trigonometric
{
    
    public class CosecantTest
    {

        [Fact]
        public void CalculateDegreeTest()
        {
            IExpression exp = new Csc(new Number(1));

            Assert.Equal(MathExtentions.Csc(Math.PI / 180), exp.Calculate(AngleMeasurement.Degree));
        }

        [Fact]
        public void CalculateRadianTest()
        {
            IExpression exp = new Csc(new Number(1));

            Assert.Equal(MathExtentions.Csc(1), exp.Calculate(AngleMeasurement.Radian));
        }

        [Fact]
        public void CalculateGradianTest()
        {
            IExpression exp = new Csc(new Number(1));

            Assert.Equal(MathExtentions.Csc(Math.PI / 200), exp.Calculate(AngleMeasurement.Gradian));
        }

    }

}
