using System;
using xFunc.Maths.Expressions.Trigonometric;
using xFunc.Maths.Expressions;
using Xunit;

namespace xFunc.Test.Expressions.Maths
{
    
    public class UnaryTest
    {

        [Fact]
        public void EqualsTest1()
        {
            var sine1 = new Sin(new Number(2));
            var sine2 = new Sin(new Number(2));

            Assert.Equal(sine1, sine2);
        }

        [Fact]
        public void EqualsTest2()
        {
            var sine = new Sin(new Number(2));
            var ln = new Ln(new Number(2));

            Assert.NotEqual<IExpression>(sine, ln);
        }

    }

}
