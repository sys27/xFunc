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
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Programming;
using Xunit;

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests.ProgrammingTests
{
    public class ShiftOperatorsTests : TypeAnalyzerBaseTests
    {
        [Theory]
        [InlineData(typeof(LeftShift))]
        [InlineData(typeof(RightShift))]
        public void TestAssignUndefined(Type type)
        {
            var exp = Create(type, Variable.X, Variable.X);

            Test(exp, ResultTypes.Number);
        }

        [Theory]
        [InlineData(typeof(LeftShift))]
        [InlineData(typeof(RightShift))]
        public void TestAssignUndefinedNumber(Type type)
        {
            var exp = Create(type, Variable.X, Number.One);

            Test(exp, ResultTypes.Number);
        }

        [Theory]
        [InlineData(typeof(LeftShift))]
        [InlineData(typeof(RightShift))]
        public void TestAssignNumberUndefined(Type type)
        {
            var exp = Create(type, Number.One, Variable.X);

            Test(exp, ResultTypes.Number);
        }

        [Theory]
        [InlineData(typeof(LeftShift))]
        [InlineData(typeof(RightShift))]
        public void TestAssignNumbers(Type type)
        {
            var exp = Create(type, Number.One, Number.One);

            Test(exp, ResultTypes.Number);
        }

        [Theory]
        [InlineData(typeof(LeftShift))]
        [InlineData(typeof(RightShift))]
        public void TestAssignLeftException(Type type)
        {
            var exp = CreateBinary(type, Bool.False, Number.One);

            TestBinaryException(exp);
        }

        [Theory]
        [InlineData(typeof(LeftShift))]
        [InlineData(typeof(RightShift))]
        public void TestAssignRightException(Type type)
        {
            var exp = CreateBinary(type, Number.One, Bool.False);

            TestBinaryException(exp);
        }

        [Theory]
        [InlineData(typeof(LeftShift))]
        [InlineData(typeof(RightShift))]
        public void TestAssignException(Type type)
        {
            var exp = Create(type, Bool.False, Bool.False);

            TestException(exp);
        }
    }
}