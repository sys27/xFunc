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
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using Xunit;

namespace xFunc.Tests.Expressions.LogicalAndBitwise
{
    public class NotTest : BaseExpressionTests
    {
        [Fact]
        public void ExecuteTest1()
        {
            var exp = new Not(Number.Two);
            var expected = new NumberValue(-3.0);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteTest3()
        {
            var exp = new Not(Bool.True);

            Assert.False((bool) exp.Execute());
        }

        [Fact]
        public void ExecuteTestValueIsNotInt()
        {
            var exp = new Not(new Number(1.5));

            Assert.Throws<ArgumentException>(() => exp.Execute());
        }

        [Fact]
        public void ExecuteResultIsNotSupported()
            => TestNotSupported(new Not(new ComplexNumber(1)));

        [Fact]
        public void CloneTest()
        {
            var exp = new Not(Bool.False);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}