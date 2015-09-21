using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;
using Xunit;

namespace xFunc.Test.Expressions.Maths.Trigonometric
{
    
    public class ArccscTest
    {
        
        [Fact]
        public void CalculateRadianTest()
        {
            IExpression exp = new Arccsc(new Number(1));

            Assert.Equal(MathExtentions.Acsc(1), exp.Calculate(AngleMeasurement.Radian));
        }

        [Fact]
        public void CalculateDegreeTest()
        {
            IExpression exp = new Arccsc(new Number(1));

            Assert.Equal(MathExtentions.Acsc(1) / Math.PI * 180, exp.Calculate(AngleMeasurement.Degree));
        }

        [Fact]
        public void CalculateGradianTest()
        {
            IExpression exp = new Arccsc(new Number(1));

            Assert.Equal(MathExtentions.Acsc(1) / Math.PI * 200, exp.Calculate(AngleMeasurement.Gradian));
        }

    }

}
