using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Programming;
using Xunit;

namespace xFunc.Tests.Expressions.Maths.Programming
{

    public class NotEqualTest
    {

        [Fact]
        public void NumberEqualTest()
        {
            var equal = new NotEqual(new Number(11), new Number(10));
            var result = (bool)equal.Calculate();

            Assert.Equal(true, result);
        }

        [Fact]
        public void NumberVarEqualTest()
        {
            var parameters = new ParameterCollection()
            {
                new Parameter("x", 11),
                new Parameter("y", 10)
            };
            var equal = new NotEqual(new Variable("x"), new Variable("y"));
            var result = (bool)equal.Calculate(parameters);

            Assert.Equal(true, result);
        }

        [Fact]
        public void NumberAndBoolEqualTest()
        {
            var equal = new NotEqual(new Number(10), new Bool(false));

            Assert.Throws<NotSupportedException>(()=> (bool)equal.Calculate());
        }

        [Fact]
        public void NumberAndBoolVarEqualTest()
        {
            var parameters = new ParameterCollection()
            {
                new Parameter("x", 10),
                new Parameter("y", false)
            };
            var equal = new NotEqual(new Variable("x"), new Variable("y"));

            Assert.Throws<NotSupportedException>(() => (bool)equal.Calculate(parameters));
        }

        [Fact]
        public void BoolTrueEqualTest()
        {
            var equal = new NotEqual(new Bool(true), new Bool(true));
            var result = (bool)equal.Calculate();

            Assert.Equal(false, result);
        }

        [Fact]
        public void BoolTrueVarEqualTest()
        {
            var parameters = new ParameterCollection()
            {
                new Parameter("x", true),
                new Parameter("y", true)
            };
            var equal = new NotEqual(new Variable("x"), new Variable("y"));
            var result = (bool)equal.Calculate(parameters);

            Assert.Equal(false, result);
        }

        [Fact]
        public void BoolTrueAndFalseEqualTest()
        {
            var equal = new NotEqual(new Bool(true), new Bool(false));
            var result = (bool)equal.Calculate();

            Assert.Equal(true, result);
        }

        [Fact]
        public void BoolTrueAndFalseVarEqualTest()
        {
            var parameters = new ParameterCollection()
            {
                new Parameter("x", true),
                new Parameter("y", false)
            };
            var equal = new NotEqual(new Variable("x"), new Variable("y"));
            var result = (bool)equal.Calculate(parameters);

            Assert.Equal(true, result);
        }

        [Fact]
        public void BoolFalseEqualTest()
        {
            var equal = new NotEqual(new Bool(false), new Bool(false));
            var result = (bool)equal.Calculate();

            Assert.Equal(false, result);
        }

        [Fact]
        public void BoolFalseVarEqualTest()
        {
            var parameters = new ParameterCollection()
            {
                new Parameter("x", false),
                new Parameter("y", false)
            };
            var equal = new NotEqual(new Variable("x"), new Variable("y"));
            var result = (bool)equal.Calculate(parameters);

            Assert.Equal(false, result);
        }

    }

}
