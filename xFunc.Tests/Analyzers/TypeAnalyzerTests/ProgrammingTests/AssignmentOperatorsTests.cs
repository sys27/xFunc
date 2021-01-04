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
    public class AssignmentOperatorsTests : TypeAnalyzerBaseTests
    {
        [Theory]
        [InlineData(typeof(AddAssign))]
        [InlineData(typeof(SubAssign))]
        [InlineData(typeof(MulAssign))]
        [InlineData(typeof(DivAssign))]
        [InlineData(typeof(LeftShiftAssign))]
        [InlineData(typeof(RightShiftAssign))]
        public void TestAssignUndefined(Type type)
        {
            var exp = Create(type, Variable.X, Variable.X);

            Test(exp, ResultTypes.Number);
        }

        [Theory]
        [InlineData(typeof(AddAssign))]
        [InlineData(typeof(SubAssign))]
        [InlineData(typeof(MulAssign))]
        [InlineData(typeof(DivAssign))]
        [InlineData(typeof(LeftShiftAssign))]
        [InlineData(typeof(RightShiftAssign))]
        public void TestAssignNumber(Type type)
        {
            var exp = Create(type, Variable.X, new Number(10));

            Test(exp, ResultTypes.Number);
        }

        [Theory]
        [InlineData(typeof(AddAssign))]
        [InlineData(typeof(SubAssign))]
        [InlineData(typeof(MulAssign))]
        [InlineData(typeof(DivAssign))]
        [InlineData(typeof(LeftShiftAssign))]
        [InlineData(typeof(RightShiftAssign))]
        public void TestAssignException(Type type)
        {
            var exp = Create(type, Variable.X, Bool.False);

            TestException(exp);
        }
    }
}