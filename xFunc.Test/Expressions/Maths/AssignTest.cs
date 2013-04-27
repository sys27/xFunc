using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class AssignTest
    {

        [TestMethod]
        public void CalculateTest1()
        {
            IMathExpression exp = new Assign(new Variable("x"), new Number(1));
            MathParameterCollection parameters = new MathParameterCollection();

            double answer = exp.Calculate(parameters);

            Assert.AreEqual(1, parameters["x"]);
            Assert.AreEqual(double.NaN, answer);
        }

        [TestMethod]
        public void CalculateTest2()
        {
            IMathExpression exp = new Assign(new Variable("x"), new Sin(new Number(1)) { AngleMeasurement = AngleMeasurement.Radian });
            MathParameterCollection parameters = new MathParameterCollection();

            double answer = exp.Calculate(parameters);

            Assert.AreEqual(Math.Sin(1), parameters["x"]);
            Assert.AreEqual(double.NaN, answer);
        }

        [TestMethod]
        public void CalculateTest3()
        {
            IMathExpression exp = new Assign(new Variable("x"), new Mul(new Number(4), new Add(new Number(8), new Number(1))));
            MathParameterCollection parameters = new MathParameterCollection();

            double answer = exp.Calculate(parameters);

            Assert.AreEqual(36, parameters["x"]);
            Assert.AreEqual(double.NaN, answer);
        }

    }

}
