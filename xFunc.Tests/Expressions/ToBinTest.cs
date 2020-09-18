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
    public class ToBinTest
    {
        [Fact]
        public void ExecutNumberTest()
        {
            var exp = new ToBin(Number.Two);

            Assert.Equal("0b10", exp.Execute());
        }

        [Fact]
        public void ExecutNumberExceptionTest()
        {
            var exp = new ToBin(new Number(2.5));

            Assert.Throws<ArgumentException>(() => exp.Execute());
        }

        [Fact]
        public void ExecutLongMaxNumberTest()
        {
            var exp = new ToBin(new Number(int.MaxValue));

            Assert.Equal("0b1111111111111111111111111111111", exp.Execute());
        }

        [Fact]
        public void ExecutNegativeNumberTest()
        {
            var exp = new ToBin(new Number(-2));

            Assert.Equal("0b11111111111111111111111111111110", exp.Execute());
        }

        [Fact]
        public void ExecutBoolTest()
        {
            var exp = new ToBin(Bool.False);

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void CloseTest()
        {
            var exp = new ToBin(new Number(10));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}