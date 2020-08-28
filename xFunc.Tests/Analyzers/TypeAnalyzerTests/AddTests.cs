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

using xFunc.Maths.Analyzers.TypeAnalyzers;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Angles;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Matrices;
using Xunit;

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests
{
    public class AddTests : TypeAnalyzerBaseTests
    {
        [Fact]
        public void TestAddTwoNumberTest()
        {
            var add = new Add(Number.One, Number.Two);

            Test(add, ResultTypes.Number);
        }

        [Fact]
        public void TestAddNumberVarTest()
        {
            var add = new Add(Number.One, Variable.X);

            Test(add, ResultTypes.Undefined);
        }

        [Fact]
        public void TestAddComplicatedTest()
        {
            var add = new Add(new Mul(Number.One, Number.Two), Variable.X);

            Test(add, ResultTypes.Undefined);
        }

        [Fact]
        public void TestAddTwoVectorTest()
        {
            var add = new Add(
                new Vector(new IExpression[] { Number.One }),
                new Vector(new IExpression[] { Number.Two })
            );

            Test(add, ResultTypes.Vector);
        }

        [Fact]
        public void TestAddTwoMatrixTest()
        {
            var add = new Add(
                new Matrix(new[] { new Vector(new IExpression[] { Number.One }) }),
                new Matrix(new[] { new Vector(new IExpression[] { Number.Two }) })
            );

            Test(add, ResultTypes.Matrix);
        }

        [Fact]
        public void TestAddNumberVectorTest()
        {
            var exp = new Add(
                Number.One,
                new Vector(new IExpression[] { Number.One })
            );

            TestBinaryException(exp);
        }

        [Fact]
        public void TestAddBoolVectorException()
        {
            var exp = new Add(Bool.True, new Vector(new IExpression[] { Number.One }));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestAddVectorNumberTest()
        {
            var exp = new Add(new Vector(new IExpression[] { Number.One }), Bool.True);

            TestBinaryException(exp);
        }

        [Fact]
        public void TestAddNumberMatrixTest()
        {
            var exp = new Add(
                Number.One,
                new Matrix(new[] { new Vector(new IExpression[] { Number.Two }) })
            );

            TestBinaryException(exp);
        }

        [Fact]
        public void TestAddMatrixNumberTest()
        {
            var exp = new Add(
                new Matrix(new[] { new Vector(new IExpression[] { Number.Two }) }),
                Number.One
            );

            TestBinaryException(exp);
        }

        [Fact]
        public void TestAddVectorMatrixTest()
        {
            var exp = new Add(
                new Vector(new IExpression[] { Number.One }),
                new Matrix(new[] { new Vector(new IExpression[] { Number.Two }) })
            );

            TestBinaryException(exp);
        }

        [Fact]
        public void TestAddMatrixVectorTest()
        {
            var exp = new Add(
                new Matrix(new[] { new Vector(new IExpression[] { Number.Two }) }),
                new Vector(new IExpression[] { Number.One })
            );

            TestBinaryException(exp);
        }

        [Fact]
        public void TestAddNumberComplexNumberTest()
        {
            var add = new Add(Number.One, new ComplexNumber(2, 1));

            Test(add, ResultTypes.ComplexNumber);
        }

        [Fact]
        public void TestAddBoolComplexNumberException()
        {
            var add = new Add(Bool.True, new ComplexNumber(2, 1));

            TestBinaryException(add);
        }

        [Fact]
        public void TestAddComplexNumberNumberTest()
        {
            var add = new Add(new ComplexNumber(1, 3), Number.Two);

            Test(add, ResultTypes.ComplexNumber);
        }

        [Fact]
        public void TestAddComplexNumberBoolException()
        {
            var add = new Add(new ComplexNumber(1, 3), Bool.True);

            TestBinaryException(add);
        }

        [Fact]
        public void TestAddComplexNumberComplexNumberTest()
        {
            var add = new Add(new ComplexNumber(1, 3), new ComplexNumber(2, 5));

            Test(add, ResultTypes.ComplexNumber);
        }

        [Fact]
        public void TestAddNumberAllTest()
        {
            var exp = new Add(Number.One, new UserFunction("f", new IExpression[1]));

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestAddComplexNumberAllTest()
        {
            var exp = new Add(new ComplexNumber(3, 2), new UserFunction("f", new IExpression[1]));

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestAddVectorAllTest()
        {
            var vector = new Vector(new IExpression[] { Number.One });
            var exp = new Add(vector, new UserFunction("f", new IExpression[1]));

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestAddMatrixAllTest()
        {
            var matrix = new Matrix(new[]
            {
                new Vector(new IExpression[] { Number.One })
            });
            var exp = new Add(matrix, new UserFunction("f", new IExpression[1]));

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestAddNumberSqrtComplexTest()
        {
            var exp = new Add(Number.Two, new Sqrt(new Number(-9)));

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestAddTwoVarTest()
        {
            var exp = new Add(Variable.X, Variable.X);

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestAddThreeVarTest()
        {
            var exp = new Add(new Add(Variable.X, Variable.X), Variable.X);

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestAddException()
        {
            var exp = new Add(Bool.False, Bool.False);

            TestException(exp);
        }

        [Fact]
        public void TestAddNumberAngle()
        {
            var exp = new Add(
                new Number(10),
                Angle.Radian(10).AsExpression()
            );

            Test(exp, ResultTypes.AngleNumber);
        }

        [Fact]
        public void TestAddAngleNumber()
        {
            var exp = new Add(
                Angle.Degree(10).AsExpression(),
                new Number(10)
            );

            Test(exp, ResultTypes.AngleNumber);
        }

        [Fact]
        public void TestAddAngleAngle()
        {
            var exp = new Add(
                Angle.Degree(10).AsExpression(),
                Angle.Radian(10).AsExpression()
            );

            Test(exp, ResultTypes.AngleNumber);
        }
    }
}