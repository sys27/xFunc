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
using xFunc.Maths.Expressions.Programming;
using Xunit;

namespace xFunc.Tests.Expressions.Programming
{
    public class LeftShiftTest
    {
        [Fact]
        public void ExecuteTest()
        {
            var exp = new LeftShift(Number.One, new Number(10));
            var actual = exp.Execute();
            var expected = 1024.0;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ExecuteDoubleLeftTest()
        {
            var exp = new LeftShift(new Number(1.5), new Number(10));

            Assert.Throws<ArgumentException>(() => exp.Execute());
        }

        [Fact]
        public void ExecuteDoubleRightTest()
        {
            var exp = new LeftShift(Number.One, new Number(10.1));

            Assert.Throws<ArgumentException>(() => exp.Execute());
        }

        [Fact]
        public void ExecuteBoolTest()
        {
            var exp = new LeftShift(Bool.False, Bool.True);

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new LeftShift(Number.One, new Number(10));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}