using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Test.Expressions.Maths.Trigonometric
{

    [TestClass]
    public class CotangentTest
    {

        [TestMethod]
        public void CalculateRadianTest()
        {
            IMathExpression exp = new Cotangent(new Number(1)) { AngleMeasurement = AngleMeasurement.Radian };

            Assert.AreEqual(MathExtentions.Cot(1), exp.Calculate(null));
        }

        [TestMethod]
        public void CalculateDegreeTest()
        {
            IMathExpression exp = new Cotangent(new Number(1)) { AngleMeasurement = AngleMeasurement.Degree };

            Assert.AreEqual(MathExtentions.Cot(1 * Math.PI / 180), exp.Calculate(null));
        }

        [TestMethod]
        public void CalculateGradianTest()
        {
            IMathExpression exp = new Cotangent(new Number(1)) { AngleMeasurement = AngleMeasurement.Gradian };

            Assert.AreEqual(MathExtentions.Cot(1 * Math.PI / 200), exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IMathExpression exp = new Cotangent(new Variable('x'));
            IMathExpression deriv = exp.Differentiate();

            Assert.AreEqual("-(1 / (sin(x) ^ 2))", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            IMathExpression exp = new Cotangent(new Multiplication(new Number(2), new Variable('x')));
            IMathExpression deriv = exp.Differentiate();

            Assert.AreEqual("-((2 * 1) / (sin(2 * x) ^ 2))", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest3()
        {
            // cot(2x)
            Number num = new Number(2);
            Variable x = new Variable('x');
            Multiplication mul = new Multiplication(num, x);

            IMathExpression exp = new Cotangent(mul);
            IMathExpression deriv = exp.Differentiate();

            Assert.AreEqual("-((2 * 1) / (sin(2 * x) ^ 2))", deriv.ToString());

            num.Value = 3;
            Assert.AreEqual("cot(3 * x)", exp.ToString());
            Assert.AreEqual("-((2 * 1) / (sin(2 * x) ^ 2))", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = new Cotangent(new Multiplication(new Variable('x'), new Variable('y')));
            IMathExpression deriv = exp.Differentiate();
            Assert.AreEqual("-((1 * y) / (sin(x * y) ^ 2))", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = new Cotangent(new Multiplication(new Variable('x'), new Variable('y')));
            IMathExpression deriv = exp.Differentiate(new Variable('y'));
            Assert.AreEqual("-((x * 1) / (sin(x * y) ^ 2))", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IMathExpression exp = new Cotangent(new Variable('x'));
            IMathExpression deriv = exp.Differentiate(new Variable('y'));
            Assert.AreEqual("0", deriv.ToString());
        }

    }

}
