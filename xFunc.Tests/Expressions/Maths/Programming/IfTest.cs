using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.Programming;
using Xunit;

namespace xFunc.Test.Expressions.Maths.Programming
{

    public class IfTest
    {

        [Fact]
        public void CalculateIfElseTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 10) };

            var cond = new Equal(new Variable("x"), new Number(10));
            var @if = new If(cond, new Number(20), new Number(0));

            Assert.Equal(20.0, @if.Calculate(parameters));

            parameters["x"] = 0;

            Assert.Equal(0.0, @if.Calculate(parameters));
        }

        [Fact]
        public void CalculateIfTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 10) };

            var cond = new Equal(new Variable("x"), new Number(10));
            var @if = new If(cond, new Number(20));

            Assert.Equal(20.0, @if.Calculate(parameters));
        }

        [Fact]
        public void CalculateElseTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 0) };

            var cond = new Equal(new Variable("x"), new Number(10));
            var @if = new If(cond, new Number(20));

            Assert.Throws<ArgumentNullException>(() => @if.Calculate(parameters));
        }

    }

}
