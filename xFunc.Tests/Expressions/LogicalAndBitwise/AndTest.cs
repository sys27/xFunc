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
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using Xunit;

namespace xFunc.Tests.Expressions.LogicalAndBitwise
{
    public class AndTest : BaseExpressionTests
    {
        [Fact]
        public void ExecuteTest1()
        {
            var exp = new And(Number.One, new Number(3));

            Assert.Equal(1.0, exp.Execute());
        }

        [Fact]
        public void ExecuteTest3()
        {
            var exp = new And(Bool.True, Bool.False);

            Assert.False((bool) exp.Execute());
        }

        [Fact]
        public void ExecuteTest4()
        {
            var exp = new And(Bool.True, Bool.True);

            Assert.True((bool) exp.Execute());
        }

        [Fact]
        public void ExecuteTestLeftIsNotInt()
        {
            var exp = new And(new Number(1.5), Number.One);

            Assert.Throws<ArgumentException>(() => exp.Execute());
        }

        [Fact]
        public void ExecuteTestRightIsNotInt()
        {
            var exp = new And(Number.One, new Number(1.5));

            Assert.Throws<ArgumentException>(() => exp.Execute());
        }

        [Fact]
        public void ExecuteResultIsNotSupported()
        {
            var exp = new And(new ComplexNumber(1), new ComplexNumber(2));

            TestNotSupported(exp);
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new And(Bool.True, Bool.False);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}