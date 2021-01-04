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
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Matrices;
using Xunit;

namespace xFunc.Tests.Expressions.Matrices
{
    public class DeterminantTest
    {
        [Fact]
        public void ExecuteSingleTest()
        {
            var matrix = new Matrix(new[]
            {
                new Vector(new IExpression[] { Number.One })
            });

            var det = new Determinant(matrix);
            var expected = new NumberValue(1.0);

            Assert.Equal(expected, det.Execute());
        }

        [Fact]
        public void ExecuteDoubleTest()
        {
            var matrix = new Matrix(new[]
            {
                new Vector(new IExpression[] { Number.One, Number.Two }),
                new Vector(new IExpression[] { new Number(3), new Number(4) }),
            });

            var det = new Determinant(matrix);
            var expected = new NumberValue(-2.0);

            Assert.Equal(expected, det.Execute());
        }

        [Fact]
        public void ExecuteTest()
        {
            var matrix = new Matrix(new[]
            {
                new Vector(new IExpression[] { Number.One, new Number(-2), new Number(3) }),
                new Vector(new IExpression[] { new Number(4), Number.Zero, new Number(6) }),
                new Vector(new IExpression[] { new Number(-7), new Number(8), new Number(9) })
            });

            var det = new Determinant(matrix);
            var expected = new NumberValue(204.0);

            Assert.Equal(expected, det.Execute());
        }

        [Fact]
        public void ExecuteNegativeTest()
        {
            var matrix = new Matrix(new[]
            {
                new Vector(new IExpression[] { new Number(1), new Number(-10), new Number(3), }),
                new Vector(new IExpression[] { new Number(4), new Number(5), new Number(6), }),
                new Vector(new IExpression[] { new Number(7), new Number(8), new Number(9), }),
            });

            var det = new Determinant(matrix);
            var expected = new NumberValue(-72.00000000000004);

            Assert.Equal(expected, det.Execute());
        }

        [Fact]
        public void ExecuteIsNotSquareTest()
        {
            var matrix = new Matrix(new[]
            {
                new Vector(new IExpression[] { Number.One, new Number(-2), new Number(3) }),
                new Vector(new IExpression[] { new Number(4), Number.Zero, new Number(6) })
            });

            var det = new Determinant(matrix);

            Assert.Throws<ArgumentException>(() => det.Execute());
        }

        [Fact]
        public void ExecuteVectorTest()
        {
            var vector = new Vector(new IExpression[] { Number.One, new Number(-2), new Number(3) });
            var det = new Determinant(vector);

            Assert.Throws<ResultIsNotSupportedException>(() => det.Execute());
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Determinant(new Matrix(new[]
            {
                new Vector(new IExpression[] { Number.One, new Number(-2), new Number(3) }),
                new Vector(new IExpression[] { new Number(4), Number.Zero, new Number(6) }),
                new Vector(new IExpression[] { new Number(-7), new Number(8), new Number(9) })
            }));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}