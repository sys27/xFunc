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

using xFunc.Maths.Expressions;
using Xunit;

namespace xFunc.Tests.Expressions
{
    public class NumberTest
    {
        [Fact]
        public void EqualsNumberNullTest()
        {
            var number = new Number(0);

            Assert.False(number.Equals(null));
        }

        [Fact]
        public void EqualsObjectNullTest()
        {
            var number = new Number(0);

            Assert.False(number.Equals((object)null));
        }

        [Fact]
        public void EqualsNumberThisTest()
        {
            var number = new Number(0);

            Assert.True(number.Equals(number));
        }

        [Fact]
        public void EqualsObjectThisTest()
        {
            var number = new Number(0);

            Assert.True(number.Equals((object)number));
        }

        [Fact]
        public void EqualsTest()
        {
            var left = new Number(0);
            var right = new Number(0);

            Assert.True(left.Equals(right));
        }

        [Fact]
        public void NotEqualsTest()
        {
            var left = new Number(0);
            var right = new Number(1);

            Assert.False(left.Equals(right));
        }

        [Fact]
        public void ExecuteTest()
        {
            var number = new Number(1);

            Assert.Equal(1.0, number.Execute());
        }
    }
}