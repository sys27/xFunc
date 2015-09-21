using System;
using xFunc.Maths.Expressions;
using Xunit;

namespace xFunc.Test.Expressions.Maths
{
    
    public class FactTest
    {

        [Fact]
        public void CalculateTest1()
        {
            var fact = new Fact(new Number(4));

            Assert.Equal(24.0, fact.Calculate());
        }

        [Fact]
        public void CalculateTest2()
        {
            var fact = new Fact(new Number(0));

            Assert.Equal(1.0, fact.Calculate());
        }

        [Fact]
        public void CalculateTest3()
        {
            var fact = new Fact(new Number(1));

            Assert.Equal(1.0, fact.Calculate());
        }

        [Fact]
        public void CalculateTest4()
        {
            var fact = new Fact(new Number(-1));

            Assert.Equal(double.NaN, fact.Calculate());
        }

    }

}
