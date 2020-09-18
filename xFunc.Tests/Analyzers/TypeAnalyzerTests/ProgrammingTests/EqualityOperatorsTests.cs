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
using xFunc.Maths.Analyzers.TypeAnalyzers;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Angles;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Programming;
using Xunit;

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests.ProgrammingTests
{
    public class EqualityOperatorsTests : TypeAnalyzerBaseTests
    {
        [Theory]
        [InlineData(typeof(Equal))]
        [InlineData(typeof(NotEqual))]
        public void TestUndefined(Type type)
        {
            var exp = Create(type, Variable.X, Variable.X);

            Test(exp, ResultTypes.Boolean);
        }

        [Theory]
        [InlineData(typeof(Equal))]
        [InlineData(typeof(NotEqual))]
        public void TestNumberAndUndefined(Type type)
        {
            var exp = Create(type, Number.One, Variable.X);

            Test(exp, ResultTypes.Boolean);
        }

        [Theory]
        [InlineData(typeof(Equal))]
        [InlineData(typeof(NotEqual))]
        public void TestUndefinedAndNumber(Type type)
        {
            var exp = Create(type, Variable.X, Number.One);

            Test(exp, ResultTypes.Boolean);
        }

        [Theory]
        [InlineData(typeof(Equal))]
        [InlineData(typeof(NotEqual))]
        public void TestBoolAndUndefined(Type type)
        {
            var exp = Create(type, Bool.True, Variable.X);

            Test(exp, ResultTypes.Boolean);
        }

        [Theory]
        [InlineData(typeof(Equal))]
        [InlineData(typeof(NotEqual))]
        public void TestUndefinedAndBool(Type type)
        {
            var exp = Create(type, Variable.X, Bool.True);

            Test(exp, ResultTypes.Boolean);
        }

        [Theory]
        [InlineData(typeof(Equal))]
        [InlineData(typeof(NotEqual))]
        public void TestAngleAndUndefined(Type type)
        {
            var exp = Create(type, AngleValue.Degree(1).AsExpression(), Variable.X);

            Test(exp, ResultTypes.Boolean);
        }

        [Theory]
        [InlineData(typeof(Equal))]
        [InlineData(typeof(NotEqual))]
        public void TestUndefinedAndAngle(Type type)
        {
            var exp = Create(type, Variable.X, AngleValue.Degree(1).AsExpression());

            Test(exp, ResultTypes.Boolean);
        }

        [Theory]
        [InlineData(typeof(Equal))]
        [InlineData(typeof(NotEqual))]
        public void TestNumber(Type type)
        {
            var exp = Create(type, new Number(20), new Number(10));

            Test(exp, ResultTypes.Boolean);
        }

        [Theory]
        [InlineData(typeof(Equal))]
        [InlineData(typeof(NotEqual))]
        public void TestBoolean(Type type)
        {
            var exp = Create(type, Bool.False, Bool.True);

            Test(exp, ResultTypes.Boolean);
        }

        [Theory]
        [InlineData(typeof(Equal))]
        [InlineData(typeof(NotEqual))]
        public void TestAngleNumber(Type type)
        {
            var exp = Create(type,
                AngleValue.Degree(10).AsExpression(),
                AngleValue.Degree(10).AsExpression()
            );

            Test(exp, ResultTypes.Boolean);
        }

        [Theory]
        [InlineData(typeof(Equal))]
        [InlineData(typeof(NotEqual))]
        public void TestComplexAndNumber(Type type)
        {
            var exp = CreateBinary(type, new ComplexNumber(1, 2), new Number(20));

            TestBinaryException(exp);
        }

        [Theory]
        [InlineData(typeof(Equal))]
        [InlineData(typeof(NotEqual))]
        public void TestNumberAndComplex(Type type)
        {
            var exp = CreateBinary(type, new Number(20), new ComplexNumber(1, 2));

            TestBinaryException(exp);
        }

        [Theory]
        [InlineData(typeof(Equal))]
        [InlineData(typeof(NotEqual))]
        public void TestComplexAndBool(Type type)
        {
            var exp = CreateBinary(type, new ComplexNumber(1, 2), Bool.True);

            TestBinaryException(exp);
        }

        [Theory]
        [InlineData(typeof(Equal))]
        [InlineData(typeof(NotEqual))]
        public void TestBoolAndComplex(Type type)
        {
            var exp = CreateBinary(type, Bool.False, new ComplexNumber(1, 2));

            TestBinaryException(exp);
        }

        [Theory]
        [InlineData(typeof(Equal))]
        [InlineData(typeof(NotEqual))]
        public void TestComplexAndAngle(Type type)
        {
            var exp = CreateBinary(type,
                new ComplexNumber(1, 2),
                AngleValue.Degree(10).AsExpression()
            );

            TestBinaryException(exp);
        }

        [Theory]
        [InlineData(typeof(Equal))]
        [InlineData(typeof(NotEqual))]
        public void TestAngleAndComplex(Type type)
        {
            var exp = CreateBinary(type,
                AngleValue.Degree(10).AsExpression(),
                new ComplexNumber(1, 2)
            );

            TestBinaryException(exp);
        }

        [Theory]
        [InlineData(typeof(Equal))]
        [InlineData(typeof(NotEqual))]
        public void TestInvalidArgsException(Type type)
        {
            var exp = Create(type, new ComplexNumber(2, 3), new ComplexNumber(2, 3));

            TestException(exp);
        }
    }
}