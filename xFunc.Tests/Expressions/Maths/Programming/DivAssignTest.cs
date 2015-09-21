using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Programming;
using Xunit;

namespace xFunc.Test.Expressions.Maths.Programming
{

    public class DivAssignTest
    {

        [Fact]
        public void DivAssignCalc()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 10) };
            var div = new DivAssign(new Variable("x"), new Number(2));
            var result = div.Calculate(parameters);
            var expected = 5.0;

            Assert.Equal(expected, result);
            Assert.Equal(expected, parameters["x"]);
        }

        [Fact]
        public void BoolDivNumberTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", true) };
            var add = new DivAssign(new Variable("x"), new Number(2));

            Assert.Throws<NotSupportedException>(() => add.Calculate(parameters));
        }

        [Fact]
        public void NumberDivBoolTest()
        {
            Assert.Throws<ParameterTypeMismatchException>(() =>
            {
                var parameters = new ParameterCollection() { new Parameter("x", 2) };
                var add = new DivAssign(new Variable("x"), new Bool(true));
            });
        }

    }

}
