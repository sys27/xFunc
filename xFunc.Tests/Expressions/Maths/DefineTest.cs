using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;
using xFunc.Maths.Expressions.Collections;
using Xunit;

namespace xFunc.Test.Expressions.Maths
{
    
    public class DefineTest
    {

        [Fact]
        public void SimpDefineTest()
        {
            IExpression exp = new Define(new Variable("x"), new Number(1));
            ParameterCollection parameters = new ParameterCollection();

            var answer = exp.Calculate(parameters);

            Assert.Equal(1.0, parameters["x"]);
            Assert.Equal(double.NaN, answer);
        }

        [Fact]
        public void DefineWithFuncTest()
        {
            IExpression exp = new Define(new Variable("x"), new Sin(new Number(1)));
            ParameterCollection parameters = new ParameterCollection();
            ExpressionParameters expParams = new ExpressionParameters(AngleMeasurement.Radian, parameters);

            var answer = exp.Calculate(expParams);

            Assert.Equal(Math.Sin(1), parameters["x"]);
            Assert.Equal(double.NaN, answer);
        }

        [Fact]
        public void DefineExpTest()
        {
            IExpression exp = new Define(new Variable("x"), new Mul(new Number(4), new Add(new Number(8), new Number(1))));
            ParameterCollection parameters = new ParameterCollection();

            var answer = exp.Calculate(parameters);

            Assert.Equal(36.0, parameters["x"]);
            Assert.Equal(double.NaN, answer);
        }

        [Fact]
        public void OverrideConstTest()
        {
            IExpression exp = new Define(new Variable("π"), new Number(1));
            ParameterCollection parameters = new ParameterCollection();

            var answer = exp.Calculate(parameters);

            Assert.Equal(1.0, parameters["π"]);
        }

    }

}
