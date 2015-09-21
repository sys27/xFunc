using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Programming;
using Xunit;

namespace xFunc.Test.Expressions.Maths.Programming
{

    public class AddAssignTest
    {

        [Fact]
        public void AddAssignCalc()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 10) };
            var add = new AddAssign(new Variable("x"), new Number(2));
            var result = add.Calculate(parameters);
            var expected = 12.0;

            Assert.Equal(expected, result);
            Assert.Equal(expected, parameters["x"]);
        }

        [Fact]
        public void BoolAddNumberTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", true) };
            var add = new AddAssign(new Variable("x"), new Number(2));

            Assert.Throws<NotSupportedException>(() => add.Calculate(parameters));
        }

        [Fact]
        public void NumberAddBoolTest()
        {
            Assert.Throws<ParameterTypeMismatchException>(() =>
            {
                var parameters = new ParameterCollection() { new Parameter("x", 2) };
                var add = new AddAssign(new Variable("x"), new Bool(true));
            });
        }

    }

}
