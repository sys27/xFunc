using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.Programming;
using Xunit;

namespace xFunc.Test.Expressions.Maths.Programming
{
    
    public class AndTest
    {

        [Fact]
        public void CalculateAndTrueTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 0) };
            var lessThen = new LessThan(new Variable("x"), new Number(10));
            var greaterThen = new GreaterThan(new Variable("x"), new Number(-10));
            var and = new And(lessThen, greaterThen);

            Assert.Equal(true, and.Calculate(parameters));
        }

        [Fact]
        public void CalculateAndFalseTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 0) };
            var lessThen = new LessThan(new Variable("x"), new Number(10));
            var greaterThen = new GreaterThan(new Variable("x"), new Number(10));
            var and = new And(lessThen, greaterThen);

            Assert.Equal(false, and.Calculate(parameters));
        }

    }

}
