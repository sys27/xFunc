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
using xFunc.Maths.Expressions.Units.AngleUnits;
using xFunc.Maths.Expressions.Units.PowerUnits;
using Xunit;

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests
{
    public class SubTests : TypeAnalyzerBaseTests
    {
        [Fact]
        public void SubTwoNumberTest()
        {
            var sub = new Sub(Number.One, Number.Two);

            Test(sub, ResultTypes.Number);
        }

        [Fact]
        public void SubNumberVarTest()
        {
            var sub = new Sub(Number.One, Variable.X);

            Test(sub, ResultTypes.Undefined);
        }

        [Fact]
        public void SubComplicatedTest()
        {
            var sub = new Sub(new Mul(Number.One, Number.Two), Variable.X);

            Test(sub, ResultTypes.Undefined);
        }

        [Fact]
        public void SubTwoVectorTest()
        {
            var sub = new Sub(
                new Vector(new IExpression[] { Number.One }),
                new Vector(new IExpression[] { Number.Two })
            );

            Test(sub, ResultTypes.Vector);
        }

        [Fact]
        public void SubTwoMatrixTest()
        {
            var sub = new Sub(
                new Matrix(new[] { new Vector(new IExpression[] { Number.One }) }),
                new Matrix(new[] { new Vector(new IExpression[] { Number.Two }) })
            );

            Test(sub, ResultTypes.Matrix);
        }

        [Fact]
        public void SubNumberVectorTest()
        {
            var exp = new Sub(
                Number.One,
                new Vector(new IExpression[] { Number.One })
            );

            TestBinaryException(exp);
        }

        [Fact]
        public void SubVectorNumberTest()
        {
            var exp = new Sub(
                new Vector(new IExpression[] { Number.One }),
                Number.One
            );

            TestBinaryException(exp);
        }

        [Fact]
        public void SubNumberMatrixTest()
        {
            var exp = new Sub(
                Number.One,
                new Matrix(new[] { new Vector(new IExpression[] { Number.Two }) })
            );

            TestBinaryException(exp);
        }

        [Fact]
        public void SubMatrixNumberTest()
        {
            var exp = new Sub(
                new Matrix(new[] { new Vector(new IExpression[] { Number.Two }) }),
                Number.One
            );

            TestBinaryException(exp);
        }

        [Fact]
        public void SubVectorMatrixTest()
        {
            var exp = new Sub(
                new Vector(new IExpression[] { Number.One }),
                new Matrix(new[] { new Vector(new IExpression[] { Number.Two }) })
            );

            TestBinaryException(exp);
        }

        [Fact]
        public void SubMatrixVectorTest()
        {
            var exp = new Sub(
                new Matrix(new[] { new Vector(new IExpression[] { Number.Two }) }),
                new Vector(new IExpression[] { Number.One })
            );

            TestBinaryException(exp);
        }

        [Fact]
        public void SubNumberComplexNumberTest()
        {
            var sub = new Sub(Number.One, new ComplexNumber(2, 1));

            Test(sub, ResultTypes.ComplexNumber);
        }

        [Fact]
        public void SubComplexNumberNumberTest()
        {
            var sub = new Sub(new ComplexNumber(1, 3), Number.Two);

            Test(sub, ResultTypes.ComplexNumber);
        }

        [Fact]
        public void SubComplexNumberComplexNumberTest()
        {
            var sub = new Sub(new ComplexNumber(1, 3), new ComplexNumber(3, 5));

            Test(sub, ResultTypes.ComplexNumber);
        }

        [Fact]
        public void SubComplexNumberBoolException()
        {
            var sub = new Sub(new ComplexNumber(1, 3), Bool.True);

            TestBinaryException(sub);
        }

        [Fact]
        public void SubBoolComplexNumberException()
        {
            var sub = new Sub(Bool.True, new ComplexNumber(1, 3));

            TestBinaryException(sub);
        }

        [Fact]
        public void SubNumberAllTest()
        {
            var exp = new Sub(
                Number.One,
                new UserFunction("f", new IExpression[] { Number.One }));

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void SubComplexNumberAllTest()
        {
            var exp = new Sub(
                new ComplexNumber(3, 2),
                new UserFunction("f", new IExpression[] { Number.One }));

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void SubVectorVectorTest()
        {
            var left = new Vector(new IExpression[] { new Number(3) });
            var right = new Vector(new IExpression[] { Number.One });
            var exp = new Sub(left, right);

            Test(exp, ResultTypes.Vector);
        }

        [Fact]
        public void SubVectorBoolTest()
        {
            var vector = new Vector(new IExpression[] { Number.One });
            var exp = new Sub(vector, Bool.True);

            TestBinaryException(exp);
        }

        [Fact]
        public void SubBoolVectorTest()
        {
            var vector = new Vector(new IExpression[] { Number.One });
            var exp = new Sub(Bool.True, vector);

            TestBinaryException(exp);
        }

        [Fact]
        public void SubVectorAllTest()
        {
            var vector = new Vector(new IExpression[] { Number.One });
            var exp = new Sub(vector, new UserFunction("f", new IExpression[] { Number.One }));

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void SubMatrixAllTest()
        {
            var matrix = new Matrix(new[]
            {
                new Vector(new IExpression[] { Number.One })
            });
            var exp = new Sub(matrix, new UserFunction("f", new IExpression[] { Number.One }));

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestSubBoolAndMatrixTest()
        {
            var matrix = new Matrix(new[]
            {
                new Vector(new IExpression[] { Number.One })
            });
            var exp = new Sub(Bool.True, matrix);

            TestBinaryException(exp);
        }

        [Fact]
        public void TestSubMatrixAndBoolTest()
        {
            var matrix = new Matrix(new[]
            {
                new Vector(new IExpression[] { Number.One })
            });
            var exp = new Sub(matrix, Bool.True);

            TestBinaryException(exp);
        }

        [Fact]
        public void SubNumberComplexTest()
        {
            var exp = new Sub(Number.Two, new Sqrt(new Number(-9)));

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void SubTwoVarTest()
        {
            var exp = new Sub(Variable.X, Variable.X);

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void SubThreeVarTest()
        {
            var exp = new Sub(new Add(Variable.X, Variable.X), Variable.X);

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestSubBoolsException()
        {
            var exp = new Sub(Bool.False, Bool.False);

            TestException(exp);
        }

        [Fact]
        public void TestSubNumberAngle()
        {
            var exp = new Sub(
                new Number(10),
                AngleValue.Radian(10).AsExpression()
            );

            Test(exp, ResultTypes.AngleNumber);
        }

        [Fact]
        public void TestSubAngleNumber()
        {
            var exp = new Sub(
                AngleValue.Degree(10).AsExpression(),
                new Number(10)
            );

            Test(exp, ResultTypes.AngleNumber);
        }

        [Fact]
        public void TestSubAngleAngle()
        {
            var exp = new Sub(
                AngleValue.Degree(10).AsExpression(),
                AngleValue.Radian(10).AsExpression()
            );

            Test(exp, ResultTypes.AngleNumber);
        }

        [Fact]
        public void TestSubNumberPower()
        {
            var exp = new Sub(
                new Number(10),
                PowerValue.Watt(10).AsExpression()
            );

            Test(exp, ResultTypes.PowerNumber);
        }

        [Fact]
        public void TestSubPowerNumber()
        {
            var exp = new Sub(
                PowerValue.Watt(10).AsExpression(),
                new Number(10)
            );

            Test(exp, ResultTypes.PowerNumber);
        }

        [Fact]
        public void TestSubPowerPower()
        {
            var exp = new Sub(
                PowerValue.Watt(10).AsExpression(),
                PowerValue.Watt(10).AsExpression()
            );

            Test(exp, ResultTypes.PowerNumber);
        }
    }
}