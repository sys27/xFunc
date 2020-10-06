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
using Xunit;

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests
{
    public class DivTests : TypeAnalyzerBaseTests
    {
 [Fact]
        public void TestDivNumberNumberTest()
        {
            var exp = new Div(Number.One, Number.Two);

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestDivComplexNumberComplexNumberTest()
        {
            var exp = new Div(new ComplexNumber(3, 2), new ComplexNumber(2, 4));

            Test(exp, ResultTypes.ComplexNumber);
        }

        [Fact]
        public void TestDivNumberComplexNumberTest()
        {
            var exp = new Div(new Number(3), new ComplexNumber(2, 4));

            Test(exp, ResultTypes.ComplexNumber);
        }

        [Fact]
        public void TestDivComplexNumberNumberTest()
        {
            var exp = new Div(new ComplexNumber(3, 2), Number.Two);

            Test(exp, ResultTypes.ComplexNumber);
        }

        [Fact]
        public void TestDivComplexNumberBoolException()
        {
            var exp = new Div(new ComplexNumber(3, 2), Bool.True);

            TestBinaryException(exp);
        }

        [Fact]
        public void TestDivBoolComplexNumberException()
        {
            var exp = new Div(Bool.True, new ComplexNumber(3, 2));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestDivNumberBoolException()
        {
            var exp = new Div(new Number(3), Bool.True);

            TestBinaryException(exp);
        }

        [Fact]
        public void TestDivBoolNumberException()
        {
            var exp = new Div(Bool.True, new Number(3));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestDivNumberSqrtComplexTest()
        {
            var exp = new Div(new Sqrt(new Number(-16)), Number.Two);

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestDivTwoVarTest()
        {
            var exp = new Div(Variable.X, Variable.X);

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestDivNumberAndVarTest()
        {
            var exp = new Div(Number.One, Variable.X);

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestDivThreeVarTest()
        {
            var exp = new Div(new Add(Variable.X, Variable.X), Variable.X);

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestDivException()
        {
            var exp = new Div(Bool.False, Bool.False);

            TestException(exp);
        }

        [Fact]
        public void TestDivNumberAngle()
        {
            var exp = new Div(
                new Number(10),
                AngleValue.Radian(10).AsExpression()
            );

            Test(exp, ResultTypes.AngleNumber);
        }

        [Fact]
        public void TestDivAngleNumber()
        {
            var exp = new Div(
                AngleValue.Degree(10).AsExpression(),
                new Number(10)
            );

            Test(exp, ResultTypes.AngleNumber);
        }

        [Fact]
        public void TestDivAngleAngle()
        {
            var exp = new Div(
                AngleValue.Degree(10).AsExpression(),
                AngleValue.Radian(10).AsExpression()
            );

            Test(exp, ResultTypes.AngleNumber);
        }
    }
}