using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class SubTest
    {

        [TestMethod]
        public void CalculateTest()
        {
            IExpression exp = new Sub(new Number(1), new Number(2));

            Assert.AreEqual(-1.0, exp.Calculate());
        }

        [TestMethod]
        public void SubTwoVectorsTest()
        {
            var vector1 = new Vector(new[] { new Number(2), new Number(3) });
            var vector2 = new Vector(new[] { new Number(7), new Number(1) });
            var sub = new Sub(vector1, vector2);

            var expected = new Vector(new[] { new Number(-5), new Number(2) });
            var result = sub.Calculate();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void SubTwoMatricesTest()
        {
            var matrix1 = new Matrix(new[] 
            { 
                new Vector(new[] { new Number(6), new Number(3) }), 
                new Vector(new[] { new Number(2), new Number(1) }) 
            });
            var matrix2 = new Matrix(new[] 
            { 
                new Vector(new[] { new Number(9), new Number(2) }), 
                new Vector(new[] { new Number(4), new Number(3) }) 
            });
            var sub = new Sub(matrix1, matrix2);

            var expected = new Matrix(new[] 
            { 
                new Vector(new[] { new Number(-3), new Number(1) }), 
                new Vector(new[] { new Number(-2), new Number(-2) }) 
            });
            var result = sub.Calculate();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            // x - sin(x)
            IExpression exp = new Sub(new Variable("x"), new Sin(new Variable("x")));
            IExpression deriv = exp.Differentiate();

            Assert.AreEqual("1 - (cos(x) * 1)", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            Number num1 = new Number(2);
            Variable x = new Variable("x");
            Mul mul1 = new Mul(num1, x);

            Number num2 = new Number(3);
            Mul mul2 = new Mul(num2, x.Clone());

            IExpression exp = new Sub(mul1, mul2);
            IExpression deriv = exp.Differentiate();

            Assert.AreEqual("(2 * 1) - (3 * 1)", deriv.ToString());

            num1.Value = 5;
            num2.Value = 4;
            Assert.AreEqual("(5 * x) - (4 * x)", exp.ToString());
            Assert.AreEqual("(2 * 1) - (3 * 1)", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IExpression exp = new Sub(new Mul(new Variable("x"), new Variable("y")), new Variable("y"));
            IExpression deriv = exp.Differentiate(new Variable("y"));
            Assert.AreEqual("(x * 1) - 1", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IExpression exp = new Sub(new Variable("x"), new Variable("y"));
            IExpression deriv = exp.Differentiate();
            Assert.AreEqual("1", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IExpression exp = new Sub(new Variable("x"), new Variable("y"));
            IExpression deriv = exp.Differentiate(new Variable("y"));
            Assert.AreEqual("-1", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest4()
        {
            IExpression exp = new Sub(new Variable("x"), new Number(1));
            IExpression deriv = exp.Differentiate(new Variable("y"));
            Assert.AreEqual("0", deriv.ToString());
        }

    }

}
