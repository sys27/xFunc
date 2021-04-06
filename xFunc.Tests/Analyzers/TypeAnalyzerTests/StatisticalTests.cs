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
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions.Statistical;
using Xunit;

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests
{
    public class StatisticalTests : TypeAnalyzerBaseTests
    {
        [Theory]
        [InlineData(typeof(Avg))]
        [InlineData(typeof(Count))]
        [InlineData(typeof(Max))]
        [InlineData(typeof(Min))]
        [InlineData(typeof(Product))]
        [InlineData(typeof(Stdev))]
        [InlineData(typeof(Stdevp))]
        [InlineData(typeof(Sum))]
        [InlineData(typeof(Var))]
        [InlineData(typeof(Varp))]
        public void TestUndefined(Type type)
        {
            var exp = Create(type, new IExpression[] { Variable.X, Variable.Y });

            Test(exp, ResultTypes.Undefined);
        }

        [Theory]
        [InlineData(typeof(Avg))]
        [InlineData(typeof(Count))]
        [InlineData(typeof(Max))]
        [InlineData(typeof(Min))]
        [InlineData(typeof(Product))]
        [InlineData(typeof(Stdev))]
        [InlineData(typeof(Stdevp))]
        [InlineData(typeof(Sum))]
        [InlineData(typeof(Var))]
        [InlineData(typeof(Varp))]
        public void TestAvgNumber(Type type)
        {
            var exp = Create(type, new IExpression[] { new Number(3), Number.Two });

            Test(exp, ResultTypes.Number);
        }

        [Theory]
        [InlineData(typeof(Avg))]
        [InlineData(typeof(Count))]
        [InlineData(typeof(Max))]
        [InlineData(typeof(Min))]
        [InlineData(typeof(Product))]
        [InlineData(typeof(Stdev))]
        [InlineData(typeof(Stdevp))]
        [InlineData(typeof(Sum))]
        [InlineData(typeof(Var))]
        [InlineData(typeof(Varp))]
        public void TestVector(Type type)
        {
            var arguments = new IExpression[]
            {
                new Vector(new IExpression[] { new Number(3), Number.Two })
            };
            var exp = Create(type, arguments);

            Test(exp, ResultTypes.Number);
        }

        [Theory]
        [InlineData(typeof(Avg))]
        [InlineData(typeof(Count))]
        [InlineData(typeof(Max))]
        [InlineData(typeof(Min))]
        [InlineData(typeof(Product))]
        [InlineData(typeof(Stdev))]
        [InlineData(typeof(Stdevp))]
        [InlineData(typeof(Sum))]
        [InlineData(typeof(Var))]
        [InlineData(typeof(Varp))]
        public void TestOneParamException(Type type)
        {
            var exp = CreateDiff(type, new IExpression[] { Bool.False });

            TestDiffParamException(exp);
        }

        [Theory]
        [InlineData(typeof(Avg))]
        [InlineData(typeof(Count))]
        [InlineData(typeof(Max))]
        [InlineData(typeof(Min))]
        [InlineData(typeof(Product))]
        [InlineData(typeof(Stdev))]
        [InlineData(typeof(Stdevp))]
        [InlineData(typeof(Sum))]
        [InlineData(typeof(Var))]
        [InlineData(typeof(Varp))]
        public void TestParamException(Type type)
        {
            var exp = CreateDiff(type, new IExpression[] { Bool.False, Bool.False });

            TestDiffParamException(exp);
        }
    }
}