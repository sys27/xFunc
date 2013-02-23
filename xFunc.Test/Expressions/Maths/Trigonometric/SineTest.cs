using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Test.Expressions.Maths.Trigonometric
{

    [TestClass]
    public class SineMathExpressionTest
    {

        [TestMethod]
        public void CalculateRadianTest()
        {
            IMathExpression exp = new Sine(new Number(1)) { AngleMeasurement = AngleMeasurement.Radian };

            Assert.AreEqual(Math.Sin(1), exp.Calculate(null));
        }

        [TestMethod]
        public void CalculateDegreeTest()
        {
            IMathExpression exp = new Sine(new Number(1)) { AngleMeasurement = AngleMeasurement.Degree };

            Assert.AreEqual(Math.Sin(1 * Math.PI / 180), exp.Calculate(null));
        }

        [TestMethod]
        public void CalculateGradianTest()
        {
            IMathExpression exp = new Sine(new Number(1)) { AngleMeasurement = AngleMeasurement.Gradian };

            Assert.AreEqual(Math.Sin(1 * Math.PI / 200), exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IMathExpression exp = new Sine(new Variable('x'));
            IMathExpression deriv = exp.Differentiation();

            Assert.AreEqual("cos(x) * 1", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            IMathExpression exp = new Sine(new Multiplication(new Number(2), new Variable('x')));
            IMathExpression deriv = exp.Differentiation();

            Assert.AreEqual("cos(2 * x) * (2 * 1)", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest3()
        {
            // sin(2x)
            Number num = new Number(2);
            Variable x = new Variable('x');
            Multiplication mul = new Multiplication(num, x);

            IMathExpression exp = new Sine(mul);
            IMathExpression deriv = exp.Differentiation();

            Assert.AreEqual("cos(2 * x) * (2 * 1)", deriv.ToString());

            num.Value = 3;
            Assert.AreEqual("sin(3 * x)", exp.ToString());
            Assert.AreEqual("cos(2 * x) * (2 * 1)", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = new Sine(new Multiplication(new Variable('x'), new Variable('y')));
            IMathExpression deriv = exp.Differentiation();
            Assert.AreEqual("cos(x * y) * (1 * y)", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = new Sine(new Multiplication(new Variable('x'), new Variable('y')));
            IMathExpression deriv = exp.Differentiation(new Variable('y'));
            Assert.AreEqual("cos(x * y) * (x * 1)", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IMathExpression exp = new Sine(new Variable('y'));
            IMathExpression deriv = exp.Differentiation();
            Assert.AreEqual("0", deriv.ToString());
        }

    }

}
