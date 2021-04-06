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

using xFunc.Maths.Analyzers.TypeAnalyzers;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions.Units;
using xFunc.Maths.Expressions.Units.AngleUnits;
using xFunc.Maths.Expressions.Units.PowerUnits;
using Xunit;

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests
{
    public class MulTests : TypeAnalyzerBaseTests
    {
        [Fact]
        public void TestMulTwoNumberTest()
        {
            var mul = new Mul(Number.One, Number.Two);

            Test(mul, ResultTypes.Number);
        }

        [Fact]
        public void TestMulNumberVarTest()
        {
            var mul = new Mul(Number.One, Variable.X);

            Test(mul, ResultTypes.Undefined);
        }

        [Fact]
        public void TestMulNumberBoolTest()
        {
            var mul = new Mul(Number.One, Bool.True);

            TestBinaryException(mul);
        }

        [Fact]
        public void TestMulBoolNumberTest()
        {
            var mul = new Mul(Bool.True, Number.One);

            TestBinaryException(mul);
        }

        [Fact]
        public void TestMulVarNumberTest()
        {
            var mul = new Mul(Variable.X, Number.One);

            Test(mul, ResultTypes.Undefined);
        }

        [Fact]
        public void TestMulTwoMatrixTest()
        {
            var mul = new Mul(
                new Matrix(new[] { new Vector(new IExpression[] { Number.One }) }),
                new Matrix(new[] { new Vector(new IExpression[] { Number.Two }) })
            );

            Test(mul, ResultTypes.Matrix);
        }

        [Fact]
        public void TestMulLeftMatrixRightException()
        {
            var mul = new Mul(
                new Matrix(new[] { new Vector(new IExpression[] { Number.One }) }),
                Bool.False
            );

            TestBinaryException(mul);
        }

        [Fact]
        public void TestMulRightMatrixLeftException()
        {
            var mul = new Mul(
                Bool.False,
                new Matrix(new[] { new Vector(new IExpression[] { Number.One }) })
            );

            TestBinaryException(mul);
        }

        [Fact]
        public void TestMulNumberVectorTest()
        {
            var mul = new Mul(
                Number.One,
                new Vector(new IExpression[] { Number.One })
            );

            Test(mul, ResultTypes.Vector);
        }

        [Fact]
        public void TestMulVectorNumber()
        {
            var mul = new Mul(
                new Vector(new IExpression[] { Number.One }),
                Number.Two
            );

            Test(mul, ResultTypes.Vector);
        }

        [Fact]
        public void TestMulVectors()
        {
            var mul = new Mul(
                new Vector(new IExpression[] { Number.One }),
                new Vector(new IExpression[] { Number.One })
            );

            Test(mul, ResultTypes.Vector);
        }

        [Fact]
        public void TestMulNumberMatrixTest()
        {
            var mul = new Mul(
                Number.One,
                new Matrix(new[] { new Vector(new IExpression[] { Number.Two }) })
            );

            Test(mul, ResultTypes.Matrix);
        }

        [Fact]
        public void TestMulMatrixAndNumberTest()
        {
            var mul = new Mul(
                new Matrix(new[] { new Vector(new IExpression[] { Number.Two }) }),
                Number.One
            );

            Test(mul, ResultTypes.Matrix);
        }

        [Fact]
        public void TestMulVectorMatrixTest()
        {
            var mul = new Mul(
                new Vector(new IExpression[] { Number.One }),
                new Matrix(new[] { new Vector(new IExpression[] { Number.Two }) })
            );

            Test(mul, ResultTypes.Matrix);
        }

        [Fact]
        public void TestMulMatrixAndVectorTest()
        {
            var mul = new Mul(
                new Matrix(new[] { new Vector(new IExpression[] { Number.Two }) }),
                new Vector(new IExpression[] { Number.One })
            );

            Test(mul, ResultTypes.Matrix);
        }

        [Fact]
        public void TestMulVectorBoolException()
        {
            var mul = new Mul(new Vector(new IExpression[] { Number.One }), Bool.False);

            TestBinaryException(mul);
        }

        [Fact]
        public void TestMulBoolVectorException()
        {
            var mul = new Mul(Bool.False, new Vector(new IExpression[] { Number.One }));

            TestBinaryException(mul);
        }

        [Fact]
        public void TestMulComplexNumberComplexNumberTest()
        {
            var exp = new Mul(new ComplexNumber(2, 5), new ComplexNumber(3, 2));

            Test(exp, ResultTypes.ComplexNumber);
        }

        [Fact]
        public void TestMulComplexNumberNumberTest()
        {
            var exp = new Mul(new ComplexNumber(2, 5), Number.Two);

            Test(exp, ResultTypes.ComplexNumber);
        }

        [Fact]
        public void TestMulComplexNumberBoolTest()
        {
            var exp = new Mul(new ComplexNumber(2, 5), Bool.True);

            TestBinaryException(exp);
        }

        [Fact]
        public void TestMulNumberComplexNumberTest()
        {
            var exp = new Mul(Number.Two, new ComplexNumber(3, 2));

            Test(exp, ResultTypes.ComplexNumber);
        }

        [Fact]
        public void TestMulBoolComplexNumberTest()
        {
            var exp = new Mul(Bool.True, new ComplexNumber(2, 5));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestMulException()
        {
            var exp = new Mul(Bool.False, Bool.True);

            TestException(exp);
        }

        [Fact]
        public void TestMulNumberAngle()
        {
            var exp = new Mul(
                new Number(10),
                AngleValue.Radian(10).AsExpression()
            );

            Test(exp, ResultTypes.AngleNumber);
        }

        [Fact]
        public void TestMulAngleNumber()
        {
            var exp = new Mul(
                AngleValue.Degree(10).AsExpression(),
                new Number(10)
            );

            Test(exp, ResultTypes.AngleNumber);
        }

        [Fact]
        public void TestMulAngleAngle()
        {
            var exp = new Mul(
                AngleValue.Degree(10).AsExpression(),
                AngleValue.Radian(10).AsExpression()
            );

            Test(exp, ResultTypes.AngleNumber);
        }

        [Fact]
        public void TestMulNumberPower()
        {
            var exp = new Mul(
                new Number(10),
                PowerValue.Watt(10).AsExpression()
            );

            Test(exp, ResultTypes.PowerNumber);
        }

        [Fact]
        public void TestMulPowerNumber()
        {
            var exp = new Mul(
                PowerValue.Watt(10).AsExpression(),
                new Number(10)
            );

            Test(exp, ResultTypes.PowerNumber);
        }

        [Fact]
        public void TestMulPowerPower()
        {
            var exp = new Mul(
                PowerValue.Watt(10).AsExpression(),
                PowerValue.Watt(10).AsExpression()
            );

            Test(exp, ResultTypes.PowerNumber);
        }
    }
}