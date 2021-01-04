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
using xFunc.Maths.Analyzers.TypeAnalyzers;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using Xunit;

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests
{
    public class LogicalBitwiseTests : TypeAnalyzerBaseTests
    {
        [Theory]
        [InlineData(typeof(And))]
        [InlineData(typeof(Or))]
        [InlineData(typeof(XOr))]
        public void TestLeftUndefined(Type type)
        {
            var exp = Create(type, Variable.X, Number.One);

            Test(exp, ResultTypes.Undefined);
        }

        [Theory]
        [InlineData(typeof(And))]
        [InlineData(typeof(Or))]
        [InlineData(typeof(XOr))]
        public void TestRightUndefined(Type type)
        {
            var exp = Create(type, Number.One, Variable.X);

            Test(exp, ResultTypes.Undefined);
        }

        [Theory]
        [InlineData(typeof(And))]
        [InlineData(typeof(Or))]
        [InlineData(typeof(XOr))]
        public void TestNumber(Type type)
        {
            var exp = Create(type, Number.One, Number.One);

            Test(exp, ResultTypes.Number);
        }

        [Theory]
        [InlineData(typeof(And))]
        [InlineData(typeof(Or))]
        [InlineData(typeof(XOr))]
        public void TestBool(Type type)
        {
            var exp = Create(type, Bool.True, Bool.True);

            Test(exp, ResultTypes.Boolean);
        }

        [Theory]
        [InlineData(typeof(And))]
        [InlineData(typeof(Or))]
        [InlineData(typeof(XOr))]
        public void TestComplexAndNumber(Type type)
        {
            var exp = CreateBinary(type, new ComplexNumber(3, 2), Number.One);

            TestBinaryException(exp);
        }

        [Theory]
        [InlineData(typeof(And))]
        [InlineData(typeof(Or))]
        [InlineData(typeof(XOr))]
        public void TestNumberAndComplex(Type type)
        {
            var exp = CreateBinary(type, Number.One, new ComplexNumber(3, 2));

            TestBinaryException(exp);
        }

        [Theory]
        [InlineData(typeof(And))]
        [InlineData(typeof(Or))]
        [InlineData(typeof(XOr))]
        public void TestComplexAndBool(Type type)
        {
            var exp = CreateBinary(type, new ComplexNumber(3, 2), Bool.False);

            TestBinaryException(exp);
        }

        [Theory]
        [InlineData(typeof(And))]
        [InlineData(typeof(Or))]
        [InlineData(typeof(XOr))]
        public void TestBoolAndComplex(Type type)
        {
            var exp = CreateBinary(type, Bool.False, new ComplexNumber(3, 2));

            TestBinaryException(exp);
        }

        [Theory]
        [InlineData(typeof(And))]
        [InlineData(typeof(Or))]
        [InlineData(typeof(XOr))]
        public void TestParamTypeException(Type type)
        {
            var exp = Create(type, new ComplexNumber(3, 2), new ComplexNumber(3, 2));

            TestException(exp);
        }
    }
}