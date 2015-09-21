using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.Programming;
using Xunit;

namespace xFunc.Test.Expressions.Maths.Programming
{
    
    public class GreaterOrEqualTest
    {

        [Fact]
        public void CalculateGreaterTrueTest1()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 463) };
            var lessThen = new GreaterOrEqual(new Variable("x"), new Number(10));

            Assert.Equal(true, lessThen.Calculate(parameters));
        }

        [Fact]
        public void CalculateGreaterTrueTest2()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 10) };
            var lessThen = new GreaterOrEqual(new Variable("x"), new Number(10));

            Assert.Equal(true, lessThen.Calculate(parameters));
        }

        [Fact]
        public void CalculateGreaterFalseTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 0) };
            var lessThen = new GreaterOrEqual(new Variable("x"), new Number(10));

            Assert.Equal(false, lessThen.Calculate(parameters));
        }

    }

}
