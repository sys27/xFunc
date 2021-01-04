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
using Xunit;

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests
{
    public class NumericConvertionTests : TypeAnalyzerBaseTests
    {
        [Theory]
        [InlineData(typeof(ToBin))]
        [InlineData(typeof(ToOct))]
        [InlineData(typeof(ToHex))]
        public void UndefinedTest(Type type)
        {
            var exp = Create(type, Variable.X);

            Test(exp, ResultTypes.String);
        }

        [Theory]
        [InlineData(typeof(ToBin))]
        [InlineData(typeof(ToOct))]
        [InlineData(typeof(ToHex))]
        public void NumberTest(Type type)
        {
            var exp = Create(type, new Number(10));

            Test(exp, ResultTypes.String);
        }

        [Theory]
        [InlineData(typeof(ToBin))]
        [InlineData(typeof(ToOct))]
        [InlineData(typeof(ToHex))]
        public void BoolTest(Type type)
        {
            var exp = Create(type, Bool.False);

            TestException(exp);
        }
    }
}