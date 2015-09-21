using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.Programming;
using Xunit;

namespace xFunc.Test.Expressions.Maths.Programming
{

    public class DecTest
    {

        [Fact]
        public void DecCalcTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 10) };
            var dec = new Dec(new Variable("x"));
            var result = (double)dec.Calculate(parameters);

            Assert.Equal(9.0, result);
            Assert.Equal(9.0, parameters["x"]);
        }

        [Fact]
        public void DecBoolTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", true) };
            var dec = new Inc(new Variable("x"));

            Assert.Throws<NotSupportedException>(() => dec.Calculate(parameters));
        }

    }

}
