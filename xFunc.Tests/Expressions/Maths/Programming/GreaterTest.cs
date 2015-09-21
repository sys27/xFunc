using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.Programming;
using Xunit;

namespace xFunc.Test.Expressions.Maths.Programming
{
    
    public class GreaterTest
    {

        [Fact]
        public void CalculateGreaterTrueTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 463) };
            var greaterThen = new GreaterThan(new Variable("x"), new Number(10));

            Assert.Equal(true, greaterThen.Calculate(parameters));
        }

        [Fact]
        public void CalculateGreaterFalseTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 0) };
            var lessThen = new GreaterThan(new Variable("x"), new Number(10));

            Assert.Equal(false, lessThen.Calculate(parameters));
        }

    }

}
