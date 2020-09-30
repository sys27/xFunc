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
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using Xunit;

namespace xFunc.Tests.Expressions
{
    public class LCMTest : BaseExpressionTests
    {
        [Fact]
        public void NullArgTest()
            => Assert.Throws<ArgumentNullException>(() => new LCM(null));

        [Fact]
        public void ExecuteTest1()
        {
            var exp = new LCM(new Number(12), new Number(16));

            Assert.Equal(48.0, exp.Execute());
        }

        [Fact]
        public void ExecuteTest2()
        {
            var exp = new LCM(new IExpression[] { new Number(4), new Number(16), new Number(8) });

            Assert.Equal(16.0, exp.Execute());
        }

        [Fact]
        public void ExecuteNotSupportedTest()
            => TestNotSupported(new LCM(new IExpression[] { Bool.False, Bool.True }));

        [Fact]
        public void CloneTest()
        {
            var exp = new LCM(Variable.X, Number.Zero);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}