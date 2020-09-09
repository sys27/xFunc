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
    public class SubTest
    {
        [Fact]
        public void ExecuteTest1()
        {
            var exp = new Sub(Number.One, Number.Two);

            Assert.Equal(-1.0, exp.Execute());
        }

        [Fact]
        public void ExecuteTest2()
        {
            var exp = new Sub(new ComplexNumber(7, 3), new ComplexNumber(2, 4));
            var expected = new Complex(5, -1);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteTest3()
        {
            var exp = new Sub(new Number(7), new ComplexNumber(2, 4));
            var expected = new Complex(5, -4);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteTest4()
        {
            var exp = new Sub(new ComplexNumber(7, 3), Number.Two);
            var expected = new Complex(5, 3);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteTest6()
        {
            var exp = new Sub(Number.Two, new Sqrt(new Number(-9)));
            var expected = new Complex(2, -3);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void SubTwoVectorsTest()
        {
            var vector1 = new Vector(new IExpression[] { Number.Two, new Number(3) });
            var vector2 = new Vector(new IExpression[] { new Number(7), Number.One });
            var sub = new Sub(vector1, vector2);

            var expected = new Vector(new IExpression[] { new Number(-5), Number.Two });
            var result = sub.Execute();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void SubTwoMatricesTest()
        {
            var matrix1 = new Matrix(new[]
            {
                new Vector(new IExpression[] { new Number(6), new Number(3) }),
                new Vector(new IExpression[] { Number.Two, Number.One })
            });
            var matrix2 = new Matrix(new[]
            {
                new Vector(new IExpression[] { new Number(9), Number.Two }),
                new Vector(new IExpression[] { new Number(4), new Number(3) })
            });
            var sub = new Sub(matrix1, matrix2);

            var expected = new Matrix(new[]
            {
                new Vector(new IExpression[] { new Number(-3), Number.One }),
                new Vector(new IExpression[] { new Number(-2), new Number(-2) })
            });
            var result = sub.Execute();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Sub4MatricesTest()
        {
            var vector1 = new Vector(new IExpression[] { Number.One, Number.Two });
            var vector2 = new Vector(new IExpression[] { Number.One, Number.Two });
            var vector3 = new Vector(new IExpression[] { Number.One, Number.Two });
            var vector4 = new Vector(new IExpression[] { Number.One, Number.Two });
            var sub1 = new Sub(vector1, vector2);
            var sub2 = new Sub(vector3, vector4);
            var sub3 = new Sub(sub1, sub2);

            var expected = new Vector(new IExpression[] { Number.Zero, Number.Zero });

            Assert.Equal(expected, sub3.Execute());
        }

        [Fact]
        public void SubNumberAndDegree()
        {
            var exp = new Sub(Number.One, AngleValue.Degree(10).AsExpression());
            var actual = exp.Execute();
            var expected = AngleValue.Degree(-9);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SubRadianAndNumber()
        {
            var exp = new Sub(AngleValue.Radian(10).AsExpression(), Number.One);
            var actual = exp.Execute();
            var expected = AngleValue.Radian(9);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SubDegreeAndRadian()
        {
            var exp = new Sub(
                AngleValue.Radian(Math.PI).AsExpression(),
                AngleValue.Degree(10).AsExpression()
            );
            var actual = exp.Execute();
            var expected = AngleValue.Degree(170);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SubGradianAndGradian()
        {
            var exp = new Sub(
                AngleValue.Gradian(30).AsExpression(),
                AngleValue.Gradian(10).AsExpression()
            );
            var actual = exp.Execute();
            var expected = AngleValue.Gradian(20);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ExecuteWrongArgumentTypeTest()
        {
            var exp = new Sub(Bool.True, Bool.True);

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Sub(new Number(5), Number.Zero);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}