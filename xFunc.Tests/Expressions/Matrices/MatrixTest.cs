// Copyright 2012-2020 Dmytro Kyshchenko
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions;
using Xunit;

namespace xFunc.Tests.Expressions.Matrices
{
    public class MatrixTest
    {
        [Fact]
        public void MulByNumberMatrixTest()
        {
            var vector1 = new Vector(new[] { new Number(2), new Number(3) });
            var vector2 = new Vector(new[] { new Number(9), new Number(5) });
            var matrix = new Matrix(new[] { vector1, vector2 });
            var number = new Number(5);
            var exp = new Mul(matrix, number);

            var expected = new Matrix(new[]
            {
                new Vector(new[] { new Number(10), new Number(15) }),
                new Vector(new[] { new Number(45), new Number(25) })
            });
            var result = exp.Execute();

            Assert.Equal(expected, result);
        }

        [Fact]
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
            var exp = new Add(matrix1, matrix2);

            var expected = new Matrix(new[]
            {
                new Vector(new[] { new Number(15), new Number(5) }),
                new Vector(new[] { new Number(6), new Number(4) })
            });
            var result = exp.Execute();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void AddMatricesDiffSizeTest1()
        {
            Assert.Throws<MatrixIsInvalidException>(() =>
            {
                var matrix2 = new Matrix(new[]
                {
                    new Vector(new[] { new Number(9), new Number(2) }),
                    new Vector(new[] { new Number(4), new Number(3), new Number(9) })
                });
            });
        }

        [Fact]
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
            var exp = new Add(matrix1, matrix2);

            Assert.Throws<ArgumentException>(() => exp.Execute());
        }

        [Fact]
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
            var exp = new Sub(matrix1, matrix2);

            var expected = new Matrix(new[]
            {
                new Vector(new[] { new Number(-3), new Number(1) }),
                new Vector(new[] { new Number(-2), new Number(-2) })
            });
            var result = exp.Execute();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void SubMatricesDiffSizeTest1()
        {
            Assert.Throws<MatrixIsInvalidException>(() =>
            {
                var matrix2 = new Matrix(new[]
                {
                    new Vector(new[] { new Number(9), new Number(2) }),
                    new Vector(new[] { new Number(4), new Number(3), new Number(9) })
                });
            });
        }

        [Fact]
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
            var exp = new Sub(matrix1, matrix2);

            Assert.Throws<ArgumentException>(() => exp.Execute());
        }

        [Fact]
        public void TransposeMatrixTest()
        {
            var matrix = new Matrix(new[]
            {
                new Vector(new[] { new Number(1), new Number(2) }),
                new Vector(new[] { new Number(3), new Number(4) }),
                new Vector(new[] { new Number(5), new Number(6) })
            });
            var exp = new Transpose(matrix);

            var expected = new Matrix(new[]
            {
                new Vector(new[] { new Number(1), new Number(3), new Number(5) }),
                new Vector(new[] { new Number(2), new Number(4), new Number(6) })
            });
            var result = exp.Execute();

            Assert.Equal(expected, result);
        }

        [Fact]
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
            var exp = new Mul(left, right);

            var expected = new Matrix(new[]
            {
                new Vector(new[] { new Number(-7) }),
                new Vector(new[] { new Number(11) })
            });
            var result = exp.Execute();

            Assert.Equal(expected, result);
        }

        [Fact]
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
            var exp = new Mul(left, right);

            var expected = new Matrix(new[]
            {
                new Vector(new[] { new Number(11), new Number(-22), new Number(29) }),
                new Vector(new[] { new Number(9), new Number(-27), new Number(32) }),
                new Vector(new[] { new Number(13), new Number(-17), new Number(26) })
            });
            var result = exp.Execute();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void MulMatrices3()
        {
            var left = new Matrix(new[]
            {
                new Vector(new[] { new Number(3) }),
                new Vector(new[] { new Number(-1) })
            });
            var right = new Matrix(new[]
            {
                new Vector(new[] { new Number(-2), new Number(1) }),
                new Vector(new[] { new Number(5), new Number(4) })
            });
            var exp = new Mul(left, right);

            Assert.Throws<ArgumentException>(() => exp.Execute());
        }

        [Fact]
        public void MatrixMulVectorTest()
        {
            var vector = new Vector(new[] { new Number(-2), new Number(1) });
            var matrix = new Matrix(new[]
            {
                new Vector(new[] { new Number(3) }),
                new Vector(new[] { new Number(-1) })
            });
            var exp = new Mul(matrix, vector);

            var expected = new Matrix(new[]
            {
                new Vector(new[] { new Number(-6), new Number(3) }),
                new Vector(new[] { new Number(2), new Number(-1) })
            });
            var result = exp.Execute();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void DetTest()
        {
            var matrix = new Matrix(new[]
            {
                new Vector(new[] { new Number(1), new Number(-2), new Number(3) }),
                new Vector(new[] { new Number(4), new Number(0), new Number(6) }),
                new Vector(new[] { new Number(-7), new Number(8), new Number(9) })
            });
            var exp = new Determinant(matrix);

            var det = exp.Execute();

            Assert.Equal(204.0, det);
        }

        [Fact]
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

            Assert.Equal(expected, matrix);
        }

        [Fact]
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

            Assert.Equal(expected, matrix);
        }

        [Fact]
        public void InverseTest()
        {
            var matrix = new Matrix(new[]
            {
                new Vector(new[] { new Number(3), new Number(7), new Number(2), new Number(5) }),
                new Vector(new[] { new Number(1), new Number(8), new Number(4), new Number(2) }),
                new Vector(new[] { new Number(2), new Number(1), new Number(9), new Number(3) }),
                new Vector(new[] { new Number(5), new Number(4), new Number(7), new Number(1) })
            });
            var exp = new Inverse(matrix);

            var expected = new Matrix(new[]
            {
                new Vector(new[] { new Number(0.0970873786407767), new Number(-0.18270079435128), new Number(-0.114739629302736), new Number(0.224183583406884) }),
                new Vector(new[] { new Number(-0.0194174757281553), new Number(0.145631067961165), new Number(-0.0679611650485437), new Number(0.00970873786407767) }),
                new Vector(new[] { new Number(-0.087378640776699), new Number(0.0644307149161518), new Number(0.103265666372463), new Number(-0.00176522506619595) }),
                new Vector(new[] { new Number(0.203883495145631), new Number(-0.120035304501324), new Number(0.122683142100618), new Number(-0.147396293027361) })
            });

            var actual = exp.Execute();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Matrix(new[]
            {
                new Vector(new[] { new Number(1), new Number(-2), new Number(3) }),
                new Vector(new[] { new Number(4), new Number(0), new Number(6) }),
                new Vector(new[] { new Number(-7), new Number(8), new Number(9) })
            });
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}