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
using System.Collections.Immutable;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Statistical;
using Xunit;

namespace xFunc.Tests.Expressions.Statistical
{
    public class StatisticalTests :  BaseExpressionTests
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
        public void NotSupportedException(Type type)
        {
            var exp = Create(type, new IExpression[] { Bool.False, Bool.False });

            TestNotSupported(exp);
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
        public void CloneTest(Type type)
        {
            var exp = Create<StatisticalExpression>(type, new IExpression[] { Number.One, Number.Two });
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
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
        public void CloneWithReplaceTest(Type type)
        {
            var exp = Create<StatisticalExpression>(type, new IExpression[] { Number.One, Number.Two });
            var arg = ImmutableArray.Create<IExpression>(Number.One);
            var clone = exp.Clone(arg);
            var expected = Create(type, arg);

            Assert.Equal(expected, clone);
        }
    }
}