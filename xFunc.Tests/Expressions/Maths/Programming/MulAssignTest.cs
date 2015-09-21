using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Programming;
using Xunit;

namespace xFunc.Test.Expressions.Maths.Programming
{
    
    public class MulAssignTest
    {

        [Fact]
        public void MulAssignCalc()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 10) };
            var mul = new MulAssign(new Variable("x"), new Number(2));
            var result = mul.Calculate(parameters);
            var expected = 20.0;

            Assert.Equal(expected, result);
            Assert.Equal(expected, parameters["x"]);
        }

        [Fact]
        public void BoolMulNumberTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", true) };
            var add = new MulAssign(new Variable("x"), new Number(2));

            Assert.Throws<NotSupportedException>(() => add.Calculate(parameters));
        }

        [Fact]
        public void NumberMulBoolTest()
        {
            Assert.Throws<ParameterTypeMismatchException>(() =>
            {
                var parameters = new ParameterCollection() { new Parameter("x", 2) };
                var add = new MulAssign(new Variable("x"), new Bool(true));
            });
        }

    }

}
