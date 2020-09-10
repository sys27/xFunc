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
using Xunit;

namespace xFunc.Tests.Expressions
{
    public class NumberTest
    {
        [Fact]
        public void EqualsNumberNullTest()
        {
            var number = Number.Zero;

            Assert.False(number.Equals(null));
        }

        [Fact]
        public void EqualsObjectNullTest()
        {
            var number = Number.Zero;

            Assert.False(number.Equals((object)null));
        }

        [Fact]
        public void EqualsNumberThisTest()
        {
            var number = Number.Zero;

            Assert.True(number.Equals(number));
        }

        [Fact]
        public void EqualsObjectThisTest()
        {
            var number = Number.Zero;

            Assert.True(number.Equals((object)number));
        }

        [Fact]
        public void EqualsTest()
        {
            var left = Number.Zero;
            var right = Number.Zero;

            Assert.True(left.Equals(right));
        }

        [Fact]
        public void NotEqualsTest()
        {
            var left = Number.Zero;
            var right = Number.One;

            Assert.False(left.Equals(right));
        }

        [Fact]
        public void ExecuteTest()
        {
            var number = Number.One;

            Assert.Equal(1.0, number.Execute());
        }

        [Fact]
        public void NanTest()
        {
            var number = new Number(double.NaN);

            Assert.True(number.IsNaN);
        }

        [Fact]
        public void PositiveInfinityTest()
        {
            var number = new Number(double.PositiveInfinity);

            Assert.True(number.IsPositiveInfinity);
        }

        [Fact]
        public void NegativeInfinityTest()
        {
            var number = new Number(double.NegativeInfinity);

            Assert.True(number.IsNegativeInfinity);
        }

        [Fact]
        public void InfinityTest()
        {
            var number = new Number(double.NegativeInfinity);

            Assert.True(number.IsInfinity);
        }

        [Fact]
        public void ImplicitNullToNumber()
        {
            Number x = null;

            Assert.Throws<ArgumentNullException>(() => (double)x);
        }

        [Fact]
        public void NullAnalyzerTest1()
        {
            var exp = Number.One;

            Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
        }

        [Fact]
        public void NullAnalyzerTest2()
        {
            var exp = Number.One;

            Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
        }
    }
}