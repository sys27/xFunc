using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class DefineTest
    {

        [TestMethod]
        public void SimpDefineTest()
        {
            IExpression exp = new Define(new Variable("x"), new Number(1));
            MathParameterCollection parameters = new MathParameterCollection();

            double answer = exp.Calculate(parameters);

            Assert.AreEqual(1, parameters["x"]);
            Assert.AreEqual(double.NaN, answer);
        }

        [TestMethod]
        public void DefineWithFuncTest()
        {
            IExpression exp = new Define(new Variable("x"), new Sin(new Number(1)));
            MathParameterCollection parameters = new MathParameterCollection();
            ExpressionParameters expParams = new ExpressionParameters(AngleMeasurement.Radian, parameters);

            double answer = exp.Calculate(expParams);

            Assert.AreEqual(Math.Sin(1), parameters["x"]);
            Assert.AreEqual(double.NaN, answer);
        }

        [TestMethod]
        public void DefineExpTest()
        {
            IExpression exp = new Define(new Variable("x"), new Mul(new Number(4), new Add(new Number(8), new Number(1))));
            MathParameterCollection parameters = new MathParameterCollection();

            double answer = exp.Calculate(parameters);

            Assert.AreEqual(36, parameters["x"]);
            Assert.AreEqual(double.NaN, answer);
        }

        [TestMethod]
        public void OverrideConstTest()
        {
            IExpression exp = new Define(new Variable("π"), new Number(1));
            MathParameterCollection parameters = new MathParameterCollection();

            double answer = exp.Calculate(parameters);

            
        }

    }

}
