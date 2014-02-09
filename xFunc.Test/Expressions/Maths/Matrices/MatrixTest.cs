using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths.Matrices
{

    [TestClass]
    public class MatrixTest
    {

        [TestMethod]
        public void MulByNumberMatrixTest()
        {
            var vector1 = new Vector(new[] { new Number(2), new Number(3) });
            var vector2 = new Vector(new[] { new Number(9), new Number(5) });
            var matrix = new Matrix(new[] { vector1, vector2 });
            var number = new Number(5);

            var expected = new Matrix(new[] 
            { 
                new Vector(new[] { new Number(10), new Number(15) }),
                new Vector(new[] { new Number(45), new Number(25) })
            });
            var result = matrix.Mul(number);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void AddMatricesTest()
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

            var expected = new Matrix(new[] 
            { 
                new Vector(new[] { new Number(15), new Number(5) }), 
                new Vector(new[] { new Number(6), new Number(4) }) 
            });
            var result = matrix1.Add(matrix2);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(MatrixIsInvalidException))]
        public void AddMatricesDiffSizeTest1()
        {
            var matrix1 = new Matrix(new[] 
            { 
                new Vector(new[] { new Number(6), new Number(3) }), 
                new Vector(new[] { new Number(2), new Number(1) }) 
            });
            var matrix2 = new Matrix(new[] 
            { 
                new Vector(new[] { new Number(9), new Number(2) }), 
                new Vector(new[] { new Number(4), new Number(3), new Number(9) }) 
            });

            var result = matrix1.Add(matrix2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddMatricesDiffSizeTest2()
        {
            var matrix1 = new Matrix(new[] 
            { 
                new Vector(new[] { new Number(6), new Number(3) }), 
                new Vector(new[] { new Number(2), new Number(1) }) 
            });
            var matrix2 = new Matrix(new[] 
            { 
                new Vector(new[] { new Number(9), new Number(2) }), 
                new Vector(new[] { new Number(4), new Number(3) }),
                new Vector(new[] { new Number(1), new Number(7) })
            });

            var result = matrix1.Add(matrix2);
        }

        [TestMethod]
        public void SubMatricesTest()
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

            var expected = new Matrix(new[] 
            { 
                new Vector(new[] { new Number(-3), new Number(1) }), 
                new Vector(new[] { new Number(-2), new Number(-2) }) 
            });
            var result = matrix1.Sub(matrix2);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(MatrixIsInvalidException))]
        public void SubMatricesDiffSizeTest1()
        {
            var matrix1 = new Matrix(new[] 
            { 
                new Vector(new[] { new Number(6), new Number(3) }), 
                new Vector(new[] { new Number(2), new Number(1) }) 
            });
            var matrix2 = new Matrix(new[] 
            { 
                new Vector(new[] { new Number(9), new Number(2) }), 
                new Vector(new[] { new Number(4), new Number(3), new Number(9) }) 
            });

            var result = matrix1.Sub(matrix2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SubMatricesDiffSizeTest2()
        {
            var matrix1 = new Matrix(new[] 
            { 
                new Vector(new[] { new Number(6), new Number(3) }), 
                new Vector(new[] { new Number(2), new Number(1) }) 
            });
            var matrix2 = new Matrix(new[] 
            { 
                new Vector(new[] { new Number(9), new Number(2) }), 
                new Vector(new[] { new Number(4), new Number(3) }),
                new Vector(new[] { new Number(6), new Number(1) })
            });

            var result = matrix1.Sub(matrix2);
        }

        [TestMethod]
        public void TransposeMatrixTest()
        {
            var matrix = new Matrix(new[]
            {
                new Vector(new[] { new Number(1), new Number(2) }),
                new Vector(new[] { new Number(3), new Number(4) }),
                new Vector(new[] { new Number(5), new Number(6) })
            });

            var expected = new Matrix(new[]
            {
                new Vector(new[] { new Number(1), new Number(3), new Number(5) }),
                new Vector(new[] { new Number(2), new Number(4), new Number(6) })
            });
            var result = matrix.Transpose();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void MulMatrices1()
        {
            var left = new Matrix(new[]
            {
                new Vector(new[] { new Number(-2), new Number(1) }),
                new Vector(new[] { new Number(5), new Number(4) })
            });
            var right = new Matrix(new[]
            {
                new Vector(new[] { new Number(3) }),
                new Vector(new[] { new Number(-1) })
            });

            var expected = new Matrix(new[]
            {
                new Vector(new[] { new Number(-7) }),
                new Vector(new[] { new Number(11) })
            });
            var result = left.Mul(right);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void MulMatrices2()
        {
            var left = new Matrix(new[]
            {
                new Vector(new[] { new Number(5), new Number(8), new Number(-4) }),
                new Vector(new[] { new Number(6), new Number(9), new Number(-5) }),
                new Vector(new[] { new Number(4), new Number(7), new Number(-3) })
            });
            var right = new Matrix(new[]
            {
                new Vector(new[] { new Number(3), new Number(2), new Number(5) }),
                new Vector(new[] { new Number(4), new Number(-1), new Number(3) }),
                new Vector(new[] { new Number(9), new Number(6), new Number(5) })
            });

            var expected = new Matrix(new[]
            {
                new Vector(new[] { new Number(11), new Number(-22), new Number(29) }),
                new Vector(new[] { new Number(9), new Number(-27), new Number(32) }),
                new Vector(new[] { new Number(13), new Number(-17), new Number(26) })
            });
            var result = left.Mul(right);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MulMatrices3()
        {
            var left = new Matrix(new[]
            {
                new Vector(new[] { new Number(-2), new Number(1) }),
                new Vector(new[] { new Number(5), new Number(4) })
            });
            var right = new Matrix(new[]
            {
                new Vector(new[] { new Number(3) }),
                new Vector(new[] { new Number(-1) })
            });

            var expected = new Matrix(new[]
            {
                new Vector(new[] { new Number(-7) }),
                new Vector(new[] { new Number(11) })
            });
            var result = right.Mul(left);
        }

        [TestMethod]
        public void MatrixMulVectorTest()
        {
            var vector = new Vector(new[] { new Number(-2), new Number(1) });
            var matrix = new Matrix(new[]
            {
                new Vector(new[] { new Number(3) }),
                new Vector(new[] { new Number(-1) })
            });

            var expected = new Matrix(new[] 
            { 
                new Vector(new[] { new Number(-6), new Number(3) }),
                new Vector(new[] { new Number(2), new Number(-1) })
            });
            var result = matrix.Mul(vector);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DetTest()
        {
            var matrix = new Matrix(new[]
            {
                new Vector(new[] { new Number(1), new Number(-2), new Number(3) }),
                new Vector(new[] { new Number(4), new Number(0), new Number(6) }),
                new Vector(new[] { new Number(-7), new Number(8), new Number(9) })
            });

            var det = matrix.Determinant();

            Assert.AreEqual(204, det);
        }

        [TestMethod]
        public void SwapRowsTest()
        {
            var matrix = new Matrix(new[]
            {
                new Vector(new[] { new Number(1), new Number(-2), new Number(3) }),
                new Vector(new[] { new Number(4), new Number(0), new Number(6) }),
                new Vector(new[] { new Number(-7), new Number(8), new Number(9) })
            });
            var expected = new Matrix(new[]
            {
                new Vector(new[] { new Number(-7), new Number(8), new Number(9) }),
                new Vector(new[] { new Number(4), new Number(0), new Number(6) }),
                new Vector(new[] { new Number(1), new Number(-2), new Number(3) })
                
            });

            matrix.SwapRows(0, 2);

            Assert.AreEqual(expected, matrix);
        }

        [TestMethod]
        public void SwapColumnsTest()
        {
            var matrix = new Matrix(new[]
            {
                new Vector(new[] { new Number(1), new Number(-2), new Number(3) }),
                new Vector(new[] { new Number(4), new Number(0), new Number(6) }),
                new Vector(new[] { new Number(-7), new Number(8), new Number(9) })
            });
            var expected = new Matrix(new[]
            {
                new Vector(new[] { new Number(3), new Number(-2), new Number(1) }),
                new Vector(new[] { new Number(6), new Number(0), new Number(4) }),
                new Vector(new[] { new Number(9), new Number(8), new Number(-7) })
            });

            matrix.SwapColumns(0, 2);

            Assert.AreEqual(expected, matrix);
        }

        [TestMethod]
        public void LUPTest()
        {
            var matrix = new Matrix(new[]
            {
                new Vector(new[] { new Number(3), new Number(7), new Number(2), new Number(5) }),
                new Vector(new[] { new Number(1), new Number(8), new Number(4), new Number(2) }),
                new Vector(new[] { new Number(2), new Number(1), new Number(9), new Number(3) }),
                new Vector(new[] { new Number(5), new Number(4), new Number(7), new Number(1) })
            });
            var expected = new Matrix(new[]
            {
                new Vector(new[] { new Number(5), new Number(4), new Number(7), new Number(1) }),
                new Vector(new[] { new Number(0.2), new Number(7.2), new Number(2.6), new Number(1.8) }),
                new Vector(new[] { new Number(0.4), new Number(-0.0833333333333333), new Number(6.41666666666667), new Number(2.75) }),
                new Vector(new[] { new Number(0.6), new Number(0.638888888888889), new Number(-0.601731601731602), new Number(4.90476190476191) })
            });

            int[] perm;
            int toggle;
            var actual = MatrixExtentions.LUPDecomposition(matrix, null, out perm, out toggle);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InverseTest()
        {
            var matrix = new Matrix(new[]
            {
                new Vector(new[] { new Number(3), new Number(7), new Number(2), new Number(5) }),
                new Vector(new[] { new Number(1), new Number(8), new Number(4), new Number(2) }),
                new Vector(new[] { new Number(2), new Number(1), new Number(9), new Number(3) }),
                new Vector(new[] { new Number(5), new Number(4), new Number(7), new Number(1) })
            });
            var expected = new Matrix(new[]
            {
                new Vector(new[] { new Number(0.0970873786407767), new Number(-0.18270079435128), new Number(-0.114739629302736), new Number(0.224183583406884) }),
                new Vector(new[] { new Number(-0.0194174757281553), new Number(0.145631067961165), new Number(-0.0679611650485437), new Number(0.00970873786407767) }),
                new Vector(new[] { new Number(-0.087378640776699), new Number(0.0644307149161518), new Number(0.103265666372463), new Number(-0.00176522506619595) }),
                new Vector(new[] { new Number(0.203883495145631), new Number(-0.120035304501324), new Number(0.122683142100618), new Number(-0.147396293027361) })
            });

            var actual = MatrixExtentions.Inverse(matrix, null);

            Assert.AreEqual(expected, actual);
        }

    }

}
