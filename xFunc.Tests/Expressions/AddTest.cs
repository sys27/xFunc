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
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Units.AngleUnits;
using xFunc.Maths.Expressions.Units.PowerUnits;
using Xunit;
using Vector = xFunc.Maths.Expressions.Matrices.Vector;
using Matrix = xFunc.Maths.Expressions.Matrices.Matrix;

namespace xFunc.Tests.Expressions
{
    public class AddTest : BaseExpressionTests
    {
        [Fact]
        public void ExecuteTestNumber1()
        {
            var exp = new Add(Number.One, Number.Two);
            var expected = new NumberValue(3.0);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteTestNumber2()
        {
            var exp = new Add(new Number(-3), Number.Two);
            var expected = new NumberValue(-1.0);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteTestComplexNumber()
        {
            var exp = new Add(new ComplexNumber(7, 3), new ComplexNumber(2, 4));
            var expected = new Complex(9, 7);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteTestNumberComplexNumber()
        {
            var exp = new Add(new Number(7), new ComplexNumber(2, 4));
            var expected = new Complex(9, 4);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteTestComplexNumberNumber()
        {
            var exp = new Add(new ComplexNumber(7, 3), Number.Two);
            var expected = new Complex(9, 3);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteTest6()
        {
            var exp = new Add(Number.Two, new Sqrt(new Number(-9)));
            var expected = new Complex(2, 3);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void AddTwoVectorsTest()
        {
            var vector1 = new Vector(new IExpression[] { Number.Two, new Number(3) });
            var vector2 = new Vector(new IExpression[] { new Number(7), Number.One });
            var add = new Add(vector1, vector2);

            var expected = new Vector(new IExpression[] { new Number(9), new Number(4) });
            var result = add.Execute();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void AddTwoMatricesTest()
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
            var add = new Add(matrix1, matrix2);

            var expected = new Matrix(new[]
            {
                new Vector(new IExpression[] { new Number(15), new Number(5) }),
                new Vector(new IExpression[] { new Number(6), new Number(4) })
            });
            var result = add.Execute();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Add4MatricesTest()
        {
            var vector1 = new Vector(new IExpression[] { Number.One, Number.Two });
            var vector2 = new Vector(new IExpression[] { Number.One, Number.Two });
            var vector3 = new Vector(new IExpression[] { Number.One, Number.Two });
            var vector4 = new Vector(new IExpression[] { Number.One, Number.Two });
            var add1 = new Add(vector1, vector2);
            var add2 = new Add(vector3, vector4);
            var add3 = new Add(add1, add2);

            var expected = new Vector(new IExpression[] { new Number(4), new Number(8) });

            Assert.Equal(expected, add3.Execute());
        }

        [Fact]
        public void AddNumberAndDegree()
        {
            var exp = new Add(Number.One, AngleValue.Degree(10).AsExpression());
            var actual = exp.Execute();
            var expected = AngleValue.Degree(11);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddRadianAndNumber()
        {
            var exp = new Add(AngleValue.Radian(10).AsExpression(), Number.One);
            var actual = exp.Execute();
            var expected = AngleValue.Radian(11);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddDegreeAndRadian()
        {
            var exp = new Add(
                AngleValue.Degree(10).AsExpression(),
                AngleValue.Radian(Math.PI).AsExpression()
            );
            var actual = exp.Execute();
            var expected = AngleValue.Degree(190);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddGradianAndGradian()
        {
            var exp = new Add(
                AngleValue.Gradian(10).AsExpression(),
                AngleValue.Gradian(20).AsExpression()
            );
            var actual = exp.Execute();
            var expected = AngleValue.Gradian(30);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ExecuteNumberAndPower()
        {
            var exp = new Add(
                Number.One,
                PowerValue.Watt(1).AsExpression()
            );
            var actual = exp.Execute();
            var expected = PowerValue.Watt(2);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ExecutePowerAndNumber()
        {
            var exp = new Add(
                PowerValue.Watt(1).AsExpression(),
                Number.One
            );
            var actual = exp.Execute();
            var expected = PowerValue.Watt(2);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ExecutePowerAndPower()
        {
            var exp = new Add(
                PowerValue.Watt(1).AsExpression(),
                PowerValue.Watt(2).AsExpression()
            );
            var actual = exp.Execute();
            var expected = PowerValue.Watt(3);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ExecuteStringAndString()
        {
            var exp = new Add(
                new StringExpression("a"),
                new StringExpression("b")
            );
            var actual = exp.Execute();
            var expected = "ab";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ExecuteStringAndNumber()
        {
            var exp = new Add(
                new StringExpression("a"),
                Number.One
            );
            var actual = exp.Execute();
            var expected = "a1";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ExecuteNumberAndString()
        {
            var exp = new Add(
                Number.One,
                new StringExpression("b")
            );
            var actual = exp.Execute();
            var expected = "1b";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ExecuteTestException()
            => TestNotSupported(new Add(Bool.False, Bool.False));

        [Fact]
        public void ExecuteComplexNumberAndBool()
            => TestNotSupported(new Add(new ComplexNumber(7, 3), Bool.False));

        [Fact]
        public void ExecuteBoolAndComplexNumber()
            => TestNotSupported(new Add(Bool.False, new ComplexNumber(7, 3)));

        [Fact]
        public void AnalyzeNull()
        {
            var exp = new Add(Number.One, Number.One);

            Assert.Throws<ArgumentNullException>(() => exp.Analyze<object>(null));
        }

        [Fact]
        public void AnalyzeContextNull()
        {
            var exp = new Add(Number.One, Number.One);

            Assert.Throws<ArgumentNullException>(() => exp.Analyze<object, object>(null, null));
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Add(Variable.X, Number.Zero);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}