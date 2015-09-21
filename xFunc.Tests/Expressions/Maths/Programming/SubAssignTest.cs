using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Programming;
using Xunit;

namespace xFunc.Test.Expressions.Maths.Programming
{

    public class SubAssignTest
    {

        [Fact]
        public void SubAssignCalc()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 10) };
            var sub = new SubAssign(new Variable("x"), new Number(2));
            var result = sub.Calculate(parameters);
            var expected = 8.0;

            Assert.Equal(expected, result);
            Assert.Equal(expected, parameters["x"]);
        }

        [Fact]
        public void BoolSubNumberTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", true) };
            var add = new SubAssign(new Variable("x"), new Number(2));

            Assert.Throws<NotSupportedException>(() => add.Calculate(parameters));
        }

        [Fact]
        public void NumberSubBoolTest()
        {
            Assert.Throws<ParameterTypeMismatchException>(() =>
            {
                var parameters = new ParameterCollection() { new Parameter("x", 2) };
                var add = new SubAssign(new Variable("x"), new Bool(true));
            });
        }

    }

}
