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
            IMathExpression exp = new Cot(new Number(1)) { AngleMeasurement = AngleMeasurement.Radian };

            Assert.AreEqual(MathExtentions.Cot(1), exp.Calculate(null));
        }

        [TestMethod]
        public void CalculateDegreeTest()
        {
            IMathExpression exp = new Cot(new Number(1)) { AngleMeasurement = AngleMeasurement.Degree };

            Assert.AreEqual(MathExtentions.Cot(1 * Math.PI / 180), exp.Calculate(null));
        }

        [TestMethod]
        public void CalculateGradianTest()
        {
            IMathExpression exp = new Cot(new Number(1)) { AngleMeasurement = AngleMeasurement.Gradian };

            Assert.AreEqual(MathExtentions.Cot(1 * Math.PI / 200), exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IMathExpression exp = new Cot(new Variable("x"));
            IMathExpression deriv = exp.Differentiate();

            Assert.AreEqual("-(1 / (sin(x) ^ 2))", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            IMathExpression exp = new Cot(new Mul(new Number(2), new Variable("x")));
            IMathExpression deriv = exp.Differentiate();

            Assert.AreEqual("-((2 * 1) / (sin(2 * x) ^ 2))", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest3()
        {
            // cot(2x)
            Number num = new Number(2);
            Variable x = new Variable("x");
            Mul mul = new Mul(num, x);

            IMathExpression exp = new Cot(mul);
            IMathExpression deriv = exp.Differentiate();

            Assert.AreEqual("-((2 * 1) / (sin(2 * x) ^ 2))", deriv.ToString());

            num.Value = 3;
            Assert.AreEqual("cot(3 * x)", exp.ToString());
            Assert.AreEqual("-((2 * 1) / (sin(2 * x) ^ 2))", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTestWithAngle()
        {
            var exp = new Cot(new Variable("x"), AngleMeasurement.Radian);
            var deriv = exp.Differentiate();

            var un = deriv as UnaryMinus;
            var div = un.Argument as Div;
            var pow = div.Right as Pow;
            var sin = pow.Left as Sin;

            Assert.AreEqual(AngleMeasurement.Radian, sin.AngleMeasurement);
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = new Cot(new Mul(new Variable("x"), new Variable("y")));
            IMathExpression deriv = exp.Differentiate();
            Assert.AreEqual("-((1 * y) / (sin(x * y) ^ 2))", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = new Cot(new Mul(new Variable("x"), new Variable("y")));
            IMathExpression deriv = exp.Differentiate(new Variable("y"));
            Assert.AreEqual("-((x * 1) / (sin(x * y) ^ 2))", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IMathExpression exp = new Cot(new Variable("x"));
            IMathExpression deriv = exp.Differentiate(new Variable("y"));
            Assert.AreEqual("0", deriv.ToString());
        }

    }

}
