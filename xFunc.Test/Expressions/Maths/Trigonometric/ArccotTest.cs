using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions;
using xFunc.Maths;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Test.Expressions.Maths.Trigonometric
{

    [TestClass]
    public class ArccotTest
    {

        [TestMethod]
        public void CalculateRadianTest()
        {
            IMathExpression exp = new Arccot(new Number(1)) { AngleMeasurement = AngleMeasurement.Radian };

            Assert.AreEqual(MathExtentions.Acot(1), exp.Calculate(null));
        }

        [TestMethod]
        public void CalculateDegreeTest()
        {
            IMathExpression exp = new Arccot(new Number(1)) { AngleMeasurement = AngleMeasurement.Degree };

            Assert.AreEqual(MathExtentions.Acot(1) / Math.PI * 180, exp.Calculate(null));
        }

        [TestMethod]
        public void CalculateGradianTest()
        {
            IMathExpression exp = new Arccot(new Number(1)) { AngleMeasurement = AngleMeasurement.Gradian };

            Assert.AreEqual(MathExtentions.Acot(1) / Math.PI * 200, exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IMathExpression exp = new Arccot(new Variable('x'));
            IMathExpression deriv = exp.Differentiation();

            Assert.AreEqual("-(1 / (1 + (x ^ 2)))", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            IMathExpression exp = new Arccot(new Multiplication(new Number(2), new Variable('x')));
            IMathExpression deriv = exp.Differentiation();

            Assert.AreEqual("-((2 * 1) / (1 + ((2 * x) ^ 2)))", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest3()
        {
            // arccot(2x)
            Number num = new Number(2);
            Variable x = new Variable('x');
            Multiplication mul = new Multiplication(num, x);

            IMathExpression exp = new Arccot(mul);
            IMathExpression deriv = exp.Differentiation();

            Assert.AreEqual("-((2 * 1) / (1 + ((2 * x) ^ 2)))", deriv.ToString());

            num.Value = 4;
            Assert.AreEqual("arccot(4 * x)", exp.ToString());
            Assert.AreEqual("-((2 * 1) / (1 + ((2 * x) ^ 2)))", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = new Arccot(new Multiplication(new Variable('x'), new Variable('y')));
            IMathExpression deriv = exp.Differentiation();
            Assert.AreEqual("-((1 * y) / (1 + ((x * y) ^ 2)))", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = new Arccot(new Multiplication(new Variable('x'), new Variable('y')));
            IMathExpression deriv = exp.Differentiation(new Variable('y'));
            Assert.AreEqual("-((x * 1) / (1 + ((x * y) ^ 2)))", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IMathExpression exp = new Arccot(new Variable('x'));
            IMathExpression deriv = exp.Differentiation(new Variable('y'));
            Assert.AreEqual("0", deriv.ToString());
        }

    }

}
