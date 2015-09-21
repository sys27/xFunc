using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.Programming;
using Xunit;

namespace xFunc.Test.Expressions.Maths.Programming
{
    
    public class LessOrEqualTest
    {

        [Fact]
        public void CalculateLessTrueTest1()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 0) };
            var lessThen = new LessOrEqual(new Variable("x"), new Number(10));

            Assert.Equal(true, lessThen.Calculate(parameters));
        }

        [Fact]
        public void CalculateLessTrueTest2()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 10) };
            var lessThen = new LessOrEqual(new Variable("x"), new Number(10));

            Assert.Equal(true, lessThen.Calculate(parameters));
        }

        [Fact]
        public void CalculateLessFalseTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 666) };
            var lessThen = new LessOrEqual(new Variable("x"), new Number(10));

            Assert.Equal(false, lessThen.Calculate(parameters));
        }

    }

}
