using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Test.Expressions.Maths.Trigonometric
{

    [TestClass]
    public class CosineTest
    {

        [TestMethod]
        public void CalculateRadianTest()
        {
            IMathExpression exp = new Cos(new Number(1));

            Assert.AreEqual(Math.Cos(1), exp.Calculate(AngleMeasurement.Radian));
        }

        [TestMethod]
        public void CalculateDegreeTest()
        {
            IMathExpression exp = new Cos(new Number(1));

            Assert.AreEqual(Math.Cos(1 * Math.PI / 180), exp.Calculate(AngleMeasurement.Degree));
        }

        [TestMethod]
        public void CalculateGradianTest()
        {
            IMathExpression exp = new Cos(new Number(1));

            Assert.AreEqual(Math.Cos(1 * Math.PI / 200), exp.Calculate(AngleMeasurement.Gradian));
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IMathExpression exp = new Cos(new Variable("x"));
            IMathExpression deriv = exp.Differentiate();

            Assert.AreEqual("-(sin(x) * 1)", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            IMathExpression exp = new Cos(new Mul(new Number(2), new Variable("x")));
            IMathExpression deriv = exp.Differentiate();

            Assert.AreEqual("-(sin(2 * x) * (2 * 1))", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest3()
        {
            // cos(2x)
            Number num = new Number(2);
            Variable x = new Variable("x");
            Mul mul = new Mul(num, x);

            IMathExpression exp = new Cos(mul);
            IMathExpression deriv = exp.Differentiate();

            Assert.AreEqual("-(sin(2 * x) * (2 * 1))", deriv.ToString());

            num.Value = 7;
            Assert.AreEqual("cos(7 * x)", exp.ToString());
            Assert.AreEqual("-(sin(2 * x) * (2 * 1))", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = new Cos(new Mul(new Variable("x"), new Variable("y")));
            IMathExpression deriv = exp.Differentiate();
            Assert.AreEqual("-(sin(x * y) * (1 * y))", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = new Cos(new Mul(new Variable("x"), new Variable("y")));
            IMathExpression deriv = exp.Differentiate(new Variable("y"));
            Assert.AreEqual("-(sin(x * y) * (x * 1))", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IMathExpression exp = new Cos(new Variable("x"));
            IMathExpression deriv = exp.Differentiate(new Variable("y"));
            Assert.AreEqual("0", deriv.ToString());
        }

    }

}
