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
    public class RoundTest
    {
        [Fact]
        public void CalculateRoundWithoutDigits()
        {
            var round = new Round(new Number(5.555555));
            var result = round.Execute();
            var expected = 6.0;

            Assert.Equal(expected, result);
        }

        [Fact]
        public void CalculateRoundWithDigits()
        {
            var round = new Round(new Number(5.555555), new Number(2));
            var result = round.Execute();
            var expected = 5.56;

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ExecuteArgumentIsNotNumber()
        {
            var exp = new Round(new Bool(false), new Number(2));

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void ExecuteDigitstIsNotNumber()
        {
            var exp = new Round(new Number(5.5), new Bool(false));

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void ExecuteArgumentIsNotInteger()
        {
            var exp = new Round(new Number(5.5), new Number(2.5));

            Assert.Throws<InvalidOperationException>(() => exp.Execute());
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Round(new Number(5.555555), new Number(2));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}