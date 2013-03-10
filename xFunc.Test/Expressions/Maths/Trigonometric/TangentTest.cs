using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Test.Expressions.Maths.Trigonometric
{

    [TestClass]
    public class TangentTest
    {

        [TestMethod]
        public void CalculateRadianTest()
        {
            IMathExpression exp = new Tangent(new Number(1)) { AngleMeasurement = AngleMeasurement.Radian };

            Assert.AreEqual(Math.Tan(1), exp.Calculate(null));
        }

        [TestMethod]
        public void CalculateDegreeTest()
        {
            IMathExpression exp = new Tangent(new Number(1)) { AngleMeasurement = AngleMeasurement.Degree };

            Assert.AreEqual(Math.Tan(1 * Math.PI / 180), exp.Calculate(null));
        }

        [TestMethod]
        public void CalculateGradianTest()
        {
            IMathExpression exp = new Tangent(new Number(1)) { AngleMeasurement = AngleMeasurement.Gradian };

            Assert.AreEqual(Math.Tan(1 * Math.PI / 200), exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IMathExpression exp = new Tangent(new Variable('x'));
            IMathExpression deriv = exp.Differentiate();

            Assert.AreEqual("1 / (cos(x) ^ 2)", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            IMathExpression exp = new Tangent(new Multiplication(new Number(2), new Variable('x')));
            IMathExpression deriv = exp.Differentiate();

            Assert.AreEqual("(2 * 1) / (cos(2 * x) ^ 2)", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest3()
        {
            Number num = new Number(2);
            Variable x = new Variable('x');
            Multiplication mul = new Multiplication(num, x);

            IMathExpression exp = new Tangent(mul);
            IMathExpression deriv = exp.Differentiate();

            Assert.AreEqual("(2 * 1) / (cos(2 * x) ^ 2)", deriv.ToString());

            num.Value = 5;
            Assert.AreEqual("tan(5 * x)", exp.ToString());
            Assert.AreEqual("(2 * 1) / (cos(2 * x) ^ 2)", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = new Tangent(new Multiplication(new Variable('x'), new Variable('y')));
            IMathExpression deriv = exp.Differentiate();
            Assert.AreEqual("(1 * y) / (cos(x * y) ^ 2)", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = new Tangent(new Multiplication(new Variable('x'), new Variable('y')));
            IMathExpression deriv = exp.Differentiate(new Variable('y'));
            Assert.AreEqual("(x * 1) / (cos(x * y) ^ 2)", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IMathExpression exp = new Tangent(new Variable('x'));
            IMathExpression deriv = exp.Differentiate(new Variable('y'));
            Assert.AreEqual("0", deriv.ToString());
        }

    }

}
