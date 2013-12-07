using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class MatrixTest
    {

        [TestMethod]
        public void MulByNumberMatrixTest()
        {
            var vector1 = new Vector(new[] { new Number(2), new Number(3) });
            var vector2 = new Vector(new[] { new Number(9), new Number(5) });
            var matrix = new Matrix(new[] { vector1, vector2 }, 2);
            var number = new Number(5);

            var expected = new Matrix(new[] 
            { 
                new Vector(new[] { new Number(10), new Number(15) }),
                new Vector(new[] { new Number(45), new Number(25) })
            }, 2);
            var result = matrix.Mul(number);

            Assert.AreEqual(expected, result);
        }

    }

}
