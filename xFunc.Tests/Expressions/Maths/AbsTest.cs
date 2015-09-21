using System;
using xFunc.Maths.Expressions;
using Xunit;

namespace xFunc.Test.Expressions.Maths
{
    
    public class AbsTest
    {

        [Fact]
        public void CalculateTest()
        {
            IExpression exp = new Abs(new Number(-1));

            Assert.Equal(1.0, exp.Calculate());
        }

        [Fact]
        public void EqualsTest1()
        {
            Variable x1 = "x";
            Number num1 = 2;
            Mul mul1 = new Mul(num1, x1);
            Abs abs1 = new Abs(mul1);

            Variable x2 = "x";
            Number num2 = 2;
            Mul mul2 = new Mul(num2, x2);
            Abs abs2 = new Abs(mul2);

            Assert.True(abs1.Equals(abs2));
            Assert.True(abs1.Equals(abs1));
        }

        [Fact]
        public void EqualsTest2()
        {
            Variable x1 = "x";
            Number num1 = 2;
            Mul mul1 = new Mul(num1, x1);
            Abs abs1 = new Abs(mul1);

            Variable x2 = "x";
            Number num2 = 3;
            Mul mul2 = new Mul(num2, x2);
            Abs abs2 = new Abs(mul2);

            Assert.False(abs1.Equals(abs2));
            Assert.False(abs1.Equals(mul2));
            Assert.False(abs1.Equals(null));
        }

    }

}
