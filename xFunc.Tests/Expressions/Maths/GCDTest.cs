using System;
using xFunc.Maths.Expressions;
using Xunit;

namespace xFunc.Test.Expressions.Maths
{
    
    public class GCDTest
    {

        [Fact]
        public void CalcucateTest1()
        {
            var exp = new GCD(new Number(12), new Number(16));

            Assert.Equal(4.0, exp.Calculate());
        }

        [Fact]
        public void CalcucateTest2()
        {
            var exp = new GCD(new IExpression[] { new Number(64), new Number(16), new Number(8) }, 3);

            Assert.Equal(8.0, exp.Calculate());
        }

        [Fact]
        public void DifferentArgsParentTest()
        {
            var num1 = new Number(64);
            var num2 = new Number(16);
            var gcd = new GCD(new[] { num1, num2 }, 2);

            Assert.Equal(gcd, num1.Parent);
            Assert.Equal(gcd, num2.Parent);
        }

    }

}
