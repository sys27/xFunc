using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.Programming;
using Xunit;

namespace xFunc.Test.Expressions.Maths.Programming
{
    
    public class OrTest
    {

        [Fact]
        public void CalculateOrTrueTest1()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 0) };
            var lessThen = new LessThan(new Variable("x"), new Number(10));
            var greaterThen = new GreaterThan(new Variable("x"), new Number(-10));
            var or = new Or(lessThen, greaterThen);

            Assert.Equal(true, or.Calculate(parameters));
        }

        [Fact]
        public void CalculateOrTrueTest2()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 0) };
            var lessThen = new LessThan(new Variable("x"), new Number(-10));
            var greaterThen = new GreaterThan(new Variable("x"), new Number(-10));
            var or = new Or(lessThen, greaterThen);

            Assert.Equal(true, or.Calculate(parameters));
        }

    }

}
