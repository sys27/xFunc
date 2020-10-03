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
    public class ToHexTest : BaseExpressionTests
    {
        [Theory]
        [InlineData(0x7, "0x07")]
        [InlineData(0x7FF, "0x07FF")]
        [InlineData(0x7FFFF, "0x07FFFF")]
        [InlineData(0x7FFFFFF, "0x07FFFFFF")]
        public void ExecuteNumberTest(double number, string result)
        {
            var exp = new ToHex(new Number(number));

            Assert.Equal(result, exp.Execute());
        }

        [Fact]
        public void ExecuteNumberExceptionTest()
        {
            var exp = new ToHex(new Number(2.5));

            Assert.Throws<ArgumentException>(() => exp.Execute());
        }

        [Fact]
        public void ExecuteLongMaxNumberTest()
        {
            var exp = new ToHex(new Number(int.MaxValue));

            Assert.Equal("0x7FFFFFFF", exp.Execute());
        }

        [Fact]
        public void ExecuteNegativeNumberTest()
        {
            var exp = new ToHex(new Number(-2));

            Assert.Equal("0xFFFFFFFE", exp.Execute());
        }

        [Fact]
        public void ExecuteBoolTest()
            => TestNotSupported(new ToHex(Bool.False));

        [Fact]
        public void CloseTest()
        {
            var exp = new ToHex(new Number(10));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}