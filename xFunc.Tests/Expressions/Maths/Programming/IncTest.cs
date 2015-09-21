using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.Programming;
using Xunit;

namespace xFunc.Test.Expressions.Maths.Programming
{
    
    public class IncTest
    {

        [Fact]
        public void IncCalcTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 10) };
            var inc = new Inc(new Variable("x"));
            var result = (double)inc.Calculate(parameters);

            Assert.Equal(11.0, result);
            Assert.Equal(11.0, parameters["x"]);
        }

        [Fact]
        public void IncBoolTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", true) };
            var inc = new Inc(new Variable("x"));

            Assert.Throws<NotSupportedException>(() => inc.Calculate(parameters));
        }

    }

}
