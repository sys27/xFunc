// Copyright 2012-2021 Dmytro Kyshchenko
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
using System.Numerics;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Angles;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using Xunit;
using Vector = xFunc.Maths.Expressions.Matrices.Vector;
using Matrix = xFunc.Maths.Expressions.Matrices.Matrix;

namespace xFunc.Tests.Expressions
{
    public class MulTest : BaseExpressionTests
    {
        [Fact]
        public void ExecuteMulNumberByNumberTest()
        {
            var exp = new Mul(Number.Two, Number.Two);
            var expected = new NumberValue(4.0);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteMulComplexByComplexTest()
        {
            var exp = new Mul(new ComplexNumber(2, 5), new ComplexNumber(3, 2));
            var expected = new Complex(-4, 19);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteMulComplexByNumberTest()
        {
            var exp = new Mul(new ComplexNumber(2, 5), Number.Two);
            var expected = new Complex(4, 10);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteMulNumberByComplexTest()
        {
            var exp = new Mul(Number.Two, new ComplexNumber(3, 2));
            var expected = new Complex(6, 4);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteMulNumberBySqrtComplexTest()
        {
            var exp = new Mul(Number.Two, new Sqrt(new Number(-9)));
            var expected = new Complex(0, 6);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteDotProductTest()
        {
            var vector1 = new Vector(new IExpression[]
            {
                Number.One, Number.Two, new Number(3)
            });
            var vector2 = new Vector(new IExpression[]
            {
                new Number(4), new Number(5), new Number(6)
            });
            var exp = new Mul(vector1, vector2);
            var expected = new NumberValue(32.0);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteMulComplexByBool()
        {
            var complex = new ComplexNumber(3, 2);
            var boolean = Bool.True;
            var mul = new Mul(complex, boolean);

            Assert.Throws<ResultIsNotSupportedException>(() => mul.Execute());
        }

        [Fact]
        public void ExecuteMulBoolByComplex()
        {
            var boolean = Bool.True;
            var complex = new ComplexNumber(3, 2);
            var mul = new Mul(boolean, complex);

            Assert.Throws<ResultIsNotSupportedException>(() => mul.Execute());
        }

        [Fact]
        public void ExecuteMulVectorByMatrixTest()
        {
            var vector = new Vector(new IExpression[]
            {
                Number.One,
                Number.Two,
                new Number(3)
            });
            var matrix = new Matrix(new[]
            {
                new Vector(new IExpression[] { new Number(4) }),
                new Vector(new IExpression[] { new Number(5) }),
                new Vector(new IExpression[] { new Number(6) })
            });
            var exp = new Mul(vector, matrix);

            var expected = new Matrix(new[]
            {
                new Vector(new IExpression[] { new Number(32) })
            });

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteMulMatrixByVectorTest()
        {
            var matrix = new Matrix(new[]
            {
                new Vector(new IExpression[] { new Number(4) }),
                new Vector(new IExpression[] { new Number(5) }),
                new Vector(new IExpression[] { new Number(6) })
            });
            var vector = new Vector(new IExpression[]
            {
                Number.One,
                Number.Two,
                new Number(3)
            });
            var exp = new Mul(matrix, vector);

            var expected = new Matrix(new[]
            {
                new Vector(new IExpression[]
                {
                    new Number(4), new Number(8), new Number(12)
                }),
                new Vector(new IExpression[]
                {
                    new Number(5), new Number(10), new Number(15)
                }),
                new Vector(new IExpression[]
                {
                    new Number(6), new Number(12), new Number(18)
                })
            });

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteMulMatrixByMatrixTest()
        {
            var matrix1 = new Matrix(new[]
            {
                new Vector(new IExpression[] { Number.One, Number.Two, new Number(3) })
            });
            var matrix2 = new Matrix(new[]
            {
                new Vector(new IExpression[] { new Number(4) }),
                new Vector(new IExpression[] { new Number(5) }),
                new Vector(new IExpression[] { new Number(6) })
            });
            var exp = new Mul(matrix1, matrix2);

            var expected = new Matrix(new[]
            {
                new Vector(new IExpression[] { new Number(32) })
            });

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteMulNumberByVectorTest()
        {
            var number = new Number(5);
            var vector = new Vector(new IExpression[]
            {
                Number.One,
                Number.Two,
                new Number(3)
            });
            var exp = new Mul(number, vector);

            var expected = new Vector(new IExpression[]
            {
                new Number(5),
                new Number(10),
                new Number(15)
            });

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteMulMatrixByNumberTest()
        {
            var matrix = new Matrix(new[]
            {
                new Vector(new IExpression[] { Number.One, Number.Two }),
                new Vector(new IExpression[] { new Number(3), new Number(4) })
            });
            var number = new Number(5);
            var exp = new Mul(matrix, number);

            var expected = new Matrix(new[]
            {
                new Vector(new IExpression[] { new Number(5), new Number(10) }),
                new Vector(new IExpression[] { new Number(15), new Number(20) })
            });

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteMulNumberByMatrixTest()
        {
            var number = new Number(5);
            var matrix = new Matrix(new[]
            {
                new Vector(new IExpression[] { Number.One, Number.Two }),
                new Vector(new IExpression[] { new Number(3), new Number(4) })
            });
            var exp = new Mul(number, matrix);

            var expected = new Matrix(new[]
            {
                new Vector(new IExpression[] { new Number(5), new Number(10) }),
                new Vector(new IExpression[] { new Number(15), new Number(20) })
            });

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void MulNumberAndDegree()
        {
            var exp = new Mul(Number.Two, AngleValue.Degree(10).AsExpression());
            var actual = exp.Execute();
            var expected = AngleValue.Degree(20);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MulRadianAndNumber()
        {
            var exp = new Mul(AngleValue.Radian(10).AsExpression(), Number.Two);
            var actual = exp.Execute();
            var expected = AngleValue.Radian(20);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MulDegreeAndRadian()
        {
            var exp = new Mul(
                AngleValue.Radian(Math.PI).AsExpression(),
                AngleValue.Degree(10).AsExpression()
            );
            var actual = exp.Execute();
            var expected = AngleValue.Degree(1800);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MulGradianAndGradian()
        {
            var exp = new Mul(
                AngleValue.Gradian(10).AsExpression(),
                AngleValue.Gradian(20).AsExpression()
            );
            var actual = exp.Execute();
            var expected = AngleValue.Gradian(200);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ExecuteMulBoolByBoolTest()
            => TestNotSupported(new Mul(Bool.True, Bool.True));

        [Fact]
        public void CloneTest()
        {
            var exp = new Mul(Variable.X, Number.Zero);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}