using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.Programming;
using Xunit;

namespace xFunc.Test.Expressions.Maths.Programming
{
    
    public class LessThanTest
    {

        [Fact]
        public void CalculateLessTrueTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 0) };
            var lessThen = new LessThan(new Variable("x"), new Number(10));

            Assert.Equal(true, lessThen.Calculate(parameters));
        }

        [Fact]
        public void CalculateLessFalseTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 10) };
            var lessThen = new LessThan(new Variable("x"), new Number(10));

            Assert.Equal(false, lessThen.Calculate(parameters));
        }

    }

}
